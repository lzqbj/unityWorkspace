using UnityEngine;
using System.Collections;
using System.Xml;
using System.Collections.Generic;
namespace Core.Template {
    /// <summary>
    /// 道具数据类
    /// </summary>
    public class PropPrototype : PrototypeData {

        /// <summary>
        /// 说明
        /// </summary>
        public string Description = "";
        /// <summary>
        /// 名称
        /// </summary>
        public string Name = "";
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon= string.Empty;
        /// <summary>
        /// 价格
        /// </summary>
        public int Price = 0;
        //示例延时路径
        public string Path = string.Empty;

        protected override void OnLoadData(XmlNode data) {

            if (data.Attributes["Name"] != null)
                Name = data.Attributes["Name"].Value;
            if (data.Attributes["Description"] != null)
                Description = data.Attributes["Description"].Value;
            if (data.Attributes["Price"] != null)
                int.TryParse(data.Attributes["Price"].Value, out Price);
            if (data.Attributes["Path"] != null)
                Path = data.Attributes["Path"].Value;
            if (data.Attributes["Icon"] != null)
                Path = data.Attributes["Icon"].Value;
        }
    }

}

