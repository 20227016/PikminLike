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

        Debug.Log ( speed);
        //移動速度を設定
        agent.speed = speed;

        // 目標地点を設定
        agent.destination = _storagePlace.position;
    }

    /// <summary>
    /// 置かれたとき
    /// </summary>
    /// <param name="agent">参照している荷物のナビ</param>
    public void OutCarray(NavMeshAgent agent)
    {
        //ナビを停止
        agent.isStopped = true;

    }

  
}
