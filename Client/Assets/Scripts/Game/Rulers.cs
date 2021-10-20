using System.Collections;
using System.Collections.Generic;

public static class Rulers
{
    // 单张
    public static bool isSingle(List<CardAttribute> cards)
    {
        if (cards.Count == 1) return true;
        else return false;
    }

    // 对子
    public static bool isDouble(List<CardAttribute> cards)
    {
        if (cards.Count == 2)
        {
            if (cards[0].weight == cards[1].weight)
                return true;
        }
        return false;
    }

    // 连对，≥2种点数，3344，QQKKAA等。
    public static bool isDoubleMore(List<CardAttribute> cards)
    {
        //①一定是双数牌
        if (cards.Count % 2 != 0)
            return false;

        //②排序
        cards.Sort((x, y) => ((int)x.weight).CompareTo((int)y.weight)); //升序排列

        bool condition = true;

        //③验证两张同点数
        for (int i = 0; i < cards.Count; i += 2)
        {
            int t = i + 1;
            bool cond = (cards[t].weight == cards[i].weight);
            condition = condition && cond;
        }

        //④验证连续
        for (int i = 0; i < cards.Count; i += 2)
        {
            int t = i + 2;
            bool cond = (cards[t].weight == cards[i].weight + 1);
            condition = condition && cond;
        }

        return condition;
    }

    // 顺子
    public static bool isStraight(List<CardAttribute> cards)
    {
        if (cards.Count < 5 || cards.Count > 12) return false;

        for (int i = 0; i < cards.Count; i++)
        {
            Weight tempWeight = cards[i].weight;
            if (cards[i + 1].weight - tempWeight != 1)
                return false;

            //不能超过A
            if (tempWeight > Weight.Ace || cards[i + 1].weight > Weight.Ace)
                return false;
        }

        return true;
    }

    // 炸弹
    public static bool isBomb(List<CardAttribute> cards)
    {
        if (cards.Count == 4)
        {
            var value = cards[0].weight;

            bool condition = true;
            for (int i = 0; i < cards.Count; i++)
            {
                bool condX = (cards[i].weight == value);
                condition = condition && condX;
            }
            return condition;
        }
        return false;
    }
}