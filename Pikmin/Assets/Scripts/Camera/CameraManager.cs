// ---------------------------------------------------------  
// MoveCamera.cs  
//   
// 作成日:  2/20~2/20
// 作成者:  湯元来輝
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class CameraManager : MonoBehaviour
{

    #region 変数  

    [Header("オブジェクト")]
    [SerializeField, Tooltip("Playerのオブジェクト")]
    private GameObject _playerObj = default;
    [Header("スクリプト")]
    [SerializeField, Tooltip("Moveスクリプト")]
    private PlayerManager _playerManagerClass = default;
    [Header("InputSystem")]
    [SerializeField, Tooltip("InputSystemのRoteが入る")]
    private InputActionReference _onCametaRote;
    [Header("ステータス")]
    [SerializeField, Tooltip("カメラが回転する速さ")]
    private float _roteSpeed = 5f;
    //インプットコールバックの値
    private float _inputValue = default;

    #endregion

    #region メソッド  

    /// <summary>  
    /// 更新処理  
    /// </summary>  
    void Update ()
     {

        //入力している時
        if (_onCametaRote.action.IsInProgress())
        {

            //プレイヤーを中心に回転
            transform.RotateAround(_playerObj.transform.position,Vector3.up , _inputValue * Time.deltaTime);
        }

     }



    public void OnRote(InputAction.CallbackContext context)
    {

        //入力値を取得
        _inputValue = context.ReadValue<float>() * _roteSpeed ;
    }

    #endregion
}
