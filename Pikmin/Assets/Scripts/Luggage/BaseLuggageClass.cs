// ---------------------------------------------------------  
// BaseLuggageClass.cs  
//   荷物たちの共通の参照
// 作成日:  2/27
// 作成者:  湯元来輝
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;

public class BaseLuggageClass : MonoBehaviour
{

    [Header ( "スクリプト" )]
    [SerializeField, Tooltip ( "Carrayのスクリプト" )]
    protected CarrayClass _carrayClass = default;
    [SerializeField, Tooltip ( "MoneyManagerのスクリプト" )]
    protected MoneyManagerClass _moneyManager = default;

}
