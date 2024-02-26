// ---------------------------------------------------------  
// CameraMove.cs  
//   
// 作成日:  2/21
// 作成者:  湯元来輝
// ---------------------------------------------------------  
using UnityEngine;

public class CameraTrack 
{

    #region メソッド  


    /// <summary>
    /// プレイヤーを追跡
    /// </summary>
    /// <param name="playerTrans">プレイヤーのトランスフォーム</param>
    /// <param name="cameraTrans">カメラのトランスフォーム</param>
    /// /// <param name="targetTrans">移動先</param>
    /// <param name="cameraSpeed">プレイヤーと等速</param>
    public void Tracking(Transform playerTrans ,Transform cameraTrans, Transform targetTrans , float cameraSpeed)
    {

        cameraSpeed *= 0.3f;

        //カメラを移動
        cameraTrans.position = Vector3.Lerp ( cameraTrans.position , targetTrans.position , cameraSpeed * Time.deltaTime );


    }
    #endregion
}
