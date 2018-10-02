using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Translate : MonoBehaviour
{
    BotDatabase botDatabase;
    [SerializeField] List<Toggle> toggles;
    [SerializeField] List<Text> textList;

    void Awake()
    {
        botDatabase = Resources.Load<BotDatabase>("ScriptableObject/BotDatabase");

        for (int i = 0; i < toggles.Count; i++)
        {
            int id = i;
            toggles[i].onValueChanged.AddListener((bool value) => OnToggleClick(id, value));
        }
    }

    void Start()
    {
        OnToggleClick(0, true);
    }

    void OnToggleClick(int id, bool value)
    {
        if (value)
        {
            //Debug.Log(id.ToString() + ":" + value);
            switch (id)
            {
                case 0:
                    for (int i = 0; i < textList.Count; i++)
                    {
                        textList[i].text = botDatabase.botList[i].user_id;
                    }
                    break;
                case 1:
                    for (int i = 0; i < textList.Count; i++)
                    {
                        textList[i].text = botDatabase.botList[i].user_id;
                    }
                    break;
                case 2:
                    for (int i = 0; i < textList.Count; i++)
                    {
                        textList[i].text = botDatabase.botList[i].user_id;
                    }
                    break;
            }
        }
    }
}
