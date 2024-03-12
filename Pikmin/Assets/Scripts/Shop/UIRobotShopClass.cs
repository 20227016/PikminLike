// ---------------------------------------------------------  
// UIRobotShop.cs  
//   
// 作成日:  2/8
// 作成者:  湯元来輝
// ---------------------------------------------------------  
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIRobotShopClass : MonoBehaviour
{

    [Header ( "TextMeshPro" )]
    [SerializeField, Tooltip ( "合計金額のTextMeshが入る" )]
    private TextMeshProUGUI _sumPrice = default;
    [SerializeField, Tooltip ( "個数のTextMeshが入る" )]
    private TextMeshProUGUI _quantity = default;

    /// <summary>
    /// 購入ボタンが押されたかの判定
    /// </summary>
    private ReactiveProperty<bool> _isBuy = new ReactiveProperty<bool> ( false );
    public IReadOnlyReactiveProperty<bool> IsBuy => _isBuy;

    /// <summary>
    /// 追加ボタンが押されたかの判定
    /// </summary>
    private ReactiveProperty<bool> _isAdd = new ReactiveProperty<bool> ( false );
    public IReadOnlyReactiveProperty<bool> IsAdd => _isAdd;

    /// <summary>
    /// 削除ボタンが押されたかの判定
    /// </summary>
    private ReactiveProperty<bool> _isDelete = new ReactiveProperty<bool> ( false );
    public IReadOnlyReactiveProperty<bool> IsDelete => _isDelete;

    /// <summary>
    /// 購入ボタンが押されたとき
    /// </summary>
    public void OnBuy()
    {

        print ( "購入" );
        _isBuy.Value = true;
        _isBuy.Value = false;
    }

    /// <summary>
    /// 追加ボタンが押されたとき
    /// </summary>
    public void OnAdd()
    {

        print ( "追加" );
        _isAdd.Value = true;
        _isAdd.Value = false;
    }

    /// <summary>
    /// 削除ボタンが押されたとき
    /// </summary>
    public void OnDelete()
    {

        print ( "削除" );
        _isDelete.Value = true;
        _isDelete.Value = false;
    }

    /// <summary>
    /// 購入個数のUI更新
    /// </summary>
    public void CountUpDate(int count)
    {
    
        _quantity.text = $"{count}個";
    }

    /// <summary>
    /// 合計金額の個数のUI更新
    /// </summary>
    public void SumPriceUpDate(int sumPrice)
    {

        _sumPrice.text = $"{sumPrice}円";
    }
}
