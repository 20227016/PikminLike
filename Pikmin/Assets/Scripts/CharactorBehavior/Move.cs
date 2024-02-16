// ---------------------------------------------------------  
// Move.cs  
//   引数とオブジェクトを利用して移動する
// 作成日:  2/16~16
// 作成者:  
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;


public class Move : MonoBehaviour
{

    #region 変数  

    //関数を呼び出したクラスがアタッチされているオブジェクト
    private Transform   _objTransfrom = default;

    //オブジェクトの移動先
    private Vector3 _pos = default;

    //移動できるかの判断 
    private bool _isMove = false;
    #endregion

    #region メソッド  

    private void Update()
    {

        if (_objTransfrom == null)
        {

            return;
        }
        _objTransfrom.position += _pos;
    }

    /// <summary>
    /// 移動する
    /// </summary>
    /// <param name="obj">移動するオブジェクト</param>
    /// /// <param name="moveVal">移動方向の値（0か1で入っている）</param>
    /// <param name="speed">オブジェクトの移動する速さ</param>
    public void MoveMethod(GameObject obj , Vector2 moveVal , float speed)
    {
        //オブジェクトのトランスフォームを取得
        _objTransfrom = obj.transform;

        //オブジェクトの移動方向に指定された速さで移動する
        _pos = new Vector3(moveVal.x , 0 , moveVal.y) * speed * Time.deltaTime;


    }


    #endregion
}
