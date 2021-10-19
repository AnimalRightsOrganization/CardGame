using UnityEngine;

public class Consts
{
    /// <summary>
    /// 游戏数据路径
    /// </summary>
    //跨平台都存在，可以读写的路径
    public static readonly string DataPath = Application.persistentDataPath + @"\data.xml";
}

//summary可以再鼠标悬停时显示提示。
/// <summary> 
/// 角色类型
/// </summary>
public enum CharacterType
{
    Library = 0, //牌库
    Player = 1, //玩家
    ComputerRight = 2, //电脑1
    ComputerLeft = 3, //电脑2
    Desk = 4, //桌子
}

/// <summary>
/// 卡牌花色
/// </summary>
public enum Colors //黑桃>红桃>梅花>方块
{
	Spade,  //黑桃
	Heart,  //红桃
	Club,   //梅花
	Square, //方片
	King,   //王
}

/// <summary>
/// 卡牌权值 2,3,4,5,6,7,8,9,10,J,Q,K,A,2,SmallJoker,BigJoker
/// </summary>
public enum Weight
{
	One     = 1,
	Two     = 2,
    Three   = 3,
    Four    = 4,
    Five    = 5,
    Six     = 6,
    Seven   = 7,
    Eight   = 8,
    Nine    = 9,
    Ten     = 10,
    Jack    = 11,
    Queen   = 12,
    King    = 13,
    SJoker  = 14,
    BJoker  = 15,
}

/// <summary>
/// 出牌类型
/// </summary>
public enum CardType
{
    None,
    Single, //单张
    Double, //对子
    Straight1, //顺子
    Straight2, //双顺子
    Straight3, //三顺子
    Three, //三张不带
    ThreeAndOne, //三带一
    ThreeAndTwo, //三带二
    Boom, //炸弹
    JokerBoom, //王炸弹
}

// 数据库玩家信息
[System.Serializable]
public class DBPlayer
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