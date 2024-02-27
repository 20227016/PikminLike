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
    private float _liftHeightweight = 0.7f;

    /// <summary>
    /// ルートを決めるクラス
    /// </summary>
    private RootClass _rootingClass = default;

    /// <summary>
    /// ルートが入る
    /// </summary>
    private List<Vector3> _root = new List<Vector3> { };

    /// <summary>
    /// 
    /// </summary>
    private Vector3 _moveValue = default;

    //運べるようになった判定
    private bool _isCarray;

    /// <summary>
    /// 持たれている数
    /// </summary>
    private int _heldCount = default;

    /// <summary>
    /// 運ばれる速さ
    /// </summary>
    private float _carraySpeed = default;

    /// <summary>
    /// 運ばれる速さのメモリー
    /// </summary>
    private float _carraySpeedMemory = default;

    #endregion

    #region メソッド  

    private void Start()
    {

        //スクリプト取得
        _rootingClass = GameObject.Find ( "Rooting" ).GetComponent<RootClass> ();
        //移動ルートを取得
        _root　=  _rootingClass.Rooting ( this.transform , _root);
    }

    /// <summary>  
    /// 更新処理  
    /// </summary>  
    void Update ()
    {

        //持たれている数が重さを上回った時
        if (_isCarray)
        {

            //重さで割った加算してきた速さを入れる
            _carraySpeed = _carraySpeedMemory / _weight;

        }

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="carryCapacity">運搬容量</param>
    /// <param name="carryCapacity">荷物を持ったキャラクターの速さ</param>
    public void BeHeld(int carryWeight , float speed)
    {
        Debug.Log ( "Hold" );

        //持たれている数を足す
        _heldCount += carryWeight;
        //持っているオブジェクトの速さを加算
        _carraySpeedMemory += speed;

        //持たれている数が重さを上回った時
        if (_weight <= _heldCount)
        {

            //運ばれてる判定にする
            _isCarray = true;

            //持ち上げられる処理
            this.transform.position = this.transform.position + Vector3.up * _liftHeightweight;

            print ( "持ち上げられる" );
        }

    }

    public void BePlaced(int carryWeight , float speed)
    {
        Debug.Log ( "Put" );

        //持たれている数を減らす
        _heldCount -= carryWeight;
        //持っているオブジェクトの速さを減算
        _carraySpeedMemory -= speed;

        //持たれている数が重さを下回った時
        if (_weight >= _heldCount)
        {

            //運ばれてない判定にする
            _isCarray = false;

            //降ろされる処理
            this.transform.position = this.transform.position - Vector3.up * _liftHeightweight;
            print ( "降ろされる" );
        }
    }

    #endregion
}
