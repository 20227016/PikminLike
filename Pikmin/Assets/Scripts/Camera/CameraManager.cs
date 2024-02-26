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
    CameraTrack _cameraMoveClass = new CameraTrack();
    CameraTarget _cameraTargetClass = new CameraTarget();

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
    private float _cameraHeight = 10f;
    [SerializeField, Tooltip("開始時のプレイヤーとカメラの距離")]
    private float _cameraFixDistance = -15;
    [SerializeField, Tooltip("カメラが回転する速さ")]
    private float _roteSpeed = 5f;

    //プレイヤーの速さ
    private float _cameraSpeed = default; 

    //インプットコールバックの値
    private float _inputValue = default;

    //カメラ移動の目的の位置
    private Transform _targetTrans = default;

    #endregion

    #region メソッド  

    private void Start()
    {
        //インターフェースからプレイヤーの速さを取得
        _cameraSpeed = _playerTrans.gameObject.GetComponent<IGetValue> ().GetSpeed;
        transform.position = new Vector3(_playerTrans.position.x , _cameraHeight, _cameraFixDistance);
        this.transform.LookAt ( _playerTrans );
        _targetTrans = new GameObject ( "TargetObj" ).transform;
        _cameraTargetClass.CopyTransformValues (this.transform);
    }

    /// <summary>  
    /// 更新処理  
    /// </summary>  
    void Update ()
     {

        //カメラの移動先を取得
        _targetTrans = _cameraTargetClass.Target (  _playerTrans , _targetTrans , _inputValue );

        //回転するためのボタンを押した時
        if (_onCametaRote.action.IsPressed ())
        {

            //プレイヤーを追う
            this.transform.LookAt ( _playerTrans );
        }

        //プレイヤーを追跡
        _cameraMoveClass.Tracking ( _playerTrans.transform , this.transform ,_targetTrans , _cameraSpeed);
        
     }



    public void OnRote(InputAction.CallbackContext context)
    {

        //入力値を取得
        _inputValue = context.ReadValue<float>() * _roteSpeed ;
    }


    #endregion
}
