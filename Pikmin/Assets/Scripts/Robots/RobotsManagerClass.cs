// ---------------------------------------------------------  
// RobotsManager.cs  
//   命令を受け取り対象のロボットに指示をするロボットたちのマネージャー
// 作成日:  3/1
// 作成者:  湯元来輝
// ---------------------------------------------------------  
using System;
using UnityEngine;
using System.Collections.Generic;
using UniRx;

public class RobotsManagerClass : MonoBehaviour
{

    #region 変数 

    [Header ( "プレハブ" )]
    [SerializeField, Tooltip ( "NomalRobotのプレハブ" )]
    private GameObject _nomalRobot = default;

    [Header ( "トランスフォーム" )]
    [SerializeField, Tooltip ( "Cursorオブジェクトのトランスフォーム" )]
    protected Transform _cursorTrans = default;
    [SerializeField, Tooltip ( "Shopオブジェクトのトランスフォーム" )]
    protected Transform _shopTrans = default;

    [Header ( "スクリプト" )]
    [SerializeField, Tooltip ( "PlayerManagerスクリプト" )]
    private PlayerManagerClass _playerManager = default;

    /// <summary>
    /// 命令されたこと
    /// </summary>
    private OrderStatus _enumOrderStatus = OrderStatus.None;


    /// <summary>
    /// プレイヤーの配下のロボット
    /// </summary>
    private List<NormalRobotsClass> _robotsList = new List<NormalRobotsClass> { };

    /// <summary>
    /// 行動中のロボット
    /// </summary>
    private List<NormalRobotsClass> _inActionRobotsList = default;
    #endregion

    #region メソッド  

     /// <summary>  
     /// ロボットの生成と購読側の設定
     /// </summary>  
    private  void Start ()
    {
        //ロボットを生成
        RobotCreat ();

        //中身の値が変わったときに実行
        _playerManager.EnumOrderState.
        Subscribe ( status =>
        {
            _enumOrderStatus = status;
            Order ();
        } ).AddTo(this);
    }

    /// <summary>
    /// 命令されたときにRobotListの先頭に対して指示をする
    /// </summary>
    private void Order()
    {

        //命令されていることで分岐
        switch (_enumOrderStatus)
        {

            //何も命令されていない
            case OrderStatus.None:

                
                break;

            //目的の場所（カーソル）まで移動の指示を出す
            case OrderStatus.GoToLocation:

                _robotsList [ 0 ].GoToLocation(_cursorTrans.position);

                break;

            //作業中のロボットを呼び戻す
            case OrderStatus.Call:

                break;
        }
    }

    /// <summary>
    /// 指定されたロボットを作りリストに入れる
    /// </summary>
    public void RobotCreat()
    {
        //ロボット生成
        GameObject robot = Instantiate ( _nomalRobot );

        //NormalRobotsクラスを取り出す
        NormalRobotsClass normalRobotsClass = robot.transform.GetComponent<NormalRobotsClass> ();

        if (normalRobotsClass != null)
        {

            print ( normalRobotsClass.transform.name );
        }

        //生成したオブジェクトのNormalRobotsクラスをリストに格納
        _robotsList.Add(normalRobotsClass);

        //最後尾の生成されたばかりのオブジェクトの位置をショップにする
        _robotsList [ _robotsList.Count - 1 ].transform.position = _shopTrans.position;
    }
  

    #endregion
}
