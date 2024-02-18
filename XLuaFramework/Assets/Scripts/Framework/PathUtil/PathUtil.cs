using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Framework;
using Unity.VisualScripting;
using UnityEngine;

public class PathUtil
{
    /// <summary>
    /// 根目录
    /// </summary>
    public static readonly string AssetsPath = Application.dataPath;

    /// <summary>
    /// 需要打Bundle的目录
    /// </summary>
    public static readonly string BuildResourcesPath = AssetsPath + "/BuildResources/";

    /// <summary>
    /// Bundle 输出目录
    /// </summary>
    public static readonly string BundleOutPath = Application.streamingAssetsPath;

    /// <summary>
    /// 只读目录
    /// </summary>
    public static readonly string ReadPath=Application.streamingAssetsPath;

    /// <summary>
    /// 可读写目录
    /// </summary>
    public static readonly string ReadWritePath = Application.persistentDataPath;
    public static string BundleResourcePath
    {
        get
        {
            if (AppConst.GameMode == GameMode.UpdateMode)
            {
                return ReadWritePath;
            }

            return ReadPath;
        }
    }

    /// <summary>
    /// 获取unity 的相对路径
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

    public static string GetLuaPath(string name)
    {
        return string.Format("Assets/BuildResources/LuaScripts/{0}.bytes",name);
    }

    public static string GetUIPath(string name)
    {
        return string.Format("Assets/BuildResources/UI/Prefab/{0}.prefab",name);
    }
    public static string GetMusicPath(string name)
    {
        return string.Format("Assets/BuildResources/Audio/Music/{0}", name);
    }
    public static string GetSoundPath(string name)
    {
        return string.Format("Assets/BuildResources/Audio/Sound/{0}", name);
    }
    public static string GetEffectPath(string name)
    {
        return string.Format("Assets/BuildResources/Effect/Prefab/{0}.prefab", name);
    }

    public static string GetSpritePath(string name)
    {
        return string.Format("Assets/BuildResources/Sprites/{0}.prefab", name);
    }

    public static string GetScenePath(string name)
    {
        return string.Format("Assets/BuildResources/Scene/{0}.unity", name);
    }
}
