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
    
    private Vector3 _origin = default;
    private Vector3 _cubeSize = default;
    private float _dist = default;
    Transform _objTransfrom = default;

    /// <summary>
    /// 移動方向を調べたRayの情報を返す
    /// </summary>
    /// <param name="moveDirection">移動方向(xyzそれぞれ0~1)</param>
    /// <param name="objTransform">移動するオブジェクトのトランスフォーム</param>
    /// <returns>Ray情報</returns>
    public RaycastHit Check(Transform objTransform,Vector3 moveDirection)
    {
        _objTransfrom = objTransform;

        //Rayの結果の情報が入る
        RaycastHit hit = default;

        //開始地点のずれ
        float startOffset = 0.6f;

        //Rayの開始地点
        _origin = objTransform.position + objTransform.forward * startOffset;

        //BoxCastの厚み
        float forwardValue = 0.2f;

        //ボックスキャストのサイズ
        _cubeSize = new Vector3 ( objTransform.localScale.x , objTransform.localScale.y , forwardValue );

        //Rayの距離
        _dist = 0.1f;



        //BoxCastを打つ
        Physics.BoxCast ( _origin , _cubeSize , moveDirection , out hit , objTransform.rotation , _dist );
        

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

        Gizmos.DrawWireCube ( _origin , _cubeSize );
    }


}
