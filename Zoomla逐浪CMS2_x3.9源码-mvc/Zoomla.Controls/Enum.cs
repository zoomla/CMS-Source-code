using System;
using System.Collections.Generic;
using System.Text;

namespace Controls
{
    /// <summary>
    /// 属性的值的位置
    /// </summary>
    public enum AttributeValuePosition
    {
        /// <summary>
        /// 起始
        /// </summary>
        First,
        /// <summary>
        /// 结尾
        /// </summary>
        Last
    }

    /// <summary>
    /// 导出文件的格式
    /// </summary>
    public enum ExportFormat
    {
        /// <summary>
        /// CSV
        /// </summary>
        CSV,
        /// <summary>
        /// DOC
        /// </summary>
        DOC,
        /// <summary>
        /// TXT
        /// </summary>
        TXT
    }

    /// <summary>
    /// 自定义分页的显示模式
    /// </summary>
    public enum PagingMode
    {
        /// <summary>
        /// 系统自带分页的显示模式
        /// </summary>
        Default,
        /// <summary>
        /// Webabcd分页的显示模式
        /// </summary>
        Webabcd
    }
}
