using System.Linq;
using System.Collections.Generic;
using Debug = UnityEngine.Debug;

public static class Rulers
{
    #region 牌型检测（双端）
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

    // 三带二（=3张同点数，带任意两张杂牌，33344，33357等）
    public static bool isThreeWithTwo(List<CardAttribute> cards, bool withTwo = true)
    {
        //①数量检测
        if (withTwo == false)
        {
            if (cards.Count < 3 || cards.Count > 4) //3张或4张牌
                return false;
        }
        else
        {
            if (cards.Count != 5)
                return false;
        }

        //②排序
        cards.Sort((x, y) => ((int)x.weight).CompareTo((int)y.weight)); //升序排列
        
        //③分类
        Dictionary<Weight, List<CardAttribute>> tmp = new Dictionary<Weight, List<CardAttribute>>(); //全部点数，2种或3种
        for (int i = 0; i < cards.Count; i++)
        {
            var card = cards[i];
            var cardValue = card.weight;
            if (tmp.ContainsKey(cardValue) == false)
            {
                var lst = new List<CardAttribute>();
                lst.Add(card);
                tmp.Add(cardValue, lst);
            }
            else
            {
                var lst = tmp[cardValue];
                lst.Add(card);
            }
        }
        Debug.Log($"{tmp.Keys.Count}种点数");

        bool cond1 = false;
        if (withTwo == false)
        {
            cond1 = tmp.Keys.Count == 1 || tmp.Keys.Count == 2; //总共有一组或两组
        }
        else
        {
            cond1 = tmp.Keys.Count == 2 || tmp.Keys.Count == 3; //总共有两组或三组
        }
        bool cond2 = false; //其中一组有3个元素
        foreach (var item in tmp)
        {
            Debug.Log($"{item.Key}---{item.Value.Count}");
            if (item.Value.Count == 3)
            {
                cond2 = true;
                break;
            }
        }
        return cond1 && cond2;
    }

    // 飞机（≥2种点数连续的三同张，带同样数量对子，如33344455577JJKK等）
    public static bool isAirplane(List<CardAttribute> cards, bool withTwo = true)
    {
        //①数量检测
        if (withTwo)
        {
            if (!(cards.Count >= 10 && cards.Count % 5 == 0))
                return false;
        }
        else
        {
            if (!(cards.Count >= 6 && cards.Count % 3 == 0))
                return false;
        }

        //②排序
        cards.Sort((x, y) => ((int)x.weight).CompareTo((int)y.weight)); //升序排列

        //③检测三同张，检测连续
        Dictionary<Weight, List<CardAttribute>> tmp = new Dictionary<Weight, List<CardAttribute>>(); //全部点数
        List<Weight> tmpMain = new List<Weight>(); //三同张，保证同点数有3张
        List<Weight> tmpSub = new List<Weight>(); //带牌，保证同点数有2张
        for (int i = 0; i < cards.Count; i++)
        {
            var card = cards[i];
            var cardValue = card.weight;
            if (tmp.ContainsKey(cardValue) == false)
            {
                var lst = new List<CardAttribute>();
                lst.Add(card);
                tmp.Add(cardValue, lst);
            }
            else
            {
                var lst = tmp[cardValue];
                lst.Add(card);
            }
        }
        Debug.Log($"{tmp.Keys.Count}种点数");

        if (withTwo)
        {
            foreach (var item in tmp)
            {
                if (item.Value.Count == 2)
                {
                    tmpSub.Add(item.Value.First().weight);
                }
                else if (item.Value.Count == 3)
                {
                    tmpMain.Add(item.Value.First().weight);
                }
                else
                {
                    return false; //数量只能是2张或3张
                }
            }
            tmpMain.Sort((x, y) => x.CompareTo(y)); //升序排列
            bool cond1 = (tmpMain.Count == tmpSub.Count); //三同张和对子数量一致
            bool cond2 = true; //三同张连续
            for (int i = 0; i < tmpMain.Count - 1; i++)
            {
                var curr = tmpMain[i];
                var next = tmpMain[i + 1];
                cond2 = cond2 && (curr + 1 == next);
            }
            return cond1 && cond2;
        }
        else
        {
            foreach (var item in tmp)
            {
                if (item.Value.Count == 3)
                {
                    tmpMain.Add(item.Value.First().weight);
                }
                else
                {
                    return false; //数量只能是2张或3张
                }
            }
            tmpMain.Sort((x, y) => x.CompareTo(y)); //升序排列
            //bool cond1 = (tmpMain.Count == tmpSub.Count); //三同张和对子数量一致
            bool cond2 = true; //三同张连续
            for (int i = 0; i < tmpMain.Count - 1; i++)
            {
                var curr = tmpMain[i];
                var next = tmpMain[i + 1];
                cond2 = cond2 && (curr + 1 == next);
            }
            return cond2;
        }
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

            //不能超过A。二只有一张，必须单出。
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
    #endregion

    #region 推荐出牌（客户端）

    //①被动跟牌，限定牌型、数量，出更大的牌。
    //②主动出牌，（通过随机）确定好牌型，不需要比大小。

    //通常可以得到多个结果，都推荐给玩家。

    // 单张
    public static List<CardAttribute> doSingle(List<CardAttribute> handCards)
    {
        return null;
    }

    // 对子
    public static List<CardAttribute> doDouble(List<CardAttribute> handCards)
    {
        return null;
    }

    // 连对
    public static List<CardAttribute> doDoubleMore(List<CardAttribute> handCards)
    {
        return null;
    }

    // 三带二
    public static List<CardAttribute> doThreeWithTwo(List<CardAttribute> handCards)
    {
        return null;
    }

    // 飞机
    public static List<CardAttribute> doAirplane(List<CardAttribute> handCards)
    {
        return null;
    }

    // 顺子
    public static List<CardAttribute> doStraight(List<CardAttribute> handCards)
    {
        return null;
    }

    // 炸弹
    public static List<CardAttribute> doBomb(List<CardAttribute> handCards)
    {
        return null;
    }
    #endregion
}