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

    public void Rotate(Transform cameraTrans , Transform playerTrans , float inputValue)
    {
        //プレイヤーを中心に回転
        cameraTrans.RotateAround(playerTrans.transform.position , Vector3.up , inputValue * Time.deltaTime);
    }
  
    #endregion
}
