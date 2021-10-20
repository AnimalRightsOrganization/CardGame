using System.Collections.Generic;

// 客户端玩家属性
[System.Serializable]
public class ClientPlayer : DBPlayer
{
    public int SeatID; //这盘中的顺位
    public List<CardAttribute> HandCards = new List<CardAttribute>(); //手牌List
}
// 数据库玩家属性
[System.Serializable]
public class DBPlayer
{
    public string username; //用户名 nn_1234567890
    public AvatarModel avatar;
    public long money;
    //public string nickname;
    //public string gender;
    //public string headimg;
}
// 角色模型
public enum AvatarModel
{
    Reimu = 0,
    Marisa = 1,
    Alice = 2,
    Ellen = 3,
    Anaberal = 4,
}