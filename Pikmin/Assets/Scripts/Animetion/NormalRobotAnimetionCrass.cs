// ---------------------------------------------------------  
// RobotAnimetionCrass.cs  
//   
// 作成日:  
// 作成者:  
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;

public class NormalRobotAnimetionCrass : MonoBehaviour
{


    public void IdelAnima(Animator animator)
    {

        animator.SetTrigger ( "isIdel" );
    }

    public void RunAnima(Animator animator)
    {

        animator.SetTrigger ( "isWalking" );
    }

    public void CarrayRunAnima(Animator animator)
    {
        animator.SetTrigger ( "isCarrayRunning" );
    }
}
