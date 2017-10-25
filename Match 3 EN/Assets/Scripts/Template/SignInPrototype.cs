using UnityEngine;
using System.Collections;
using System.Xml;
using System.Collections.Generic;
namespace Core.Template {
    /// <summary>
    /// 签到数据类
    /// </summary>
    public class SignInPrototype : PrototypeData {

        /// <summary>
        /// 说明
        /// </summary>
        public string Description = "";
        /// <summary>
        /// 名称
        /// </summary>
        public string Name = "";
        /// <summary>
        /// 奖品
        /// </summary>
        public string Value= string.Empty;
        /// <summary>
        /// 类型
        /// </summary>
        public int Type = 0;

        protected override void OnLoadData(XmlNode data) {

            if (data.Attributes["Name"] != null)
                Name = data.Attributes["Name"].Value;
            if (data.Attributes["Description"] != null)
                Description = data.Attributes["Description"].Value;
            if (data.Attributes["Type"] != null)
                int.TryParse(data.Attributes["Type"].Value, out Type);
            if (data.Attributes["Value"] != null)
                Value = data.Attributes["Value"].Value;
        }
    }

}

