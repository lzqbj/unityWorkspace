using UnityEngine;
using System.Collections;
using System.Xml;
using System.Collections.Generic;
namespace Core.Template {
    /// <summary>
    /// 签到数据类
    /// </summary>
    public class GoldPrototype : PrototypeData {

        
        /// <summary>
        /// 名称
        /// </summary>
        public string Name = "";
        /// <summary>
        /// 类型
        /// </summary>
        public int Type = 0;
        /// <summary>
        /// 价格 人民币
        /// </summary>
        public int Price = 0;
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon = string.Empty;
        /// <summary>
        /// 数量
        /// </summary>
        public int Num = 0;
        /// <summary>
        /// 赠送数量
        /// </summary>
        public int giftNum = 0;
        

        protected override void OnLoadData(XmlNode data) {

            if (data.Attributes["Name"] != null)
                Name = data.Attributes["Name"].Value;
            if (data.Attributes["Type"] != null)
                int.TryParse(data.Attributes["Type"].Value, out Type);
            if (data.Attributes["Price"] != null)
                int.TryParse(data.Attributes["Price"].Value, out Price);
            if (data.Attributes["Num"] != null)
                int.TryParse(data.Attributes["Num"].Value, out Num);
            if (data.Attributes["giftNum"] != null)
                int.TryParse(data.Attributes["giftNum"].Value, out giftNum);
            if (data.Attributes["Icon"] != null)
                Icon = data.Attributes["Icon"].Value;
        }
    }

}

