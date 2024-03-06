// ---------------------------------------------------------  
// Pointer.cs  
//   
// 作成日:  3/5
// 作成者:  湯元来輝
// ---------------------------------------------------------  
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class CursorClass : MonoBehaviour
{

    #region 変数  

    [Header ( "トランスフォーム" )]
    [SerializeField, Tooltip ( "Playerオブジェクトのトランスフォーム" )]
    private Transform _playerTrans = default;
    [Header ( "スクリプト" )]
    [SerializeField, Tooltip ( "Moveスクリプト" )]
    private WalkClass _wakeClass = default;
    [SerializeField, Tooltip ( "Rotateスクリプト" )]
    private RotateClass _rotateClass = default;
    [Header ( "InputSystem" )]
    [SerializeField, Tooltip ( "InputSystemのMoveが入る" )]
    private InputActionReference _onCursorMove = default;
    [Header ( "ステータス" )]
    [SerializeField, Tooltip ( "ポインターの動く速さ" )]
    private float _speed = 20f;
    [SerializeField, Tooltip ( "プレイヤーとの距離" )]
    private float _pointerDist = 15f;
    [SerializeField, Tooltip ( "Rayの始点の高さ" )]
    private float _rayHeight = 30f;

    /// <summary>
    /// Rayの情報が入る
    /// </summary>
    private RaycastHit _hit;

    /// <summary>
    /// 動く方向が入る
    /// </summary>
    private Vector3 _moveVec　= default;

    /// <summary>
    /// ポインターのRay方向が入る
    /// </summary>
    private Vector3 _direction = default;

    /// <summary>
    /// Rayの目指す位置
    /// </summary>
    private Vector3 _targetPos = default;

    #endregion

    #region メソッド  


    private void Start()
    {
        //
        _targetPos = Vector3.forward * _playerTrans.position.z +
                     Vector3.right * _playerTrans.position.x;
    }

    private void Update()
    {

        if (_onCursorMove.action.IsPressed ())
        {

            DrowRay ();
            MoveCursor ();
        }
    }

    /// <summary>
    /// カーソルの値を取得
    /// </summary>
    public void OnCursor(InputAction.CallbackContext context)
    {

        //入力値取得
        Vector3 inputValue = context.ReadValue<Vector2> ();

        //入力値のY軸の値とZ軸の値を入れ替える
        inputValue = Vector3.right * inputValue.x +
                     Vector3.up * 0 +
                     Vector3.forward * inputValue.y;

        //カメラのY軸以外の単位ベクトル（1・0のベクトル）を取得
        Vector3 cameraForward = Camera.main.transform.forward .normalized;
        cameraForward.y = 0;
        Vector3 cameraRight = Camera.main.transform.right.normalized;
        cameraRight.y = 0;

        //カメラから見た入力値を取得
        _moveVec = (cameraForward * inputValue.z) + (cameraRight * inputValue.x);
        //print ( _moveVec );
    }


    private void MoveCursor()
    {

        this.transform.position = _hit.point;
    }

    private void DrowRay()
    {

        //Rayを打つ始発地点 （プレイヤーの頭の上）
        Vector3 origin = _playerTrans.position +
                         Vector3.up * _rayHeight;

        //x軸の位置を求める
        _targetPos.x += _moveVec.x * _speed * Time.deltaTime;
        //z軸の位置を求める
        _targetPos.z += _moveVec.z * _speed * Time.deltaTime;

        //距離の固定化
        _targetPos.x = Mathf.Clamp ( _targetPos.x , _playerTrans.position.x - _pointerDist , _playerTrans.position.x + _pointerDist);
        _targetPos.z = Mathf.Clamp ( _targetPos.z , _playerTrans.position.z - _pointerDist , _playerTrans.position.z + _pointerDist);

        // Rayの方向を取得
        _direction = _targetPos - origin  ;

        Ray ray = new Ray ( origin , _direction );

        Physics.Raycast ( ray , out _hit );

        //Debug.DrawRay ( origin , _direction * 100f , Color.green , 3f );
    }

    #endregion
}
