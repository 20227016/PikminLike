// ---------------------------------------------------------  
// Hold.cs  
//   荷物を持つ
// 作成日:  2/27~
// 作成者:  湯元来輝
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections.Generic;

public class HoldClass : MonoBehaviour
{
    /// <summary>
    /// 運ぶルート
    /// </summary>
    private List<Vector3> _root = default;

    #region メソッド  


    /// <summary>
    /// 
    /// </summary>
    /// <param name="carryWeight">呼び出したクラスの持てる重さ</param>
    /// <param name="speed">呼び出したクラスの速さ</param>
    /// <param name="hit">目の前にある荷物の情報が入っている</param>
    /// <returns></returns>
    public (List<Vector3> , float) Holding (int carryWeight , float speed , RaycastHit hit)
     {

        //目の前にある荷物の Luggageクラス
        LuggageManagerClass luggageManagerClass = hit.collider.gameObject.GetComponent<LuggageManagerClass>();

        //目の前にある荷物を運ぶ（ルートと運ぶ速さを取得）
        luggageManagerClass.BeHeld (carryWeight , speed);

        return (_root , speed);
     }
  
    #endregion
}
