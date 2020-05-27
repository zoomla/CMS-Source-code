using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZoomLa.Components
{
    [Serializable]
    public class BarOption
    {
        public int CateID { get; set; }
        /// <summary>
        /// 新注册用户发帖间隔
        /// </summary>
        public int UserTime { get; set; }
        /// <summary>
        /// 发帖间隔
        /// </summary>
        public int SendTime { get; set; }
    }
}
