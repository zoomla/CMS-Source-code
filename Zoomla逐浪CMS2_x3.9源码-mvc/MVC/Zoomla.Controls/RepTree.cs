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
    public class RepTree : System.Web.UI.WebControls.Repeater
    {
        public override void DataBind()
        {
            if (SPage != null)
            {
                this.DataSource = SPage(PageSize, (PageIndex + 1));
            }
            base.DataBind();
        }
        public int PageIndex
        {
            get
            {
                int result = 0;
                if (ViewState[this.UniqueID + "_PageIndex"] != null)
                {
                    Int32.TryParse(ViewState[this.UniqueID + "_PageIndex"].ToString(), out result);
                }
                result = result < 0 ? 0 : result;
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
        //总页数
        private int PageCount
        {
            get
            {
                return ItemCount % PageSize > 0 ? (ItemCount / PageSize) + 1 : ItemCount / PageSize;
            }
        }
        //总记录数
        public int ItemCount
        {
            get
            {
                int result = 0;
                if (ViewState[this.UniqueID + "ItemCount"] != null)
                {
                    Int32.TryParse(ViewState[this.UniqueID + "ItemCount"].ToString(), out result);
                }
                return result;
            }
            set { ViewState[this.UniqueID + "ItemCount"] = value; }
        }
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
        public delegate DataTable SelPage(int pageSize, int pageIndex);
        public delegate DataTable GetChildS(int pid);
        public SelPage SPage;//用来接收分页信息
        public GetChildS GetChild;

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
                this.PageIndex = 0;//改变后回到首页
                box.Text = pageSize.ToString();
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
            this.PageIndex = pageCount - 1;
            box.Text = pageCount.ToString();
            this.DataBind();
            //if (this.IsHoldState)
            //{
            //    this.Context.Session[this.m_UniqueControlPageIndex] = this.PageIndex;
            //}
            //OnPageIndexChanging(this.PageIndex);
        }
        //分页下拉框的处理
        protected void DropCurrentPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList list = (DropDownList)sender;
            this.PageIndex = Convert.ToInt32(list.SelectedValue) - 1;
            //if (this.IsHoldState)//自定义,是否保存当前状态,即保持下拉框选页后的PageIndex,这里应该用true
            //{
            //    this.Context.Session[this.m_UniqueControlPageIndex] = this.PageIndex;
            //}
            this.DataBind();
        }
        //页面发生变更时执行的方法
        protected void LbtnFirst_Click(object sender, EventArgs e)
        {
            PageIndex = 0;
            this.DataBind();
        }

        protected void LbtnLast_Click(object sender, EventArgs e)
        {
            PageIndex = PageCount - 1;
            this.DataBind();
        }

        protected void LbtnNext_Click(object sender, EventArgs e)
        {
            PageIndex = PageIndex + 1;
            this.DataBind();
        }

        protected void LbtnPrev_Click(object sender, EventArgs e)
        {
            PageIndex = this.PageIndex - 1;
            this.DataBind();
        }
        //生成分页栏,加上配置项，让其可自定义前与后
        private void CreateCustomPagerRow(RepeaterItemEventArgs e)
        {
            e.Item.Controls.Clear();
            e.Item.Controls.Add(new LiteralControl(PagePre + "共&nbsp;"));
            Label child = new Label();
            child.ID = "LblRowsCount";
            child.Text = this.ItemCount.ToString();
            child.Font.Bold = true;
            e.Item.Controls.Add(child);
            e.Item.Controls.Add(new LiteralControl("&nbsp;" + this.ItemUnit + this.ItemName));
            e.Item.Controls.Add(new LiteralControl("&nbsp;&nbsp;"));
            LinkButton button = new LinkButton();
            button.CommandName = "Page";
            button.CommandArgument = "First";
            button.Enabled = this.PageIndex != 0;
            if (!button.Enabled) button.Style.Add("color", "gray");
            button.Text = Resources.ExtendedGridView_FirstPageText;
            e.Item.Controls.Add(button);
            e.Item.Controls.Add(new LiteralControl("&nbsp;"));
            LinkButton button2 = new LinkButton();
            button2.CommandName = "Page";
            button2.CommandArgument = "Prev";
            button2.Enabled = this.PageIndex != 0;
            if (!button2.Enabled) button2.Style.Add("color", "gray");
            button2.Text = Resources.ExtendedGridView_PrevPageText;
            e.Item.Controls.Add(button2);
            e.Item.Controls.Add(new LiteralControl("&nbsp;"));
            LinkButton button3 = new LinkButton();
            button3.CommandName = "Page";
            button3.CommandArgument = "Next";
            button3.Enabled = this.PageIndex != (this.PageCount - 1);
            if (!button3.Enabled) button3.Style.Add("color", "gray");
            button3.Text = Resources.ExtendedGridView_NextPageText;
            e.Item.Controls.Add(button3);
            e.Item.Controls.Add(new LiteralControl("&nbsp;"));
            LinkButton button4 = new LinkButton();
            button4.CommandName = "Page";
            button4.CommandArgument = "Last";
            button4.Enabled = this.PageIndex != (this.PageCount - 1);
            if (!button4.Enabled) button4.Style.Add("color", "gray");
            button4.Text = Resources.ExtendedGridView_LastPageText;
            e.Item.Controls.Add(button4);
            e.Item.Controls.Add(new LiteralControl("&nbsp;&nbsp;"));
            button.Click += new EventHandler(this.LbtnFirst_Click);//指定分页事件处理方法,首页,上一页,下一页,最后一页
            button2.Click += new EventHandler(this.LbtnPrev_Click);
            button3.Click += new EventHandler(this.LbtnNext_Click);
            button4.Click += new EventHandler(this.LbtnLast_Click);
            e.Item.Controls.Add(new LiteralControl(Resources.ExtendedGridView_PageNun));
            Label label2 = new Label();
            label2.Text = Convert.ToString((int)(this.PageIndex + 1));
            label2.Font.Bold = true;
            label2.ForeColor = Color.Red;
            e.Item.Controls.Add(label2);
            e.Item.Controls.Add(new LiteralControl("/"));
            Label label3 = new Label();
            label3.Text = Convert.ToString(this.PageCount);
            label3.Font.Bold = true;
            e.Item.Controls.Add(label3);
            e.Item.Controls.Add(new LiteralControl(Resources.ExtendedGridView_Page));
            e.Item.Controls.Add(new LiteralControl("&nbsp;&nbsp;"));
            TextBox box = new TextBox();
            box.ApplyStyleSheetSkin(this.Page);
            box.MaxLength = 3;
            box.Width = 0x16;
            box.Text = Convert.ToString(this.PageSize);
            box.AutoPostBack = true;
            box.TextChanged += new EventHandler(this.TxtMaxPerPage_TextChanged);
            e.Item.Controls.Add(box);
            e.Item.Controls.Add(new LiteralControl(this.ItemUnit + this.ItemName + Resources.ExtendedGridView_Page2));
            e.Item.Controls.Add(new LiteralControl("&nbsp;&nbsp;"));
            e.Item.Controls.Add(new LiteralControl(Resources.ExtendedGridView_JumpTurn));
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
                e.Item.Controls.Add(list);
            }
            else
            {
                TextBox box2 = new TextBox();
                box2.ApplyStyleSheetSkin(this.Page);
                box2.Width = 30;
                box2.Text = Convert.ToString((int)(this.PageIndex + 1));
                box2.AutoPostBack = true;
                box2.TextChanged += new EventHandler(this.TxtCurrentPage_TextChanged);
                e.Item.Controls.Add(box2);
            }
            e.Item.Controls.Add(new LiteralControl(Resources.ExtendedGridView_Page3 + PageEnd));
        }
    }
}
