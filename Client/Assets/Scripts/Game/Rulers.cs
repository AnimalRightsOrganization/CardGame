using System.Collections;
using System.Collections.Generic;

public static class Rulers
{
    /// <summary>
    /// 是否为单牌
    /// </summary>
    /// <param name="cards">选择的手牌</param>
    /// <returns></returns>
    public static bool isSingle(List<CardAttribute> cards)
    {
        if (cards.Count == 1) return true;
        else return false;
    }

    /// <summary>
    /// 对子
    /// </summary>
    /// <param name="cards"></param>
    /// <returns></returns>
    public static bool isDouble(List<CardAttribute> cards)
    {
        if (cards.Count == 2)
        {
            if (cards[0].weight == cards[1].weight)
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
    public static bool isStraight(List<CardAttribute> cards)
    {
        if (cards.Count < 5 || cards.Count > 12) return false;

        for (int i = 0; i < cards.Count; i++)
        {
            Weight tempWeight = cards[i].weight;
            if (cards[i + 1].weight - tempWeight != 1)
                return false;
            
            //不能超过A
            if (tempWeight > Weight.Ace || cards[i+1].weight > Weight.Ace)
                return false;
        }

        return true;
    }

    /// <summary>
    /// 双顺子
    /// </summary>
    /// <param name="cards"></param>
    /// <returns></returns>
    public static bool isDoubleStraight(List<CardAttribute> cards)
    {
        if (cards.Count < 6 || cards.Count % 2 != 0)
            return false;

        for (int i = 0; i < cards.Count; i += 2)
        {
            if (cards[i].weight != cards[i + 1].weight)
                return false;
            if (cards[i + 2].weight - cards[i].weight != 1)
                return false;
            //不能超过A
            if (cards[i].weight > Weight.Ace || cards[i + 2].weight > Weight.Ace)
                return false;
        }
        return true;
    }

    /// <summary>
    /// 三顺子
    /// </summary>
    /// <param name="cards"></param>
    /// <returns></returns>
    public static bool isTripleStraight(List<CardAttribute> cards)
    {
        if (cards.Count < 6 || cards.Count % 3 != 0)
            return false;

        for (int i = 0; i < cards.Count; i += 3)
        {
            //三张牌相互判断相等
            if (cards[i].weight != cards[i + 1].weight)
                return false;
            if (cards[i + 2].weight != cards[i+1].weight)
                return false;
            if (cards[i].weight != cards[i + 2].weight)
                return false;

            //权值差
            if (cards[i + 3].weight - cards[i].weight != 1)
                return false;

            //不能超过A
            if (cards[i].weight > Weight.Ace || cards[i + 2].weight > Weight.Ace)
                return false;
        }
        return true;
    }
}