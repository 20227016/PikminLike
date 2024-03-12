// ---------------------------------------------------------  
// MoneyPresenter.cs  
//   仲介役
// 作成日:  3/7
// 作成者:  湯元来輝
// ---------------------------------------------------------  
using UnityEngine;
using UniRx;

public class MoneyPresenterClass : MonoBehaviour
{

    [Header ( "スクリプト" )]
    [SerializeField, Tooltip ( "GameManagerクラス（Model）" )]
    private GameManagerClass _gameManager = default;
    [SerializeField, Tooltip ( "UIMoneyクラス（View）" )]
    private UIMoneyClass _uIMoney = default;

    /// <summary>  
    /// 仲介処理
    /// </summary>  
    void Awake()
    {

        //中の値が変わったときに実行
        _gameManager.Money.Subscribe ( money =>
        {
            _uIMoney.View ( money );
        }
        ).AddTo ( this );
    }
  
}
