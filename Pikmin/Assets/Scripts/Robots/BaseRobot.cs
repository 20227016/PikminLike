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
    protected float _stopPlayerDist = 3;
    [SerializeField, Tooltip ( "プレイヤーとの距離" )]
    protected float _stopLuggageDist = 3;
    [SerializeField, Tooltip ( "目的地についたときの探索範囲" )]
    protected float _searchRange = 10;
    [SerializeField, Tooltip ( "歩く速さ" )]
    protected float _speed = 10;

    //インスタンス化
    protected GoToLocationClass _goToLocation = new GoToLocationClass ();
    protected FollowClass _follow = new FollowClass ();
    protected SearchClass _search = new SearchClass ();
    protected GetClopserClass _getClopser = new GetClopserClass ();

    /// <summary>
    /// プレイヤーのトランスフォーム
    /// </summary>
   protected Transform _playerTrans = default;

    /// <summary>
    /// カーソルのトランスフォーム
    /// </summary>
    protected Transform _cursorTrans = default;

    /// <summary>
    /// 普通のロボットの親のトランスフォーム
    /// </summary>
    protected Transform _normalRobotsTrans = default;

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
        //NormalRobotsオブジェクトのトランスフォームを取得
        _normalRobotsTrans = GameObject.Find ( "NormalRobots" ).transform;
    }

    #endregion
}
