using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel;
using System.Web.UI;

namespace Controls
{
    /// <summary>
    /// ExGridView类的属性部分
    /// </summary>
    public partial class ExGridView
    {
        private ContextMenus _contextMenus;
        /// <summary>
        /// 数据行的右键菜单
        /// </summary>
        [
        Description("数据行的右键菜单"),
        Category("扩展"),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
        PersistenceMode(PersistenceMode.InnerProperty)
        ]
        public virtual ContextMenus ContextMenus
        {
            get 
            {
                if (_contextMenus == null)
                {
                    _contextMenus = new ContextMenus();
                }
                return _contextMenus; 
            }
        }

        private string _contextMenuCssClass;
        /// <summary>
        /// 右键菜单的级联样式表 CSS 类名
        /// 右键菜单的结构 div ul li a
        /// </summary>
        [
        Description("右键菜单的级联样式表 CSS 类名"),
        Category("扩展"),
        NotifyParentProperty(true)
        ]
        public string ContextMenuCssClass
        {
            get { return this._contextMenuCssClass; }
            set { this._contextMenuCssClass = value; }
        }

    }
}
