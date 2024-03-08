// ---------------------------------------------------------  
// PutClass.cs  
//   荷物を置く
// 作成日:  2/27
// 作成者:  湯元来輝
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;

public class PutClass : MonoBehaviour
{

    /// <summary>
    ///     荷物を置く処理
    /// </summary>
    /// <param name="muscleStrength">呼び出したクラスの持てる重さ</param>
    /// <param name="speed">呼び出したクラスの速さ</param>
    /// <param name="hitObjTrans">目の前にある荷物の情報が入っている</param>
    public void Put (int muscleStrength , float speed , Transform hitObjTrans)
    {

        //置いた荷物の Luggageクラス
        LuggagesClass luggageManagerClass = hitObjTrans.GetComponent<LuggagesClass> ();

        //置いた荷物を運ぶ（ルートと運ぶ速さを取得）
        luggageManagerClass.BePlaced( muscleStrength , speed );

    }
}
