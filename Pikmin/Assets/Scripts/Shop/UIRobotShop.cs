// ---------------------------------------------------------  
// UIRobotShop.cs  
//   
// 作成日:  2/8
// 作成者:  湯元来輝
// ---------------------------------------------------------  
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class UIRobotShop : MonoBehaviour
{

    [Header ( "InputSystem(UI)" )]
    [SerializeField, Tooltip ( "InputSystemのMoveが入る" )]
    private InputActionReference _onMove = default;
    [SerializeField, Tooltip ( "InputSystemのDecisonが入る" )]
    private InputActionReference _onDecision = default;


}
