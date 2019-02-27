using System;
using System.Collections.Generic;
using System.Text;

using System.Web.UI.WebControls;
using System.Web.UI;

namespace Controls.ExGridViewFunction
{
    /// <summary>
    /// 扩展功能：给数据行增加右键菜单
    /// </summary>
    public class ContextMenuFunction : ExtendFunction
    {
        List<string> _rowRightClickButtonUniqueIdList = new List<string>();

        private string _menuItem;
        private string _target;

        /// <summary>
        /// 构造函数
        /// </summary>
        public ContextMenuFunction(): base()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sgv">ExGridView</param>
        public ContextMenuFunction(ExGridView sgv): base(sgv)
        {

        }

        /// <summary>
        /// 扩展功能的实现
        /// </summary>
        protected override void Execute()
        {
            this._sgv.RowDataBoundDataRow += new ExGridView.RowDataBoundDataRowHandler(_sgv_RowDataBoundDataRow);
            this._sgv.PreRender += new EventHandler(_sgv_PreRender);
            this._sgv.RenderBegin += new ExGridView.RenderBeginHandler(_sgv_RenderBegin);

            foreach (ContextMenu cm in this._sgv.ContextMenus)
            {
                string text = cm.Text == null ? "" : cm.Text;
                string target = cm.Target == null ? "" : cm.Target;

                this._menuItem += String.Format(",\"{0}\"", text.Replace(",", "，"));
                this._target += String.Format(",\"{0}\"", target.Replace(",", "，"));
            }

            this._menuItem = String.Format("new Array({0})", this._menuItem.TrimStart(','));
            this._target = String.Format("new Array({0})", this._target.TrimStart(','));
        }

        void _sgv_RowDataBoundDataRow(object sender, GridViewRowEventArgs e)
        {
            string handle = "";

            // 从用户定义的ContextMenus集合中分解出ContextMenu
            foreach (ContextMenu cm in this._sgv.ContextMenus)
            {
                if (!String.IsNullOrEmpty(cm.NavigateUrl))
                {
                    handle += String.Format(",\"{0}\"", cm.NavigateUrl);
                    continue;
                }
                else if (String.IsNullOrEmpty(cm.BoundCommandName))
                {
                    handle += String.Format(",\"{0}\"", "#");
                    continue;
                }

                foreach (TableCell tc in e.Row.Cells)//这里若不处理,第一个参将为#,无法指向链接
                {
                    bool bln = false;
                    foreach (Control c in tc.Controls)
                    {
                        // 如果控件继承自接口IButtonControl
                        if (c is IButtonControl && ((IButtonControl)c).CommandName == cm.BoundCommandName)
                        {
                            handle += String.Format(",\"{0}\"", this._sgv.Page.ClientScript.GetPostBackClientHyperlink(c, ""));
                            _rowRightClickButtonUniqueIdList.Add(c.UniqueID);
                            bln = true;
                            break;
                        }
                    }

                    if (bln)
                    {
                        break;
                    }
                }
            }

            handle = String.Format("new Array({0})", handle.TrimStart(','));

            string oncontextmenuValue =
                String.Format("yy_sgv_setRightMenu({0},{1}_rightMenuItem,{1}_rightMenuTarget, {2})",
                    handle,this._sgv.ClientID,
                String.IsNullOrEmpty(this._sgv.ContextMenuCssClass) ? "null" : "'" + this._sgv.ContextMenuCssClass + "'"
                );

            // 设置按钮的客户端属性
            Controls.Helper.Common.SetAttribute(
                e.Row,
                "oncontextmenu",
                oncontextmenuValue,
                AttributeValuePosition.Last);
        }

        /// <summary>
        /// ExGridView的PreRender事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _sgv_PreRender(object sender, EventArgs e)
        {
            // 构造所需脚本
            string scriptString = "";
            scriptString += "document.oncontextmenu=function(evt){return yy_sgv_rightMenu.show(evt);};";
            scriptString += "document.onclick=function(){yy_sgv_rightMenu.hidden();};";

            // 注册所需脚本
            if (!this._sgv.Page.ClientScript.IsClientScriptBlockRegistered("yy_sgv_rightMenu"))
            {
                this._sgv.Page.ClientScript.RegisterClientScriptBlock
                (
                    this._sgv.GetType(),
                    "yy_sgv_rightMenu",
                    scriptString,
                    true
                );
            }

            // 为每个ExGridView注册与右键菜单相关的变量
            if (!this._sgv.Page.ClientScript.IsClientScriptBlockRegistered(String.Format("yy_sgv_rightMenu_{0}", this._sgv.ClientID)))
            {
                this._sgv.Page.ClientScript.RegisterClientScriptBlock
                (
                    this._sgv.GetType(),
                    String.Format("yy_sgv_rightMenu_{0}", this._sgv.ClientID),
                    String.Format(
                    "var {0}_rightMenuItem={1};var {0}_rightMenuTarget={2};",
                        this._sgv.ClientID,
                        this._menuItem,
                        this._target),
                    true
                );
            }

        }

        /// <summary>
        /// RenderBegin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="writer"></param>
        void _sgv_RenderBegin(object sender, HtmlTextWriter writer)
        {
            foreach (string uniqueId in this._rowRightClickButtonUniqueIdList)
            {
                // 注册回发或回调数据以进行验证
                this._sgv.Page.ClientScript.RegisterForEventValidation(uniqueId);
            }
        }

    }
}
