// ---------------------------------------------------------  
// CameraMove.cs  
//   
// 作成日:  2/21
// 作成者:  湯元来輝
// ---------------------------------------------------------  
using UnityEngine;

public class CameraTrack 
{

    /// <summary>
    /// プレイヤー近くの時の速度
    /// </summary>
    private Vector3 _targetPos;

    public CameraTrack()
    {

        _targetPos = default;
    }

    #region メソッド  


    /// <summary>
    /// プレイヤーを追跡
    /// </summary>
    /// <param name="playerTrans">プレイヤーのトランスフォーム</param>
    /// <param name="cameraTrans">カメラのトランスフォーム</param>
    /// <param name="cameraSpeed">プレイヤーと等速</param>
    /// /// <param name="cameraFixDistans">カメラとプレイヤーの距離</param>
    public void Tracking(Transform playerTrans , Transform cameraTrans , float cameraSpeed , float cameraFixDistans )
    {
        //移動先のポジションを作る

        //プレイヤーからの距離
        _targetPos = new Vector3 ( 0 , cameraTrans.position.y - playerTrans.position.y , cameraFixDistans );

        //プレイヤーの位置を足し、カメラがあってほしい位置を取得
        _targetPos += playerTrans.position;

        //カメラを更新してしまうため回転できなくなる
        //カメラを移動
        cameraTrans.position = Vector3.Lerp ( cameraTrans.position , _targetPos , cameraSpeed * Time.deltaTime );
    }
    #endregion
}
