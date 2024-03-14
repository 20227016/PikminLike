// ---------------------------------------------------------  
// PlayerManager.cs  
//   プレイヤーオブジェクトのマネージャー
// 作成日:  2/15
// 作成者:  湯元来輝
// ---------------------------------------------------------  
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UniRx;


public class PlayerManagerClass : MonoBehaviour, IGetValue
{

    #region 変数  

    #region インスペクター表示
    [Header ( "トランスフォーム" )]
    [SerializeField, Tooltip ( "Cursorオブジェクトのトランスフォーム" )]
    private Transform _cursorTrans = default;
    [SerializeField, Tooltip ( "RadioWavesオブジェクトのトランスフォーム" )]
    private Transform _radioWavesTrans = default;
    [SerializeField, Tooltip ( "PlayerGroupオブジェクトのトランスフォーム" )]
    private Transform _playerGroupTrans = default;

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
    [SerializeField, Tooltip ( "PlayerAnimetionスクリプト" )]
    private PlayerAnimationClass _playerAnimation = default;

    [Header ( "アニメーター" )]
    [SerializeField, Tooltip ( "プレイヤーのアニメーター" )]
    private Animator _playerAnimator = default;

    [Header ( "InputSystem(Player)" )]
    [SerializeField, Tooltip ( "InputSystemのMoveが入る" )]
    private InputActionReference _onMove = default;
    [SerializeField, Tooltip ( "InputSystemのHoldOrGotoLocationが入る" )]
    private InputActionReference _onHoldOrGotoLocation = default;
    [SerializeField, Tooltip ( "InputSystemのPutOrCallが入る" )]
    private InputActionReference _onPutOrCall = default;
    [SerializeField, Tooltip ( "InputSystemのSelectが入る" )]
    private InputActionReference _onSelect = default;

    [Header ( "データ" )]
    [SerializeField, Tooltip ( "歩く速さ" )]
    private float _speed = 10f;
    [SerializeField, Tooltip ( "移動時の回転する速さ" )]
    private float _moveRoteSpeed = 500f;
    [SerializeField, Tooltip ( "カーソルを見るときの回転する速さ" )]
    private float _cursorRoteSpeed = 150f;
    [SerializeField, Tooltip ( "持てる重さ" )]
    private int _muscleStrength = 3;
    [SerializeField, Tooltip ( "電波が広がる速さ" )]
    private float _expansionSpeed = 1;
    [SerializeField, Tooltip ( "電波の最大の広さ" )]
    private float _maxExpansion = 5;

   
    #endregion

    #region インスペクター非表示

    /// <summary>
    /// プレイヤーの命令状況
    /// </summary>
    private ReactiveProperty<SelectStatus> _enumSelectStatus = new ReactiveProperty<SelectStatus> ( SelectStatus.NormalRobot );
    public IReadOnlyReactiveProperty<SelectStatus> EnumSelectState => _enumSelectStatus;

    /// <summary>
    /// 目的の場所（カーソル）まで行かせる入力
    /// </summary>
    private ReactiveProperty<int> _goToLocation = new ReactiveProperty<int> ( );
    public IReadOnlyReactiveProperty<int> GaToLocation => _goToLocation;

    /// <summary>
    /// プレイヤーの状態
    /// 持っている
    /// 置いている
    /// が入っている
    /// </summary>
    private PlayerStatus _enumPlayerStatus = PlayerStatus.Put;

    /// <summary>
    /// 目の前のオブジェクトが入る
    /// </summary>
    private RaycastHit _moveHit = default;

    /// <summary>
    /// プレイヤーが向く方向のベクトル
    /// </summary>
    private Vector3 _moveVec = default;

    /// <summary>
    /// 電波オブジェクトのサイズを記憶
    /// </summary>
    private Vector3 _raidioWavesScaleMemory = default;

    /// <summary>
    /// enumの要素数
    /// </summary>
    private int _enumSelectMaxIndex = default;

    /// <summary>
    /// 現在選んでいるキャラのインデックス(Enumの)
    /// </summary>
    private int _nowSelectNober = default;

    #endregion

    #endregion

    #region プロパティ

    public float GetSpeed
    {

        get => _speed;
    }

    #endregion

    #region メソッド

    /// <summary>
    /// EnumのSelevtをするための準備
    /// </summary>
    private void Start()
    {

        //電波のサイズを記憶
        _raidioWavesScaleMemory = _radioWavesTrans.localScale;
        //SelectStatus(Enum)の最大インデックス取得
        _enumSelectMaxIndex = Enum.GetValues ( typeof ( SelectStatus ) ).Length -1;
    }

    /// <summary>
    /// プレイヤーの処理と選択しているキャラクターへの命令
    /// </summary>
    private void Update()
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
                    _moveHit = _moveCheckClass.Check ( this.transform );

                    //回転
                    _rotateClass.Rotate ( this.transform , targetRota , _moveRoteSpeed );

                    //移動先に物がないときの処理
                    if (_moveHit.collider == false)
                    {

                        //移動
                        _wakeClass.Walk ( this.transform , _moveVec , _speed );
                        _playerAnimation.RunAnime (_playerAnimator);
                    }
                }
                else
                {
                    _playerAnimation.IdelAnime ( _playerAnimator );

                    //目標の位置
                    Vector3 targetPos = _cursorTrans.position - this.transform.position;

                    //Y軸の値を無視する
                    targetPos.y = 0;

                    //カーソルの向きを向く
                    Quaternion targetRote = Quaternion.LookRotation ( targetPos );

                    //回転
                    _rotateClass.Rotate ( this.transform , targetRote , _cursorRoteSpeed );
                }

                //目の前に荷物があった時
                if (_moveHit.collider == true && _moveHit.collider.CompareTag ( "Luggage" ))
                {

                    //持つボタンが押されたらかつ何も持っていなかったら
                    if (_onHoldOrGotoLocation.action.WasPressedThisFrame ())
                    {

                        //荷物を持つ処理
                        Hold ();
                        _playerAnimation.CarrayRunAnime ( _playerAnimator );
                    }
                }

                //カーソルの処理
                CursorProcess ();
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
        _enumSelectStatus.Value = (SelectStatus)_nowSelectNober;

    }

    /// <summary>
    /// カーソルの処理
    /// </summary>
    private void CursorProcess()
    {

        //目的の場所に行かせるボタンが押された時
        if (_onHoldOrGotoLocation.action.WasPressedThisFrame ())
        {

            //目的の場所（カーソル）まで行かせる入力をする
            _goToLocation.Value++;
        }

        //戻って来させるボタンが長押しされたら
        if (_onPutOrCall.action.IsInProgress ())
        {

            //戻ってこさせるボタンが押された時
            if (_onPutOrCall.action.WasPressedThisFrame ())
            {

                //電波の当たり判定を起動
                _radioWavesTrans.GetComponent<CapsuleCollider> ().enabled = true;
            }

            //電波のサイズを拡大
            _radioWavesTrans.localScale += (Vector3.right + Vector3 .forward) * _expansionSpeed * Time.deltaTime;

            //電波のサイズを制限
            _radioWavesTrans.localScale = Vector3.Min ( _radioWavesTrans.localScale , ((Vector3.right + Vector3.forward) * _maxExpansion ) + Vector3.up);
        }
        else
        {

            //電波のサイズをもとに戻す
            _radioWavesTrans.localScale = _raidioWavesScaleMemory;

            //電波の当たり判定を停止
            _radioWavesTrans.GetComponent<CapsuleCollider> ().enabled = false;
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
        Transform luggagTrans = _moveHit.collider.gameObject.transform;

        //荷物をもつ(ルートとスピードを取得)あとでSpeedメモリーを作り置いたときに代入
        bool isBeHeld = _holdClass.Hold ( _muscleStrength , _speed , luggagTrans );

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
        
        //持った荷物のオブジェクトのトランスフォームを取得
        Transform luggagTrans = _moveHit.collider.gameObject.transform;

        //荷物を離す
        _putClass.Put ( _muscleStrength , _speed , luggagTrans );

        //親となっている荷物を離しプレイヤーグループを親とする
        transform.SetParent ( _playerGroupTrans );

        //持ってない判定にする
        _enumPlayerStatus = PlayerStatus.Put;
    }

    private void OnTriggerEnter(Collider other)
    {

        //当たったものが保管所の時
        if (other.CompareTag ( "StoragePlace" )　&& _enumPlayerStatus == PlayerStatus.Hold)
        {

            //2秒待つコルーチンを開始(Robotが保管所の中心まで行く時間)
            StartCoroutine ( WaitOne () );
        }
    }

    private IEnumerator WaitOne()
    {

        // 1秒待機
        yield return new WaitForSeconds ( 0.5f );

        //荷物を離す
        Put ();
    }

    #endregion
}
