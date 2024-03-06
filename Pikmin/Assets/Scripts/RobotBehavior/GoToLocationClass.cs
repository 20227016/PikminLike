// ---------------------------------------------------------  
// GoToLocation.cs  
//   
// 作成日:  3/6
// 作成者:  湯元来輝
// ---------------------------------------------------------  
using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class GoToLocationClass : MonoBehaviour
{

    #region 変数  

    #endregion

    #region メソッド  

    public void GoToLocation(Vector3 target,NavMeshAgent myNav , float speed ,float searchRange )
    {

        //Agent起動
        myNav.isStopped = false;

        //速さを設定
        myNav.speed = speed;

        //目的地を設定
        myNav.destination = target;
    }

    #endregion
}
