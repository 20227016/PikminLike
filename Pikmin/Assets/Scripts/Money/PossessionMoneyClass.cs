// ---------------------------------------------------------  
// MoneyManager.cs  
//   所持金
// 作成日:  3/8
// 作成者:  湯元来輝
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;
using UniRx;

public class PossessionMoneyClass : MonoBehaviour
{

    /// <summary>
    /// 所持金
    /// </summary>
    private ReactiveProperty<int> _possessionMoney = new ReactiveProperty<int>(100);
    public IReadOnlyReactiveProperty<int> possessionMoney => _possessionMoney;

    public ReactiveProperty<int> PossessionMoneyCupsule
    {
        get => _possessionMoney;
        set => _possessionMoney = value;
    }
}
