using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 自己拥有在0号位
// 其他人按顺序坐到1-5
public class SpawnPoints : MonoBehaviour
{
    private Vector3[] seatPoints = {
        new Vector3(0, 0, -3),
        new Vector3(-3, 0, -2),
        new Vector3(-3, 0, 2),
        new Vector3(0, 0, 3),
        new Vector3(3, 0, 2),
        new Vector3(3, 0, -2),
    };

    // <座位序号，坐着的人>
    private Dictionary<int, GamePlayer> seatDic = new Dictionary<int, GamePlayer>();

    // 对所有玩家分配入座
    void SeatAll()
    {

    }

    // 对最后一个加入的玩家，分配入座
    void SeatOne()
    {

    }
}
