using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathUtil
{
    /// <summary>
    /// ��Ŀ¼
    /// </summary>
    public static readonly string AssetsPath = Application.dataPath;

    /// <summary>
    /// ��Ҫ��Bundle��Ŀ¼
    /// </summary>
    public static readonly string BuildResourcesPath = AssetsPath + "/BuildResources/";

    /// <summary>
    /// Bundle ���Ŀ¼
    /// </summary>
    public static readonly string BundleOutPath = Application.streamingAssetsPath;

    public static string BundleResourcePath
    {
        get { return Application.streamingAssetsPath; }
    }

    /// <summary>
    /// ��ȡunity �����·��
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string GetUnityPath(string path)
    {
        if (string.IsNullOrEmpty(path)) return string.Empty;
        return path.Substring(path.IndexOf("Assets"));
    }

    public static string GetStandardPath(string path)
    {
        if (string.IsNullOrEmpty(path)) return string.Empty;
        return path.Trim().Replace("\\", "/");
    }
}
