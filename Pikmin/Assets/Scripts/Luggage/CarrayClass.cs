// ---------------------------------------------------------  
// CarrayClass.cs  
//   運ぶ
// 作成日:  2/27
// 作成者:  湯元来輝
// ---------------------------------------------------------  
using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class CarrayClass : MonoBehaviour
{

    [Header ( "オブジェクト" )]
    [SerializeField, Tooltip ( "StoragePlace" )]
    private Transform _storagePlace = default;

    /// <summary>
    /// 持たれたとき
    /// ナビゲーションで動かす処理
    /// </summary>
    /// <param name="luggaTrans">参照している荷物のトランスフォーム</param>
    /// <param name="agent">参照している荷物のナビ</param>
    /// <param name="speed">参照している荷物の速さ</param>
    public void OnCarray(Transform luggaTrans , NavMeshAgent agent , float speed )
    {
        //ナビを起動
        agent.isStopped = false;

        //移動速度を設定
        agent.speed = speed;

        //地面からの高さ
        const float HEIGHT = 0.7f;

        // 目標地点を設定
        agent.destination = _storagePlace.position + Vector3.up * HEIGHT;

    }

    /// <summary>
    /// 置かれたとき
    /// </summary>
    /// <param name="agent">参照している荷物のナビ</param>
    public void StopCarray(NavMeshAgent agent)
    {

        //ナビを停止
        agent.isStopped = true;

    }

  
}
