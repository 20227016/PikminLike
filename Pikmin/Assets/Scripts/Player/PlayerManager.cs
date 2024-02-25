// ---------------------------------------------------------  
// PlayerManager.cs  
//   プレイヤーオブジェクトのマネージャー
// 作成日:  2/15
// 作成者:  湯元来輝
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;


public class PlayerManager : MonoBehaviour, IGetValue
{

    #region 変数  
    [Header ( "オブジェクト" )]
    [SerializeField, Tooltip ( "Cameraのオブジェクト" )]
    private GameObject _cameraObj = default;

    [Header ( "スクリプト" )]
    [SerializeField, Tooltip ( "Moveスクリプト" )]
    private Move _moveClass = default;
    [SerializeField, Tooltip ( "Moveスクリプト" )]
    private Rotate _rotateClass = default;
    [SerializeField, Tooltip ( "Holdスクリプト" )]
    private Hold _holdClass = default;
    [SerializeField, Tooltip ( "Putスクリプト" )]
    private Put _putClass = default;

    [Header ( "InputSystem" )]
    [SerializeField, Tooltip ( "InputSystemのMoveが入る" )]
    private InputActionReference _onMove;

    [Header ( "ステータス" )]
    //歩く速さ
    [SerializeField, Tooltip ( "歩く速さ" )]
    private float _speed = 10f;
    [SerializeField, Tooltip ( "回転する速さ" )]
    private float _roteSpeed = 10f;
    [SerializeField, Tooltip ( "移動する角度" )]
    private float _moveAngle = 10f;

    /// <summary>
    /// プレイヤーが向く方向のベクトル
    /// </summary>
    Vector3 _playerRotaVec = default;


    #endregion

    #region プロパティ

    public float GetSpeed
    {

        get => _speed;
    }

    #endregion

    #region メソッド

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {

        //ボタンが入力判定の時
        if (_onMove.action.IsPressed ())
        {

            //目的の回転ベクトルを元に、プレイヤーの方向を向く回転を取得
            Quaternion targetRotation = Quaternion.LookRotation ( _playerRotaVec );

            // カメラの回転を取得し、Z軸を基準に-180〜180度の範囲に変換
            float objectAngle = transform.eulerAngles.y;

            ////オブジェクトの角度が180度を超えたとき
            //if (objectAngle > 180f)
            //{

            //    //
            //    objectAngle -= 360f;
            //}
              

            // 目標回転角度を取得し、Z軸を基準に-180〜180度の範囲に変換
            float targetAngle = targetRotation.eulerAngles.y;

            ////オブジェクトの角度が180度を超えたとき
            //if (targetAngle > 180f)
            //{

            //    //
            //    targetAngle -= 360f;
            //}
                

            // オブジェクトの回転角度と目標回転角度の差分を計算
            float angleDifference = Mathf.Abs ( targetAngle - objectAngle );

            //入力方向と向いている方向があった時
            if (angleDifference <= _moveAngle)
            {

                //移動
                _moveClass.MoveMethod ( this.transform , _speed );

            }
            else
            {

                //回転
                _rotateClass.RotaMethod ( this.transform , targetRotation , _roteSpeed );
            }
        }


    }


    public void OnMove(InputAction.CallbackContext context)
    {

        //入力値
        Vector2 inputValue = context.ReadValue<Vector2> ();

        //入力値のベクトル
        Vector3 inputVec = new Vector3 ( inputValue.x , 0 , inputValue.y );

        /*
         * カメラの方向に前と右向きのベクトル（1,0,1)をかけて正規化することで
         * XとZ平面の単位ベクトル（1・0のベクトル）を取得
         */
        Vector3 cameraRotaVec = Vector3.Scale ( _cameraObj.transform.forward , Vector3.forward + Vector3.right ).normalized;


        /*
         * 入力値とカメラの向きから、移動方向を決定
         * ワールドから見た入力ベクトル取得
         */
        _playerRotaVec = (cameraRotaVec * inputVec.z) + (_cameraObj.transform.right * inputVec.x);

    }

    public void OnHold(InputAction.CallbackContext context)
    {


    }

    public void OederAttach(InputAction.CallbackContext context)
    {

        Debug.Log ( "つける入力" );
    }

    public void OederCall(InputAction.CallbackContext context)
    {

        Debug.Log ( "呼ぶ入力" );
    }

    public void OederLineUp(InputAction.CallbackContext context)
    {

        Debug.Log ( "並ぶ入力" );
    }


    #endregion
}
