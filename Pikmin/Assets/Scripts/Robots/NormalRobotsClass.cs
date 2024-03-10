// ---------------------------------------------------------  
// NormalRobots.cs  
//   
// 作成日:  3/1
// 作成者:  湯元来輝
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using UniRx;

public class NormalRobotsClass : BaseRobot　
{

    #region 変数  

    /// <summary>
    /// 到着したかの判断
    /// </summary>
    private ReactiveProperty<bool> _isHitRadioWaves = new ReactiveProperty<bool> ( false );
    public IReadOnlyReactiveProperty<bool> IsHitRadioWaves => _isHitRadioWaves;

    /// <summary>
    /// 持つためのクラス
    /// </summary>
    private HoldClass _hold = default;

    /// <summary>
    /// 置くためのクラス
    /// </summary>
    private PutClass _put = default;

    /// <summary>
    /// ロボットの状態
    /// </summary>
    private RobotStatus _enumRobotStatus = RobotStatus.Follow;

    /// <summary>
    /// 探索した結果
    /// </summary>
    private RaycastHit _hit = default;

    /// <summary>
    /// 目的地に向かわせたかの判定
    /// </summary>
    private bool _isGoToLocation = default;

    /// <summary>
    /// 荷物に近づかせたかの判定
    /// </summary>
    private bool _isGetCloserl = default;

    /// <summary>
    /// 荷物を持たせたかの判定
    /// </summary>
    private bool _isHold = default;

    /// <summary>
    /// 荷物をおく必要があるかの判定
    /// </summary>
    private bool _isPut = default;

    #endregion

    #region メソッド  

    /// <summary>  
    /// 初期化処理  
    /// </summary>  
    private void Awake()
     {

        //自分のNavMeshAgent取得
        _myAgent = this.GetComponent<NavMeshAgent> ();
        //持つスクリプト取得
        _hold = GameObject.Find ( "Hold" ).GetComponent<HoldClass> ();
        //置くスクリプト取得
        _put = GameObject.Find ( "Put" ).GetComponent<PutClass> ();
    }


    private void Update()
    {

        //命令による行動分岐
        switch (_enumRobotStatus)
        {

            case RobotStatus.Idel:

                //Collで呼ばれるまで変わらない
                break;

            case RobotStatus.Follow:

                //ついていく処理
                _follow.Follow ( _playerTrans.position , _myAgent , _speed , _stopPlayerDist );
                break;

            case RobotStatus.GoToLocation:

                //目的の場所に向かわせてない判定の時
                if (_isGoToLocation == false)
                {

                    //目的の場所に向かわせた判定にする
                    _isGoToLocation = true;
                    //目的の場所(カーソル)まで移動
                    _goToLocation.GoToLocation ( _cursorTrans.position , _myAgent , _speed , _searchRange );
                }

                //目的の場所までたどり着いたとき
                if (_myAgent.remainingDistance <= _myAgent.stoppingDistance)
                {

                    //周りを確認
                    _hit = _search.Search ( this.transform , _searchRange );

                    //コライダーの中に荷物があった場合
                    if (_hit.collider != null)
                    {

                        //ステータスを持つに切り替え
                        _enumRobotStatus = RobotStatus.Hold;

                        return;
                    }
                    //コライダーの中に荷物がない場合
                    else
                    {

                        //ステータスを待機に切り替える
                        _enumRobotStatus = RobotStatus.Idel;
                    }

                }
                break;

            case RobotStatus.Hold:

                //荷物に近づけてないときかつ荷物を持っていないとき
                if (_isGetCloserl == false && _isHold == false)
                {

                    //荷物まで近づけた判定にする
                    _isGetCloserl = true;
                    //荷物まで近づける
                    _getClopser.GetCloser ( _hit.collider.transform.position , _myAgent , _stopLuggageDist );
                }

                
                //荷物の近くまで近づいたとき
                if (_myAgent.remainingDistance <= _myAgent.stoppingDistance)
                {
            
                    //荷物まで近づけてない判定にする
                    _isGetCloserl = false;

                    //持たせてない判定の時
                    if (_isHold == false)
                    {
                        //持たせた判定にする
                        _isHold = true;
                        //荷物を自分の親に設定
                        transform.SetParent ( _hit.collider.transform );
                        //Agentの動きを止める
                        _myAgent.isStopped = true;
                        // NavMeshAgentが停止している場合でも、親のトランスフォームに追従するように設定
                        _myAgent.updatePosition = false;
                        _myAgent.updateRotation = false;
                        //荷物を持つ処理
                        _isPut = _hold.Hold ( _muscleStrength , _speed , _hit.collider.transform );
                    }

                    //荷物を置く必要があるとき
                    if (_isPut == false)
                    {

                        //ステータスを奥に切り替える
                        _enumRobotStatus = RobotStatus.Put;
                    }
                }

                

                break;

            case RobotStatus.Put:


                //持たせてない判定にする
                _isHold = false;
                //Agentの動きを止める
                _myAgent.isStopped = true;
                // NavMeshAgentが停止している場合でも、親のトランスフォームに追従するように設定
                _myAgent.updatePosition = true;
                _myAgent.updateRotation = true;
                _put.Put ( _muscleStrength , _speed , _hit.collider.transform );
                //親を自分の普通のロボットの親に設定
                transform.SetParent ( _normalRobotsTrans );
                //ステータスを待機に切り替える
                _enumRobotStatus = RobotStatus.Idel;
                break;

            case RobotStatus.Call:

                //目的の場所まで向かわせてない判定にする
                _isGoToLocation = false;
                //荷物まで近づけていない判定にする
                _isGetCloserl = false;

                //荷物を持っているとき
                if (_isHold == true)
                {

                    //ステータスを置く切り替える
                    _enumRobotStatus = RobotStatus.Put;
                }
                else
                {

                    //ステータスをついて行くに切り替える
                    _enumRobotStatus = RobotStatus.Follow;
                }

                break;
        }
    }

    /// <summary>
    /// 命令によってステータスをGoToLocationに切り替える
    /// </summary>
    public void SwitchStatusGoToLocation()
    {

        //ステータスを目的の場所まで移動に切り替え
        _enumRobotStatus = RobotStatus.GoToLocation;
    }


    /// <summary>
    /// 命令によってステータスをCallに切り替える
    /// </summary>
    public void SwitchStatusCall()
    {

        //ステータスを[呼ばれる]に切り替え
        _enumRobotStatus = RobotStatus.Call;
    }


    /// <summary>
    /// 電波にあたった時にBoolを反応させる
    /// </summary>
    /// <param name="other">当たったTriggerCollider</param>
    private void OnTriggerEnter(Collider other)
    {

        //当たったものが電波の場合
        if (other.CompareTag ( "RadioWaves" ))
        {

            //電波にあたった判定にする
            _isHitRadioWaves.Value = true;
            //電波にあたってない判定にする
            _isHitRadioWaves.Value = false;
        }

        //当たったものが保管所の時
        else if (other.CompareTag ( "StoragePlace" ))
        {

            //2秒待つコルーチンを開始(Robotが保管所の中心まで行く時間)
            StartCoroutine ( WaitOne () );
        }
    }
    private IEnumerator WaitOne()
    {

        // 1秒待機
        yield return new WaitForSeconds ( 1 );

        //親を自分の普通のロボットの親に設定
        this.transform.SetParent ( _normalRobotsTrans );
    }

    #endregion
}
