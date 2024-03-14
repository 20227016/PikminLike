// ---------------------------------------------------------  
// PlayerAnimetionClass.cs  
//   
// 作成日:  
// 作成者:  
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;

public class PlayerAnimationClass : MonoBehaviour
{

    public void IdelAnime(Animator animator)
    {

        animator.SetTrigger ( "isIdel");
    }

    public void RunAnime (Animator animator){

        animator.SetTrigger("isRunning");
    }

    public void CarrayRunAnime(Animator animator)
    {
        animator.SetTrigger ( "isCarrayRunning" );
    }
}
