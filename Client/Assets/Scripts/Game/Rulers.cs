using System.Collections;
using System.Collections.Generic;

public static class Rulers
{
    /// <summary>
    /// 是否为单牌
    /// </summary>
    /// <param name="cards">选择的手牌</param>
    /// <returns></returns>
    public static bool isSingle(List<Card> cards)
    {
        if (cards.Count == 1) return true;
        else return false;
    }

    /// <summary>
    /// 对子
    /// </summary>
    /// <param name="cards"></param>
    /// <returns></returns>
    public static bool isDouble(List<Card> cards)
    {
        if (cards.Count == 2)
        {
            if (cards[0].CardWeight == cards[1].CardWeight)
            {
                return true; //已经return就不会往下走
            }
        }
        return false;
    }

    /// <summary>
    /// 单顺子
    /// </summary>
    /// <param name="cards"></param>
    /// <returns></returns>
    public static bool isStraight(List<Card> cards)
    {
        if (cards.Count < 5 || cards.Count > 12) return false;

        for (int i = 0; i < cards.Count; i++)
        {
            Weight tempWeight = cards[i].CardWeight;
            if (cards[i + 1].CardWeight - tempWeight != 1)
                return false;
            
            //不能超过A
            if (tempWeight > Weight.One || cards[i+1].CardWeight > Weight.One)
                return false;
        }

        return true;
    }

    /// <summary>
    /// 双顺子
    /// </summary>
    /// <param name="cards"></param>
    /// <returns></returns>
    public static bool isDoubleStraight(List<Card> cards)
    {
        if (cards.Count < 6 || cards.Count % 2 != 0)
            return false;

        for (int i = 0; i < cards.Count; i += 2)
        {
            if (cards[i].CardWeight != cards[i + 1].CardWeight)
                return false;
            if (cards[i + 2].CardWeight - cards[i].CardWeight != 1)
                return false;
            //不能超过A
            if (cards[i].CardWeight > Weight.One || cards[i + 2].CardWeight > Weight.One)
                return false;
        }
        return true;
    }

    /// <summary>
    /// 三顺子
    /// </summary>
    /// <param name="cards"></param>
    /// <returns></returns>
    public static bool isTripleStraight(List<Card> cards)
    {
        if (cards.Count < 6 || cards.Count % 3 != 0)
            return false;

        for (int i = 0; i < cards.Count; i += 3)
        {
            //三张牌相互判断相等
            if (cards[i].CardWeight != cards[i + 1].CardWeight)
                return false;
            if (cards[i + 2].CardWeight != cards[i+1].CardWeight)
                return false;
            if (cards[i].CardWeight != cards[i + 2].CardWeight)
                return false;

            //权值差
            if (cards[i + 3].CardWeight - cards[i].CardWeight != 1)
                return false;

            //不能超过A
            if (cards[i].CardWeight > Weight.One || cards[i + 2].CardWeight > Weight.One)
                return false;
        }
        return true;
    }
}