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
    //private const float CONST_SLOWVALUE = 0.7f;

    #region メソッド  


    /// <summary>
    /// プレイヤーを追跡
    /// </summary>
    /// <param name="playerTrans"プレイヤーのトランスフォーム></param>
    /// <param name="cameraTrans">カメラのトランスフォーム</param>
    /// <param name="playeRB">プレイヤーのリジットボディー</param>
    /// /// <param name="cameraFixDistans">カメラとプレイヤーの距離</param>
    /// <param name="maxSpeedDist">カメラがプレイヤーと等速になる距離</param>
    public void Tracking(Transform playerTrans , Transform cameraTrans , float cameraSpeed , float cameraFixDistans ,  float maxSpeedDist)
    {

        //Z軸のプレイヤーとの距離を取得
         float dist = GetDistans (playerTrans ,cameraTrans);

        cameraSpeed = CalculateSpeed ( dist , cameraSpeed , cameraFixDistans, maxSpeedDist );

        //カメラを移動させる
        AdjustCamera(cameraTrans,cameraSpeed);
    }

    /// <summary>
    /// プレイヤーとの距離を測る
    /// </summary>
    /// <param name="playerTrans">プレイヤーのトランスフォーム</param>
    /// <param name="cameraTrans">カメラのトランスフォーム</param>
    private float GetDistans(Transform playerTrans , Transform cameraTrans)
    {

        //X軸のプレイヤーとの距離を取得
        float dist = playerTrans.transform.position.z - cameraTrans.position.z;
        Debug.LogWarning ( dist );
        return dist;
    }



    /// <summary>
    /// 移動する速さを求める
    /// </summary>
    /// <param name="dist">プレイヤーとの距離</param>
    /// <param name="cameraSpeed">プレイヤーと等速の値</param>
    /// /// <param name="cameraFixDistans">プレイヤーとの望ましい間隔（この距離になるようにカメラを移動）</param>
    /// /// <param name="maxSpeedDist">最大速度になる望ましい間隔との間隔（近くに来た時と離れた時）</param>
    /// <returns>計算された速度</returns>
    private float CalculateSpeed(float dist, float cameraSpeed , float cameraFixDistans , float maxSpeedDist)
    {
        //プレイヤーが近くにいるときの速度の割合
        const float CONST_SLOWVALUE = 0.7f;

        //プレイヤーとの望ましい間隔からのずれを取得
        //-34  -17/15
        float playerDesDist = cameraFixDistans - dist;

        Debug.LogError ( playerDesDist　+"="+cameraFixDistans+"+"+dist);

        if (playerDesDist == 0)
        {
            cameraSpeed = 0;
        }
        else
        {
            // プレイヤーが近くにいる場合は割合を適用
            cameraSpeed = cameraSpeed * (1 - Mathf.Clamp01 ( playerDesDist / maxSpeedDist ) * (1 - CONST_SLOWVALUE));
        }


        return cameraSpeed;
    }

    //// プレイヤーの速さから何割減少させるかを計算（最大で70%）
    //float speedRatio = playerDesDist / maxSpeedDist * 0.7f;
    //Debug.Log ( speedRatio + "パーセンテージ" );
    //// 減速後の速度を計算
    //cameraSpeed = cameraSpeed * (1 - speedRatio);

    //Debug.Log ( cameraSpeed + "減速" );


    /// <summary>
    /// 
    /// </summary>
    private void AdjustCamera(Transform cameraTrans , float cameraSpeed)
    {

        cameraTrans.position += new Vector3 ( 0 , 0 , cameraSpeed );
    }
    #endregion
}
