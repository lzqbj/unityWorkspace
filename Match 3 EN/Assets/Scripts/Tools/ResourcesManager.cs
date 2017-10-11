using System;
using System.Collections.Generic;
using UnityEngine;
using Core;


namespace Core
{
    public class ResourcesManager
    {
        //资源包
        public Dictionary<string, AssetBundle> ResourcesDic;
        //图片包
        public Dictionary<string, Texture> ImageDic;


        private static ResourcesManager resourcesManager;
        public static ResourcesManager Instance
        {
            get
            {
                if (resourcesManager == null)
                    resourcesManager = new ResourcesManager();
                return resourcesManager;
            }
        }

        public ResourcesManager()
        {
            ResourcesDic = new Dictionary<string, AssetBundle>();
            ImageDic = new Dictionary<string, Texture>();
        }

        //获得资源，包名+资源名
        public UnityEngine.Object GetResources(string package, string name)
        {
            UnityEngine.Object obj;
            if (ResourcesDic.ContainsKey(package))
            {
                AssetBundle assetBundle = ResourcesDic[package];
                if (assetBundle == null) return null;
                obj = assetBundle.LoadAsset(name, typeof(GameObject));
            }
            else
            {
                obj = Resources.Load(name);
            }
            return obj;
        }

        public UnityEngine.Object GetResources(string name)
        {
            UnityEngine.Object obj = Resources.Load(name);
            return obj;
        }
        public T GetResources<T>(string name) where T : UnityEngine.Object
        {
            T obj = default(T);
            obj = Resources.Load<T>(name);
            return obj;
        }
    }
}
