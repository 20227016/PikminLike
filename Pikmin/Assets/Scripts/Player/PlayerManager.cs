// ---------------------------------------------------------  
// PlayerManager.cs  
//   プレイヤーオブジェクトのマネージャー
// 作成日:  2/15
// 作成者:  湯元来輝
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Move) , typeof(Hold) , typeof(Put))]
public class PlayerManager : MonoBehaviour
{

    #region 変数  

    [Header("スクリプト")]
    [SerializeField, Tooltip("Moveスクリプト")]
    private Move _moveClass = default;
    [SerializeField, Tooltip("Holdスクリプト")]
    private Hold _holdClass = default;
    [SerializeField, Tooltip("Putスクリプト")]
    private Put _putClass = default;
    [Header("InputSystem")]
    [SerializeField, Tooltip("InputSystemのMoveが入る")]
    private InputActionReference _onMove;
    [Header("ステータス")]
    //歩く速さ
    [SerializeField,Tooltip("歩く速さ")]
    private float _speed = 10f;

    //動く方向
    private Vector3 _moveVal = default;

    #endregion


    #region メソッド  

    private void Awake()
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

    /// <summary>
    /// 移動入力されたときに動く方向を指定し向きを決める
    /// </summary>
    /// <param name="context"></param>
    public void OnMove(InputAction.CallbackContext context)
    {

        _moveVal = context.ReadValue<Vector2>();

        _moveClass.MoveMethod(this.gameObject,_moveVal,_speed);
    }

    public void OederAttach(InputAction.CallbackContext context)
    {

        Debug.Log("つける入力");
    }

    public void OederCall(InputAction.CallbackContext context)
    {

        Debug.Log("呼ぶ入力");
    }

    public void OederLineUp(InputAction.CallbackContext context)
    {

        Debug.Log("並ぶ入力");
    }

    #endregion
}
