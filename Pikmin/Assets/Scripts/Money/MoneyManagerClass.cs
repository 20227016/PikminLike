// ---------------------------------------------------------  
// MoneyManager.cs  
//   
// 作成日:  3/8
// 作成者:  湯元来輝
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;
using UniRx;

public class MoneyManagerClass : MonoBehaviour
{

    /// <summary>
    /// 所持金
    /// </summary>
    private ReactiveProperty<int> _money = new ReactiveProperty<int>();
    public IReadOnlyReactiveProperty<int> Money => _money;

    public ReactiveProperty<int> MoneyCupsule
    {
        get => _money;
        set => _money = value;
    }
}
