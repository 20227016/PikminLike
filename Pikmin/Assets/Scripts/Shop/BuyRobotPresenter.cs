// ---------------------------------------------------------  
// BuyRobotPresenter.cs  
//   
// 作成日:  2/8
// 作成者:  湯元来輝
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;

public class BuyRobotPresenter : MonoBehaviour
{

    [Header ( "スクリプト" )]
    [SerializeField, Tooltip ( "ShopManagerクラス（Model）" )]
    private GameManagerClass _shopManager = default;
    [SerializeField, Tooltip ( "UIRobotShopクラス（View）" )]
    private UIMoneyClass _uIRobotShop = default;
}
