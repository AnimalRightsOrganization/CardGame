using System.IO;
using System.Diagnostics;
using UnityEditor;
using Debug = UnityEngine.Debug;

public class DebugTools : Editor
{
    [MenuItem("Tools/服务器/运行", false, 0)]
    private static void StartServer()
    {
        string root = new DirectoryInfo(UnityEngine.Application.dataPath).Parent.Parent.FullName;
        string exePath = $"{root}/Server/NetCoreApp/bin/Debug/netcoreapp3.1/NetCoreServer.exe";
        //Debug.Log(exePath);

        Process m_Process = new Process();
        m_Process.StartInfo.FileName = exePath;
        m_Process.Start();
    }
    //[MenuItem("Tools/服务器/编译", false, 0)]
    private static void CompileServer()
    {

    }
    [MenuItem("Tools/服务器/编辑", false, 0)]
    private static void EditServerCode()
    {
        string root = new DirectoryInfo(UnityEngine.Application.dataPath).Parent.Parent.FullName;
        string slnPath = $"{root}/Server/NetCoreApp.sln";

        Process m_Process = new Process();
        m_Process.StartInfo.FileName = @"D:\Program Files (x86)\Microsoft Visual Studio\2019\Enterprise\Common7\IDE\devenv.exe";
        m_Process.StartInfo.Arguments = slnPath;
        m_Process.Start();
    }

    [MenuItem("Tools/协议/生成", false, 0)]
    private static void GenerateProto()
    {
        string root = new DirectoryInfo(UnityEngine.Application.dataPath).Parent.Parent.FullName;

        Process m_Process = new Process();
        m_Process.StartInfo.WorkingDirectory = root;
        m_Process.StartInfo.FileName = "generate_proto.bat";
        m_Process.Start();

        AssetDatabase.Refresh();
    }
    [MenuItem("Tools/协议/编辑", false, 0)]
    private static void EditProto()
    {
        string root = new DirectoryInfo(UnityEngine.Application.dataPath).Parent.Parent.FullName;

        Process m_Process = new Process();
        m_Process.StartInfo.FileName = $"{root}/Proto";
        m_Process.Start();
    }
}