using Controls.ExGridViewFunction;
using Controls.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


[assembly: System.Web.UI.WebResource("Controls.ExGridView.StyleLibrary.css", "text/css")]
#if DEBUG
[assembly: System.Web.UI.WebResource("Controls.ExGridView.ScriptLibraryDebug.js", "text/javascript")]
#else
[assembly: System.Web.UI.WebResource("Controls.ExGridView.ScriptLibrary.js", "text/javascript")]
#endif
[assembly: System.Web.UI.WebResource("Controls.ExGridView.ExtendedGridView.js", "text/javascript")]
namespace Controls
{
    /*
     * 2013.10.05:完成
     * 2013.12.07:增加自定义PageSize处理功能
     * 2013.12.10:增加默认PageSize处理
     * 2016.07.27:改为下拉选单形式
     */ 

    [ToolboxBitmap(typeof(ExGridView), "ExGridView.bmp"), ToolboxData("<{0}:ExGridView ID=\"Egv\" runat=\"server\"></{0}:ExGridView>"), Themeable(true)]
    public partial class ExGridView:GridView
    {
        private const string CheckBoxColumHeaderID = "{0}_HeaderButton";
        //private const string BottomCheckbox = "<input type='checkbox' hidefocus='true' id='AllID_Chk' name='AllID_Chk' onclick='$(\"input:checkbox[name=idchk]:enabled\").each(function () {this.checked = $(\"#AllID_Chk\")[0].checked;})'>";
        private const string ExtendedGridView_JS = "Controls.ExtendedGridView.ExtendedGridView.js";
        private int m_RawPageIndex;
        private static string m_SerialText = Resources.ExtendedGridView_SerialText;
        private string m_UniqueControlPageIndex;
        private string m_UniqueControlPageSize;
        private const string ResizeTableColumn_JS = "Controls.ExtendedGridView.ResizeTableColumn.js";

        //------------ContextMenu
        // 需要扩展的功能对象容器
        private List<ExtendFunction> _efs = new List<ExtendFunction>();
        public delegate void TxtPageSizeChange(string size);
        public TxtPageSizeChange txtFunc = null;//用户可自定义pageSize处理
        protected override void OnInit(EventArgs e)
        {
            this.PreRender += new EventHandler(ExGridView_PreRender);//注册脚本
            // 右键菜单,将需扩展的功能加入列表中
            if (this._contextMenus != null)
                this._efs.Add(new ContextMenuFunction());

            // 遍历需要实现的功能扩展，并实现它
            foreach (ExtendFunction ef in this._efs)
            {
                ef.ExGridView = this;
                ef.Complete();
            }
           
            base.OnInit(e);
        }
        //设置好CheckBox并返回,使用InputCheckBoxField
        private ArrayList AddCheckBoxColumn(ICollection columnList)
        {
            ArrayList list = new ArrayList(columnList);
            string format = "<input type='checkbox' hidefocus='true' id='{0}' name='{0}' onclick='CheckAll(this,\"" + base.RowStyle.CssClass + "\",\"" + this.SelectedCssClass + "\")'>";
            InputCheckBoxField field = new InputCheckBoxField();
            string str2 = string.Format("{0}_HeaderButton", this.ClientID);
            field.HeaderText = string.Format(format, str2);
            field.HeaderStyle.Width = this.CheckBoxFieldHeaderWidth;
            field.ReadOnly = true;
            field.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            if (this.CheckBoxColumnIndex > list.Count)
            {
                list.Add(field);
                this.CheckBoxColumnIndex = list.Count - 1;
                return list;
            }
            list.Insert(this.CheckBoxColumnIndex, field);
            return list;
        }
        //应该是列索引的信息
        private ArrayList AddSerialColumn(ICollection columnList)
        {
            ArrayList list = new ArrayList(columnList);
            TemplateField field = new TemplateField();
            field.HeaderText = this.SerialText;
            if (this.SerialColumnIndex > list.Count)
            {
                list.Add(field);
                this.SerialColumnIndex = list.Count - 1;
                return list;
            }
            list.Insert(this.SerialColumnIndex, field);
            return list;
        }
        //创建用来构建控件层次结构的列字段集,包括CheckBox与序列号
        protected override ICollection CreateColumns(PagedDataSource dataSource, bool useDataSource)
        {
            ICollection columnList = base.CreateColumns(dataSource, useDataSource);//数据源,true表示使用指定数据源,false则不使用
            if (!this.AutoGenerateCheckBoxColumn && !this.AutoGenerateSerialColumn)
            {
                return columnList;
            }
            ArrayList list = new ArrayList();
            return list;
        }
        //创建自定义分页控件
        private void CreateCustomPagerRow(GridViewRowEventArgs e)
        {
            e.Row.Cells[0].Controls.Clear();
            Panel div = new Panel();
            div.ID = "egv_page";
            div.CssClass = "text-center";
            e.Row.Cells[0].Controls.Add(div);
            Literal chk = new Literal(); chk.EnableViewState = false;
            chk.Text = "<label class='allchk_l'><input type='checkbox' id='AllID_Chk' onclick=\"$('input:checkbox[name=idchk]:enabled').each(function () {this.checked = $('#AllID_Chk')[0].checked;});\" /><span class='allchk_sp'>全选</span></label>";
            div.Controls.Add(chk);
            div.Controls.Add(new LiteralControl("共&nbsp;"));
            Label child = new Label();
            child.ID = "LblRowsCount";
            child.Text = this.VirtualItemCount.ToString();
            child.Font.Bold = true;
            div.Controls.Add(child);
            div.Controls.Add(new LiteralControl("&nbsp;" + this.ItemUnit + this.ItemName));
            div.Controls.Add(new LiteralControl("&nbsp;&nbsp;"));
            LinkButton button = new LinkButton();
            button.CommandName = "Page";
            button.CommandArgument = "First";
            button.Enabled = this.PageIndex != 0;
            if (!button.Enabled) button.Style.Add("color","gray");
            button.Text = Resources.ExtendedGridView_FirstPageText;
            div.Controls.Add(button);
            div.Controls.Add(new LiteralControl("&nbsp;"));
            LinkButton button2 = new LinkButton();
            button2.CommandName = "Page";
            button2.CommandArgument = "Prev";
            button2.Enabled = this.PageIndex != 0;
            if (!button2.Enabled) button2.Style.Add("color", "gray");
            button2.Text = Resources.ExtendedGridView_PrevPageText;
            div.Controls.Add(button2);
            div.Controls.Add(new LiteralControl("&nbsp;"));
            LinkButton button3 = new LinkButton();
            button3.CommandName = "Page";
            button3.CommandArgument = "Next";
            button3.Enabled = this.PageIndex != (this.PageCount - 1);
            if (!button3.Enabled) button3.Style.Add("color", "gray");
            button3.Text = Resources.ExtendedGridView_NextPageText;
            div.Controls.Add(button3);
            div.Controls.Add(new LiteralControl("&nbsp;"));
            LinkButton button4 = new LinkButton();
            button4.CommandName = "Page";
            button4.CommandArgument = "Last";
            button4.Enabled = this.PageIndex != (this.PageCount - 1);
            if (!button4.Enabled) button4.Style.Add("color", "gray");
            button4.Text = Resources.ExtendedGridView_LastPageText;
            div.Controls.Add(button4);
            div.Controls.Add(new LiteralControl("&nbsp;&nbsp;"));
            //if (this.IsHoldState)
            //{
            //    button.Click += new EventHandler(this.LbtnFirst_Click);//指定分页事件处理方法,首页,上一页,下一页,最后一页
            //    button2.Click += new EventHandler(this.LbtnPrev_Click);
            //    button3.Click += new EventHandler(this.LbtnNext_Click);
            //    button4.Click += new EventHandler(this.LbtnLast_Click);
            //}
            div.Controls.Add(new LiteralControl(Resources.ExtendedGridView_PageNun));
            Label label2 = new Label();
            label2.Text = Convert.ToString((int)(this.PageIndex + 1));
            label2.Font.Bold = true;
            label2.ForeColor = Color.Red;
            div.Controls.Add(label2);
            div.Controls.Add(new LiteralControl("/"));
            Label label3 = new Label();
            label3.Text = Convert.ToString(this.PageCount);
            label3.Font.Bold = true;
            div.Controls.Add(label3);
            div.Controls.Add(new LiteralControl(Resources.ExtendedGridView_Page));
            div.Controls.Add(new LiteralControl("&nbsp;&nbsp;"));

            switch (BoxType)
            {
                case "dp":
                    {
                        int[] boxitem = new[] { 10, 20, 50, 100, 200, 500 };
                        bool box_has = false;
                        DropDownList box = new DropDownList();
                        for (int i = 0; i < boxitem.Length; i++)
                        {
                            string size = boxitem[i].ToString();
                            box.Items.Insert(i, new ListItem(size, size));
                            if (boxitem[i] == PageSize) { box_has = true; }
                        }
                        if (!box_has)
                        {
                            box.Items.Insert(boxitem.Length, new ListItem(this.PageSize.ToString(), this.PageSize.ToString()));
                        }
                        box.SelectedValue = this.PageSize.ToString();
                        box.Width = 45;
                        box.AutoPostBack = true;
                        box.SelectedIndexChanged += new EventHandler(this.TxtMaxPage_SelectedIndexChanged);
                        div.Controls.Add(box);
                    }
                    break;
                case "text":
                default:
                    {
                        TextBox box = new TextBox();
                        box.ApplyStyleSheetSkin(this.Page);
                        box.MaxLength = 3;
                        box.Style.Add("text-align", "center");
                        box.Width = 35;
                        box.Text = Convert.ToString(this.PageSize);
                        box.AutoPostBack = true;
                        box.TextChanged += new EventHandler(this.TxtMaxPerPage_TextChanged);
                        div.Controls.Add(box);
                    }
                    break;
            }
           
            div.Controls.Add(new LiteralControl(this.ItemUnit + this.ItemName + Resources.ExtendedGridView_Page2));
            div.Controls.Add(new LiteralControl("&nbsp;&nbsp;"));
            div.Controls.Add(new LiteralControl(Resources.ExtendedGridView_JumpTurn));
            if (this.PageCount < 10)
            {
                DropDownList list = new DropDownList();
                list.ApplyStyleSheetSkin(this.Page);
                list.AutoPostBack = true;
                list.SelectedIndexChanged += new EventHandler(this.DropCurrentPage_SelectedIndexChanged);
                ArrayList list2 = new ArrayList();
                for (int i = 1; i <= this.PageCount; i++)
                {
                    list2.Add(i);
                }
                list.DataSource = list2;
                list.DataBind();
                list.SelectedIndex = this.PageIndex;
                div.Controls.Add(list);
            }
            else
            {
                TextBox box2 = new TextBox();
                box2.ApplyStyleSheetSkin(this.Page);
                box2.Width = 30;
                box2.Text = Convert.ToString((int)(this.PageIndex + 1));
                //box2.Attributes.Add("onchange", "if(!CheckInputValue(this))return;");
                box2.AutoPostBack = true;
                box2.TextChanged += new EventHandler(this.TxtCurrentPage_TextChanged);
                div.Controls.Add(box2);
            }
            div.Controls.Add(new LiteralControl(Resources.ExtendedGridView_Page3));
        }
        //分页下拉框的处理
        protected void DropCurrentPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList list = (DropDownList)sender;
            this.PageIndex = Convert.ToInt32(list.SelectedValue) - 1;
            this.OnPageIndexChanging(new GridViewPageEventArgs(this.PageIndex));//切换页面
        }
        //分页处理,读Session中的值
        protected void LbtnFirst_Click(object sender, EventArgs e)
        {
            this.Context.Session[this.m_UniqueControlPageIndex] = 0;
        }
        protected void LbtnLast_Click(object sender, EventArgs e)
        {
            this.Context.Session[this.m_UniqueControlPageIndex] = this.PageCount - 1;
        }
        protected void LbtnNext_Click(object sender, EventArgs e)
        {
            this.Context.Session[this.m_UniqueControlPageIndex] = this.PageIndex + 1;
        }
        protected void LbtnPrev_Click(object sender, EventArgs e)
        {
            this.Context.Session[this.m_UniqueControlPageIndex] = this.PageIndex - 1;
        }
        //相当于构造方法,加载各项,如对分页的session存值字符串命名,其最终在目标aspx中执行
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        /// <summary>
        /// ExGridView的PreRender事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ExGridView_PreRender(object sender, EventArgs e)
        {
            if (!this.Page.ClientScript.IsClientScriptIncludeRegistered(this.GetType(), "yy_sgv_ScriptLibrary"))
            {
                // 注册所需脚本
                this.Page.ClientScript.RegisterClientScriptInclude
                (
                    this.GetType(),"yy_sgv_ScriptLibrary",
                    this.Page.ClientScript.GetWebResourceUrl
                    (
                        #if DEBUG
                        this.GetType(), "Controls.ExGridView.ScriptLibraryDebug.js"
                        #else
                        this.GetType(), "Controls.ExGridView.ScriptLibrary.js"
                        #endif
                        )
                );
                this.Page.ClientScript.RegisterClientScriptInclude
                (
                    this.GetType(),
                    "yy_sgv_ExtendGridView",
                    this.Page.ClientScript.GetWebResourceUrl
                    (this.GetType(), "Controls.ExGridView.ExtendedGridView.js"));

                // for asp.net ajax
                this.Page.ClientScript.RegisterStartupScript(
                    this.GetType(),
                    "yy_sgv_ScriptLibrary_ajax",
                    "if (typeof(Sys) != 'undefined') Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function endRequestHandler(sender, e){yy_sgv_ccListener();yy_sgv_crListener();});",
                    true);

                //string webResourceUrl = this.Page.ClientScript.GetWebResourceUrl(base.GetType(), "Controls.ExtendedGridView.ExtendedGridView.js");
                //if (this.ColumnResizeable && !this.Page.ClientScript.IsClientScriptIncludeRegistered(type, "Controls.ExtendedGridView.ResizeTableColumn.js"))
                //{
                //    webResourceUrl = this.Page.ClientScript.GetWebResourceUrl(type, "Controls.ExtendedGridView.ResizeTableColumn.js");
                //    this.Page.ClientScript.RegisterClientScriptInclude(type, "Controls.ExtendedGridView.ResizeTableColumn.js", webResourceUrl);
                //}
                // 注册所需样式
                //System.Web.UI.HtmlControls.HtmlLink link = new System.Web.UI.HtmlControls.HtmlLink();
                //link.Attributes["type"] = "text/css";
                //link.Attributes["rel"] = "stylesheet";
                //link.Attributes["href"] = Page.ClientScript.GetWebResourceUrl(this.GetType(), "Controls.ExGridView.StyleLibrary.css");
                //this.Page.Header.Controls.Add(link);
            }
        }
        //重写都是先调用父类的方法之后再加上自己的东西,你可放前也可放后,但需要执行次.
        //在OnLoad之后执行,判断用户是否设置分页等,显示隐藏,初始化组件.
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            if (((this.PageCount > 1) && ((this.PageIndex + 1) == this.PageCount)) && ((this.Rows.Count == 0) && base.Initialized))
            {
                base.RequiresDataBinding = true;
            }
            if ((this.PageIndex != this.m_RawPageIndex) && (this.Rows.Count == 0))
            {
                this.PageIndex = 0;
                if (base.Initialized)
                {
                    base.RequiresDataBinding = true;
                }
            }
            if (this.AllowPaging && (this.BottomPagerRow != null))
            {
                this.BottomPagerRow.Visible = true;
                if (this.ShowCustomPager)
                {
                    Label label = this.BottomPagerRow.Cells[0].FindControl("LblRowsCount") as Label;
                    if (label != null)
                    {
                        label.Text = this.VirtualItemCount.ToString();
                    }
                }
            }
            if (this.AutoGenerateCheckBoxColumn)
            {
                string str = string.Format("{0}_HeaderButton", this.ClientID);
                foreach (GridViewRow row in this.Rows)
                {
                    CheckBox box = (CheckBox)row.FindControl("CheckBoxButton");
                    box.Attributes["onclick"] = string.Concat(new object[] { "CheckItem(this,\"", str, "\",\"", base.RowStyle.CssClass, "\",\"", this.SelectedCssClass, "\",", this.Rows.Count, ")" });
                }
            }
        }
        //该事件在创建列时调用,这里用于给列加上移入移出,序列号等属性.
        protected override void OnRowCreated(GridViewRowEventArgs e)
        {
            base.OnRowCreated(e);
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (!string.IsNullOrEmpty(this.MouseOverCssClass))
                {
                    //e.Row.Attributes.Add("onmouseover", "MouseOver(this,\"" + this.MouseOverCssClass + "\")");
                    //e.Row.Attributes.Add("onmouseout", "MouseOut(this)");
                }
                if (this.AutoGenerateSerialColumn)
                {
                    e.Row.Cells[this.SerialColumnIndex].Text = Convert.ToString((int)((e.Row.DataItemIndex + (this.PageIndex * this.PageSize)) + 1));
                }
            }
            if ((e.Row.RowType == DataControlRowType.Pager) && this.ShowCustomPager)
            {
                this.CreateCustomPagerRow(e);
            }
        }
        //数据绑定时调用,这里用于绑定双击,右键等事件
        protected override void OnRowDataBound(GridViewRowEventArgs e)
        {
            DataControlRowType rowType = e.Row.RowType;
            if (rowType == DataControlRowType.DataRow)
            {
                OnRowDataBoundDataRow(e);
            }
           
            if ((e.Row.RowType == DataControlRowType.DataRow) && !string.IsNullOrEmpty(this.RowDblclickUrl))
            {
                if (!string.IsNullOrEmpty(this.RowDblclickBoundField))
                {
                    //e.Row.Attributes.Add("ondblclick", "RowDblclick('" + StringHelper.ReplaceIgnoreCase(this.RowDblclickUrl, "{$Field}", DataBinder.Eval(e.Row.DataItem, this.RowDblclickBoundField).ToString().Replace("'", "%27")) + "');");
                }
                else
                {
                    //e.Row.Attributes.Add("ondblclick", "RowDblclick('" + this.RowDblclickUrl + "');");
                }
            }
            base.OnRowDataBound(e);
        }
        //数据绑定前执行,VirtualItemCount不知有何用,统计数据数量?
        protected override void PerformDataBinding(IEnumerable data)
        {
            base.PerformDataBinding(data);
            this.ViewState["VirtualItemCount"] = this.ViewState["_!ItemCount"];
        }
        //建立层次结构,这里直接调用父类.
        protected override void PrepareControlHierarchy()
        {
            base.PrepareControlHierarchy();
        }
        //将控件内容转化为Html呈现给客户端,这里额外增加可调整列大小的功能.如果ColumnResizeable为True的话.
        protected override void Render(HtmlTextWriter writer)
        {
            OnRenderBegin(writer);
            base.Render(writer);
            OnRenderEnd(writer);
            //if (this.ColumnResizeable)
            //{
            //    writer.Write("<script type=\"text/javascript\">resizeTableColumn('" + this.ClientID + "');</script>");
            //}
        }
        //页面索引改变事件,输入页数直接跳转,自己增加的业务逻辑后调用你类OnPageIndexChanging处理业务.
        protected void TxtCurrentPage_TextChanged(object sender, EventArgs e)
        {
            int pageCount;
            TextBox box = (TextBox)sender;
            if (!int.TryParse(box.Text, out pageCount))//如果转换失败,即不是一个数字时
            {
                pageCount = 1;
            }
            else if (pageCount < 1)
            {
                pageCount = 1;
            }
            if (pageCount > this.PageCount)
            {
                pageCount = this.PageCount;
            }
            this.PageIndex = pageCount - 1;
            box.Text = pageCount.ToString();
            //if (this.IsHoldState)
            //{
            //    this.Context.Session[this.m_UniqueControlPageIndex] = this.PageIndex;
            //}
            this.OnPageIndexChanging(new GridViewPageEventArgs(this.PageIndex));
        }
        //默认pageSize处理方法
        protected void TxtMaxPerPage_TextChanged(object sender, EventArgs e)
        {
            int pageSize;
            TextBox box = (TextBox)sender;
            if (txtFunc != null)
            {
                txtFunc(box.Text);
            }
            else
            {
                if (!int.TryParse(box.Text, out pageSize))//如果转换失败,即不是一个数字时
                {
                    pageSize = this.PageSize;
                }
                else if (pageSize < 1)//小于1时,均恢复默认PageSize,默认PageSize是你给序的
                {
                    pageSize = this.PageSize;
                }
                this.PageSize = pageSize;
                this.PageIndex = 0;//改变后回到首页
                box.Text = pageSize.ToString();
            }
            this.OnPageIndexChanging(new GridViewPageEventArgs(this.PageIndex));
        }
        protected void TxtMaxPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            int pageSize;
            //TextBox box = (TextBox)sender;
            DropDownList box = (DropDownList)sender;
            if (txtFunc != null)
            {
                txtFunc(box.SelectedValue);
            }
            else
            {
                if (!int.TryParse(box.SelectedValue, out pageSize))//如果转换失败,即不是一个数字时
                {
                    pageSize = this.PageSize;
                }
                else if (pageSize < 1)//小于1时,均恢复默认PageSize,默认PageSize是你给序的
                {
                    pageSize = this.PageSize;
                }
                this.PageSize = pageSize;
                this.PageIndex = 0;//改变后回到首页
                //box.Text = pageSize.ToString();
            }
            this.OnPageIndexChanging(new GridViewPageEventArgs(this.PageIndex));
        }
        //下面为控件的一些状态设置,我们可以在aspx页面中使用的时候指定,其值存入ViewState
        [Description("是否自动生成复选框列"), Bindable(true), Category("自定义"), Localizable(true), DefaultValue(false)]
        public bool AutoGenerateCheckBoxColumn
        {
            get
            {
                object obj2 = this.ViewState["AutoGenerateCheckBoxColumn"];
                if (obj2 == null)
                {
                    return false;
                }
                return (bool)obj2;
            }
            set
            {
                this.ViewState["AutoGenerateCheckBoxColumn"] = value;
            }
        }

        [Localizable(true), Bindable(true), Category("自定义"), DefaultValue(false), Description("是否显示序号列")]
        public bool AutoGenerateSerialColumn
        {
            get
            {
                object obj2 = this.ViewState["AutoGenerateSerialColumn"];
                return ((obj2 != null) && ((bool)obj2));
            }
            set
            {
                this.ViewState["AutoGenerateSerialColumn"] = value;
            }
        }

        [Category("自定义"), DefaultValue(0), Bindable(true), Description("复选框列的索引")]
        public int CheckBoxColumnIndex
        {
            get
            {
                object obj2 = this.ViewState["CheckBoxColumnIndex"];
                if (obj2 == null)
                {
                    return 0;
                }
                return (int)obj2;
            }
            set
            {
                this.ViewState["CheckBoxColumnIndex"] = (value < 0) ? 0 : value;
            }
        }

        [Category("自定义"), Bindable(true), DefaultValue(20), Description("复选框列宽度"), Localizable(true)]
        public Unit CheckBoxFieldHeaderWidth
        {
            get
            {
                object obj2 = this.ViewState["CheckBoxFieldHeaderWidth"];
                if (obj2 == null)
                {
                    return Unit.Percentage(3.0);
                }
                return (Unit)obj2;
            }
            set
            {
                this.ViewState["CheckBoxFieldHeaderWidth"] = value;
            }
        }

        [Category("自定义"), DefaultValue(true), Bindable(false), Description("表格的列是否可以动态改变大小")]
        public bool ColumnResizeable
        {
            get
            {
                object obj2 = this.ViewState["ColumnResizeable"];
                if (obj2 != null)
                {
                    return (bool)obj2;
                }
                return true;
            }
            set
            {
                this.ViewState["ColumnResizeable"] = value;
            }
        }

        //[Bindable(true), Description("是否保持当前状态"), Localizable(true), Category("自定义"), DefaultValue(false)]
        //public bool IsHoldState
        //{   //该选项将分页下拉框等存入Session,以便在分页后仍保持状态
        //    get
        //    {
        //        object obj2 = this.ViewState["IsHoldState"];
        //        if (obj2 != null)
        //        {
        //            return (bool)obj2;
        //        }
        //        return true;
        //    }
        //    set
        //    {
        //        this.ViewState["IsHoldState"] = value;
        //    }
        //}

        [DefaultValue("记录"), Localizable(true), Bindable(true), Category("自定义"), Description("分页导航处显示的项目名称")]
        public string ItemName
        {
            get
            {
                string str = (string)this.ViewState["ItemName"];
                if (str != null)
                {
                    return str;
                }
                return Resources.ExtendedGridView_ItemName;
            }
            set
            {
                this.ViewState["ItemName"] = value;
            }
        }

        [Bindable(true), Description("分页导航处显示的项目单位"), DefaultValue("条"), Localizable(true), Category("自定义")]
        public string ItemUnit
        {
            get
            {
                string str = (string)this.ViewState["ItemUnit"];
                if (str != null)
                {
                    return str;
                }
                return Resources.ExtendedGridView_ItemUnit;
            }
            set
            {
                this.ViewState["ItemUnit"] = value;
            }
        }

        [Localizable(true), Description("鼠标移动到数据行上显示的CSS效果"), Bindable(true), Category("自定义"), DefaultValue("")]
        public string MouseOverCssClass
        {
            get
            {
                string str = (string)this.ViewState["MouseOverCssClass"];
                if (str != null)
                {
                    return str;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["MouseOverCssClass"] = value;
            }
        }

        public override int PageIndex
        {
            set
            {
                base.PageIndex = value;
                if (!base.DesignMode)
                {
                    this.Context.Session[this.m_UniqueControlPageIndex] = value;
                }
            }
        }

        public override int PageSize
        {
            set
            {
                base.PageSize = value;
                if (!base.DesignMode)
                {
                    this.Context.Session[this.m_UniqueControlPageSize] = value;
                }
            }
        }
        [Category("自定义"), DefaultValue(""), Description("行双击时绑定的数据列"), Bindable(true)]
        public virtual string RowDblclickBoundField
        {
            get
            {
                string str = (string)this.ViewState["RowDblclickBoundField"];
                if (str != null)
                {
                    return str;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["RowDblclickBoundField"] = value;
            }
        }
        [DefaultValue(""), Bindable(true), Category("自定义"), Description("行双击时跳转的URL，可以包含{$Field}来代替绑定的数据列，比如：UserShow.aspx?UserID={$Field}")]
        public virtual string RowDblclickUrl
        {
            get
            {
                string str = (string)this.ViewState["RowDblclickUrl"];
                if (str != null)
                {
                    return str;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["RowDblclickUrl"] = value;
            }
        }
        [Bindable(true), Localizable(true), Category("自定义"), DefaultValue(""), Description("选中的数据行上显示的CSS效果")]
        public string SelectedCssClass
        {
            get
            {
                string str = (string)this.ViewState["SelectedCssClass"];
                if (str != null)
                {
                    return str;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["SelectedCssClass"] = value;
            }
        }
        public StringBuilder SelectList
        {
            get
            {
                string append = "";
                StringBuilder sb = new StringBuilder("");
                for (int i = 0; i < this.Rows.Count; i++)
                {
                    CheckBox box = (CheckBox)this.Rows[i].Cells[this.CheckBoxColumnIndex].FindControl("CheckBoxButton");
                    if (box.Checked)
                    {
                        append = this.DataKeys[i].Value.ToString();
                        AppendString(sb, append, ",");
                    }
                }
                return sb;
            }
        }
        //服务于上面的方法
        public void AppendString(StringBuilder sb, string append, string split)
        {
            if (sb.Length == 0)
            {
                sb.Append(append);
            }
            else
            {
                sb.Append(split);
                sb.Append(append);
            }
        }
        [Localizable(true), DefaultValue(0), Description("序号列的索引"), Category("自定义"), Bindable(true)]
        public int SerialColumnIndex
        {
            get
            {
                object obj2 = this.ViewState["SerialColumnIndex"];
                if (obj2 != null)
                {
                    return (int)obj2;
                }
                return 0;
            }
            set
            {
                this.ViewState["SerialColumnIndex"] = value;
            }
        }
        [DefaultValue("名次"), Description("序号列的标题文字"), Localizable(true), Category("自定义"), Bindable(true)]
        public string SerialText
        {
            get
            {
                string str = (string)this.ViewState["SerialText"];
                if (str != null)
                {
                    return str;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["SerialText"] = value;
            }
        }
        [Localizable(true), Description("是否显示控件默认的分页导航方式"), Bindable(true), Category("自定义"), DefaultValue(true)]
        public bool ShowCustomPager
        {
            get
            {
                object obj2 = this.ViewState["ShowCustomPager"];
                if (obj2 != null)
                {
                    return (bool)obj2;
                }
                return true;
            }
            set
            {
                this.ViewState["ShowCustomPager"] = value;
            }
        }
        public int VirtualItemCount
        {
            get
            {
                object obj2 = this.ViewState["VirtualItemCount"];
                if (obj2 != null)
                {
                    return (int)obj2;
                }
                return 0;
            }
        }
        [Browsable(true)]//text|dp,用到的地方少,不加枚举
        public string BoxType
        {
            set { ViewState["BoxType"] = value; }
            get { return ViewState["BoxType"] == null ? "text" : ViewState["BoxType"].ToString(); }
        }

    }//Class End;
}
