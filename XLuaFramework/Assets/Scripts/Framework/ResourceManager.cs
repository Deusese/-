using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Scripts.Framework
{
    public class ResourceManager:MonoBehaviour
    {
        internal class BundleInfo
        {
            public string AssetsName;
            public string BundleName;

            /// <summary>
            /// 依赖列表
            /// </summary>
            public List<string> Dependences = new();
        }

        private Dictionary<string, BundleInfo> m_BundleInfos = new();

        /// <summary>
        /// 解析版本文件
        /// </summary>
        private void ParseVersionFile()
        {
            //版本文件的路径
            string url = Path.Combine(PathUtil.BundleResourcePath, AppConst.FileListName);
            string[] data = File.ReadAllLines(url);

            for (int i = 0; i < data.Length; i++)
            {
                BundleInfo bundleInfo=new BundleInfo();
                var info = data[i].Split('|');
                bundleInfo.AssetsName = info[0];
                bundleInfo.BundleName = info[1];
                for (int j = 2; j < info.Length; j++)
                {
                    bundleInfo.Dependences.Add(info[j]);
                }
                m_BundleInfos.Add(bundleInfo.AssetsName,bundleInfo);
            }
        }

        IEnumerator LoadBundleAsync(string assetName, Action<Object> onComplete=null)
        {
            var bundleName = m_BundleInfos[assetName].BundleName;
            var bundlePath= Path.Combine(PathUtil.BundleResourcePath, bundleName);
            var dependences = m_BundleInfos[assetName].Dependences;
            if (dependences.Count>0)
            {
                foreach (var t in dependences)
                {
                    yield return LoadBundleAsync(t);
                }
            }
            AssetBundleCreateRequest request = AssetBundle.LoadFromFileAsync(bundlePath);
            yield return request;
            AssetBundleRequest bundleRequest = request.assetBundle.LoadAssetAsync(assetName);
            yield return bundleRequest;
            onComplete?.Invoke(bundleRequest?.asset);
        }

        public void LoadAsset(string assetName,Action<Object> onComplete)
        {
            StartCoroutine(LoadBundleAsync(assetName, onComplete));
        }

        void Start()
        {
            ParseVersionFile();
            LoadAsset("Assets/BuildResources/Prefab/Monster.prefab", OnComplete);
        }

        private void OnComplete(Object obj)
        {
            GameObject go=Instantiate(obj) as GameObject;
            go.transform.SetParent(this.transform);
            go.SetActive(true);
            go.transform.localPosition=Vector3.zero;
        }
    }
}
