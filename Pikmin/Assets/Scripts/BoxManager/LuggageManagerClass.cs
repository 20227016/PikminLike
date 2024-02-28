// ---------------------------------------------------------  
// LuggageManagerClass.cs  
//   
// 作成日:  2/27
// 作成者:  湯元来輝
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections.Generic;

public class LuggageManagerClass : MonoBehaviour
{

    #region 変数  

    [Header ( "ステータス" )]
    [SerializeField, Tooltip ( "荷物の重さ" )]
    private int _weight = default;
    [SerializeField, Tooltip ( "持ち上げる高さ" )]
    private float _liftHeight = 0.7f;
    [SerializeField, Tooltip ( "持ち上げられる人数" )]
    private int _maxHave = 4;

    /// <summary>
    /// ルートを決めるクラス
    /// </summary>
    private RootClass _rootingClass = default;

    /// <summary>
    /// ルートが入る
    /// </summary>
    private List<Vector3> _root = new List<Vector3> { };

    /// <summary>
    /// 移動したい方向に向かうための疑似入力値が入る
    /// </summary>
    private Vector3 _moveValue = default;

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

        //スクリプト取得
        _rootingClass = GameObject.Find ( "Rooting" ).GetComponent<RootClass>();
        //移動ルートを取得
        _root　=  _rootingClass.Rooting ( this.transform , _root);
    }

    /// <summary>  
    /// 更新処理  
    /// </summary>  
    void Update ()
    {

        //持っているオブジェクトの合計筋力が重さを上回った時
        if (_isCarray)
        {

            //ルート道理に運ぶ処理

        }

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="muscleStrength">持っているオブジェクトの筋力</param>
    /// <param name="speed">荷物を持ったキャラクターの速さ</param>
    public bool BeHeld(int muscleStrength , float speed)
    {
        Debug.Log ( "持つ" );
        //持っているオブジェクトの数を加算
        _haveCount++;
        //持っているオブジェクトの筋力の合計を加算
        _sumMuscleStrength += muscleStrength;
        //持っているオブジェクトの速さを加算
        _sumCarraySpeed += speed;

        //持たれている数が持たれる最大数を超えたら
        if (_haveCount > _maxHave)
        {
            Debug.Log ( _haveCount + "持ってる数" );
            Debug.Log ( _maxHave +"最大" );
            Debug.Log ( "もう持てない" );
            //もう持てない判定を返す
            return false;
        }

        Debug.Log ( "まだ持てる" );

        //持っているオブジェクトの合計筋力が重さを上回った時時
        if (_weight <= _sumMuscleStrength)
        {

            Debug.Log ("持ち上げる");
            //運ばれてる判定にする
            _isCarray = true;

            //持ち上げられる処理
            this.transform.position = this.transform.position + ( Vector3.up * _liftHeight );

            //重さで割った加算してきた速さを入れる
            _carraySpeed = _sumCarraySpeed / _weight;
        }

        //まだ持てる判定を返す
        return true;
    }

    public void BePlaced(int muscleStrength , float speed)
    {
        Debug.Log ( "置く" );
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
                Debug.Log ( "降ろす" );
                //運ばれてない判定にする
                _isCarray = false;

                //降ろされる処理
                this.transform.position = this.transform.position - (Vector3.up * _liftHeight);
            }
        }
       
    }

    #endregion
}