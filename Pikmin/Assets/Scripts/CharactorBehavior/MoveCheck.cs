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
    Vector3 _origin = default;
    Vector3 _cubeSize = default;
    float _dist = default;
    Transform _objTransfrom = default;
    /// <summary>
    /// 移動方向を調べたRayの情報を返す
    /// </summary>
    /// <param name="moveDirection">移動方向(xyzそれぞれ0~1)</param>
    /// <param name="objectPos">移動するオブジェクトの位置</param>
    /// <returns></returns>
    public RaycastHit Check(Transform objTransform)
    {
        _objTransfrom = objTransform;

        //Rayの結果の情報が入る
        RaycastHit hit = default;

        //開始地点のずれ
        float startOffset = 0.3f;

        //Rayの開始地点
        _origin = objTransform.position + objTransform.forward * startOffset;


        Debug.Log ( objTransform.forward + "向き" );
        //Debug.Log ( objTransform.position.z + "オブジェクト");
        //Debug.Log ( _origin +"　開始地点　");
        //Debug.Log ( objTransform.position.z - _origin.z + "差" );
        //BoxCastの厚み
        float forwardValue = 0.1f;
        //ボックスキャストのサイズ
        _cubeSize = new Vector3 ( objTransform.localScale.x , objTransform.localScale.y , forwardValue );
        //Rayの距離
        _dist = 0.1f;



        //BoxCastを打つ
        Physics.BoxCast ( _origin , _cubeSize , objTransform.TransformDirection ( Vector3.forward ) , out hit , Quaternion.identity , _dist );

        Debug.DrawLine ( _origin , _origin + objTransform.forward * 5 , Color.red );

        return hit;
    }

    void OnDrawGizmos()
    {

        //Rayの色を指定
        Gizmos.color = Color.red;

        if (_objTransfrom == null)
        {

            return;
        }

        Gizmos.DrawWireCube ( _origin + _objTransfrom.TransformDirection ( Vector3.forward ) * _dist, _cubeSize );
    }


}
