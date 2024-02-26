// ---------------------------------------------------------  
// Target.cs  
//   
// 作成日:  2/20
// 作成者:  湯元来輝
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour
{


    public void RotaMethod(Transform objTransfrom , Quaternion roteDirection , float roteSpeed)
    {

        //回転させる
        objTransfrom.rotation =Quaternion.RotateTowards(objTransfrom.rotation , roteDirection , roteSpeed  * Time.deltaTime) ;
    }

}
