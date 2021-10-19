using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(LogicManager))]
public class LogicManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); //显示默认所有参数

        LogicManager demo = (LogicManager)target;

        if (GUILayout.Button("落座", GUILayout.Height(25)))
        {
            demo.CreateRoom();
        }
        if (GUILayout.Button("洗牌", GUILayout.Height(25)))
        {
            demo.Shuffle();
        }
        if (GUILayout.Button("发牌", GUILayout.Height(25)))
        {
            demo.Roll();
            demo.Deal();
        }
        if (GUILayout.Button("手牌排序", GUILayout.Height(25)))
        {
            demo.Sort();
        }
    }
}