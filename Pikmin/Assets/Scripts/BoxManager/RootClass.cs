// ---------------------------------------------------------  
// RootClass.cs  
//   ルートを決める
// 作成日:  2/27
// 作成者:  湯元来輝
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections.Generic;

public class RootClass : MonoBehaviour
{



    [SerializeField, Tooltip ( "StoragePlaceのオブジェクト" )]
    private GameObject _storagePlace = default;

    /// <summary>
    /// ルートを決めて曲がる箇所をListに詰めていく
    /// </summary>
    /// <param name="boxTrans">呼び出した荷物オブジェクトのトランスフォーム</param>
    /// <param name="root">ルート</param>
    public List<Vector3> Rooting(Transform boxTrans , List<Vector3> root)
    {

        //BoxCastを打って当たった地点が返される
        ShotBoxCast ();



        return root;
    }

    private void ShotBoxCast()
    {
        
    
    
    
    }


}
