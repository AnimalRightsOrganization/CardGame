using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{
    private string cardName;
    private Colors cardColor;
    private Weight cardWeight;
    private CharacterType belongTo;

    //用属性形式对外暴露private变量
    /// <summary>
    /// 卡牌名字
    /// </summary>
    public string CardName
    {
        get { return cardName; }
    }

    public Colors CardColor
    {
        get { return cardColor; }
    }

    public Weight CardWeight
    {
        get { return cardWeight; }
    }

    public CharacterType BelongTo
    {
        get { return belongTo; }
    }

    /// <summary>
    /// 卡牌构造函数，用来创建卡牌
    /// </summary>
    /// <param name="name"></param>
    /// <param name="color"></param>
    /// <param name="weight"></param>
    /// <param name="belongTo"></param>
    public Card(string name, Colors color, Weight weight, CharacterType belongTo)
    {
        this.cardName = name;
        this.cardColor = color;
        this.cardWeight = weight;
        this.belongTo = belongTo;
    }
}
