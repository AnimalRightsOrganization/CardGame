using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BotDatabase : ScriptableObject
{
    public string title = "Bots";
    public List<DBPlayer> botList;

    void OnEnable()
    {
        if (botList == null)
            botList = new List<DBPlayer>();
    }

    public void Add(DBPlayer bot)
    {
        botList.Add(bot);
    }

    public void Remove(DBPlayer bot)
    {
        botList.Remove(bot);
    }

    public void RemoveAt(int index)
    {
        botList.RemoveAt(index);
    }
}

public class CreateScriptableObject : Editor
{
    public static void CreateAsset<Type>() where Type : ScriptableObject
    {
        Type item = ScriptableObject.CreateInstance<Type>();

        string path = AssetDatabase.GenerateUniqueAssetPath("Assets/Resources/" + typeof(Type) + ".asset");

        AssetDatabase.CreateAsset(item, path);
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();
        Selection.activeObject = item;
    }

    [MenuItem("Assets/Create/CreateAvatarInfo")]
    public static void CreateAvatarInfo()
    {
        CreateAsset<BotDatabase>();
    }
}