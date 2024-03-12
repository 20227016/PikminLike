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

    public void Buy()
    {

        for (int i = 0; _normalRobotCount.Value > i; i++)
        {

            _robotsManager.RobotCreat ();
        }
    }

    /// <summary>
    /// 個数や値段などを足す処理
    /// </summary>
    public void Add()
    {
        //個数を更新
        _normalRobotCount.Value++;
        //値段を更新
        _sumPrice.Value += _normalRobotPrice; 
    }

    /// <summary>
    /// 個数や値段などを減らす処理
    /// </summary>
    public void Delete()
    {

        //個数を更新
        _normalRobotCount.Value--;
        //値段を更新
        _sumPrice.Value -= _normalRobotPrice;
    }
  
    #endregion
}
