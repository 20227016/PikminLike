// ---------------------------------------------------------  
// TimeLimitPresenterClass.cs  
//   
// 作成日:  2/7
// 作成者:  湯元来輝
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;
using UniRx;

public class TimeLimitPresenterClass : MonoBehaviour
{
    [Header ( "スクリプト" )]
    [SerializeField, Tooltip ( "GameManagerクラス（Model）" )]
    private GameManagerClass _gameManager = default;
    [SerializeField, Tooltip ( "UIMoneyクラス（View）" )]
    private UITimeLimitClass _uITimeLimitClass = default;

    private bool _isStart = true;

    /// <summary>  
    /// 仲介処理
    /// </summary>  
    void Awake()
    {



        //中の値が変わったときに実行
        _gameManager.TimeLimit.
            Subscribe ( timeLimit =>
            {
                if (_isStart == false)
                {
                    _isStart = true;
                    return;
                }
                _uITimeLimitClass.View ( timeLimit );
            }
            ).AddTo ( this );
    }
}
