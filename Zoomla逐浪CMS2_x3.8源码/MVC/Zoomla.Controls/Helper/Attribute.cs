using System;
using System.Collections.Generic;
using System.Text;

using System.Web.UI.WebControls;
using System.Web.UI;

namespace Controls.Helper
{
    /// <summary>
    /// 常用Helper
    /// </summary>
    public partial class Common
    {
        /// <summary>
        /// 设置实现了IAttributeAccessor接口的控件的Attribute（保留原来同Key的Attribute）
        /// </summary>
        /// <param name="iaa">实现了IAttributeAccessor接口的控件</param>
        /// <param name="key">属性的名称</param>
        /// <param name="value">属性的值</param>
        /// <param name="csp">属性的值的位置</param>
        public static void SetAttribute(IAttributeAccessor iaa, string key, string value, AttributeValuePosition csp)
        {
            SetAttribute(iaa, key, value, csp, ';');
        }

        /// <summary>
        /// 设置实现了IAttributeAccessor接口的控件的Attribute（保留原来同Key的Attribute）
        /// </summary>
        /// <param name="iaa">实现了IAttributeAccessor接口的控件</param>
        /// <param name="key">属性的名称</param>
        /// <param name="value">属性的值</param>
        /// <param name="csp">属性的值的位置</param>
        /// <param name="separator">属性分隔符</param>
        public static void SetAttribute(IAttributeAccessor iaa, string key, string value, AttributeValuePosition csp, char separator)
        {
            string tmp = iaa.GetAttribute(key);

            if (String.IsNullOrEmpty(tmp))
            {
                iaa.SetAttribute(key, value);
            }
            else if (csp == AttributeValuePosition.First)
            {
                tmp = tmp.TrimStart(separator);
                iaa.SetAttribute(key, value + separator + tmp);
            }
            else if (csp == AttributeValuePosition.Last)
            {
                tmp = tmp.TrimEnd(separator);
                iaa.SetAttribute(key, tmp + separator + value);
            }
        }
    }
}
