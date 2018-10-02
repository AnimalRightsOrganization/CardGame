using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProvider : MonoBehaviour
{
    public Player mPlayer;

    void Start()
    {

    }

    void Update()
    {

    }
}

// 数据库玩家信息
[System.Serializable]
public class Player
{
    public string user_id; //用户名 nn_1234567890
    public AvatarModel avatar;
    //public string nickname;
    //public string gender;
    //public string headimg;
    //public string level;
    ///public string money;
}

public enum AvatarModel
{
    kizuna = 0,
    luna = 1,
    miku = 2
}
