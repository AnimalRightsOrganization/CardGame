using System.IO;
using System.Diagnostics;
using UnityEditor;
using Debug = UnityEngine.Debug;

public class DebugTools : Editor
{
    [MenuItem("Tools/运行服务器", false, 0)]
    private static void StartServer()
    {
        string root = new DirectoryInfo(UnityEngine.Application.dataPath).Parent.Parent.FullName;
        //Debug.Log(root);
        string exePath = $"{root}/Server/NetCoreApp/bin/Debug/netcoreapp3.1/NetCoreServer.exe";
        //Debug.Log(exePath);

        Process m_Process = new Process();
        m_Process.StartInfo.FileName = exePath;
        m_Process.Start();
    }
}