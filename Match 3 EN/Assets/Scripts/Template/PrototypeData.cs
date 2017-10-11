using System;
using System.Collections.Generic;
using System.Xml;
using Core;

namespace Core.Template
{
    /// <summary>
    /// 模板数据基类.
    /// </summary>
    public class PrototypeData
    {
        public const int MAIN_PLACEHOLDER = 1000000;
        public const int KIND_PLACEHOLDER = 1000;

        protected int kindID = 0;
        public int KindID { get { return kindID; } }
        protected int mainID = 0;
        public int MainID { get { return mainID; } }

        public int mPrototypeID;
        public string mName;
        public string mParams;
        public string ItemDescript;
        protected virtual void OnLoadData(XmlNode data)
        {
        }
        public virtual void OnLoadData(string path)
        {
        }
        public virtual void OnReleaseData()
        {
        }
        /// <summary>
        /// 快速读取的实现虚方法.
        /// </summary>
        /// <param name="reader"></param>
        protected virtual void OnLoadData(XmlReader reader)
        {
        }

        /// <summary>
        /// 重载支持XmlReader快速读取.
        /// </summary>
        /// <param name="reader"></param>
        public void LoadData(XmlReader reader)
        {
            string pid = reader.GetAttribute("Static_ID");
            if (string.IsNullOrEmpty(pid) == false)
            {
                mPrototypeID = int.Parse(pid);
            }
            //string name = reader.GetAttribute("name");
            //if (string.IsNullOrEmpty(name) == false)
            //{
            //    mName = name;
            //}
            //string param = reader.GetAttribute("params");
            //if (string.IsNullOrEmpty(param) == false)
            //{
            //    mParams = param;
            //}
            
            kindID = GetKindID(this.mPrototypeID);
            mainID = GetMainID(this.mPrototypeID);

            OnLoadData(reader);
        }

        public void LoadData(XmlNode data)
        {
            if (data.Attributes["Static_ID"] != null)
                int.TryParse(data.Attributes["Static_ID"].Value, out mPrototypeID);
            //if (data.Attributes["name"] != null)
            //    mName = data.Attributes["name"].Value;
            //if (data.Attributes["params"] != null)
            //    mParams = data.Attributes["params"].Value;

            kindID = GetKindID(this.mPrototypeID);
            mainID = GetMainID(this.mPrototypeID);

            OnLoadData(data);
        }

        public static int GetMainID(int prototypeID)
        {
            return (int)Math.Floor((double)(prototypeID / MAIN_PLACEHOLDER));
        }

        public static int GetKindID(int prototypeID)
        {
            return (int)Math.Floor((double)(prototypeID / KIND_PLACEHOLDER));
        }
    }

}
