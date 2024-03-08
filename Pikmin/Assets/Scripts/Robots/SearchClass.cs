// ---------------------------------------------------------  
// Serch.cs  
//   
// 作成日:  3/6
// 作成者:  湯元来輝
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;

public class SearchClass
{
    public RaycastHit Search(Transform robotTrans , float searchRange)
    {

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

        //BoxCastにあたったオブジェクトを見ていく
        foreach (RaycastHit hit in hits)
        {

            //中身が入っているかつ荷物の時
            if (hit.collider != null && hit.collider.CompareTag ( "Luggage" ))
            {

                return hit;
            }
        }

        //空のRaycastを返す
        return new RaycastHit();
        
    }
}
