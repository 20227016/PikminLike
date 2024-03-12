// ---------------------------------------------------------  
// MoveCheck.cs  
//   移動先の確認
//   Rayを使うため再利用できるようにする
// 作成日:  2/19
// 作成者:  湯元来輝
// ---------------------------------------------------------  
using UnityEngine;

public class MoveCheckClass : MonoBehaviour
{


    //オブジェクトのトランスフォーム
    private Transform _objTransfrom = default;

    //Rayの結果情報が入る
    private RaycastHit _hit;

    /// <summary>
    /// 移動方向を調べたRayの情報を返す
    /// </summary>
    /// <param name="moveDirection">移動方向(xyzそれぞれ0~1)</param>
    /// <param name="objTransform">移動するオブジェクトのトランスフォーム</param>
    /// <returns>Rayの結果情報</returns>
    public RaycastHit Check(Transform objTransform)
    {
        //クラス変数に代入
        _objTransfrom = objTransform;

        //BoxCastを打つ
        BoxCast (objTransform);

        //Rayの中に情報が入っていた時
        if (_hit.collider != null)
        {

            return _hit;
        }

        //右斜めにRayを打つ
        RayCastDiagonalRight(objTransform);

        //Rayの中に情報が入っていた時
        if (_hit.collider != null)
        {

            return _hit;
        }

        //斜め左にRayを打つ
        RayCastDiagonalLeft (objTransform);

        return _hit;
    }

    /// <summary>
    /// BoxCastを打つ
    /// </summary>
    /// <param name="moveDirection">移動するオブジェクトのトランスフォーム</param>
    private void BoxCast(Transform objTransform )
    {

        //Rayの開始地点
        Vector3 origin = objTransform.position + objTransform.forward * objTransform.localScale.z;

        //BoxCastの横幅の補填
        float widthOffset = 1f;

        //BoxCastの奥幅
        float forwardValue = 0.5f;

        //ボックスキャストのサイズ
        Vector3 cubeSize = new Vector3 ( objTransform.localScale.x + widthOffset , objTransform.localScale.y , forwardValue );

        //Rayの距離
        float dist = 0.1f;

        //BoxCastを打つ
        Physics.BoxCast ( origin , cubeSize , objTransform.forward , out _hit , objTransform.rotation , dist );
    }

    /// <summary>
    /// 斜め左にRayを打つ
    /// </summary>
    /// <param name="objTransform">移動するオブジェクトのトランスフォーム</param>
    private void RayCastDiagonalLeft(Transform objTransform)
    {

        //始点
        Vector3 orgne = objTransform.position;

        //向き
        Vector3 dire = objTransform.forward.normalized + objTransform.right.normalized;

        //Ray
        Ray ray = new Ray (orgne,dire);

        //距離
        float dist = 1f;

        //Rayを打つ
        Physics.Raycast (ray ,out _hit , dist);
        Debug.DrawRay ( orgne , dire * _hit.distance , Color.red );
    }

    /// <summary>
    /// 斜め右にRayを打つ
    /// </summary>
    /// <param name="objTransform">移動するオブジェクトのトランスフォーム</param>
    private void RayCastDiagonalRight(Transform objTransform)
    {

        //始点
        Vector3 orgne = objTransform.position;

        //向き
        Vector3 dire = objTransform.forward.normalized + (-objTransform.right.normalized);

        //Ray
        Ray ray = new Ray ( orgne , dire );

        //距離
        float dist = 1f;

        //Rayを打つ
        Physics.Raycast ( ray , out _hit , dist );
        Debug.DrawRay ( orgne , dire * _hit.distance , Color.red );
    }

}
