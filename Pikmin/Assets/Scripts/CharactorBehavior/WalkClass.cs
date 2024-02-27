// ---------------------------------------------------------
// Walk.cs
//   引数とオブジェクトを利用して移動する
// 作成日:  2/16~16
// 作成者:  湯元来輝
// ---------------------------------------------------------
using UnityEngine;
using System.Collections;


public class WalkClass : MonoBehaviour
{

    #region 変数

    [Header("スクリプト")]
    [SerializeField, Tooltip("MoveCheckスクリプト")]
    private MoveCheck _walkCheckClass = default;

    #endregion

    #region メソッド

    /// <summary>
    /// 移動する方向を決める
    /// </summary>
    /// <param name="objTransfrom">呼び出したオブジェクトのトランスフォーム</param>
    /// <param name="speed">オブジェクトの移動する速さ</param>
    public void MoveMethod(Transform objTransfrom , float speed)
    {

        //速さで移動する
        objTransfrom.position += objTransfrom.forward * speed * Time.deltaTime;
    }


    #endregion
}
