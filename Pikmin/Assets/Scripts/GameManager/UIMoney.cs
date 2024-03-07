// ---------------------------------------------------------  
// UIMoney.cs  
//   
// 作成日:  3/7
// 作成者:  湯元来輝
// ---------------------------------------------------------  
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIMoney : MonoBehaviour
{

    /// <summary>
    /// 所持金を表示するText
    /// </summary>
    [Header("Text")]
    [SerializeField,Tooltip("所持金の表示場所")]
    private Text _text = default;

    /// <summary>
    /// 所持金をUIに表示
    /// </summary>
    /// <param name="money">所持金</param>
    public void View(int money)
    {

        //所持金表示
        _text.text = money.ToString ( );
    }


}
