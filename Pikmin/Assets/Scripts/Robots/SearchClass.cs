// ---------------------------------------------------------  
// Serch.cs  
//   
// 作成日:  3/6
// 作成者:  湯元来輝
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;

public class SearchClass : MonoBehaviour
{
    public RaycastHit Search(Transform robotTrans , float searchRange)
    {
        //Physics.BoxCast ( _origin , _cubeSize , moveDirection , out hit , objTransform.rotation , _dist );

        //複数のcolliderが入る
        RaycastHit [] hits = default;

        //始点
        Vector3 origen = robotTrans.position;

        //サイズ
        Vector3 cubeSize = (Vector3.right + Vector3.forward) * searchRange + Vector3.up * robotTrans.localScale.y;

        //Rayの距離
        float dist = searchRange;

        //Boxcast範囲内のすべてのオブジェクトを取得
        hits = Physics.BoxCastAll ( origen , cubeSize ,robotTrans.forward, Quaternion.identity ,dist );

        print ( hits.Length );
        foreach (RaycastHit hit in hits)
        {
            print ( hit.collider );
            if (hit.collider != null && hit.collider.CompareTag ( "Luggage" ))
            {

                print ("荷物を見つける");
                return hit;
            }
        }

        //空のRaycastを返す
        return new RaycastHit();
        
    }
}
