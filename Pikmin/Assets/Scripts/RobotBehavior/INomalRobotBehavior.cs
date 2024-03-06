using UnityEngine;
using System.Collections;


public interface INomalRobotBehavior
{

    void Follow();

    void GoToLocation(Transform cursorTrans);

}
