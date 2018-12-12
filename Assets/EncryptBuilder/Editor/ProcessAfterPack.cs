using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEditor.Callbacks;
using System.IO;
using System;

/// <summary>
/// 2017-04-10 Created By Spade
/// 2017-08-25 Upgraded By Spade
/// For StarField Information Technology Company
/// </summary>
public class ProcessAfterPack : MonoBehaviour
{
    [PostProcessBuildAttribute(1)]
    public static void OtherOperations(BuildTarget target, string pathToBuiltProject)
    {
        if (!AutoBuilder.isEncryptBuild) return;
        //检测Unity版本 低于5.0或高于5.6则跳出
        Debug.Log("提示:当前Unity3D版本：" + GetUnityVersion());
        AutoBuilder.isEncryptBuild = false;
        if(float.Parse(GetUnityVersion()) <= 2017.3f)
        {
            if ((float.Parse(GetUnityVersion()) < 5.0 || float.Parse(GetUnityVersion()) > 5.6) ||
                (float.Parse(GetUnityVersion()) == 4.7))
            {
                Debug.LogError("仅支持Unity3D版本为5.0~5.6和2017.3的应用加密。");
                return;
            }
        }
        //检测目标平台是否为Windows
        if (target == BuildTarget.StandaloneWindows || target == BuildTarget.StandaloneWindows64)
        {
            //目标路径
            string cpyPath = pathToBuiltProject;
            cpyPath = cpyPath.Replace("/" + GetProjectName() + ".exe", "");
            string encryptDLLPath = cpyPath + "/" + GetProjectName() + "_Data/Managed/Assembly-CSharp.dll";
            cpyPath = cpyPath + "/" + GetProjectName() + "_Data/Mono/mono.dll";
            string bp = Application.dataPath;
            bp = bp.Replace("Assets", "");
            if (float.Parse(GetUnityVersion()) != 5.5f  && float.Parse(GetUnityVersion()) != 5.6f && float.Parse(GetUnityVersion()) < 2017)
            {
                cpyPath = bp + cpyPath;
                encryptDLLPath = bp + encryptDLLPath;
            }
            else if( float.Parse(GetUnityVersion()) >= 2017)
            {
                cpyPath = cpyPath.Replace("_Data/Mono/mono.dll", "_Data/Mono/EmbedRuntime/mono.dll");
            }

            string cpyFilePath = Application.dataPath + "/EncryptBuilder/Editor/x86/mono.dll";
            encryptDLLPath = encryptDLLPath.Replace("/", "//");

            if (target == BuildTarget.StandaloneWindows)
            {
                cpyFilePath = Application.dataPath + "/EncryptBuilder/Editor/Mono/" + GetUnityVersion() + "/x86/mono.dll";
            }
            else if (target == BuildTarget.StandaloneWindows64)
            {
                cpyFilePath = Application.dataPath + "/EncryptBuilder/Editor/Mono/" + GetUnityVersion() + "/x64/mono.dll";
            }
            try
            {
                FileUtil.DeleteFileOrDirectory(cpyPath);
                FileUtil.CopyFileOrDirectory(cpyFilePath, cpyPath);
                Debug.Log("打包输出成功!");
                Debug.Log("执行加密中...请稍候...");
                string executePath = Application.dataPath + "/EncryptBuilder/Editor/Mono/Execute/Encryption.exe";
                if (ExecuteEncryption(executePath, encryptDLLPath))
                {
                    Debug.Log("成功执行!");
                }
                else
                {
                    Debug.LogError("执行失败!");
                }
            }
            catch (Exception error)
            {
                Debug.LogError(error);
            }
        }
        else
        {
            Debug.LogError("结果代码:-99");
            return;
        }
    }


    [PostProcessBuildAttribute(3)]
    public static void EncryptOperations(BuildTarget target, string pathToBuiltProject)
    {
        
        string dstPath = pathToBuiltProject;
        string prfixPath = pathToBuiltProject.Substring(0, pathToBuiltProject.LastIndexOf('/') + 1);
        string filename = pathToBuiltProject.Substring(pathToBuiltProject.LastIndexOf('/') + 1);
        string filenameNoExstension = filename.Substring(0, filename.IndexOf('.'));

        //string fullpath = prfixPath + filenameNoExstension + "_Data/Plugins/SfEnIden.dll";
        string fullpath = prfixPath + filenameNoExstension + "_Data/Plugins";
        
        string cpyFilePath;
        cpyFilePath = "";
        if (target == BuildTarget.StandaloneWindows)
        {
            cpyFilePath = Application.dataPath + "//EncryptBuilder//Plugins//x86//SfEnIden.dll";
        }
        else if (target == BuildTarget.StandaloneWindows64)
        {
            cpyFilePath = Application.dataPath + "//EncryptBuilder//Plugins//x64//SfEnIden.dll";
        }
        Debug.Log(cpyFilePath);
        Debug.Log(prfixPath);

        FileUtil.CopyFileOrDirectory(cpyFilePath, prfixPath+ "SfEnIden.dll");

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="executePath">加密DLL程序路径</param>
    /// <param name="dllPath">目标DLL文件路径</param>
    /// <returns></returns>
    static bool ExecuteEncryption(string executePath, string dllPath)
    {
        System.Diagnostics.Process pro = System.Diagnostics.Process.Start(executePath, dllPath);
        pro.WaitForExit(500);
        if (pro != null)
        {
            if (pro.ExitCode == 0)
            {
                Debug.Log("结果代码:0");
                pro.Close();
                return true;
            }
            else
            {
                if (pro.ExitCode == -1)
                {
                    Debug.LogError("结果代码:-1");
                }
                else if (pro.ExitCode == -2)
                {
                    Debug.LogError("结果代码:-2");
                }
                else if (pro.ExitCode == -3)
                {
                    Debug.LogError("结果代码:-3");
                }
                else
                {
                    Debug.LogError("结果代码:未定义");
                }
                pro.Close();
                return false;
            }
        }
        return false;
    }

    static string GetProjectName()
    {
        string[] s = Application.dataPath.Split('/');
        return s[s.Length - 2];
    }

    static string GetUnityVersion()
    {
        string version = Application.unityVersion;
        version = version.Substring(0, 3);
        if(version == "201")
        {
            version = Application.unityVersion;
            version = version.Substring(0, 6);
        }
        return version;
    }
}