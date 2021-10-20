using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestCard : MonoBehaviour
{
    public Transform root;
    public Image prefab;

    public CardAttribute CardAttribute;

    void Start()
    {
        Loadcardfrontatlasnamed();
    }

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
        Debug.Log(atlasCardfront.Count);
    }

    [ContextMenu("ChangeCard")]
    void Change()
    {
        string card_name = $"{CardAttribute.colors}_{(int)CardAttribute.weight % 14}";
        Debug.Log(card_name);
        var sp = atlasCardfront[card_name];
        prefab.sprite = sp;
    }

    [ContextMenu("ShowCard")]
    void Show()
    {
        int cardCount = 13;
        Vector2 startPos = Vector2.left * 25 * (int)(cardCount / 2);

        System.Random rd = new System.Random();
        for (int i = 0; i < cardCount; i++)
        {
            var card = Instantiate(prefab, root);
            card.name = $"card_{i}";
            card.rectTransform.anchoredPosition += Vector2.right * 25 * i + startPos;

            int rdColor = rd.Next(0, 4);
            int rdWeight = rd.Next(1, 14);
            //string card_name = $"{CardAttribute.colors}_{(int)CardAttribute.weight}";
            string card_name = $"{(Colors)rdColor}_{rdWeight}";
            Debug.Log(card_name);
            var sp = atlasCardfront[card_name];
            card.sprite = sp;
        }
        prefab.gameObject.SetActive(false);
    }
}