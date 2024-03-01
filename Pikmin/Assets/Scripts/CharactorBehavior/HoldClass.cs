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
    /// <param name="muscleStrength">呼び出したクラスの持てる重さ</param>
    /// <param name="speed">呼び出したクラスの速さ</param>
    /// <param name="hit">目の前にある荷物の情報が入っている</param>
    /// <returns>持てるかの判断を返す</returns>
    public bool Holding (int muscleStrength , float speed , Transform hitObjTrans)
     {

        //持った荷物の Luggageクラス
        LuggagesClass luggageManagerClass = hitObjTrans.GetComponent<LuggagesClass> ();

        //持った荷物を運ぶ（持てるかを取得）
        bool isBeHeld =luggageManagerClass.BeHeld (muscleStrength , speed);

        //持てるかの判断を返す
        return isBeHeld;
     }
  
    #endregion
}
