// ---------------------------------------------------------  
// Move.cs  
//   引数とオブジェクトを利用して移動する
// 作成日:  2/16~16
// 作成者:  湯元来輝
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;


public class Move : MonoBehaviour
{

    #region 変数  

    [Header("スクリプト")]
    [SerializeField, Tooltip("MoveCheckスクリプト")]
    private MoveCheck _moveCheckClass = default;

    #endregion

    #region メソッド  

    /// <summary>
    /// 移動する方向を決める
    /// </summary>
    /// <param name="objTransfrom">関数を呼び出したクラスがアタッチされているオブジェクトのトランスフォーム</param>
    /// /// <param name="moveDirection">移動方向の値（0か1で入っている）</param>
    /// <param name="speed">オブジェクトの移動する速さ</param>
    public void MoveMethod(Transform objTransfrom , float speed )
    {
        //移動先に障害物があった時
        //if (_moveCheckClass.Check(objTransfrom, moveDirection).collider == true)
        //{

        //    return;
        //}

        //速さで移動する
        objTransfrom.position += transform.forward * speed * Time.deltaTime;

    }

    public void RoteMethod(Transform objTransfrom , float roteDirection , float roteSpeed)
    {
        //回転させる
        objTransfrom.rotation *= Quaternion.Euler(0 , roteDirection * roteSpeed , 0);
    }
    #endregion
}
