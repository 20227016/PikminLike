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
    [SerializeField, Tooltip ( "目的地についたときの探索範囲" )]
    protected float _searchRange = 10;
    [SerializeField, Tooltip ( "歩く速さ" )]
    protected float _speed = 10;

    //インスタンス化
    protected GoToLocationClass _goToLocation = new GoToLocationClass ();
    protected StopToLocation _stopToLocation = new StopToLocation ();
    protected FollowClass _follow = new FollowClass ();

    //探索した結果
    protected RaycastHit _hit = default;

    /// <summary>
    /// 自分のNavMesh
    /// </summary>
    protected NavMeshAgent　_myNav = default;

    #endregion

    #region メソッド  

    private void Awake()
    {

        //自分のNavMesh取得
        _myNav = this.GetComponent<NavMeshAgent> ();
    }

    public void Follow()
    {

        //_follow.Follow ();
    }

    /// <summary>
    /// 目的地まで向かう処理
    /// </summary>
    /// <param name="cursorPos"></param>
    public void GoToLocation(Vector3 cursorPos)
    {

        //
        _goToLocation.GoToLocation (cursorPos ,_myNav ,_speed , _searchRange);
    }

    public void Call()
    {
    
    
    }
    #endregion
}
