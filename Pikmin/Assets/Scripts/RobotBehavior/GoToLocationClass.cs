// ---------------------------------------------------------  
// GoToLocation.cs  
//   
// 作成日:  3/6
// 作成者:  湯元来輝
// ---------------------------------------------------------  
using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class GoToLocationClass 
{

    public void GoToLocation(Vector3 target,NavMeshAgent myAgent , float speed ,float searchRange )
    {

        //速さを設定
        myAgent.speed = speed;

        //目的地を設定
        myAgent.destination = target;

        //目的の場所までの近さを設定
        myAgent.stoppingDistance = 0;

        //Agent起動
        myAgent.isStopped = false;


    }

}
