// ---------------------------------------------------------  
// PlayerManager.cs  
//   
// 作成日:  2/15
// 作成者:  湯元来輝
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
  
    #region 変数  
    
    #endregion
  
  
    #region メソッド  
     
  
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

    public void MoveInput(InputAction.CallbackContext context)
    {

        Debug.Log("動く入力");
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
