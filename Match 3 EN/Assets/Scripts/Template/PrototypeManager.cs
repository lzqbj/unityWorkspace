using System;
using System.Collections.Generic;
using System.Xml;
using System.Text;
using System.IO;
using UnityEngine;

namespace Core.Template
{
    public class PrototypeManager : ILoad
    {
        /// <summary>
        /// 用来保存要读取文件的路径.
        /// </summary>
        List<string> pathArray = new List<string>();
        /// <summary>
        /// 文件读取的状态,默认状态是初始化.
        /// </summary>
        private EnumObjectState state = EnumObjectState.Initial;
        /// <summary>
        /// 单例设计模式.
        /// </summary>
        private static PrototypeManager instance = null;
		public Dictionary<int, PrototypeData> dicData = new Dictionary<int, PrototypeData>();
        private Dictionary<string, List<PrototypeData>> mAllPrototyByType = new Dictionary<string, List<PrototypeData>>();
        /// <summary>
        /// 进入场景时判读该场景配置是否已经加载过.
        /// </summary>
        public Dictionary<Type, bool> XmlLoadState = new Dictionary<Type, bool>();

        private void LoadXMLDocument(string fileName, string xml)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlReaderSettings setting = new XmlReaderSettings();
            setting.IgnoreComments = true;
            setting.IgnoreWhitespace = true;
            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(xml));
            XmlReader reader = XmlReader.Create(stream, setting);
            xmlDoc.Load(reader);
            XmlNode node = xmlDoc.FirstChild;
            do
            {
                node = node.NextSibling;
            }
            while (node.NodeType == XmlNodeType.Comment);

            string type = node.Attributes["type"].Value;
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            Type refType = assembly.GetType("Core.Template." + type);

            if (refType != null)
            {
                XmlNodeList nodeList = node.ChildNodes;
                List<PrototypeData> dataList = null;

                for (int i = 0; i < nodeList.Count; i++)
                {
                    XmlNode childNode = nodeList[i];

                    if (childNode.NodeType == XmlNodeType.Comment)
                    {
                        continue;
                    }

                    PrototypeData prototypeData = System.Activator.CreateInstance(refType) as PrototypeData;
                    prototypeData.LoadData(childNode);
                    if (dicData.ContainsKey(prototypeData.mPrototypeID) == false)
                    {
                        dicData.Add(prototypeData.mPrototypeID, prototypeData);
                        if (!mAllPrototyByType.TryGetValue(type, out dataList))
                        {
                            dataList = new List<PrototypeData>();
                            mAllPrototyByType.Add(type, dataList);
                        }
                        dataList.Add(prototypeData);
                    }
                    else
                    {
                        GameDebug.LogError(fileName + "表有重复ID=" + prototypeData.mPrototypeID);
                    }
                }
            }
        }

        public static PrototypeManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PrototypeManager();
                }
                return instance;
            }
        }

        /// <summary>
        /// 解析表中的数据.
        /// </summary>
        public void StartLoadData(string filePath)
        {
            //string source = FileTools.OpenText(filePath);
//            GameDebug.Log("---------NextFile.Name=" + PathDefinition.PrototypeFilePath + filePath);
            GameDebug.Log("解析表=" + filePath);
            string source = ResourcesManager.Instance.GetResources<TextAsset>("PrototypeFiles/" + filePath).text;
            try
            {
                LoadXMLDocument(filePath, source);
            }
            catch (Exception e)
            {
                GameDebug.LogError("xml Error fileName2=" + filePath + "  " + e.Message);
            }

        }

        /// <summary>
        /// 开启线程来加在XML文件.
        /// </summary>
        public void LoadXml()
        {
            StartLoadXml();
        }

        public void Reload()
        {
            mAllPrototyByType.Clear();
            mAllPrototyByType = new Dictionary<string, List<PrototypeData>>();
            dicData.Clear();
            dicData = new Dictionary<int, PrototypeData>();
            pathArray.Clear();
            LoadXml();
        }

        /// <summary>
        /// 最好是遍历文件中所有的表,而不应该根据表的名称.
        /// </summary>
        private void StartLoadXml()
        {
            pathArray.Add("PropPrototype");
            pathArray.Add("SignInPrototype");
            pathArray.Add("ShopPrototype");
            pathArray.Add("GoldPrototype");

            for (int i = 0; i < pathArray.Count; i++)
            {
                string filePath = pathArray[i];
                StartLoadData(filePath);
            }

        }



        #region ILoad implementation
        public void Load()
        {
            State = EnumObjectState.Ready;
        }

        public void Release()
        {
            dicData.Clear();
            mAllPrototyByType.Clear();
        }

        public EnumObjectState State
        {
            get
            {
                return this.state;
            }
            private set
            {
                state = value;
            }
        }
        #endregion

        public T GetPrototype<T>(int prototypeID) where T : PrototypeData
        {
            try
            {
                PrototypeData prototypeData = null;
                if (dicData.TryGetValue(prototypeID, out prototypeData))
                {
                    if (prototypeData == null)
                    {
                        return null;
                    }
                    return prototypeData as T;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// 返回当前小类型的全部对象.
        /// </summary>
        /// <returns>The data by kind I.</returns>
        /// <param name="kindID">Kind I.</param>
        public List<PrototypeData> GetDataByKindID(int kindID)
        {
            if (dicData == null || dicData.Count <= 0) return null;
            List<PrototypeData> lst = new List<PrototypeData>();
            foreach (KeyValuePair<int, PrototypeData> element in dicData)
            {
                if (PrototypeData.GetKindID(element.Key) == kindID)
                    lst.Add(element.Value);
            }

            return lst;
        }

        /// <summary>
        /// 返回当前大类型下的全部对象.
        /// </summary>
        /// <returns>The data by main I.</returns>
        /// <param name="mainID">Main I.</param>
        public List<PrototypeData> GetDataByMainID(int mainID)
        {
            if (dicData == null || dicData.Count <= 0) return null;
            List<PrototypeData> lst = new List<PrototypeData>();
            foreach (KeyValuePair<int, PrototypeData> element in dicData)
            {
                if (PrototypeData.GetMainID(element.Key) == mainID)
                    lst.Add(element.Value);
            }
            return lst;
        }

        public List<PrototypeData> GetDataListByType(string type)
        {
            try
            {
                if (mAllPrototyByType == null || mAllPrototyByType.Count <= 0)
                {
                    return null;
                }
                List<PrototypeData> lst = null;
                if (mAllPrototyByType.TryGetValue(type, out lst))
                {
                    return lst;
                }
                return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
