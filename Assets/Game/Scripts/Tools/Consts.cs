using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Consts
{
    /// <summary>
    /// 游戏数据路径
    /// </summary>
    //跨平台都存在，可以读写的路径
    public static readonly string DataPath = Application.persistentDataPath + @"\data.xml";
}

/// <summary>
/// 面板类型
/// </summary>
public enum PanelType
{

}
