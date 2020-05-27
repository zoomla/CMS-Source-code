using System;

namespace ZoomLa.Model
{
    [Serializable]
    public class M_PanoramicCer
    {
        public M_PanoramicCer()
        {
            //TODO构造函数
        }
        /// <summary>
        /// 授权网站ID
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 域名
        /// </summary>
        public string domain { get; set; }
        /// <summary>
        /// 有效期
        /// </summary>
        public DateTime exp { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string start { get; set; }

        /// <summary>
        /// 许可文件名
        /// </summary>
        public string filename { get; set; }
    }
}
