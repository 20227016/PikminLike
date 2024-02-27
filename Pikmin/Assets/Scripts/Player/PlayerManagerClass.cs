// ---------------------------------------------------------  
// PlayerManager.cs  
//   プレイヤーオブジェクトのマネージャー
// 作成日:  2/15
// 作成者:  湯元来輝
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;


public class PlayerManagerClass : MonoBehaviour, IGetValue
{

    #region 変数  
    [Header ( "オブジェクト" )]
    [SerializeField, Tooltip ( "Cameraのオブジェクト" )]
    private GameObject _cameraObj = default;

    [Header ( "スクリプト" )]
    [SerializeField, Tooltip ( "Moveスクリプト" )]
    private WalkClass _wakeClass = default;
    [SerializeField, Tooltip ( "Rotateスクリプト" )]
    private RotateClass _rotateClass = default;
    [SerializeField, Tooltip ( "MoveCheckスクリプト" )]
    private MoveCheck _moveCheckClass = default;
    [SerializeField, Tooltip ( "Holdスクリプト" )]
    private HoldClass _holdClass = default;
    [SerializeField, Tooltip ( "Putスクリプト" )]
    private PutClass _putClass = default;

    [Header ( "InputSystem" )]
    [SerializeField, Tooltip ( "InputSystemのMoveが入る" )]
    private InputActionReference _onMove = default;
    [SerializeField, Tooltip ( "InputSystemのHoldOrAttachが入る" )]
    private InputActionReference _onHoldOrAttahc = default;
    [SerializeField, Tooltip ( "InputSystemのPutOrCallが入る" )]
    private InputActionReference _onPutOrCall = default;

    [Header ( "ステータス" )]
    [SerializeField, Tooltip ( "歩く速さ" )]
    private float _speed = 10f;
    [SerializeField, Tooltip ( "回転する速さ" )]
    private float _roteSpeed = 10f;
    [SerializeField, Tooltip ( "持てる重さ" )]
    private int _carryWeight = 3;

    /// <summary>
    /// プレイヤーの状態
    /// 持っている
    /// 置いている
    /// が入っている
    /// </summary>
    private PlayerStatus _enumPlayerStatus = PlayerStatus.Put;
    /// <summary>
    /// プレイヤーの命令状況
    /// </summary>
    private PlayerOederStatus _enumPlayerOederStatus = PlayerOederStatus.None;
    /// <summary>
    /// プレイヤーが選んでいるキャラクター
    /// </summary>
    private SelectCharactorStatus _enumSelectCharactorStatus = SelectCharactorStatus.None;

    /// <summary>
    /// 目の前のオブジェクト
    /// </summary>
    private RaycastHit _hit = default;

    /// <summary>
    /// 荷物を持った時の移動ルート
    /// </summary>
    private List<Vector3> _root = default;

    /// <summary>
    /// プレイヤーが向く方向のベクトル
    /// </summary>
    private Vector3 _rotaVec = default;

    /// <summary>
    /// 運ぶ速さ
    /// </summary>
    private float _carrySpeed = default;

    /// <summary>
    /// 連れているロボットの量
    /// </summary>
    private int _robotCount = default;


    #endregion

    #region プロパティ

    public float GetSpeed
    {

        get => _speed;
    }

    #endregion

    #region メソッド


    /// <summary>
    /// 回転と移動の呼び出し
    /// </summary>
    void Update()
    {

        //プレイヤーのステータスによる処理分け
        switch (_enumPlayerStatus)
        {

            //モノを置いている状態
            case PlayerStatus.Put:


                //移動ボタンが入力判定の時
                if (_onMove.action.IsPressed ())
                {

                    //移動の管理
                    MoveManagment ();
                }

                //目の前に荷物があった時
                if (_hit.collider == true && _hit.collider.CompareTag ( "Luggage" ))
                {

                    //ホールドボタンが押されたら
                    if (_onHoldOrAttahc.action.IsPressed ())
                    {

                        //持っている判定にする
                        _enumPlayerStatus = PlayerStatus.Hold;

                        //荷物をもつ(ルートとスピードを取得)あとでSpeedメモリーを作り置いたときに代入
                        _holdClass.Holding ( _carryWeight , _speed , _hit );

                        //持った荷物を親とする
                        transform.SetParent ( _hit.transform );
                    }
                }

                break;

            //モノを持っている状態
            case PlayerStatus.Hold:

                //プットボタンが押されたら
                if (_onPutOrCall.action.IsInProgress())
                {

                    //持っている判定にする
                    _enumPlayerStatus = PlayerStatus.Put;

                    //荷物を離す
                    _putClass.Put ( _carryWeight , _speed , _hit );

                    //親となっている荷物を離す
                    transform.SetParent ( null );
                }

                break;

        }

    }

    /// <summary>
    /// 入力からプレイヤーが見る方向を取得
    /// </summary>
    /// <param name="context">入力値</param>
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
        _rotaVec = (cameraRotaVec * inputVec.z) + (_cameraObj.transform.right * inputVec.x);

    }

    //public void OederCall(InputAction.CallbackContext context)
    //{

    //    Debug.Log ( "呼ぶ入力" );
    //}

    /// <summary>
    /// 移動の管理
    /// </summary>
    private void MoveManagment()
    {

        //移動先の確認
        _hit = _moveCheckClass.Check ( this.gameObject.transform );

        //目的の方向を元に、プレイヤーの方向を向く方向を取得
        Quaternion targetRotation = Quaternion.LookRotation ( _rotaVec );

        // プレイヤーの方向を取得
        float objectAngle = transform.eulerAngles.y;

        // 目的の方向を取得
        float targetAngle = targetRotation.eulerAngles.y;

        // 目的のの方向までの角度を取得
        float angleDifference = Mathf.Abs ( targetAngle - objectAngle );

        //入力方向と向いている方向があった時
        if (angleDifference == 0)
        {

            //前にオブジェクトがないとき
            if (_hit.collider == false)
            {

                //移動
                _wakeClass.MoveMethod ( this.transform , _speed );
            }
        }
        else
        {

            //回転
            _rotateClass.Rotate ( this.transform , targetRotation , _roteSpeed );
        }
    }
    #endregion
}
