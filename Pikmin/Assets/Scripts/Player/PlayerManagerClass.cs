// ---------------------------------------------------------  
// PlayerManager.cs  
//   プレイヤーオブジェクトのマネージャー
// 作成日:  2/15
// 作成者:  湯元来輝
// ---------------------------------------------------------  
using UnityEngine;
using UnityEngine.InputSystem;
using UniRx;
using System;


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
    private int _muscleStrength = 3;

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
    private SelectCharactorStatus _enumSelectCharactorStatus = SelectCharactorStatus.Player;

    /// <summary>
    /// 目の前のオブジェクト
    /// </summary>
    private RaycastHit _hit = default;

    /// <summary>
    /// プレイヤーが向く方向のベクトル
    /// </summary>
    private Vector3 _rotaVec = default;

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

        //選んでいるキャラクターのステータスによる処理分け
        switch (_enumSelectCharactorStatus)
        {
            case SelectCharactorStatus.Player:

                //プレイヤーの処理
                PlayerProcess ();
                break;
            case SelectCharactorStatus.NormalRobot:


                break;

            case SelectCharactorStatus.BombRobot:

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

    /// <summary>
    /// プレイヤーが選ばれているときの処理
    /// </summary>
    private void PlayerProcess()
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

                    //持つボタンが押されたらかつ何も持っていなかったら
                    if (_onHoldOrAttahc.action.WasPressedThisFrame ())
                    {
                        //荷物を持つ処理
                        Hold ();
                    }
                }

                break;

            //モノを持っている状態
            case PlayerStatus.Hold:

                //置くボタンが押されたら
                if (_onPutOrCall.action.WasPressedThisFrame ())
                {

                    //荷物を置く処理
                    Put ();
                }

                break;

        }
    }

    /// <summary>
    /// 荷物を持つ処理
    /// </summary>
    private void Hold()
    {

        //持っている判定にする
        _enumPlayerStatus = PlayerStatus.Hold;

        //持った荷物のオブジェクトのトランスフォームを取得
        Transform luggagTrans = _hit.collider.gameObject.transform;

        //荷物をもつ(ルートとスピードを取得)あとでSpeedメモリーを作り置いたときに代入
        bool isBeHeld = _holdClass.Holding ( _muscleStrength , _speed , luggagTrans );

        //荷物を持っている人数が最大に達していなくて持てる場合
        if (isBeHeld == true)
        {

            //持った荷物を親とする
            transform.SetParent ( _hit.transform );
        }
        //荷物を持っている人数が最大に達していて持てない場合
        else
        {

            //荷物を置く
            Put();
        }

    }

    /// <summary>
    /// 荷物を置く処理
    /// </summary>
    private void Put()
    {

        //持っている判定にする
        _enumPlayerStatus = PlayerStatus.Put;

        //持った荷物のオブジェクトのトランスフォームを取得
        Transform luggagTrans = _hit.collider.gameObject.transform;

        //荷物を離す
        _putClass.Put ( _muscleStrength , _speed , luggagTrans );

        //親となっている荷物を離す
        transform.SetParent ( null );
    }

    #endregion
}
