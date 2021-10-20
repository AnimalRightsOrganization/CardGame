using System.Collections;
using System.Collections.Generic;
using Debug = UnityEngine.Debug;

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

    // 连对（≥2种点数，3344，QQKKAA等）
    public static bool isDoubleMore(List<CardAttribute> cards)
    {
        //①一定是双数牌，至少4张
        if (cards.Count % 2 != 0 || cards.Count < 4)
            return false;

        //②排序
        cards.Sort((x, y) => ((int)x.weight).CompareTo((int)y.weight)); //升序排列

        bool condition = true;
        for (int i = 0; i < cards.Count / 2 - 1; i++)
        {
            int index = i * 2;

            var left = cards[index];
            //Debug.Log($"left: {left.ToString()}");
            var right = cards[index + 1];
            //Debug.Log($"right: {right.ToString()}");

            var nextLeft = cards[index + 2];
            var nextRight = cards[index + 3];

            //③验证两张同点数
            bool cond1 = (left.weight == right.weight);
            //④验证连续
            bool cond2 = (left.weight + 1 == nextLeft.weight);
            bool cond3 = (right.weight + 1 == nextRight.weight);

            condition = condition && cond1 && cond2 && cond3;
            //Debug.Log($"第{i + 1}轮检测：{cond1},{cond2},{cond3}");
        }
        return condition;
    }

    // 三带二（≥3张同点数，带任意两张杂牌，33344，33357等）
    public static bool isThreeWithTwo(List<CardAttribute> cards)
    {
        return false;
    }

    // 飞机（≥2种点数连续的三同张，带同样数量对子，如33344455577JJKK等）
    public static bool isAirplane(List<CardAttribute> cards)
    {
        return false;
    }

    // 顺子（≥5张连续点数，10JQKA等）
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

    // 炸弹（≥4张相同牌，4444，77777等）
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