// ---------------------------------------------------------  
// LuggageManagerClass.cs  
//   
// 作成日:  2/27
// 作成者:  湯元来輝
// ---------------------------------------------------------  
using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using UniRx;

public class LuggagesClass : BaseLuggageClass
{

    #region 変数  

    [Header ( "ステータス" )]
    [SerializeField, Tooltip ( "荷物の重さ" )]
    private int _weight = default;
    [SerializeField, Tooltip ( "持ち上げる高さ" )]
    private float _liftHeight = 0.7f;
    [SerializeField, Tooltip ( "持ち上げられる人数" )]
    private int _maxHave = 4;
    [SerializeField, Tooltip ( "金額" )]
    private int _money = 200;

    /// <summary>
    /// Activになっているかの判定
    /// </summary>
    private ReactiveProperty<bool> _isActiv = new ReactiveProperty<bool> ( true );
    public IReadOnlyReactiveProperty<bool> IsActiv => _isActiv;

    /// <summary>
    /// 自分のAgentが入る
    /// </summary>
    private NavMeshAgent _myAgent = default;

    /// <summary>
    /// 運べるようになった判定
    /// </summary>
    private bool _isCarray = default;

    /// <summary>
    /// 持っているオブジェクトの筋力合計
    /// </summary>
    private int _sumMuscleStrength = default;

    /// <summary>
    /// 持っているオブジェクトの数
    /// </summary>
    private int _haveCount = default;

    /// <summary>
    /// 運ばれる速さ
    /// </summary>
    private float _carraySpeed = default;

    /// <summary>
    /// 運ばれる速さのメモリー
    /// </summary>
    private float _sumCarraySpeed = default;

    #endregion

    #region メソッド  

    private void Start()
    {

        //自分のナビを取得
        _myAgent = this.transform.GetComponent<NavMeshAgent> ();

    }

    

    /// <summary>
    /// オブジェクトを持つ処理
    /// </summary>
    /// <param name="muscleStrength">持っているオブジェクトの筋力</param>
    /// <param name="speed">荷物を持ったキャラクターの速さ</param>
    /// <returns>持てるかの判断</returns>
    public bool BeHeld(int muscleStrength , float speed)
    {

        //持っているオブジェクトの数を加算
        _haveCount++;
        //持っているオブジェクトの筋力の合計を加算
        _sumMuscleStrength += muscleStrength;
        //持っているオブジェクトの速さを加算
        _sumCarraySpeed += speed;

        //持たれている数が持たれる最大数を超えたら
        if (_haveCount > _maxHave)
        {

            //もう持てない判定を返す
            return false;
        }

        //持っているオブジェクトの合計筋力が重さを上回った時時
        if (_weight <= _sumMuscleStrength)
        {

            //動きを停止
            _myAgent.isStopped = true;
            //1秒まった後に持たれている値の計算をしAgentを再開始
            StartCoroutine ( WaitHold () );
          
        }

        //まだ持てる判定を返す
        return true;
    }

    /// <summary>
    /// 荷物を降ろす処理
    /// </summary>
    /// <param name="muscleStrength">持っていたオブジェクトの力</param>
    /// <param name="speed">持っていたオブジェクトの速さ</param>
    public void BePlaced(int muscleStrength , float speed)
    {
        //持っているオブジェクトの数を減算
        _haveCount--;
        //持っているオブジェクトの筋力の合計を減算
        _sumMuscleStrength -= muscleStrength;
        //持っているオブジェクトの速さを減算
        _sumCarraySpeed -= speed;

        //運ばれている判定のとき
        if (_isCarray)
        {

            //持っているオブジェクトの合計筋力が重さを下回った時
            if (_weight >= _sumMuscleStrength)
            {

                //運ばれてない判定にする
                _isCarray = false;

                //NavMeshを止める処理
                _carrayClass.StopCarray ( _myAgent );
            }
        }
       
    }

    /// <summary>
    /// モノに当たった時の処理
    /// </summary>
    /// <param name="other">当たったトリガーコライダー</param>
    private void OnTriggerEnter(Collider other)
    {

        //当たったオブジェクトが保管場所の時
        if (other.CompareTag ( "StoragePlace" ))
        {

            //3秒待つコルーチンを開始(Robotがトランスフォームから離れる時間)
            StartCoroutine ( WaitWobotLeaves() );
        }
    }

    /// <summary>
    /// ロボットが離れるまで待つ
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaitWobotLeaves()
    {

        // 3秒待機
        yield return new WaitForSeconds ( 1.5f );

        //運ばれていない判定にする
        _isCarray = false;
        //お金を払う
        _possessionMoney.PossessionMoneyCupsule.Value = _money;

        //Activになっていない判定にする
        _isActiv.Value = false;

        //消す
        this.gameObject.SetActive ( false );
    }

    /// <summary>
    /// ロボットが荷物を持つまで待つ
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaitHold()
    {

        // 1秒待機
        yield return new WaitForSeconds ( 1 );

        //運ばれてる判定にする
        _isCarray = true;
        //重さで割った加算してきた速さを入れる
        _carraySpeed = _sumCarraySpeed / _weight;
        //NavMeshで運ぶ処理
        _carrayClass.OnCarray ( this.transform , _myAgent , _carraySpeed );
        //動きを再開
        _myAgent.isStopped = false;
    }

    #endregion
}
