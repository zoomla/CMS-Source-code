using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZoomLa.Model.Collect
{
    public class M_Collect_ListFilter
    {
        /// <summary>
        /// 列表筛选,起始和结束字符
        /// </summary>
        public string ListStart { get; set; }
        public string ListEnd { get; set; }
        /// <summary>
        /// List字符方面筛选,必须包含,起始,结束,正则,以|切割
        /// </summary>
        public string CharContain { get; set; }
        public string CharStart { get; set; }
        public string CharEnd { get; set; }
        public string CharRegex { get; set; }
        /// <summary>
        /// 前后填空条件
        /// </summary>
        public string FillStart { get; set; }
        public string FillEnd { get; set; }
    }
}
