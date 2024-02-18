using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.Framework
{
    public class HotUpdate:MonoBehaviour
    {
        internal class DownFileInfo
        {
            public string url;
            public string fileName;
            public DownloadHandler fileData;
        }

        /// <summary>
        /// 下载单个文件
        /// </summary>
        /// <param name="info"></param>
        /// <param name="complete"></param>
        /// <returns></returns>
        IEnumerator DownLoadFile(DownFileInfo info, Action<DownFileInfo> complete)
        {
            UnityWebRequest webRequest=UnityWebRequest.Get(info.url);
            yield return webRequest.SendWebRequest();
            if (webRequest.result==UnityWebRequest.Result.ProtocolError||webRequest.result==UnityWebRequest.Result.ConnectionError)
            {
              Debug.LogError($"下载文件出错{info.url}");
              yield break;
            }
            info.fileData = webRequest.downloadHandler;
            complete?.Invoke(info);
        }
        /// <summary>
        /// 下载多个文件
        /// </summary>
        /// <param name="infos"></param>
        /// <param name="complete"></param>
        /// <param name="downLoadAllComplete"></param>
        /// <returns></returns>
        IEnumerator DownLoadFile(List<DownFileInfo> infos, Action<DownFileInfo> complete, Action downLoadAllComplete)
        {
            foreach (var info in infos)
            {
                yield return DownLoadFile(info, complete);
            }
            downLoadAllComplete?.Invoke();
        }
    }
}
