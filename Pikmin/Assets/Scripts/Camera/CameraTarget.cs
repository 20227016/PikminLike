// ---------------------------------------------------------  
// CameraRota.cs  
//   カメラの移動先を決める
// 作成日:  2/21
// 作成者:  湯元来輝
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;

public class CameraTarget 
{
    //カメラのトランスフォームのコピー
    private Transform _cameraOffset;
    //プレイヤーの初期の高さ
    private const float CONST_HEIGHT = 2.5f; 

    public CameraTarget()
    {
        _cameraOffset = default;
    }

    /// <summary>
    /// カメラをプレイヤーを中心に回転させる
    /// </summary>
    /// <param name="playerTrans">プレイヤーのトランスフォーム</param>
    /// <param name="targetTrans">ターゲットのトランスフォーム</param>
    /// <param name="cameraTaransCopy">カメラのトランスフォームのコピー</param>
    /// <param name="inputValue">入力値</param>
    /// <returns>移動先</returns>
    public Transform Target( Transform playerTrans ,Transform targetTrans, float inputValue)
    {
   

        //プレイヤーの位置を加算
        targetTrans.position = _cameraOffset.position + playerTrans.position  ;

        //プレイヤーを中心に回転
        targetTrans.RotateAround ( playerTrans.position , Vector3.up , inputValue * Time.deltaTime);

        //回転した後のベクトルからプレイヤーのベクトルを抜きコピーに代入
        _cameraOffset.position = targetTrans.position - playerTrans.position;

        //ターゲットの位置を返す
        return targetTrans;
    }

    /// <summary>
    /// カメラのトランスフォームの値をコピーする
    /// </summary>
    public void CopyTransformValues(float transY , float transZ)
    {
        _cameraOffset = new GameObject ( "CameraTransCopy" ).transform;
        _cameraOffset.position = Vector3.up * transY +
                                 Vector3.forward * transZ;
    }
}
