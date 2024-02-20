// ---------------------------------------------------------  
// PlayerManager.cs  
//   プレイヤーオブジェクトのマネージャー
// 作成日:  2/15
// 作成者:  湯元来輝
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;


public class PlayerManager : MonoBehaviour
{

    #region 変数  
    [Header("オブジェクト")]
    [SerializeField, Tooltip("Cameraのオブジェクト")]
    private GameObject _cameraObj = default;

    [Header("スクリプト")]
    [SerializeField, Tooltip("Moveスクリプト")]
    private Move _moveClass = default;
    [SerializeField, Tooltip("Moveスクリプト")]
    private Rotate _rotateClass = default;
    [SerializeField, Tooltip("Holdスクリプト")]
    private Hold _holdClass = default;
    [SerializeField, Tooltip("Putスクリプト")]
    private Put _putClass = default;

    [Header("InputSystem")]
    [SerializeField, Tooltip("InputSystemのMoveが入る")]
    private InputActionReference _onMove;

    [Header("ステータス")]
    //歩く速さ
    [SerializeField, Tooltip("歩く速さ")]
    private float _speed = 10f;
    [SerializeField, Tooltip("回転する速さ")]
    private float _roteSpeed = 10f;
    [SerializeField, Tooltip("移動する角度")]
    private float _moveAngle = 10f;

    /// <summary>
    /// プレイヤーが向く方向のベクトル
    /// </summary>
    Vector3 _playerRoteVec = default;
    #endregion


    #region メソッド

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {

        //ボタンが入力判定の時
        if (_onMove.action.IsPressed())
        {

            //入力方向と向いている方向があった時
            if (transform.eulerAngles == _playerRoteVec)
            {

                print("MOVE");
                //移動
                _moveClass.MoveMethod(this.transform , _speed);

            }
            else
            {

                //回転
                _rotateClass.RoteMethod(this.transform , _playerRoteVec , _roteSpeed);
            }
        }


    }


    public void OnMove(InputAction.CallbackContext context)
    {

        Vector2 inputValue = context.ReadValue<Vector2>();

        Vector3 inputVec = new Vector3(inputValue.x,0,inputValue.y);

        print(inputVec + "　　入力ベクトル");

        /*
         * カメラの方向に前と右向きのベクトル（1,0,1)をかけて
         * XとZ平面の単位ベクトルを取得
         */
        Vector3 cameraRoteVec = Vector3.Scale(_cameraObj.transform.forward , Vector3.forward + Vector3.right ).normalized;

        print(cameraRoteVec + "　　カメラベクトル");

        /*
         * 入力値とカメラの向きから、移動方向を決定
         * 
         */
        _playerRoteVec = (cameraRoteVec * inputVec.z) + (_cameraObj.transform.right * inputVec.x);

        print(_playerRoteVec + "　　回転方向ベクトル");
    }

    public void OnHold(InputAction.CallbackContext context)
    {


    }

    public void OederAttach(InputAction.CallbackContext context)
    {

        Debug.Log("つける入力");
    }

    public void OederCall(InputAction.CallbackContext context)
    {

        Debug.Log("呼ぶ入力");
    }

    public void OederLineUp(InputAction.CallbackContext context)
    {

        Debug.Log("並ぶ入力");
    }


    #endregion
}
