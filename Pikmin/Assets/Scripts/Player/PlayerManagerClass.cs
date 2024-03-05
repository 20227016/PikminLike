// ---------------------------------------------------------  
// PlayerManager.cs  
//   プレイヤーオブジェクトのマネージャー
// 作成日:  2/15
// 作成者:  湯元来輝
// ---------------------------------------------------------  
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UniRx;


public class PlayerManagerClass : MonoBehaviour, IGetValue
{

    #region 変数  
    #region インスペクター表示
    [Header ( "オブジェクト" )]
    [SerializeField, Tooltip ( "Cameraのオブジェクト" )]
    private GameObject _cameraObj = default;
    [SerializeField, Tooltip ( "pointerのオブジェクト" )]
    private GameObject _pointerObj = default;

    [Header ( "スクリプト" )]
    [SerializeField, Tooltip ( "Moveスクリプト" )]
    private WalkClass _wakeClass = default;
    [SerializeField, Tooltip ( "Rotateスクリプト" )]
    private RotateClass _rotateClass = default;
    [SerializeField, Tooltip ( "MoveCheckスクリプト" )]
    private MoveCheckClass _moveCheckClass = default;
    [SerializeField, Tooltip ( "Holdスクリプト" )]
    private HoldClass _holdClass = default;
    [SerializeField, Tooltip ( "Putスクリプト" )]
    private PutClass _putClass = default;

    [Header ( "InputSystem" )]
    [SerializeField, Tooltip ( "InputSystemのMoveが入る" )]
    private InputActionReference _onMove = default;
    [SerializeField, Tooltip ( "InputSystemのHoldOrGotoLocationが入る" )]
    private InputActionReference _onHoldOrGotoLocation = default;
    [SerializeField, Tooltip ( "InputSystemのPutOrCallが入る" )]
    private InputActionReference _onPutOrCall = default;
    [SerializeField, Tooltip ( "InputSystemのSelectが入る" )]
    private InputActionReference _onSelect = default;

    [Header ( "ステータス" )]
    [SerializeField, Tooltip ( "歩く速さ" )]
    private float _speed = 10f;
    [SerializeField, Tooltip ( "回転する速さ" )]
    private float _roteSpeed = 10f;
    [SerializeField, Tooltip ( "持てる重さ" )]
    private int _muscleStrength = 3;

    #endregion 

    //インスタンス化
    PointerClass _pointer = new PointerClass ();

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
    private ReactiveProperty<OrderStatus> _enumOederStatus = new ReactiveProperty<OrderStatus>(OrderStatus.None);

    public IReadOnlyReactiveProperty<OrderStatus> PlayerState => _enumOederStatus;

    /// <summary>
    /// プレイヤーが選んでいるキャラクター
    /// </summary>
    private SelectCharactorStatus _enumSelectCharactorStatus = SelectCharactorStatus.Player;

    /// <summary>
    /// 目の前のオブジェクト
    /// </summary>
    private RaycastHit _moveHit = default;

    /// <summary>
    /// プレイヤーが向く方向のベクトル
    /// </summary>
    private Vector3 _moveVec = default;

    /// <summary>
    /// プレイヤーのポインターの位置
    /// </summary>
    private Vector3 _pointerPos = default;

    /// <summary>
    /// 連れているロボットの量
    /// </summary>
    private int _robotCount = default;

    /// <summary>
    /// enumの要素数
    /// </summary>
    private int _enumSelectMaxIndex = default;

    /// <summary>
    /// 現在選んでいるキャラのインデックス(Enumの)
    /// </summary>
    private int _nowSelectNober = default;

    #endregion

    #region プロパティ

    public float GetSpeed
    {

        get => _speed;
    }

    #endregion

    #region メソッド

    private void Start()
    {
        //SelectCharactorStatus(Enum)の最大インデックス取得
        _enumSelectMaxIndex = Enum.GetValues ( typeof ( SelectCharactorStatus ) ).Length -1;
    }

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

                //ロボットへの命令処理
                NormalRobotProcess ();
                break;

            case SelectCharactorStatus.BombRobot:

                break;
        }
        
    }

    /// <summary>
    /// 入力からプレイヤーの移動方向を取得
    /// </summary>
    /// <param name="context">入力値</param>
    public void OnGetMoveValue(InputAction.CallbackContext context)
    {

        //入力値
        Vector3 inputValue = context.ReadValue<Vector2> ();

        //入力値のY軸の値とZ軸の値を入れ替える
        inputValue = Vector3.right * inputValue.x +
                     Vector3.up * 0 +
                     Vector3.forward * inputValue.y;



        //カメラのY軸以外の単位ベクトル（1・0のベクトル）を取得
        Vector3 cameraForward = Camera.main.transform.forward.normalized;
        cameraForward.y = 0;
        Vector3 cameraRight = Camera.main.transform.right.normalized;
        cameraRight.y = 0;

        //カメラから見た入力値を取得
        _moveVec = (cameraForward * inputValue.z) + (cameraRight * inputValue.x);

    }

    /// <summary>
    /// プレイヤーまたはロボっト達を選ぶ
    /// </summary>
    /// <param name="context">入力値</param>
    public void OnCharaSelect(InputAction.CallbackContext context)
    {
        //押す以外の時
        if (!context.started)
        {
            //帰る
            return;
        }

        //入力値取得
        float value = context.ReadValue<float>();

        //入力値による分岐
        switch (value)
        {
            //入力値がプラスの場合
            case 1:

                //インデックス数を足す
                _nowSelectNober += 1;
                break;

            //入力値がマイナスの場合
            case -1:

                //インデックス数を減らす
                _nowSelectNober -= 1;
                break;
        }

        //最大インデックスを超えた時
        if (_nowSelectNober > _enumSelectMaxIndex)
        {

            //最小インデックスにする
            _nowSelectNober = 0;
        }

        //最小インデックスを超えた時
        if (_nowSelectNober < 0)
        {

            //最大インデックスにする
            _nowSelectNober = _enumSelectMaxIndex;
        }

        //Enumの変更
        _enumSelectCharactorStatus = (SelectCharactorStatus)_nowSelectNober;
        Debug.Log ( "現在の選んでいるキャラクタ" + _enumSelectCharactorStatus );

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

                    //目的の方向を元に、プレイヤーの向く方向を取得
                    Quaternion targetRota = Quaternion.LookRotation ( _moveVec );

                    //移動先確認
                    _moveHit = _moveCheckClass.Check ( this.transform , _moveVec );

                    //回転
                    _rotateClass.Rotate ( this.transform , targetRota , _roteSpeed );

                    //移動先に物がないときの処理
                    if (_moveHit.collider == false)
                    {
                        //移動
                        _wakeClass.Walk ( this.transform , _moveVec , _speed );
                    }
                }

                //目の前に荷物があった時
                if (_moveHit.collider == true && _moveHit.collider.CompareTag ( "Luggage" ))
                {

                    //持つボタンが押されたらかつ何も持っていなかったら
                    if (_onHoldOrGotoLocation.action.WasPressedThisFrame ())
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
    /// ノーマルロボットが選ばれているときの処理
    /// </summary>
    private void NormalRobotProcess()
    {
    

    }

    /// <summary>
    /// 荷物を持つ処理
    /// </summary>
    private void Hold()
    {

        //持っている判定にする
        _enumPlayerStatus = PlayerStatus.Hold;

        //持った荷物のオブジェクトのトランスフォームを取得
        Transform luggagTrans = _moveHit.collider.gameObject.transform;

        //荷物をもつ(ルートとスピードを取得)あとでSpeedメモリーを作り置いたときに代入
        bool isBeHeld = _holdClass.Holding ( _muscleStrength , _speed , luggagTrans );

        //荷物を持っている人数が最大に達していなくて持てる場合
        if (isBeHeld == true)
        {

            //持った荷物を親とする
            transform.SetParent ( _moveHit.transform );
        }
        //荷物を持っている人数が最大に達していて持てない場合
        else
        {

            //荷物を置く
            Put ();
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
        Transform luggagTrans = _moveHit.collider.gameObject.transform;

        //荷物を離す
        _putClass.Put ( _muscleStrength , _speed , luggagTrans );

        //親となっている荷物を離す
        transform.SetParent ( null );
    }

    /// <summary>
    /// プレイヤー以外の選択中のキャラをポインターの位置まで動かす
    /// </summary>
    private void GoToLocation()
    {
    
    
    }


    #endregion
}
