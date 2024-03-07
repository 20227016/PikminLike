// ---------------------------------------------------------  
// GameManager.cs  
//   
// 作成日:  2/7
// 作成者: 湯元来輝 
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections.Generic;
using UniRx;

public class GameManagerClass : MonoBehaviour
{

    #region 変数  

    [Header("ステータス")]
    [SerializeField,Tooltip("制限時間")]
    private ReactiveProperty<float> _timeLimit = new ReactiveProperty<float> ();
    public IReadOnlyReactiveProperty<float> TimeLimit => _timeLimit;

    /// <summary>
    /// 所持金
    /// </summary>
    private ReactiveProperty<int> _money = new ReactiveProperty<int> ();
    public IReadOnlyReactiveProperty<int> Money => _money;

    /// <summary>
    /// 残りの荷物数
    /// </summary>
    private ReactiveProperty<int> _remainingLuggage = new ReactiveProperty<int> ();
    public IReadOnlyReactiveProperty<int> RemainingLuggage => _remainingLuggage;

    /// <summary>
    /// 配置中のLuggagesClassクラス
    /// </summary>
    private List<LuggagesClass> _luggagesClass = default;

    /// <summary>
    /// ゲームのステータス
    /// </summary>
    private GameStatus _gameStatus = GameStatus.Title;

    #endregion

    #region メソッド  

    private void Start()
    {

        //荷物たちの親オブジェクトが入る
        GameObject luggagesObj = GameObject.Find ( "Luggages" );

        //子オブジェクトを見ていく
        foreach (Transform luggage in luggagesObj.transform)
        {

            //クラスを取得
            LuggagesClass luggagesClass = luggage.GetComponent<LuggagesClass> ();

            //残りの荷物数を数える
            _remainingLuggage.Value++;

            print ( luggagesClass.transform );

            //中身の値が変わったときに実行
            luggagesClass.Pay.
            Subscribe 
            ( 
                pay =>
                {

                    AddMonay ( pay );
                }
            ).AddTo (this);
        
        }
    }

    /// <summary>  
    /// 更新処理  
    /// </summary>  
    void Update ()
    {

        //タイムカウント
        _timeLimit.Value -= Time.deltaTime;
    }

    private void AddMonay(int money)
    {

        //残りの荷物の数を減らす
        _remainingLuggage.Value--;

        //所持金加算
        _money.Value += money;
    }
  
    #endregion
}
