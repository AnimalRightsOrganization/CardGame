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
        string exePath = $"{root}/Server/NetCoreApp/bin/Debug/netcoreapp3.1/NetCoreServer.exe";
        //Debug.Log(exePath);

        Process m_Process = new Process();
        m_Process.StartInfo.FileName = exePath;
        m_Process.Start();
    }

    // 编译服务器代码
    private static void CompileServer()
    {

    }

    // 编辑服务器代码
    [MenuItem("Tools/编辑服务器代码", false, 0)]
    private static void EditServerCode()
    {
        string root = new DirectoryInfo(UnityEngine.Application.dataPath).Parent.Parent.FullName;
        string slnPath = $"{root}/Server/NetCoreApp.sln";

        Process m_Process = new Process();
        m_Process.StartInfo.FileName = @"D:\Program Files (x86)\Microsoft Visual Studio\2019\Enterprise\Common7\IDE\devenv.exe";
        m_Process.StartInfo.Arguments = slnPath;
        m_Process.Start();
    }

    [MenuItem("Tools/生成协议", false, 0)]
    private static void GenerateProto()
    {
        string root = new DirectoryInfo(UnityEngine.Application.dataPath).Parent.Parent.FullName;

        Process m_Process = new Process();
        m_Process.StartInfo.WorkingDirectory = root;
        m_Process.StartInfo.FileName = "generate_proto.bat";
        m_Process.Start();

        AssetDatabase.Refresh();
    }
}