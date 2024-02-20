// ---------------------------------------------------------  
// Rotate.cs  
//   
// 作成日:  2/20
// 作成者:  湯元来輝
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour
{


    public void RoteMethod(Transform objTransfrom , Vector3 roteDirection , float roteSpeed)
    {

        roteDirection *= roteSpeed * Time.deltaTime;

        //回転させる
        objTransfrom.eulerAngles += roteDirection;
    }

}
