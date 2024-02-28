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
    private Transform _cameraTaransCopy = default;
    //プレイヤーの初期の高さ
    private const float CONST_HEIGHT = 2.5f; 

    public CameraTarget()
    {
        _cameraTaransCopy = default;
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
        targetTrans.position = ((_cameraTaransCopy.position.x + playerTrans.position.x)  * Vector3.right  ) +
                               ((_cameraTaransCopy.position.y + playerTrans.position.y)  * Vector3.up     ) +
                               ((_cameraTaransCopy.position.z + playerTrans.position.z)  * Vector3.forward);

        //プレイヤーを中心に回転
        targetTrans.RotateAround ( playerTrans.position , Vector3.up , inputValue * Time.deltaTime);

        //回転した後のベクトルからプレイヤーのベクトルを抜きコピーに代入
        _cameraTaransCopy.position = targetTrans.position - playerTrans.position;

        //ターゲットの位置を返す
        return targetTrans;
    }

    /// <summary>
    /// カメラのトランスフォームの値をコピーする
    /// </summary>
    public void CopyTransformValues(Transform cameraTrans)
    {
        _cameraTaransCopy = new GameObject ( "CameraTransCopy" ).transform;
        _cameraTaransCopy.position = cameraTrans.position;
        _cameraTaransCopy.rotation = cameraTrans.rotation;
        _cameraTaransCopy.localScale = cameraTrans.localScale;
    }
}
