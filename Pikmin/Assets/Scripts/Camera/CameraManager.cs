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
    CameraMove _cameraMove = new CameraMove();
    CameraRota _cameraRota = new CameraRota();

    [Header("オブジェクト")]
    [SerializeField, Tooltip("Playerのオブジェクト")]
    private GameObject _playerObj = default;
    [SerializeField, Tooltip("Playerのリジットボディ")]
    private Rigidbody _playerRB = default;

    [Header("スクリプト")]
    [SerializeField, Tooltip("Moveスクリプト")]
    private PlayerManager _playerManagerClass = default;

    [Header("InputSystem")]
    [SerializeField, Tooltip("InputSystemのRoteが入る")]
    private InputActionReference _onCametaRote = default;

    [Header("ステータス")]
    [SerializeField, Tooltip("プレイヤーを基準としたカメラの高さ")]
    private float _cameraHeight = 5f;
    [SerializeField, Tooltip("プレイヤーとカメラの最大距離  [X , Z]")]
    private float[] _maxDistanceXZ = new float[ ] { 2, 10};
    [SerializeField, Tooltip("プレイヤーとカメラの最小距離  [X , Z]")]
    private float[] _minDistanceXZ = new float[]{ 0, 8};
    [SerializeField, Tooltip("追跡する速さ")]
    private float _speed = 5f;
    [SerializeField, Tooltip("カメラが回転する速さ")]
    private float _roteSpeed = 5f;

    //カメラが目指す位置
    private Vector3 _cameraTargetPos = default;

    //インプットコールバックの値
    private float _inputValue = default;


    #endregion

    #region メソッド  

    private void Start()
    {

        transform.position = new Vector3(_minDistanceXZ[0] , _cameraHeight, _minDistanceXZ[1]);
        transform.LookAt(_playerObj.transform.position);
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
            _cameraRota.Rotate(this.transform , _playerObj.transform , _inputValue);
        }

        //プレイヤーが動いている場合
        if (IsMovePlayer() == false)
        {
            return;
        }

        //プレイヤーを追跡
        _cameraMove.Tracking(_playerObj.transform,this.transform , _maxDistanceXZ , _minDistanceXZ);



     }



    public void OnRote(InputAction.CallbackContext context)
    {

        //入力値を取得
        _inputValue = context.ReadValue<float>() * _roteSpeed ;
    }

    private bool IsMovePlayer()
    {

        //プレイヤーが動いている場合
        if (_playerRB.velocity.magnitude > 0)
        {

            Debug.Log("PlayerMove");
            return true;
        }

        return false;
    }



    #endregion
}
