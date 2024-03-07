// ---------------------------------------------------------  
// StopToLocation.cs  
//   
// 作成日:  3/6
// 作成者:  湯元来輝
// ---------------------------------------------------------  
using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class StopToLocationClass
{

    #region メソッド  

    public void StopToLocation(NavMeshAgent myNav)
    {
        myNav.isStopped = true;
    }

    #endregion
}
