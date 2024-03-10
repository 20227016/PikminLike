// ---------------------------------------------------------  
// GameManager.cs  
//   
// 作成日:  2/7
// 作成者: 湯元来輝 
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UniRx;

public class GameManagerClass : MonoBehaviour
{

    #region 変数  
    [Header ( "トランスフォーム" )]
    [SerializeField, Tooltip ( "MainPanelのトランスフォーム" )]
    private GameObject _mainPanel = default;
    [SerializeField, Tooltip ( "ShopPanelのトランスフォーム" )]
    private GameObject _shopPanel = default;

    [Header ( "スクリプト" )]
    [SerializeField, Tooltip ( "PlayerManagerスクリプト" )]
    private PlayerManagerClass _playerManager = default;
    [SerializeField, Tooltip ( "LuggageManagerスクリプト" )]
    private LuggagesClass _luggageManager = default;
    [SerializeField, Tooltip ( "ShopManagerスクリプト" )]
    private ShopManager _shopManager = default;
    [SerializeField, Tooltip ( "RobotsManagerClassスクリプト" )]
    private RobotsManagerClass _robotsManager = default;

    [Header ( "InputSystem" )]
    [SerializeField, Tooltip ( "InputSystemのOpenShopが入る" )]
    private InputActionReference _onOpenShop = default;

    [Header ( "制限時間" )]
    [SerializeField, Tooltip ( "分" )]
    private float _minutes = default;
    [SerializeField, Tooltip ( "秒" )]
    private float _seconds = default;


    private ReactiveProperty<float> _timeLimit = new ReactiveProperty<float> ();
    public IReadOnlyReactiveProperty<float> TimeLimit => _timeLimit;

    /// <summary>
    /// 所持金
    /// </summary>
    private ReactiveProperty<int> _money = new ReactiveProperty<int> ();
    public IReadOnlyReactiveProperty<int> Money => _money;

    /// <summary>
    /// 残りの荷物数
    /// </summary>
    private ReactiveProperty<int> _remainingLuggage = new ReactiveProperty<int> ();
    public IReadOnlyReactiveProperty<int> RemainingLuggage => _remainingLuggage;

    /// <summary>
    /// 配置中のLuggagesClassクラス
    /// </summary>
    private List<LuggagesClass> _luggagesClass = default;

    /// <summary>
    /// ゲームのステータス
    /// </summary>
    private GameStatus _gameStatus = GameStatus.Main;


    #endregion

    #region メソッド  

    private void Start()
    {

        //変換
        _timeLimit.Value = (_minutes * 60) + _seconds;

        //荷物たちの親オブジェクトが入る
        GameObject luggagesObj = GameObject.Find ( "Luggages" );

        //子オブジェクトを見ていく
        foreach (Transform luggage in luggagesObj.transform)
        {

            //クラスを取得
            LuggagesClass luggagesClass = luggage.GetComponent<LuggagesClass> ();

            //残りの荷物数を数える
            _remainingLuggage.Value++;

            //中身の値が変わったときに実行
            luggagesClass.Pay.
            Subscribe
            (
                pay =>
                {

                    AddMoney ( pay );
                },
                check =>
                {

                    Completed ();
                }
            ).AddTo (this);
        
        }


       
    }

    /// <summary>  
    /// ステータスで処理
    /// </summary>  
    void Update ()
    {

        //ステータスで処理を切り替え
        switch (_gameStatus)
        {
            case GameStatus.Title:

                _playerManager.enabled = false;
                //_luggageManager.enabled = false;
                _robotsManager.enabled = false;
                _shopManager.enabled = false;
                break;

            case GameStatus.Main:

                _mainPanel.SetActive ( true );
                _shopPanel.SetActive ( false );

                _playerManager.enabled = true;
                //_luggageManager.enabled = true;
                _robotsManager.enabled = true;
                _shopManager.enabled = false;

                //タイムカウント
                _timeLimit.Value -= Time.deltaTime;

                //ショップボタンが押された時
                if (_onOpenShop.action.WasPressedThisFrame ())
                {

                    //ステータスをショップに切り替える
                    _gameStatus = GameStatus.Shop;
                }

                //時間制限に達した時
                if (_timeLimit.Value <= 0)
                {

                    //ステータスをリザルトに切り替える
                    _gameStatus = GameStatus.Result;
                }
                break;

            case GameStatus.Shop:

                _mainPanel.SetActive ( false );
                _shopPanel.SetActive ( true );

                _playerManager.enabled = false;
                //_luggageManager.enabled = false;
                _robotsManager.enabled = false;
                _shopManager.enabled = true;

                //ショップボタンが押された時
                if (_onOpenShop.action.WasPressedThisFrame ())
                {

                    //ステータスをメインに切り替える
                    _gameStatus = GameStatus.Main;
                }
                break;

            case GameStatus.Complete:

                _playerManager.enabled = false;
                //_luggageManager.enabled = false;
                _robotsManager.enabled = false;
                _shopManager.enabled = false;
                break;

            case GameStatus.Over:

                _playerManager.enabled = false;
                //_luggageManager.enabled = false;
                _robotsManager.enabled = false;
                _shopManager.enabled = false;
                break;

            case GameStatus.Result:

                _playerManager.enabled = false;
                //_luggageManager.enabled = false;
                _robotsManager.enabled = false;
                _shopManager.enabled = false;
                break;
        }
 
    }

    private void AddMoney(int money)
    {
        //所持金加算
        _money.Value += money;
    }

    /// <summary>
    /// 運べる荷物がなくなった時の処理
    /// </summary>
    private void Completed()
    {
        //残りの荷物の数を減らす
        _remainingLuggage.Value--;

        //残りの荷物がなくなった時
        if (_remainingLuggage.Value <= 0)
        {

            //コンプリートした状態に切り替える
            _gameStatus = GameStatus.Complete;
        }
    }
    #endregion
}
