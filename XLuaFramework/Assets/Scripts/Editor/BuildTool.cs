using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Assets.Scripts.Framework;
using UnityEditor;
using UnityEngine;

public class BuildTool : Editor
{
    [MenuItem("Tools/Build Windows Bundle")]
    static void BundleWindowsBuild()
    {
        Build(BuildTarget.StandaloneWindows);
    }

    [MenuItem("Tools/Build Android Bundle")]
    static void BundleAndroidBuild()
    {
        Build(BuildTarget.Android);
    }

    [MenuItem("Tools/Build ios Bundle")]
    static void BundleIosBuild()
    {
        Build(BuildTarget.iOS);
    }
    private static void Build(BuildTarget target)
    {
        List<AssetBundleBuild> assetBundleBuilds = new();
        var bundleInfos = new List<string>();
        string[] files = Directory.GetFiles(PathUtil.BuildResourcesPath, "*", SearchOption.AllDirectories);

        for (int i = 0; i < files.Length; i++)
        {
            if (files[i].EndsWith(".meta")) continue;
            
            AssetBundleBuild assetBundle = new AssetBundleBuild();
            string fileName = PathUtil.GetStandardPath(files[i]);
            string assetName = PathUtil.GetUnityPath(fileName);
            assetBundle.assetNames = new string[] { assetName };
            string bundleName = fileName.Replace(PathUtil.BuildResourcesPath, "").ToLower();
            assetBundle.assetBundleName= bundleName+ ".ab";
            assetBundleBuilds.Add(assetBundle);

            var dependenceInfos = GetDependence(assetName);
            var bundleInfo = assetName + "|" + bundleName+".ab";
            if (dependenceInfos.Count>0)
            {
                bundleInfo += "|" + string.Join("|", dependenceInfos);
            }
            bundleInfos.Add(bundleInfo);
        }
        if (Directory.Exists(PathUtil.BundleOutPath))
        {
            Directory.Delete(PathUtil.BundleOutPath,true);
        }
        Directory.CreateDirectory(PathUtil.BundleOutPath);
        BuildPipeline.BuildAssetBundles(PathUtil.BundleOutPath, assetBundleBuilds.ToArray(),
            BuildAssetBundleOptions.None, target);
        File.WriteAllLines(PathUtil.BundleOutPath+"/"+AppConst.FileListName,bundleInfos);
        AssetDatabase.Refresh();
    }
    /// <summary>
    /// ªÒ»°“¿¿µœÓ
    /// </summary>
    /// <param name="curFile"></param>
    /// <returns></returns>
    private static List<string> GetDependence(string curFile)
    {
        var dependence = new List<string>();
        string[] files = AssetDatabase.GetDependencies(curFile);
        dependence = files.Where(file => !file.EndsWith(".cs") && !file.Equals(curFile)).ToList();
        return dependence;
    }
}
