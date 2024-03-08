// ---------------------------------------------------------
// Walk.cs
//   引数とオブジェクトを利用して移動する
// 作成日:  2/16
// 作成者:  湯元来輝
// ---------------------------------------------------------
using UnityEngine;
using System.Collections;


public class WalkClass : MonoBehaviour
{


    /// <summary>
    /// 移動する方向を決める
    /// </summary>
    /// <param name="objTransfrom">呼び出したオブジェクトのトランスフォーム</param>
    /// <param name="moveDirection">オブジェクトの移動する方向</param>
    /// <param name="speed">オブジェクトの移動する速さ</param>
    public void Walk(Transform objTransfrom , Vector3 moveDirection , float speed)
    {

        //速さで移動する
        objTransfrom.position += moveDirection * speed * Time.deltaTime;
    }


}
