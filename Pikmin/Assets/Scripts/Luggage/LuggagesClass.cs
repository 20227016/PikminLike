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

public class LuggagesClass : MonoBehaviour
{

    #region 変数  

    [Header ( "スクリプト" )]
    [SerializeField, Tooltip ( "Carrayのスクリプト" )]
    private CarrayClass _carrayClass = default;
    [Header ( "ステータス" )]
    [SerializeField, Tooltip ( "荷物の重さ" )]
    private int _weight = default;
    [SerializeField, Tooltip ( "持ち上げる高さ" )]
    private float _liftHeight = 0.7f;
    [SerializeField, Tooltip ( "持ち上げられる人数" )]
    private int _maxHave = 4;
    [SerializeField, Tooltip ( "金額" )]
    private int _monay = 200;

    /// <summary>
    /// 保管所にいるかの判定
    /// </summary>
    private ReactiveProperty<int> _pay = new ReactiveProperty<int>();
    public IReadOnlyReactiveProperty<int> Pay => _pay;

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

        //動きを停止
        _myAgent.isStopped = true;
        //1秒まった後に動きを再開コルーチンを開始
        StartCoroutine ( WaitOne () );


        //持っているオブジェクトの合計筋力が重さを上回った時時
        if (_weight <= _sumMuscleStrength)
        {
            //運ばれてる判定にする
            _isCarray = true;

            //持ち上げられる処理
           this.transform.position = this.transform.position + ( Vector3.up * _liftHeight );

            //重さで割った加算してきた速さを入れる
            _carraySpeed = _sumCarraySpeed / _weight;

            //NavMeshで運ぶ処理
            _carrayClass.OnCarray ( this.transform , _myAgent , _carraySpeed );
        }

        //まだ持てる判定を返す
        return true;
    }

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

                //降ろされる処理
                this.transform.position = this.transform.position - (Vector3.up * _liftHeight);

                //子オブジェクトを見ていく
                foreach (Transform childTransform in this.transform)
                {

                    //持ち下げられた分上げる
                    childTransform.position = this.transform.position + (Vector3.up * _liftHeight );
                }

                //NavMeshを止める処理
                _carrayClass.OutCarray ( _myAgent );
            }
        }
       
    }

    private void OnTriggerEnter(Collider other)
    {

        //当たったオブジェクトが保管場所の時
        if (other.CompareTag ( "StoragePlace" ))
        {

            //3秒待つコルーチンを開始(Robotがトランスフォームから離れる時間)
            StartCoroutine ( WaitThree() );

            // 3秒経過後に行いたい処理をここに記述
            _pay.Value += _monay;

           
        }
    }

    private IEnumerator WaitThree()
    {

        // 3秒待機
        yield return new WaitForSeconds ( 3 );
        print ( "3秒待った" );
        //消す
        this.gameObject.SetActive ( false );
    }

    private IEnumerator WaitOne()
    {

        // 1秒待機
        yield return new WaitForSeconds ( 1 );
        //動きを再開
        _myAgent.isStopped = false;
    }

    #endregion
}
