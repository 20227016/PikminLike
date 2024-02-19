// ---------------------------------------------------------  
// MoveCheck.cs  
//   移動先の確認
//   Rayを使うため再利用できるようにする
// 作成日:  2/19
// 作成者:  湯元来輝
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;

public class MoveCheck : MonoBehaviour 
{
    //デバック用
    Vector3 _center = default;
    Vector3 _objectSize = default;
    Vector3 _direction = default;
    Transform _objTransfrom = default;
    /// <summary>
    /// 移動方向を調べたRayの情報を返す
    /// </summary>
    /// <param name="moveDirection">移動方向(xyzそれぞれ0~1)</param>
    /// <param name="objectPos">移動するオブジェクトの位置</param>
    /// <returns></returns>
    public RaycastHit  Check(Transform objTransfrom,Vector3 moveDirection)
    {

        /*
         * moveDirectionはXY軸のため
         * Y軸をZ軸に変える
         */
        moveDirection.z = moveDirection.y;
        moveDirection.y = 0;

        //Rayの結果の情報が入る
        RaycastHit hit = default;

        //BoxCastの厚み
        float fowrdValue = 0.1f ;

        //ボックスキャストのサイズ
        Vector3 cubeSize = new Vector3(objTransfrom.localScale.x , objTransfrom.localScale.y , fowrdValue);

        //BoxCastを打つ
        Physics.BoxCast(objTransfrom.position , cubeSize , moveDirection ,out hit);

        //デバック用
        _center = objTransfrom.position;
        _objectSize = cubeSize;
        _direction = moveDirection;
        _objTransfrom = objTransfrom;

        return hit;
    }
     void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
        if (_objTransfrom != null)
        {
            Gizmos.DrawWireCube(_center + _objTransfrom.forward , _objectSize);
        }
    }


}
