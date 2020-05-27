using System;
using System.Collections.Generic;
using System.Text;

namespace ZoomLa.Components
{
    [Serializable]
   public class WaterConfig
    {
        public bool IsUsed
        {
            get;
            set;
        }
        /// <summary>
        /// 缩略图宽,超过则压缩,0则忽略
        /// </summary>
        public int imageWidth
        {
            get;
            set;
        }
        /// <summary>
        /// 缩略图高,超过则压缩
        /// </summary>
        public int imageHeigh
        {
            get;
            set;
        }
        public int WaterClassType
        {
            get;
            set;
        }
        public string WaterClass
        {
            get;
            set;
        }
        public string imgLogo
        {
            get;
            set;
        }
        /// <summary>
        /// 透明度
        /// </summary>
        public int imgAlph
        {
            get;
            set;
        }
        /// <summary>
        /// 水印文字
        /// </summary>
        public string WaterWord
        {
            get;
            set;
        }
        public string WaterWordType
        {
            get;
            set;
        }
        public string WaterWordStyle
        {
            get;
            set;
        }
        public int WaterWordSize
        {
            get;
            set;
        }
        public string WaterWordColor
        {
            get;
            set;
        }
        public int WaterWordAlph
        {
            get;
            set;
        }
        public string lopostion
        {
            get;
            set;
        }
        public int loX
        {
            get;
            set;
        }
        public int loY
        {
            get;
            set;
        }
        private string _waterimgs = "";
        /// <summary>
        /// 水印图片列表,Json格式
        /// </summary>
        public string WaterImgs
        {
            get { return string.IsNullOrEmpty(_waterimgs) ? "[]" : _waterimgs; }
            set { _waterimgs = value; }
        }
    }
}
