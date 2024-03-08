// ---------------------------------------------------------  
// UITimeLimitClass.cs  
//   
// 作成日:  
// 作成者:  
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UITimeLimitClass : MonoBehaviour
{
    /// <summary>
    /// タイムリミットを表示するText
    /// </summary>
    [Header ( "Text" )]
    [SerializeField, Tooltip ( "タイムリミットの表示場所" )]
    private Text _text = default;

    /// <summary>
    /// タイムリミットをUIに表示
    /// </summary>
    /// <param name="money">所持金</param>
    public void View(float timeLimit)
    {

        // 分と秒に分解
        int minutes = Mathf.FloorToInt ( timeLimit / 60 );
        int seconds = Mathf.FloorToInt ( timeLimit % 60 );

        //フォーマットに直す
        string formatTime = string.Format ( "{0:00}:{1:00}" , minutes , seconds );

        //タイムリミット表示
        _text.text = formatTime;
    }
}
