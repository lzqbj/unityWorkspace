using UnityEngine;
using System.Collections;
using System.Xml;
using System.Collections.Generic;
namespace Core.Template {
    /// <summary>
    /// 商店道具数据类
    /// </summary>
    public class ShopPrototype : PrototypeData {

        /// <summary>
        /// 说明
        /// </summary>
        public string Description = "";
        /// <summary>
        /// 名称
        /// </summary>
        public string Name = "";
        /// <summary>
        /// 原价
        /// </summary>
        public int OriginalPrice = 0;
        /// <summary>
        /// 数量
        /// </summary>
        public int Num = 0;
        /// <summary>
        /// 价格 现价
        /// </summary>
        public int Price = 0;
        /// <summary>
        /// 折扣
        /// </summary>
        public int Sale = 0;
        /// <summary>
        /// 图标
        /// </summary>
        //public string Icon = string.Empty;
        public int Icon = 1;
        /// <summary>
        /// 限时时间（天）
        /// </summary>
        public int Time = 0;
        /// <summary>
        /// 奖品内容
        /// </summary>
        public string Value = string.Empty;

        protected override void OnLoadData(XmlNode data) {

            if (data.Attributes["Name"] != null)
                Name = data.Attributes["Name"].Value;
            if (data.Attributes["Description"] != null)
                Description = data.Attributes["Description"].Value;
            if (data.Attributes["Num"] != null)
                int.TryParse(data.Attributes["Num"].Value, out Num);
            if (data.Attributes["OriginalPrice"] != null)
                int.TryParse(data.Attributes["OriginalPrice"].Value, out OriginalPrice);
            if (data.Attributes["Price"] != null)
                int.TryParse(data.Attributes["Price"].Value, out Price);
            if (data.Attributes["Sale"] != null)
                int.TryParse(data.Attributes["Sale"].Value, out Sale);
            if (data.Attributes["Time"] != null)
                int.TryParse(data.Attributes["Time"].Value, out Time);
            if (data.Attributes["Icon"] != null)
                int.TryParse(data.Attributes["Icon"].Value, out Icon);
                //Icon = data.Attributes["Icon"].Value;
            if (data.Attributes["Value"] != null)
                Value = data.Attributes["Value"].Value;
        }
    }

}

