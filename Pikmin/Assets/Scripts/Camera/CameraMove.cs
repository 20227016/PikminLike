// ---------------------------------------------------------  
// CameraMove.cs  
//   
// 作成日:  2/21
// 作成者:  湯元来輝
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;

public class CameraMove 
{
    //プレイヤーとの距離
    private float _distanceX;
    private float _distanceZ;

    //  コンストラクタ
    public CameraMove()
    {

        _distanceX = default;
        _distanceZ = default;
    }

    #region メソッド  


    public void Tracking(Transform playerTrans , Transform cameraTrans , float[ ] maxDistanceXZ , float[ ] minDistancesXZ)
    {
        //X・Z軸の距離を取得
        GetDistans(playerTrans ,cameraTrans);

        //動かないといけない距離になったか調べる
        bool isMove = CheckDistance(maxDistanceXZ , minDistancesXZ);

        if (isMove == false)
        {

            return;
        }
        ////距離が追跡し始める値を超えた時
        //if (Mathf.Abs(_distanceX) >= _moveDistanceX)
        //{

        //    //プレイヤーを追跡
        //    transform.position += Vector3.right * _speed * Time.deltaTime * Mathf.Sign(_distanceX);
        //    //X軸のプレイヤーとの距離を更新
        //    _distanceX = _playerObj.transform.position.x - transform.position.x;
        //}
        //if (Mathf.Abs(_distanceZ) >= _moveDistanceZ)
        //{

        //    //プレイヤーを追跡
        //    transform.position += Vector3.forward * _speed * Time.deltaTime * Mathf.Sign(_distanceZ);
        //    //Y軸のプレイヤーとの距離を更新
        //    _distanceZ = _playerObj.transform.position.z - transform.position.z;
        //}

    }

    private void GetDistans(Transform playerTrans , Transform cameraTrans)
    {

        //X軸のプレイヤーとの距離を取得
        _distanceX = playerTrans.transform.position.x - cameraTrans.position.x;

        //Y軸のプレイヤーとの距離を取得
        _distanceZ = playerTrans.transform.position.z - cameraTrans.position.z;

    }

    private bool CheckDistance(float[ ] maxDistance , float[ ] minDistanceXZ)
    {

        //カメラとの最小距離を超えた時
        if (Mathf.Abs(_distanceZ) >= minDistanceXZ[0]  )

        {
        }

        return false;
    }
    #endregion
}
