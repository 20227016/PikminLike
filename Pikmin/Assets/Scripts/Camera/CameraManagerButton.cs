// ---------------------------------------------------------  
// CameraManagerButton.cs  
//   ボタンを押したときにカメラをプレイヤーの向いている方向に動かす
// 作成日:  2/20~2/20
// 作成者:  湯元来輝
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class CameraManagerButton : MonoBehaviour
{

    #region 変数  

    //インスタンス化
    CameraTrack _cameraMoveClass = new CameraTrack ();
    CameraTarget _cameraTargetClass = new CameraTarget ();

    [Header ( "Transform" )]
    [SerializeField, Tooltip ( "PlayerのTransform" )]
    private Transform _playerTrans = default;

    [Header ( "InputSystem" )]
    [SerializeField, Tooltip ( "InputSystemのRoteが入る" )]
    private InputActionReference _onCametaRote = default;

    [Header ( "スクリプト" )]
    [SerializeField, Tooltip ( "Moveスクリプト" )]
    private PlayerManagerClass _playerManagerClass = default;

    [Header ( "ステータス" )]
    [SerializeField, Tooltip ( "プレイヤーを基準としたカメラの高さ" )]
    private float _cameraHeight = 10f;
    [SerializeField, Tooltip ( "開始時のプレイヤーとカメラの距離" )]
    private float _cameraFixDistance = -15;
    [SerializeField, Tooltip ( "カメラが回転する速さ" )]
    private float _roteSpeed = 5f;
    [SerializeField, Tooltip ( "カメラがプレイヤーを見なくなる許容値" )]
    private float _roteTolerance = 5f;

    //今のフレームの位置
    private Vector3 _nowPos = default;

    //１つ前のフレームの位置 
    private Vector3 _lastPos = default;

    //前のフレームとの差
    private Vector3 _offsetPos = default;

    //プレイヤーの速さ
    private float _cameraSpeed = default;

    //インプットコールバックの値
    private float _inputValue = default;

    //カメラ移動の目的の位置
    private Transform _targetTrans = default;

    //カメラがまだ回っている判定
    private bool _isRote = default;

    #endregion

    #region メソッド  

    private void Start()
    {
        //インターフェースからプレイヤーの速さを取得
        _cameraSpeed = _playerTrans.gameObject.GetComponent<IGetValue> ().GetSpeed;
        //カメらの初期の位置取得
        transform.position = new Vector3 ( _playerTrans.position.x , _cameraHeight , _cameraFixDistance );
        //プレイヤーのほうを見る
        this.transform.LookAt ( _playerTrans );
        //トランスフォームの実体を作る
        _targetTrans = new GameObject ( "TargetObj" ).transform;
        //カメラのトランスフォームをコピー
        _cameraTargetClass.CopyTransformValues ( this.transform );
        //今のフレームの位置と最後のフレームの位置を更新
        _nowPos = this.transform.position;
        _lastPos = this.transform.position;
        //前のフレームとの位置の差を更新
        _offsetPos = _nowPos - _lastPos;
    }

    /// <summary>  
    /// ターゲットを作るクラスの呼び出しと追いかけるクラスの呼び出し
    /// </summary>  
    void Update()
    {

        //カメラの移動先を取得
        _targetTrans = _cameraTargetClass.Target ( _playerTrans , _targetTrans , _inputValue );

        //  現在のフレームの位置を取得
        _nowPos = this.transform.position;

        //回転するためのボタンを押した時
        if (_onCametaRote.action.IsInProgress ())
        {

            //回っている判定にする
            _isRote = true;
        }

        //回っている判定の時
        if (_isRote == true)
        {

            //前のフレームとの位置の差を更新
            _offsetPos = _nowPos - _lastPos;

            //許容値を超えた時
            if (Mathf.Abs ( _offsetPos.x ) > _roteTolerance && Mathf.Abs ( _offsetPos.z ) > _roteTolerance)
            {

                //プレイヤーを追う
                this.transform.LookAt ( _playerTrans );
            }
            else
            {

                //回っていない判定にする
                _isRote = false;
            }

        }


        //プレイヤーを追跡
        _cameraMoveClass.Tracking ( _playerTrans.transform , this.transform , _targetTrans , _cameraSpeed );

        //現在のフレームの位置をまえのフレームとする
        _lastPos = _nowPos;

    }

    public void OnRote(InputAction.CallbackContext context)
    {

        //入力値を取得
        _inputValue = context.ReadValue<float> () * _roteSpeed;
    }

    #endregion
}
