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
    /// 探索した結果
    /// </summary>
    private RaycastHit _hit = default;

    /// <summary>
    /// ロボットの状態
    /// </summary>
    private RobotStatus _enumRobotStatus = RobotStatus.Follow;


    #endregion

    #region メソッド  

    /// <summary>  
    /// 初期化処理  
    /// </summary>  
    private void Awake()
     {

        //自分のNavMeshAgent取得
        _myAgent = this.GetComponent<NavMeshAgent> ();
        
     }

    private void Update()
    {

        //命令による行動分岐
        switch (_enumRobotStatus)
        {

            case RobotStatus.Follow:

                //ついていく
                Follow ();
                break;

            case RobotStatus.GoToLocation:

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

                    //周りに何もない場合プレイヤーについていく
                    _enumRobotStatus = RobotStatus.Follow;
                }
                break;

            case RobotStatus.Hold:

                print ( "持つ" );

                break;

            case RobotStatus.Call:

                break;
        }
    }

    public void SwitchStatusGoToLocation()
    {
        //ステータスを目的の場所まで移動に切り替え
        _enumRobotStatus = RobotStatus.GoToLocation;
        //目的の場所(カーソル)まで移動
        GoToLocation ();
    }


    public void SwitchStatusCall()
    {

        //ステータスを[呼ばれる]に切り替え
        _enumRobotStatus = RobotStatus.Call;
        //呼ばれる
        Call();
    }

    private void OnTriggerEnter(Collider other)
    {

        //当たったものが電波の場合
        if (other.CompareTag ( "RadioWaves" ))
        {

            _isHitRadioWaves.Value = true;
            _isHitRadioWaves.Value = false;
        }
    }

    #endregion
}
