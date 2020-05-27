using System;
using System.Collections.Generic;
using System.Text;

namespace ZoomLa.Model.Shop
{
    public class M_ApplyBuss
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string TrueName { get; set; }
        /// <summary>
        /// 商店地址
        /// </summary>
        public string Store { get; set; }
        /// <summary>
        /// 经营范围
        /// </summary>
        public string Scope { get; set; }
        /// <summary>
        /// 所属省份
        /// </summary>
        public string Dicname { get; set; }
        /// <summary>
        /// 所属城市
        /// </summary>
        public string Shi { get; set; }
        /// <summary>
        /// 所属县
        /// </summary>
        public string Xian { get; set; }
        /// <summary>
        /// 商店缩略图
        /// </summary>
        public string AbbImage { get; set; }
        /// <summary>
        /// 商家QQ
        /// </summary>
        public string QQ { get; set; }
        /// <summary>
        /// 营业执照
        /// </summary>
        public string Busslicense { get; set; }
        /// <summary>
        /// 商家实景图
        /// </summary>
        public string EntityImage { get; set; }
    }
}
