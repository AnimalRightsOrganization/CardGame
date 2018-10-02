using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 自己拥有在0号位
// 其他人按顺序坐到1-5
public class SpawnPoints : MonoBehaviour
{
    public static SpawnPoints instance;

    private Vector3[] seatPoints = {
        new Vector3(0, 0, -3),
        new Vector3(-3, 0, -2),
        new Vector3(-3, 0, 2),
        new Vector3(0, 0, 3),
        new Vector3(3, 0, 2),
        new Vector3(3, 0, -2),
    };

    // <座位序号，坐着的人>
    private Dictionary<int, Transform> seatDic = new Dictionary<int, Transform>()
    {
        { 0, null },
        { 1, null },
        { 2, null },
        { 3, null },
        { 4, null },
        { 5, null },
    };

    void Awake()
    {
        instance = this;
    }

    // 对最后一个加入的玩家，分配入座
    public void SitDown(Transform player)
    {
        foreach (var item in seatDic)
        {
            if (item.Value == null)
            {
                //Debug.Log(player.GetComponent<PlayerProvider>().mPlayer.avatar + "坐到" + item.Key + "号位");
                seatDic[item.Key] = player;
                player.transform.position = seatPoints[item.Key];
                return;
            }
        }
    }
}
