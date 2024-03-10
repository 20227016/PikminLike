// ---------------------------------------------------------  
// ShopManager.cs  
//   ロボットを買うショップのマネジャー
// 作成日:  2/7
// 作成者:  湯元来輝
// ---------------------------------------------------------  
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class ShopManager : MonoBehaviour
{

    #region 変数  

    [Header ( "InputSystem(UI)" )]
    [SerializeField, Tooltip ( "InputSystemのMoveが入る" )]
    private InputActionReference _onMove = default;
    [SerializeField, Tooltip ( "InputSystemのMoveが入る" )]
    private InputActionReference _onDecision = default;

    [Header ( "トランスフォーム" )]
    [SerializeField, Tooltip ( "Cursorオブジェクトのトランスフォーム" )]
    private Transform _cursorTrans = default;
    [SerializeField, Tooltip ( "RadioWavesオブジェクトのトランスフォーム" )]
    private Transform _radioWavesTrans = default;
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
  
     }
  
     /// <summary>  
     /// 更新処理  
     /// </summary>  
     void Update ()
     {


     }
  
    #endregion
}
