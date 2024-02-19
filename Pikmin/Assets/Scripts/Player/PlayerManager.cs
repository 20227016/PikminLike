// ---------------------------------------------------------  
// PlayerManager.cs  
//   プレイヤーオブジェクトのマネージャー
// 作成日:  2/15
// 作成者:  湯元来輝
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;


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
    [SerializeField, Tooltip("InputSystemのRoteが入る")]
    private InputActionReference _onRote;
    [Header("ステータス")]
    //歩く速さ
    [SerializeField, Tooltip("歩く速さ")]
    private float _speed = 10f;
    [SerializeField, Tooltip("回転する速さ")]
    private float _roteSpeed = 10f;

    //回転方向
    private float _roteDirection = default;

    //入力したか
   　//private bool _isInput = default;
    #endregion


    #region メソッド  

    private void Awake()
    {
    }

    /// <summary>  
    /// 更新前処理  
    /// </summary>  
    void Start()
    {

    }

    /// <summary>  
    /// 更新処理  
    /// </summary>  
    void Update()
    {
        //ほかのボタンが入力判定の時
        if (_onMove.action.IsPressed() || _onRote.action.IsPressed())
        {
            //動くための値を渡す
            _moveClass.MoveMethod(this.gameObject.transform , _speed);
            _moveClass.RoteMethod(this.gameObject.transform , _roteDirection , _roteSpeed);
        }
    }

    ///// <summary>
    ///// 移動入力されたときに動く方向を指定し向きを決める
    ///// </summary>
    ///// <param name="context">入力されたイベント</param>
    //public void OnMove(InputAction.CallbackContext context)
    //{

    //    switch (context.phase)
    //    {

    //        case InputActionPhase.Started:

    //            //入力されたとき
    //            //入力判定を入れ替える
    //            _isInput = !_isInput;
    //            break;

    //        case InputActionPhase.Performed:

    //            //入力中にほかの入力が入った時


    //            break;

    //        case InputActionPhase.Canceled:

    //            //入力が解除されたとき


    //            break;
    //    }

    //}

    public void OederRote(InputAction.CallbackContext context)
    {
        print(context.ReadValue<float>());
        //回転方向を保存
        _roteDirection = context.ReadValue<float>();
    }

    public void OnHold(InputAction.CallbackContext context)
    {


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
