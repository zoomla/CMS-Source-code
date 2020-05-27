using System;
using System.Collections.Generic;
using System.Text;

using System.Web.UI.WebControls;
using System.Web.UI;
using Controls.ExGridViewFunction;

[assembly: System.Web.UI.WebResource("YYControls.SmartGridView.Resources.Asc.gif", "image/gif")]
[assembly: System.Web.UI.WebResource("YYControls.SmartGridView.Resources.Desc.gif", "image/gif")]
[assembly: System.Web.UI.WebResource("YYControls.SmartGridView.Resources.Icon.bmp", "image/bmp")]

[assembly: System.Web.UI.WebResource("YYControls.SmartGridView.Resources.StyleLibrary.css", "text/css")]

#if DEBUG
[assembly: System.Web.UI.WebResource("YYControls.SmartGridView.Resources.ScriptLibraryDebug.js", "text/javascript")]
#else
[assembly: System.Web.UI.WebResource("YYControls.SmartGridView.Resources.ScriptLibrary.js", "text/javascript")]
#endif

namespace Controls
{
    /// <summary>
    /// SmartGridView类，继承自GridView
    /// </summary>
    [ToolboxData(@"<{0}:SmartGridView runat='server'></{0}:SmartGridView>")]
    [System.Drawing.ToolboxBitmap(typeof(SmartGridView), "SmartGridView.bmp")]
    public partial class SmartGridView : GridView
    {
    //    // 需要扩展的功能对象容器
    //    private List<ExtendFunction> _efs = new List<ExtendFunction>();
    //    // 数据源对象
    //    private object _dataSourceObject = null;

    //    /// <summary>
    //    /// 构造函数
    //    /// </summary>
    //    public SmartGridView()
    //    {

    //    }

    //    /// <summary>
    //    /// OnInit
    //    /// </summary>
    //    /// <param name="e"></param>
    //    protected override void OnInit(EventArgs e)
    //    {
    //        this.PreRender += new EventHandler(SmartGridView_PreRender);

    //        // 将需要扩展的功能对象添加到功能扩展列表里
    //        if (this._contextMenus != null)
    //            this._efs.Add(new ContextMenuFunction());


    //        // 遍历需要实现的功能扩展，并实现它
    //        foreach (ExtendFunction ef in this._efs)
    //        {
    //            ef.SmartGridView = this;
    //            ef.Complete();
    //        }


    //        ObjectDataSource ods = this.Parent.FindControl(this.DataSourceID) as ObjectDataSource;
    //        if (ods != null)
    //        {
    //            ods.Selected += new ObjectDataSourceStatusEventHandler(ods_Selected);
    //        }

    //        base.OnInit(e);
    //    }

    //    /// <summary>
    //    /// OnLoad
    //    /// </summary>
    //    /// <param name="e"></param>
    //    protected override void OnLoad(EventArgs e)
    //    {
    //        if (this._dataSourceObject == null)
    //        {
    //            _dataSourceObject = this.DataSource;
    //        }

    //        base.OnLoad(e);
    //    }

    //    /// <summary>
    //    /// SmartGridView的PreRender事件
    //    /// </summary>
    //    /// <param name="sender"></param>
    //    /// <param name="e"></param>
    //    void SmartGridView_PreRender(object sender, EventArgs e)
    //    {
    //        if (!this.Page.ClientScript.IsClientScriptIncludeRegistered(this.GetType(), "yy_sgv_ScriptLibrary"))
    //        {
    //            // 注册所需脚本
    //            this.Page.ClientScript.RegisterClientScriptInclude
    //            (
    //                this.GetType(),
    //                "yy_sgv_ScriptLibrary",
    //                this.Page.ClientScript.GetWebResourceUrl
    //                (
    //                    #if DEBUG
    //                    this.GetType(), "YYControls.SmartGridView.Resources.ScriptLibraryDebug.js"
    //                    #else
    //                    this.GetType(), "YYControls.SmartGridView.Resources.ScriptLibrary.js"
    //                    #endif
    //                )
    //            );

    //            // for asp.net ajax
    //            this.Page.ClientScript.RegisterStartupScript(
    //                this.GetType(), 
    //                "yy_sgv_ScriptLibrary_ajax", 
    //                "if (typeof(Sys) != 'undefined') Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function endRequestHandler(sender, e){yy_sgv_ccListener();yy_sgv_crListener();});", 
    //                true);

    //            // 注册所需样式
    //            System.Web.UI.HtmlControls.HtmlLink link = new System.Web.UI.HtmlControls.HtmlLink();
    //            link.Attributes["type"] = "text/css";
    //            link.Attributes["rel"] = "stylesheet";
    //            link.Attributes["href"] = Page.ClientScript.GetWebResourceUrl(this.GetType(), "YYControls.SmartGridView.Resources.StyleLibrary.css");
    //            this.Page.Header.Controls.Add(link);
    //        }

    //        // this.Page.ClientScript.RegisterClientScriptResource(this.GetType(), "YYControls.SmartGridView.ScriptLibrary.js");
    //    }

    //    /// <summary>
    //    /// SmartGridView的对象数据源控件的Selected事件
    //    /// </summary>
    //    /// <param name="sender"></param>
    //    /// <param name="e"></param>
    //    void ods_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    //    {
    //        if (this._dataSourceObject == null)
    //        {
    //            this._dataSourceObject = e.ReturnValue;
    //        }
    //    }

    //    /// <summary>
    //    /// OnRowDataBound
    //    /// </summary>
    //    /// <param name="e">e</param>
    //    protected override void OnRowDataBound(GridViewRowEventArgs e)
    //    {
    //        DataControlRowType rowType = e.Row.RowType;

          

    //        if (rowType == DataControlRowType.DataRow)
    //        {
    //            OnRowDataBoundDataRow(e);
    //        }

    //        base.OnRowDataBound(e);
    //    }

    //    /// <summary>
    //    /// Render
    //    /// </summary>
    //    /// <param name="writer">writer</param>
    //    protected override void Render(HtmlTextWriter writer)
    //    {
    //        OnRenderBegin(writer);

    //        base.Render(writer);

    //        OnRenderEnd(writer);
    //    }

    //    /// <summary>
    //    /// InitializePager
    //    /// </summary>
    //    /// <param name="row">一个 System.Web.UI.WebControls.GridViewRow，表示要初始化的页导航行</param>
    //    /// <param name="columnSpan">页导航行应跨越的列数</param>
    //    /// <param name="pagedDataSource">一个 System.Web.UI.WebControls.PagedDataSource，表示数据源</param>
    //    protected override void InitializePager(GridViewRow row, int columnSpan, PagedDataSource pagedDataSource)
    //    {
    //        base.InitializePager(row, columnSpan, pagedDataSource);

    //        OnInitPager(row, columnSpan, pagedDataSource);
    //    }

    //    /// <summary>
    //    /// SmartGridView的数据源对象
    //    /// </summary>
    //    public object DataSourceObject
    //    {
    //        get { return this._dataSourceObject; }
    //        set { this._dataSourceObject = value; }
    //    }
    }
}
