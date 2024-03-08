// ---------------------------------------------------------  
// GetClopser.cs  
//   
// 作成日:  3/6
// 作成者:  湯元来輝
// ---------------------------------------------------------  
using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class GetClopserClass 
{

    public void GetCloser(Vector3 target , NavMeshAgent myAgent ,float stopDist)
    {
   
        //目的地を設定
        myAgent.destination = target;

        //目的の場所までの近さを設定
        myAgent.stoppingDistance = stopDist;

        //エージェント起動
        myAgent.isStopped = false;
    }
}
