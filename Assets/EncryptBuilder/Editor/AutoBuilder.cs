using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;
#pragma warning disable

/// <summary>
/// 2017-04-10 Created By Spade
/// 2017-12-29 Upgraded By Spade
/// Version:1.1
/// For StarField Information Technology Company
/// </summary>
public  class AutoBuilder : EditorWindow
{
    public static bool isEncryptBuild = false;
    private static AutoBuilder ab;
    static string GetProjectName()
    {
        string[] s = Application.dataPath.Split('/');
        return s[s.Length - 2];
    }

    static string[] GetScenePaths()
    {
        string[] scenes = new string[EditorBuildSettings.scenes.Length];

        for (int i = 0; i < scenes.Length; i++)
        {
            scenes[i] = EditorBuildSettings.scenes[i].path;
        }

        return scenes;
    }

    [MenuItem("Encrypt Builder/Windows/32-bit")]
    static void PerformWinBuild()
    {
        if (Application.platform != RuntimePlatform.WindowsEditor) return;
        isEncryptBuild = true;
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.StandaloneWindows);
        BuildPipeline.BuildPlayer(GetScenePaths(), "Builds/Win/" + GetProjectName() + ".exe", BuildTarget.StandaloneWindows, BuildOptions.None);
    }

    [MenuItem("Encrypt Builder/Windows/32-bit(自动运行)")]
    static void PerformWinBuildAutoRun()
    {
        if (Application.platform != RuntimePlatform.WindowsEditor) return;
        isEncryptBuild = true;
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.StandaloneWindows);
        BuildPipeline.BuildPlayer(GetScenePaths(), "Builds/Win/" + GetProjectName() + ".exe", BuildTarget.StandaloneWindows, BuildOptions.AutoRunPlayer);
    }

    [MenuItem("Encrypt Builder/Windows/32-bit(开发者模式)")]
    static void PerformWinBuildDevelopment()
    {
        if (Application.platform != RuntimePlatform.WindowsEditor) return;
        isEncryptBuild = true;
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.StandaloneWindows);
        BuildPipeline.BuildPlayer(GetScenePaths(), "Builds/Win/" + GetProjectName() + ".exe", BuildTarget.StandaloneWindows, BuildOptions.Development);
    }

    [MenuItem("Encrypt Builder/Windows/64-bit")]
    static void PerformWin64Build()
    {
        if (Application.platform != RuntimePlatform.WindowsEditor) return;
        isEncryptBuild = true;
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.StandaloneWindows);
        BuildPipeline.BuildPlayer(GetScenePaths(), "Builds/Win64/" + GetProjectName() + ".exe", BuildTarget.StandaloneWindows64, BuildOptions.None);
    }

    [MenuItem("Encrypt Builder/Windows/64-bit(自动运行)")]
    static void PerformWin64BuildAutoRun()
    {
        if (Application.platform != RuntimePlatform.WindowsEditor) return;
        isEncryptBuild = true;
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.StandaloneWindows);
        BuildPipeline.BuildPlayer(GetScenePaths(), "Builds/Win64/" + GetProjectName() + ".exe", BuildTarget.StandaloneWindows64, BuildOptions.AutoRunPlayer);
    }

    [MenuItem("Encrypt Builder/Windows/64-bit(开发者模式)")]
    static void PerformWin64BuildDevelopment()
    {
        if (Application.platform != RuntimePlatform.WindowsEditor) return;
        isEncryptBuild = true;
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.StandaloneWindows);
        BuildPipeline.BuildPlayer(GetScenePaths(), "Builds/Win64/" + GetProjectName() + ".exe", BuildTarget.StandaloneWindows64, BuildOptions.Development);
    }

    [MenuItem("Encrypt Builder/打开Builds目录")]
    static void OpenBuildDir()
    {
        if (Application.platform != RuntimePlatform.WindowsEditor) return;
        string path = Application.dataPath;
        path = path.Remove(path.Length - 6) +"Builds/";
        if (Directory.Exists(path) == true)
        {
            System.Diagnostics.Process pro = System.Diagnostics.Process.Start(path);
            return;
        }
        else
        {
            Debug.LogError("请在成功打包后使用该选项。");
        }
    }

    
    [MenuItem("Encrypt Builder/检查Unity3D版本兼容性")]
    static void GetVerison()
    {
        Debug.Log("当前版本:"+Application.unityVersion);
        Debug.Log("编辑器:"+Application.platform);
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            if (float.Parse(GetUnityVersion()) <= 2017.3f)
            {
                if ((float.Parse(GetUnityVersion()) < 5.0 || float.Parse(GetUnityVersion()) > 5.6) ||
                    (float.Parse(GetUnityVersion()) == 4.7))
                {
                    Debug.LogError("仅支持Unity3D版本为5.0~5.6和2017.3的应用加密。");
                    return;
                }
                else
                    Debug.Log("支持该版本进行加密!");
            }
            else
            {
                Debug.Log("支持该版本进行加密!");
            }
        }
        else
            Debug.Log("不支持非Windows平台Editor的Mono加密");
    }

    //AboutDialog
    [MenuItem("Encrypt Builder/关于")]
    static void ShowAboutDialog()
    {
        Rect rct = new Rect(0, 0, 200, 150);
        ab = (AutoBuilder)EditorWindow.GetWindowWithRect(typeof(AutoBuilder), rct, true, "关于 Encrypt Builder");
        ab.Show();
    }
    void OnGUI()
    {
        EditorGUILayout.LabelField("创建:Spade");
        EditorGUILayout.LabelField("升级:Spade");
        EditorGUILayout.LabelField("升级日期:2017-12-29");
        EditorGUILayout.LabelField("详细使用说明:请参考文档");
        EditorGUILayout.LabelField("版本:1.1");

        if (GUILayout.Button("关闭", GUILayout.Width(80)))
        {
            this.Close();
        }
    }

    static string GetUnityVersion()
    {
        string version = Application.unityVersion;
        version = version.Substring(0, 3);
        if (version == "201")
        {
            version = Application.unityVersion;
            version = version.Substring(0, 6);
        }
        return version;
    }
}