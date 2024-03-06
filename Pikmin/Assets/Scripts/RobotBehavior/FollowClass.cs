// ---------------------------------------------------------  
// Follow.cs  
//   
// 作成日:  3/6
// 作成者:  湯元来輝
// ---------------------------------------------------------  
using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class FollowClass : MonoBehaviour
{

    #region メソッド  

    public void Follow(Vector3 target , NavMeshAgent nav , float speed)
    {

        nav.isStopped = false;

        nav.speed = speed;

        nav.destination = target;
    }
  
    #endregion
}
