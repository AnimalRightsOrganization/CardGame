using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Consts
{
    /// <summary>
    /// 游戏数据路径
    /// </summary>
    //跨平台都存在，可以读写的路径
    public static readonly string DataPath = Application.persistentDataPath + @"\data.xml";
}

/// <summary>
/// 面板类型
/// </summary>
public enum paneltype
{

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
    Desk = 4 //桌子
}

/// <summary>
/// 卡牌花色
/// </summary>
public enum Colors
{
    King, //王
    Club, //梅花
    Heart, //红桃
    Spade, //黑桃
    Square //方片
}

/// <summary>
/// 卡牌权值 2,3,4,5,6,7,8,9,10,J,Q,K,A,2,SmallJoker,BigJoker
/// </summary>
public enum Weight
{
    Three,
    Four,
    Five,
    Six,
    Seven,
    Eight,
    Nine,
    Ten,
    Jack,
    Queen,
    King,
    One,
    Two,
    SJoker,
    BJoker
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
    JokerBoom //王炸弹
}

/// <summary>
/// 身份
/// </summary>
public enum Identity
{
    Farmer, //农民
    LandLord //地主
}

