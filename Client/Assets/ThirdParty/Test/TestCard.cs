using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEditor;
[CustomEditor(typeof(TestCard))]
public class TestCardEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); //显示默认所有参数

        TestCard demo = (TestCard)target;

        if (GUILayout.Button("改变单张", GUILayout.Height(25)))
        {
            demo.ChangeSingle();
        }
        if (GUILayout.Button("手牌排序", GUILayout.Height(25)))
        {
            demo.SortHandCard();
        }
        if (GUILayout.Button("显示手牌", GUILayout.Height(25)))
        {
            demo.ShowHandCard();
        }
        if (GUILayout.Button("清理手牌", GUILayout.Height(25)))
        {
            demo.DestroyCards();
        }
        if (GUILayout.Button("牌型检测", GUILayout.Height(25)))
        {
            demo.CheckType();
        }
    }
}

public class TestCard : MonoBehaviour
{
    public Transform root;
    public Image prefab;

    public List<CardAttribute> handCards = new List<CardAttribute>();

    static Dictionary<string, Sprite> atlasCardfront = new Dictionary<string, Sprite>();
    static void Loadcardfrontatlasnamed()
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>($"Sprites/cards");
        atlasCardfront.Clear();
        foreach (Sprite asprite in sprites)
        {
            atlasCardfront[asprite.name] = asprite;
            //Debug.Log($"---{asprite.name}");
        }
        //Debug.Log(atlasCardfront.Count); //56
    }

    void Start()
    {
        Loadcardfrontatlasnamed();
    }

    // 改变单张
    public void ChangeSingle()
    {
        var card = handCards[0];
        string card_name = $"{card.colors}_{(int)card.weight % 14}";
        var sp = atlasCardfront[card_name];
        prefab.sprite = sp;
    }

    // 手牌排序
    public void SortHandCard()
    {
        //System.Comparison<CardAttribute> comp1 = (x, y) => x.weight.CompareTo(y.weight);
        //System.Comparison<CardAttribute> comp2 = (x, y) => x.colors.CompareTo(y.colors);
        //handCards.Sort(comp1); //升序
        handCards.Sort(SortComp); //升序
    }
    int SortComp(CardAttribute x, CardAttribute y)
    {
        if (x.weight.CompareTo(y.weight) != 0)
            return x.weight.CompareTo(y.weight);
        else if (x.colors.CompareTo(y.colors) != 0)
            return x.colors.CompareTo(y.colors);
        return 1;
    }

    // 随机生成手牌
    public void RandomHandCard()
    {
        int cardCount = 13;
        handCards = new List<CardAttribute>(cardCount);

        System.Random rd = new System.Random();
        for (int i = 0; i < cardCount; i++)
        {
            int rdColor = rd.Next(0, 4);
            int rdWeight = rd.Next(1, 14);
            CardAttribute card = new CardAttribute
            {
                colors = (Colors)rdColor,
                weight = (Weight)rdWeight,
            }; 
            handCards.Add(card);
        }
    }

    // 显示手牌
    public void ShowHandCard()
    {
        DestroyCards();

        int cardCount = handCards.Count;
        Vector2 startPos = Vector2.left * 25 * (int)(cardCount / 2);

        for (int i = 0; i < cardCount; i++)
        {
            var card = handCards[i];

            Image cardObj = Instantiate(prefab, this.transform);
            cardObj.name = $"card_{i}";
            cardObj.rectTransform.anchoredPosition += Vector2.right * 25 * i + startPos;
            cardObj.gameObject.SetActive(true);

            string card_name = $"{card.colors}_{(int)card.weight % 14}";
            var sp = atlasCardfront[card_name];
            cardObj.sprite = sp;
        }
        prefab.gameObject.SetActive(false);
    }
    // 清理手牌物体
    public void DestroyCards()
    {
        List<GameObject> tmp = new List<GameObject>();
        for (int i = 0; i < transform.childCount; i++)
        {
            var obj = transform.GetChild(i);
            tmp.Add(obj.gameObject);
        }
        //Debug.Log($"tmp Count: {tmp.Count}");

        transform.DetachChildren();

        for (int i = tmp.Count - 1; i >= 0; i--)
        {
            //Debug.Log($"Destroy---{i}");
            var obj = tmp[i];
            DestroyImmediate(obj);
        }
    }

    // 牌型检测
    public void CheckType()
    {
        //bool res1 = Rulers.isSingle(handCards);
        //Debug.Log($"单张：{res1}");

        //bool res2 = Rulers.isDouble(handCards);
        //Debug.Log($"对子：{res2}");

        //bool res3 = Rulers.isDoubleMore(handCards);
        //Debug.Log($"连对：{res3}");

        //bool res4 = Rulers.isThreeWithTwo(handCards);
        //Debug.Log($"三带二：{res4}");

        bool res5 = Rulers.isAirplane(handCards);
        Debug.Log($"飞机：{res5}");

        //bool res6 = Rulers.isStraight(handCards);
        //Debug.Log($"顺子：{res6}");

        //bool res7 = Rulers.isBomb(handCards);
        //Debug.Log($"炸弹：{res7}");
    }
}