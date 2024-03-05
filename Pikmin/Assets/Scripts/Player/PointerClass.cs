// ---------------------------------------------------------  
// Pointer.cs  
//   
// 作成日:  3/5
// 作成者:  湯元来輝
// ---------------------------------------------------------  
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PointerClass : MonoBehaviour
{

    #region 変数  

    [Header ( "オブジェクト" )]
    [SerializeField, Tooltip ( "pointerのオブジェクト" )]
    private GameObject _pointerObj = default;
    [SerializeField, Tooltip ( "Cameraのオブジェクト" )]
    private GameObject _cameraObj = default;
    [Header ( "InputSystem" )]
    [SerializeField, Tooltip ( "InputSystemのMoveが入る" )]
    private InputActionReference _onMove = default;
    [Header ( "ステータス" )]
    [SerializeField, Tooltip ( "ポインターの動く速さ" )]
    private float _speed = 20f;
    [SerializeField, Tooltip ( "プレイヤーとの距離" )]
    private float _pointerDist = 15f;
    [SerializeField, Tooltip ( "Rayの始点の高さ" )]
    private float _rayHeight = 30f;

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

    //入力値
    private Vector3 _inputValue = default;
    #endregion

    #region メソッド  


    private void Update()
    {

        if (_onMove.action.IsPressed ())
        {

            DrowRay ();
        }
    }

    /// <summary>
    /// ポインターの値を取得
    /// </summary>
    public void OnPointer(InputAction.CallbackContext context)
    {

        //入力値取得
        _inputValue = context.ReadValue<Vector2> ();

        //入力値のY軸の値とZ軸の値を入れ替える
        _inputValue = Vector3.right * _inputValue.x +
                     Vector3.up * 0 +
                     Vector3.forward * _inputValue.y;

        //カメラのY軸以外の単位ベクトル（1・0のベクトル）を取得
        Vector3 cameraForward = Camera.main.transform.forward .normalized;
        cameraForward.y = 0;
        Vector3 cameraRight = Camera.main.transform.right.normalized;
        cameraRight.y = 0;

        //カメラから見た入力値を取得
        _moveVec = (cameraForward * _inputValue.z) + (cameraRight * _inputValue.x);

    }


    private void MakeDirection()
    {


    }

    private void DrowRay()
    {

        //Rayを打つ始発地点 （プレイヤーの頭の上）
        Vector3 origin = transform.position +
                         Vector3.up * _rayHeight;

        //プレイヤーの位置を初期値に
        _targetPos = Vector3.forward * this.transform.position.z +
                     Vector3.right * this.transform.position.x;

        //x軸の位置を求める
        _targetPos.x += _moveVec.x * _speed * Time.deltaTime;
        //z軸の位置を求める
        _targetPos.z += _moveVec.z * _speed * Time.deltaTime;


        //距離の固定化
        _targetPos.x = Mathf.Clamp ( _targetPos.x , this.transform.position.x - _pointerDist , this.transform.position.x + _pointerDist);
        _targetPos.z = Mathf.Clamp ( _targetPos.z , this.transform.position.x - _pointerDist , this.transform.position.x + _pointerDist);

       

        // Rayの方向を取得
        _direction = _targetPos - origin;

        Ray ray = new Ray ( origin , _direction );

        Debug.Log ( "デバック" );
        Debug.DrawRay ( origin , _direction * 100f , Color.green , 3f );
    }

    #endregion
}
