using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(LogicManager))]
public class LogicManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); //显示默认所有参数

        LogicManager demo = (LogicManager)target;

        if (GUILayout.Button("开局", GUILayout.Height(25)))
        {
            demo.StartGame();
        }
        else if (GUILayout.Button("洗牌", GUILayout.Height(25)))
        {
            demo.Shuffle();
        }
        else if (GUILayout.Button("发牌", GUILayout.Height(25)))
        {
            demo.Deal();
        }
        else if (GUILayout.Button("出牌", GUILayout.Height(25)))
        {
            demo.Play();
        }
    }
}