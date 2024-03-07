// ---------------------------------------------------------  
// BaseRobot.cs  
//   
// 作成日:  3/6
// 作成者:  湯元来輝
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class BaseRobot : MonoBehaviour
{

    #region 変数  

    [Header("ステータス")]
    [SerializeField, Tooltip ( "持てる重さ" )]
    protected int _muscleStrength = 3;
    [SerializeField, Tooltip ( "維持にかかるコスト" )]
    protected int _cost = 10;
    [SerializeField, Tooltip ( "プレイヤーとの距離" )]
    protected float _stopDist = 10;
    [SerializeField, Tooltip ( "目的地についたときの探索範囲" )]
    protected float _searchRange = 10;
    [SerializeField, Tooltip ( "歩く速さ" )]
    protected float _speed = 10;

    //インスタンス化
    protected GoToLocationClass _goToLocation = new GoToLocationClass ();
    protected StopToLocationClass _stopToLocation = new StopToLocationClass ();
    protected FollowClass _follow = new FollowClass ();
    protected SearchClass _search = new SearchClass ();

    /// <summary>
    /// プレイヤーのトランスフォーム
    /// </summary>
    private Transform _playerTrans = default;

    /// <summary>
    /// カーソルのトランスフォーム
    /// </summary>
    private Transform _cursorTrans = default;

    /// <summary>
    /// 自分のNavMesh
    /// </summary>
    protected NavMeshAgent　_myAgent = default;



    #endregion

    #region メソッド  

    private void Start()
    {

        //Playerオブジェクトのトランスフォームを取得
        _playerTrans = GameObject.Find ( "Player" ).transform;
        //Cursorオブジェクトのトランスフォームを取得
        _cursorTrans = GameObject.Find ( "Cursor" ).transform;

    }

    protected void Follow()
    {

        //ついていく処理
        _follow.Follow (_playerTrans.position,_myAgent,_speed,_stopDist);
    }

    /// <summary>
    /// 目的地まで向かう処理
    /// </summary>
    /// <param name="cursorPos"></param>
    protected void  GoToLocation()
    {

        _goToLocation.GoToLocation (_cursorTrans.position ,_myAgent ,_speed , _searchRange);
        
    }

    protected void Call()
    {

        //Agentの動きを止める
        _stopToLocation.StopToLocation ( _myAgent );
        Follow ();
    }

    protected void Search()
    {
    
        
    }

    #endregion
}
