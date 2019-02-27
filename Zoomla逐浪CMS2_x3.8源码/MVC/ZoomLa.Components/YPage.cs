using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZoomLa.Components
{
    [Serializable]
    public class YPage
    {
        /// <summary>
        /// 黄页是否需要审核 True:是
        /// </summary>
        public bool IsAudit { get; set; }
        /// <summary>
        /// 用户能否添加栏目
        /// </summary>
        public bool UserCanNode { get; set; }
    }
}
