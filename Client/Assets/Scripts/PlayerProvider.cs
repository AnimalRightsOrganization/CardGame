using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProvider : MonoBehaviour
{
    public Player mPlayer;

    void Start()
    {

    }
}

// 数据库玩家信息
[System.Serializable]
public class Player
{
    public string user_id; //用户名 nn_1234567890
    public AvatarModel avatar;
    public long money;
    //public string nickname;
    //public string gender;
    //public string headimg;
}

public enum AvatarModel
{
    Reimu = 0,
    Marisa = 1,
    Alice = 2,
    Ellen = 3,
    Anaberal = 4,
}
