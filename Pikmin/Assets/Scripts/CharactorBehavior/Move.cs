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

    //オブジェクトの移動方向
    private Vector3 _moveDirection = default;

    //移動できるかの判断 
    private bool _isMove = false;

    //壁に当たったかの判定
    RaycastHit _hit = default;

    //Rayの始まり
    private Vector3 _origin = default;

    //Rayの向き
    private Vector3 _direction = default;

    //Ray本体
    private Ray _ray = default;
    #endregion

    #region メソッド  

    private void Update()
    {

        if (_objTransfrom == null)
        {

            return;
        }
        _objTransfrom.position += _moveDirection;
    }

    /// <summary>
    /// 移動する方向を決める
    /// </summary>
    /// <param name="obj">移動するオブジェクト</param>
    /// /// <param name="moveVal">移動方向の値（0か1で入っている）</param>
    /// <param name="speed">オブジェクトの移動する速さ</param>
    public void MoveMethod(GameObject obj , Vector2 moveVal , float speed)
    {
        //オブジェクトのトランスフォームを取得
        _objTransfrom = obj.transform;

        //オブジェクトの移動方向に指定された速さで移動する
        _moveDirection = new Vector3(moveVal.x , 0 , moveVal.y) * speed * Time.deltaTime;

        //移動できるかを確かめる
        Moveble(_moveDirection , _objTransfrom.position);
    }

    public bool Moveble(Vector3 moveDirection, Vector3 objectPos)
    {
        //Ray作り
        _origin = moveDirection + objectPos;
        _direction = _moveDirection;
        _ray = new Ray(_origin , _direction);

        //Rayを打つ
        Physics.Raycast(_ray,out _hit);

        return _hit;
    }

    #endregion
}
