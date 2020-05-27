using Controls.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Controls
{
    //为Null下不刷新Bug
    public class ExRepeater : System.Web.UI.WebControls.Repeater
    {
        [DefaultValue(""), Localizable(true), Bindable(true), Category("分页头"), Description("")]
        public string PagePre //不能带Uniquied
        {
            get { return ViewState["PagePre"].ToString(); }
            set { ViewState["PagePre"] = value; }
        }
        [DefaultValue(""), Localizable(true), Bindable(true), Category("分页尾"), Description("")]
        public string PageEnd
        {
            get { return ViewState["PageEnd"].ToString(); }
            set { ViewState["PageEnd"] = value; }
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
        [DefaultValue(10), Localizable(true), Bindable(true), Category("自定义"), Description("分页大小")]
        public int PageSize
        {
            get
            {
                int _pageSize = 1;
                if (ViewState["PageSize"] != null)
                {
                    Int32.TryParse(ViewState["PageSize"].ToString(), out _pageSize);
                    _pageSize = _pageSize < 1 ? 1 : _pageSize;
                }
                return _pageSize;
            }
            set
            {
                ViewState["PageSize"] = value;
            }
        }
        private string Unquied { get { return this.Page.GetType().Name + this.UniqueID; } }
        [Browsable(true)]//text|dp,用到的地方少,不加枚举
        public string BoxType
        {
            set { ViewState["BoxType"] = value; }
            get { return ViewState["BoxType"] == null ? "text" : ViewState["BoxType"].ToString(); }
        }
        [Bindable(true)]
        [Category("自定义信息区")]
        [Browsable(true)]
        [Description("无数据时")]
        [DefaultValue("")]
        public string EmptyText
        {
            get { return ViewState["EmptyText"] == null ? "" : (string)ViewState["EmptyText"]; }
            set { ViewState["EmptyText"] = value; }
        }
        //--------------------
        public int CPage
        {
            get
            {
                int result = 1;
                if (ViewState[this.UniqueID + "_PageIndex"] != null)
                {
                    Int32.TryParse(ViewState[this.UniqueID + "_PageIndex"].ToString(), out result);
                }
                result = result < 1 ? 1 : result;
                result = result > PageCount ? PageCount : result;
                return result;
            }
            set
            {
                if (!base.DesignMode)
                {
                    ViewState[this.UniqueID + "_PageIndex"] = value;
                }
            }
        }
        private int ItemCount
        {
            get
            {
                int _pageSize = 1;
                if (ViewState["ItemCount"] != null)
                {
                    Int32.TryParse(ViewState["ItemCount"].ToString(), out _pageSize);
                    _pageSize = _pageSize < 1 ? 1 : _pageSize;
                }
                return _pageSize;
            }
            set
            {
                ViewState["ItemCount"] = value;
            }
        }
        private int PageCount
        {
            get
            {
                return ItemCount % PageSize > 0 ? (ItemCount / PageSize) + 1 : ItemCount / PageSize;
            }
        }
        public DataTable MyDataSource
        {
            get
            {
                if (DataSource != null)
                { this.Context.Session[Unquied + "_Session"] = DataSource; }
                return this.Context.Session[Unquied + "_Session"] as DataTable;
            }
            set { this.Context.Session[Unquied + "_Session"] = value; }
        }
        //--------------重写事件
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }
        protected override void OnItemCreated(RepeaterItemEventArgs e)
        {
            base.OnItemCreated(e);
            if (e.Item.ItemType == ListItemType.Footer)
            { this.CreateCustomPagerRow(e); }
        }
        public override void DataBind()
        {
            if (MyDataSource == null || MyDataSource.Rows.Count < 1)
            {
                base.DataSource = null;
                MyDataSource = null; base.DataBind();
                return;
            }
            ItemCount = MyDataSource.Rows.Count;
            int temp = 0;
            base.DataSource = GetPageDT(PageSize, CPage, MyDataSource, out temp);
            base.DataBind();
        }
        //------------支持方法 
        public delegate void TxtPageSizeChange(string size);
        public TxtPageSizeChange txtFunc = null;//用户可自定义pageSize处理
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
                this.CPage = 0;//改变后回到首页
                box.Text = pageSize.ToString();
            }
            this.DataBind();
        }
        protected void TxtMaxPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            int pageSize;
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
                this.CPage = 0;//改变后回到首页
                //box.Text = pageSize.ToString();
            }
            this.DataBind();
        }

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
            this.CPage = pageCount - 1;
            box.Text = pageCount.ToString();
            this.DataBind();
        }
        //分页下拉框的处理
        protected void DropCurrentPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList list = (DropDownList)sender;
            this.CPage = Convert.ToInt32(list.SelectedValue);
            //if (this.IsHoldState)//自定义,是否保存当前状态,即保持下拉框选页后的PageIndex,这里应该用true
            //{
            //    this.Context.Session[this.m_UniqueControlPageIndex] = this.PageIndex;
            //}
            this.DataBind();
        }
        //页面发生变更时执行的方法
        protected void LbtnFirst_Click(object sender, EventArgs e)
        {
            CPage = 1;
            this.DataBind();
        }

        protected void LbtnLast_Click(object sender, EventArgs e)
        {
            CPage = PageCount;
            this.DataBind();
        }

        protected void LbtnNext_Click(object sender, EventArgs e)
        {
            CPage++;
            this.DataBind();
        }

        protected void LbtnPrev_Click(object sender, EventArgs e)
        {
            CPage = this.CPage - 1;
            this.DataBind();
        }
        //生成分页栏,支持两种分页开形式
        private void CreateCustomPagerRow(RepeaterItemEventArgs e)
        {
            //e.Item.Controls.Clear();
            Panel pagePanel = new Panel();
            e.Item.Controls.Add(pagePanel);
            pagePanel.Controls.Add(new LiteralControl(PagePre + "共&nbsp;"));
            Label child = new Label();
            child.ID = "LblRowsCount";
            child.Text = this.ItemCount.ToString();
            child.Font.Bold = true;
            pagePanel.Controls.Add(child);
            pagePanel.Controls.Add(new LiteralControl("&nbsp;" + this.ItemUnit + this.ItemName));
            pagePanel.Controls.Add(new LiteralControl("&nbsp;&nbsp;"));
            LinkButton button = new LinkButton();
            button.CommandName = "Page";
            button.CommandArgument = "First";
            button.Enabled = this.CPage != 1;
            if (!button.Enabled) button.Style.Add("color", "gray");
            button.Text = Resources.ExtendedGridView_FirstPageText;
            pagePanel.Controls.Add(button);
            pagePanel.Controls.Add(new LiteralControl("&nbsp;"));
            LinkButton button2 = new LinkButton();
            button2.CommandName = "Page";
            button2.CommandArgument = "Prev";
            button2.Enabled = this.CPage != 1;
            if (!button2.Enabled) button2.Style.Add("color", "gray");
            button2.Text = Resources.ExtendedGridView_PrevPageText;
            pagePanel.Controls.Add(button2);
            pagePanel.Controls.Add(new LiteralControl("&nbsp;"));
            LinkButton button3 = new LinkButton();
            button3.CommandName = "Page";
            button3.CommandArgument = "Next";
            button3.Enabled = this.CPage < PageCount;
            if (!button3.Enabled) button3.Style.Add("color", "gray");
            button3.Text = Resources.ExtendedGridView_NextPageText;
            pagePanel.Controls.Add(button3);
            pagePanel.Controls.Add(new LiteralControl("&nbsp;"));
            LinkButton button4 = new LinkButton();
            button4.CommandName = "Page";
            button4.CommandArgument = "Last";
            button4.Enabled = this.CPage < PageCount;
            if (!button4.Enabled) button4.Style.Add("color", "gray");
            button4.Text = Resources.ExtendedGridView_LastPageText;
            pagePanel.Controls.Add(button4);
            pagePanel.Controls.Add(new LiteralControl("&nbsp;&nbsp;"));
            button.Click += new EventHandler(this.LbtnFirst_Click);//指定分页事件处理方法,首页,上一页,下一页,最后一页
            button2.Click += new EventHandler(this.LbtnPrev_Click);
            button3.Click += new EventHandler(this.LbtnNext_Click);
            button4.Click += new EventHandler(this.LbtnLast_Click);
            pagePanel.Controls.Add(new LiteralControl(Resources.ExtendedGridView_PageNun));
            Label label2 = new Label();
            label2.Text = Convert.ToString((int)(this.CPage));
            label2.Font.Bold = true;
            label2.ForeColor = Color.Red;
            pagePanel.Controls.Add(label2);
            pagePanel.Controls.Add(new LiteralControl("/"));
            Label label3 = new Label();
            label3.Text = Convert.ToString(this.PageCount);
            label3.Font.Bold = true;
            pagePanel.Controls.Add(label3);
            pagePanel.Controls.Add(new LiteralControl(Resources.ExtendedGridView_Page));
            pagePanel.Controls.Add(new LiteralControl("&nbsp;&nbsp;"));


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
                        pagePanel.Controls.Add(box);
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
                        pagePanel.Controls.Add(box);
                    }
                    break;
            }
            pagePanel.Controls.Add(new LiteralControl(this.ItemUnit + this.ItemName + Resources.ExtendedGridView_Page2));
            pagePanel.Controls.Add(new LiteralControl("&nbsp;&nbsp;"));
            pagePanel.Controls.Add(new LiteralControl(Resources.ExtendedGridView_JumpTurn));
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
                list.SelectedValue = this.CPage.ToString();
                pagePanel.Controls.Add(list);
            }
            else
            {
                TextBox box2 = new TextBox();
                box2.ApplyStyleSheetSkin(this.Page);
                box2.Width = 30;
                box2.Text = (this.CPage + 1).ToString();
                box2.AutoPostBack = true;
                box2.TextChanged += new EventHandler(this.TxtCurrentPage_TextChanged);
                pagePanel.Controls.Add(box2);
            }
            pagePanel.Controls.Add(new LiteralControl(Resources.ExtendedGridView_Page3 + PageEnd));
        }
        //----------------Tools
        public object GetPageDT(int pageSize, int pageIndex, DataTable dt, out int pageCount)
        {
            //先临时实现，后再切换为直接取对应数据的
            PagedDataSource pds = new PagedDataSource();
            pds.AllowPaging = true;
            pds.DataSource = dt.DefaultView;
            pds.PageSize = pageSize;
            pds.CurrentPageIndex = pageIndex < 1 ? 1 : (pageIndex - 1);
            pageCount = GetPageCount(pageSize, dt.Rows.Count);
            return pds;
        }
        public int GetPageCount(int itemCount, int pageSize) { return itemCount / pageSize + ((itemCount % pageSize > 0) ? 1 : 0); }
    }
}
