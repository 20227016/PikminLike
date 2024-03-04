// ---------------------------------------------------------  
// RobotsManager.cs  
//   ロボットたちのマネージャー
// 作成日:  3/1
// 作成者:  湯元来輝
// ---------------------------------------------------------  
using System;
using UnityEngine;
using System.Collections;
using UniRx;

public class RobotsManagerClass : MonoBehaviour
{

    #region 変数  
    [Header ( "スクリプト" )]
    [SerializeField, Tooltip ( "PlayerManagerスクリプト" )]
    private PlayerManagerClass _playermanager = default;

    private OrderStatus _enumOrderStatus = OrderStatus.None;
    #endregion

    #region メソッド  

    /// <summary>  
    /// 初期化処理  
    /// </summary>  
    void Awake()
     {

     }
  
     /// <summary>  
     /// 更新前処理  
     /// </summary>  
     void Start ()
     {

        _playermanager.PlayerState.
            Subscribe ( status =>
            {
                _enumOrderStatus = status;
                Jump ();
            } ).AddTo(this);
     }
  
     /// <summary>  
     /// 更新処理  
     /// </summary>  
     void Update ()
     {
     }

    private void Jump()
    {
        print ("変更");
    }
  
    #endregion
}
