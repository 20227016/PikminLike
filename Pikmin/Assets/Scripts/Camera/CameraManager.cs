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

    //インスタンス化
    CameraTrack _cameraMove = new CameraTrack();
    CameraRota _cameraRota = new CameraRota();

    [Header("オブジェクト")]
    [SerializeField, Tooltip("PlayerのTransform")]
    private Transform _playerTrans = default;

    [Header("スクリプト")]
    [SerializeField, Tooltip("Moveスクリプト")]
    private PlayerManager _playerManagerClass = default;

    [Header("InputSystem")]
    [SerializeField, Tooltip("InputSystemのRoteが入る")]
    private InputActionReference _onCametaRote = default;

    [Header("ステータス")]
    [SerializeField, Tooltip("プレイヤーを基準としたカメラの高さ")]
    private float _cameraHeight = 13f;
    [SerializeField, Tooltip("プレイヤーとカメラの距離")]
    private float _cameraFixDistance = -15;
    [SerializeField, Tooltip("カメラが回転する速さ")]
    private float _roteSpeed = 5f;

    //プレイヤーの速さ
    private float _cameraSpeed = default; 

    //カメラが目指す位置
    private Vector3 _cameraTargetPos = default;

    //インプットコールバックの値
    private float _inputValue = default;


    #endregion

    #region メソッド  

    private void Start()
    {
        //インターフェースからプレイヤーの速さを取得
        _cameraSpeed = _playerTrans.gameObject.GetComponent<IGetValue> ().GetSpeed;
        transform.position = new Vector3(_playerTrans.position.x , _cameraHeight, _cameraFixDistance);
        transform.LookAt(transform.position);
    }

    /// <summary>  
    /// 更新処理  
    /// </summary>  
    void Update ()
     {

        //入力している時
        if (_onCametaRote.action.IsInProgress())
        {

            //カメラを回転させる
            _cameraRota.Rotate(this.transform , _playerTrans , _inputValue);
        }

        //プレイヤーを追跡
        _cameraMove.Tracking( _playerTrans.transform , this.transform , _cameraSpeed , _cameraFixDistance );
     }



    public void OnRote(InputAction.CallbackContext context)
    {

        //入力値を取得
        _inputValue = context.ReadValue<float>() * _roteSpeed ;
    }



    #endregion
}
