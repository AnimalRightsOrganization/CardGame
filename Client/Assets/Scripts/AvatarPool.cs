using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AvatarPool))]
public class AvatarPoolEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); //显示默认所有参数

        AvatarPool demo = (AvatarPool)target;

        if (GUILayout.Button("Spawn"))
        {
            GamePlayer player = new GamePlayer();
            player.avatar = AvatarModel.Reimu;
            demo.Spawn(player);
        }
        else if (GUILayout.Button("DespawnAll"))
        {
            demo.DespawnAll();
        }
        else if (GUILayout.Button("CountLog"))
        {
            demo.CountLog();
        }
    }
}

public class AvatarPool : MonoBehaviour
{
    public static AvatarPool instance;

    [SerializeField] int preloadCount;
    [SerializeField] List<GameObject> prefabList = new List<GameObject>();
    public Dictionary<string, List<GameObject>> spawnedPool = new Dictionary<string, List<GameObject>>();
    public Dictionary<string, List<GameObject>> recyclePool = new Dictionary<string, List<GameObject>>();

    void Awake()
    {
        instance = this;

        Init();
    }

    void Init()
    {
        for (int i = 0; i < prefabList.Count; i++)
        {
            List<GameObject> spawnedPrefab = new List<GameObject>();
            for (int j = 0; j < preloadCount; j++)
            {
                GameObject go = Instantiate(prefabList[i]);
                go.transform.SetParent(this.transform);
                go.SetActive(false);
                spawnedPrefab.Add(go);
            }
            spawnedPool.Add(prefabList[i].name, spawnedPrefab);
        }
    }

    public PlayerProvider Spawn(GamePlayer player)
    {
        GameObject obj = Spawn(player.avatar);
        return obj.GetComponent<PlayerProvider>();
    }

    GameObject Spawn(AvatarModel model)
    {
        return Spawn(model.ToString());
    }

    GameObject Spawn(string itemName)
    {
        // 足够就取出
        foreach (var item in spawnedPool)
        {
            if(item.Key == itemName)
            {
                List<GameObject> list = item.Value;
                if(list.Count > 0)
                {
                    GameObject obj = list[0];
                    list.Remove(obj);
                    obj.transform.SetParent(null);
                    obj.SetActive(true);
                    obj.name = itemName;

                    // 给回收做准备
                    if(recyclePool.ContainsKey(itemName))
                    {
                        recyclePool[itemName].Add(obj);
                    }
                    else
                    {
                        List<GameObject> recyclelist = new List<GameObject>() { obj };
                        recyclePool.Add(itemName, recyclelist);
                    }

                    return obj;
                }
            }
        }

        // 不够就扩容
        Debug.Log("不足");

        for (int i = 0; i < prefabList.Count; i++)
        {
            if(prefabList[i].name == itemName)
            {
                GameObject obj = Instantiate(prefabList[i]);
                obj.name = itemName;

                // 给回收做准备
                if (recyclePool.ContainsKey(itemName))
                {
                    recyclePool[itemName].Add(obj);
                }
                else
                {
                    List<GameObject> recyclelist = new List<GameObject>() { obj };
                    recyclePool.Add(itemName, recyclelist);
                }

                return obj;
            }
        }

        return null;
    }

    void Despawn(GameObject obj)
    {

    }

    public void DespawnAll()
    {
        foreach(var item in recyclePool)
        {
            List<GameObject> recycleList = item.Value;
            for (int i = 0; i < recycleList.Count; i++)
            {
                GameObject ret = recycleList[i];
                ret.transform.SetParent(this.transform);
                ret.SetActive(false);
                spawnedPool[item.Key].Add(ret);
            }
        }
        recyclePool.Clear();
    }

    public void CountLog()
    {
        // 缓存池
        foreach (var item in spawnedPool)
        {
            Debug.Log("<color=gray>" + item.Key + ":" + item.Value.Count + "</color>");
        }

        // 回收池
        foreach (var item in recyclePool)
        {
            Debug.Log("<color=green>" + item.Key + ":" + item.Value.Count + "</color>");
        }
    }
}
