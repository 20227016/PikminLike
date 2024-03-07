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
    protected Transform _radioWavieTrans = default;
    [SerializeField, Tooltip ( "Shopオブジェクトのトランスフォーム" )]
    protected Transform _shopTrans = default;
    [SerializeField, Tooltip ( "NormalRobotsオブジェクトのトランスフォーム" )]
    protected Transform _normalRobotsTrans = default;

    [Header ( "スクリプト" )]
    [SerializeField, Tooltip ( "PlayerManagerスクリプト" )]
    private PlayerManagerClass _playerManager = default;

    /// <summary>
    /// 命令されたこと
    /// </summary>
    private SelectStatus _enumOrderStatus = SelectStatus.NormalRobot;

    /// <summary>
    /// プレイヤーの配下のロボット
    /// </summary>
    private List<NormalRobotsClass> _followRobotsList = new List<NormalRobotsClass> { };

    /// <summary>
    /// 行動中のロボット
    /// </summary>
    private List<NormalRobotsClass> _inActionRobotsList = new List<NormalRobotsClass> { };

    /// <summary>
    /// 最初の一回目化の判定
    /// </summary>
    private bool _isStart = true;

    #endregion

    #region メソッド  

     /// <summary>  
     /// ロボットの生成と購読側の設定
     /// </summary>  
    private void Start ()
    {

        //ロボットを生成
        RobotCreat ();

        //中身の値が変わったときに実行
        _playerManager.EnumSelectState.
        Subscribe ( status =>
        {
            _enumOrderStatus = status;
        } ).AddTo(this);

        //中身の値が変わったときに実行
        _playerManager.GaToLocation.
        Subscribe ( status =>
        {
            //初めの一回目の時
            if (_isStart == true)
            {

                _isStart = false;
                return;
            }
            OrderGoToRocation ();
        } ).AddTo ( this );
    }

    /// <summary>
    /// 命令されたときにRobotListの先頭に対して目的の場所まで行く指示をする
    /// </summary>
    private void OrderGoToRocation()
    {
        print ("アタッチ命令");

        //ロボットリストの中身があるとき
        if (_followRobotsList.Count >= 1)
        {
            //配列の先頭のロボットに目的の場所まで行く指示
            _followRobotsList [ 0 ].SwitchStatusGoToLocation ();

            //行動中のロボットリストに追加
            _inActionRobotsList.Add ( _followRobotsList [ 0 ] );

            // プレイヤーについていくロボットのリストから削除
            _followRobotsList.RemoveAt ( 0 );
        }

    }

    /// <summary>
    /// 電波にあたったロボットを呼び戻す指示をする
    /// </summary>
    /// <param name="normalRobotsClass">電波にあたったロボットのクラス</param>
    private void OrderCall(NormalRobotsClass normalRobotsClass)
    {

        //電波に当たったロボットを呼び戻す指示
        normalRobotsClass.SwitchStatusCall ();

        //行動中のロボットのリストから削除
        _inActionRobotsList.Remove ( normalRobotsClass );

        //プレイヤーについていくロボットのリストに格納
        _followRobotsList.Add ( normalRobotsClass);

    }

    /// <summary>
    /// 指定されたロボットを作りリストに入れる
    /// </summary>
    public void RobotCreat()
    {

        //ロボット生成
        GameObject robot = Instantiate ( _nomalRobot );

        //生成したロボットを普通のロボットの親に設定
        robot.transform.SetParent (_normalRobotsTrans);

        //NormalRobotsクラスを取り出す
        NormalRobotsClass normalRobotsClass = robot.transform.GetComponent<NormalRobotsClass> ();

        normalRobotsClass.IsHitRadioWaves.
            Subscribe
            (
                isHitRadioWaves =>
                {
                    if (isHitRadioWaves == true)
                    {

                        OrderCall (normalRobotsClass);
                    }
                }
            ).AddTo(this);

        //生成したオブジェクトのNormalRobotsクラスをリストに格納
        _followRobotsList.Add(normalRobotsClass);

        //最後尾の生成されたばかりのオブジェクトの位置をショップにする
        _followRobotsList [ _followRobotsList.Count - 1 ].transform.position = _shopTrans.position;
    }
  

    #endregion
}
