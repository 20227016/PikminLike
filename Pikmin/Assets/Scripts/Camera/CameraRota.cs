// ---------------------------------------------------------  
// CameraRota.cs  
//   
// 作成日:  2/21
// 作成者:  
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;

public class CameraRota 
{

    #region メソッド  

    public void Rotate(Transform cameraTrans , Transform playerObjTrans , float inputValue)
    {
        //プレイヤーを中心に回転
        cameraTrans.RotateAround(playerObjTrans.transform.position , Vector3.up , inputValue * Time.deltaTime);
    }
  
    #endregion
}
