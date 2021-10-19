using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestCard : MonoBehaviour
{
    public Image card;

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
            Debug.Log($"---{asprite.name}");
        }
        Debug.Log(atlasCardfront.Count);
    }

    [ContextMenu("ChangeCard")]
    void Change()
    {
        string card_name = $"{CardAttribute.colors}_{(int)CardAttribute.weight}";
        Debug.Log(card_name);
        var sp = atlasCardfront[card_name];
        card.sprite = sp;
    }
}