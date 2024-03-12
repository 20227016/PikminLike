// ---------------------------------------------------------  
// BuyRobotPresenter.cs  
//   仲介
// 作成日:  2/8
// 作成者:  湯元来輝
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;
using UniRx;

public class BuyRobotPresenterClass : MonoBehaviour
{

    [Header ( "スクリプト" )]
    [SerializeField, Tooltip ( "ShopManagerクラス（Model）" )]
    private ShopManagerClass _shopManager = default;
    [SerializeField, Tooltip ( "UIRobotShopクラス（View）" )]
    private UIRobotShopClass _uIRobotShop = default;

    private void Start()
    {

        #region UI側

        _uIRobotShop.IsBuy.
            Subscribe ( isBuy =>
            {

                if (isBuy == false)
                {

                    return;
                }
                _shopManager.Buy ();
            } ).AddTo ( this );

        //購入個数を追加したとき
        _uIRobotShop.IsAdd.
            Subscribe ( isAdd =>
            {

                //追加しない判定の時
                if (isAdd == false)
                {

                    return;
                }
                _shopManager.Add ();
            } ).AddTo ( this );

        //購入個数を減らしたとき
        _uIRobotShop.IsDelete.
            Subscribe ( isDelete =>
            {

                //減らさない判定の時
                if (isDelete == false)
                {

                    return;
                }
                _shopManager.Delete ();
            } ).AddTo ( this );

        #endregion

        #region Manager側

        //購入個数が変わったとき
        _shopManager.NormalRobotCount.
            Subscribe ( normalRobotCount =>
              {

                  _uIRobotShop.CountUpDate (normalRobotCount);
              } ).AddTo ( this );

        //合計金額が変わったとき
        _shopManager.SumPrice.
            Subscribe ( sumPrice =>
            {

                _uIRobotShop.SumPriceUpDate (sumPrice);
            } ).AddTo ( this );

        #endregion
    }
}
