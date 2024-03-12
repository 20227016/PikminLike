// ---------------------------------------------------------  
// ShopManager.cs  
//   ロボットを買うショップのマネジャー
// 作成日:  2/7
// 作成者:  湯元来輝
// ---------------------------------------------------------  
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UniRx;

public class ShopManagerClass : MonoBehaviour
{

    #region 変数  

    [Header ( "スクリプト" )]
    [SerializeField, Tooltip ( "RobotManagerのスクリプト" )]
    private RobotsManagerClass _robotsManager = default;
    [Header ( "スクリプト" )]
    [SerializeField, Tooltip ( "PossessionMoneyのスクリプト" )]
    private PossessionMoneyClass _possessionMoney = default;

    [Header ( "金額" )]
    [SerializeField, Tooltip ( "ノーマルロボットの金額" )]
    private int _normalRobotPrice = 100;

    /// <summary>
    /// 購入数
    /// </summary>
    private ReactiveProperty<int> _normalRobotCount = new ReactiveProperty<int>();
    public IReadOnlyReactiveProperty<int> NormalRobotCount => _normalRobotCount;

    /// <summary>
    /// 合計価格
    /// </summary>
    private ReactiveProperty<int> _sumPrice = new ReactiveProperty<int>();
    public IReadOnlyReactiveProperty<int> SumPrice => _sumPrice;



    #endregion

    #region メソッド  

    /// <summary>
    /// ロボットを買う
    /// </summary>
    public void Buy()
    {

        //購入分回る
        for (int i = 0; _normalRobotCount.Value > i; i++)
        {

            //購入分ロボットを作る
            _robotsManager.RobotCreat ();
        }

        //所持金を減らす
        _possessionMoney.PossessionMoneyCupsule.Value -= _sumPrice.Value;
        //個数と値段を初期化
        _normalRobotCount.Value = 0;
        _sumPrice.Value = 0;
    }

    /// <summary>
    /// 個数や値段などを足す処理
    /// </summary>
    public void Add()
    {

        //個数と値段を更新
        _normalRobotCount.Value++;
        _sumPrice.Value += _normalRobotPrice;

        //値段が所持金を超えたとき
        if (_possessionMoney.PossessionMoneyCupsule.Value < _sumPrice.Value)
        {

            Delete ();
        }
    }

    /// <summary>
    /// 個数や値段などを減らす処理
    /// </summary>
    public void Delete()
    {

        //個数と値段を更新
        _normalRobotCount.Value--;
        _sumPrice.Value -= _normalRobotPrice;
    }
  
    #endregion
}
