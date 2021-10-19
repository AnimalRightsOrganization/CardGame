using System.IO;
using System.Text;
using System.Xml.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//静态类，UI图片动态加载的全局控制。
public static class Tools
{
    private static Transform uiParent;
    /// <summary>
    /// UI的父物体
    /// </summary>
    public static Transform UIParent //get;set;定义属性
    {
        get
        {
            if (uiParent == null)
            {
                uiParent = GameObject.Find("GameRoot").transform;
            }
            return uiParent;
        }
    }

    //用XML保存会产生一个字节标记，读取时处理
    public static void SaveData(GameData data)
    {
        string fileName = Consts.DataPath;
        //FileStream写入流（文件名，可能原来没有或者原来txt上继续写，save时写入）
        Stream stream = new FileStream(fileName,FileMode.OpenOrCreate, FileAccess.Write);
        StreamWriter sw = new StreamWriter(stream, Encoding.UTF8);

        XmlSerializer xmlSerializer = new XmlSerializer(data.GetType());
        xmlSerializer.Serialize(sw,data);
        sw.Close();
        stream.Close();
    }

    public static GameData getData()
    {
        GameData data = new GameData();

        //（路径，FileMode，FileAccess）
        Stream stream = new FileStream(Consts.DataPath, FileMode.Open, FileAccess.Read);
        //忽略xml标记设为true
        StreamReader sr = new StreamReader(stream, true);

        XmlSerializer xmlSerializer = new XmlSerializer(data.GetType());
        data = xmlSerializer.Deserialize(sr) as GameData; //赋值，反序列化

        stream.Close();
        sr.Close();

        return data;
    }
}