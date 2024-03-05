// ---------------------------------------------------------  
// RotateClass.cs  
//   キャラクターを目的の角度まで回転させる
// 作成日:  2/20
// 作成者:  湯元来輝
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;

public class RotateClass : MonoBehaviour
{


    /// <summary>
    /// キャラクターを回転させる
    /// </summary>
    /// <param name="objTransfrom">呼んだオブジェクトのトランスフォーム</param>
    /// <param name="roteDirection">目的の回転方向</param>
    /// <param name="roteSpeed">回転の速さ</param>
    public void Rotate(Transform objTransfrom , Quaternion roteDirection , float roteSpeed)
    {

        //回転させる
        objTransfrom.rotation = Quaternion.RotateTowards(objTransfrom.rotation , roteDirection , roteSpeed  * Time.deltaTime) ;
    }

}
