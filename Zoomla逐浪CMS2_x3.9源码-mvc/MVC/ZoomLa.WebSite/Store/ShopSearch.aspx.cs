using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ZoomLa.BLL;
using ZoomLa.Components;

namespace ZoomLaCMS.Store
{
    public partial class ShopSearch : System.Web.UI.Page
    {
        private B_Node bnode = new B_Node();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.sitename.Text = SiteConfig.SiteInfo.SiteName;
                getnodelist(0);
                SetType();
            }
        }

        //初始化查询分类
        protected void SetType()
        {
            cblCounmType.Items.Add(new ListItem("条形码", "BarCode"));
            cblCounmType.Items.Add(new ListItem("商品简介", "Proinfo"));
            cblCounmType.Items.Add(new ListItem("厂商", "Producer"));
            cblCounmType.Items.Add(new ListItem("团购", "Colonel1"));
        }

        //搜索
        protected int listnum = -1;
        protected void getnodelist(int nodeid)
        {
            //初始化分类信息
            DataTable nodetable = this.bnode.SelByNode(nodeid);
            listnum = listnum + 1;
            string spans = new string('　', listnum);
            for (int i = 0; i < nodetable.Rows.Count; i++)
            {
                this.DDLNode.Items.Add(new ListItem(spans + nodetable.Rows[i]["nodename"].ToString(), nodetable.Rows[i]["nodeid"].ToString()));
                getnodelist(Convert.ToInt32(nodetable.Rows[i]["nodeid"].ToString()));
            }
            listnum = listnum - 1;
        }

        private int GetValue(string value)
        {
            if (cblCounmType.Items.FindByValue(value).Selected)
                return 1;
            else
                return 0;
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            int CPage;
            int temppage;

            DataTable list = new DataTable();
            // list = bs.GetUserShopSearch(GetNode(), GetValue("BarCode"), GetValue("Proinfo"), GetValue("Producer"), GetValue("Colonel1"), GetValue("Colonel2"), txtStartTime.Text.Trim(), txtEndTime.Text.Trim(), txtProducer.Text.Trim());


            if (Request.Form["DropDownList1"] != null)
            {
                temppage = Convert.ToInt32(Request.Form["DropDownList1"]);
            }
            else
            {
                temppage = Convert.ToInt32(Request.QueryString["CurrentPage"]);
            }
            CPage = temppage;
            if (CPage <= 0)
            {
                CPage = 1;
            }
            PagedDataSource cc = new PagedDataSource();
            cc.DataSource = list.DefaultView;
            cc.AllowPaging = true;
            cc.PageSize = 6;
            cc.CurrentPageIndex = CPage - 1;
            Repeater1.DataSource = cc;
            Repeater1.DataBind();

            Allnum.Text = list.DefaultView.Count.ToString();
            int thispagenull = cc.PageCount;//总页数
            int CurrentPage = cc.CurrentPageIndex;
            int nextpagenum = CPage - 1;//上一页
            int downpagenum = CPage + 1;//下一页
            int Endpagenum = thispagenull;
            if (thispagenull <= CPage)
            {
                downpagenum = thispagenull;
                Downpage.Enabled = false;
            }
            else
            {
                Downpage.Enabled = true;
            }
            if (nextpagenum <= 0)
            {
                nextpagenum = 0;
                Nextpage.Enabled = false;
            }
            else
            {
                Nextpage.Enabled = true;
            }

            Toppage.Text = "<a href=ShopSearch.aspx?Currentpage=0>首页</a>";
            Nextpage.Text = "<a href=ShopSearch.aspx?Currentpage=" + nextpagenum.ToString() + ">上一页</a>";
            Downpage.Text = "<a href=ShopSearch.aspx?Currentpage=" + downpagenum.ToString() + ">下一页</a>";
            Endpage.Text = "<a href=ShopSearch.aspx?Currentpage=" + Endpagenum.ToString() + ">尾页</a>";
            Nowpage.Text = CPage.ToString();
            PageSize.Text = thispagenull.ToString();
            pagess.Text = cc.PageSize.ToString();
            for (int i = 1; i <= thispagenull; i++)
            {
                DropDownList1.Items.Add(i.ToString());
            }
        }

        protected string GetNode()
        {
            string str = "";
            for (int i = 0; i < DDLNode.Items.Count; i++)
            {
                if (DDLNode.Items[i].Selected == true)
                    str = str + DDLNode.Items[i].Value + ",";
            }
            if (str.EndsWith(","))
                str = str.Substring(0, str.Length - 1);
            return str;
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect("ShopSearch.aspx?Currentpage=" + this.DropDownList1.Text);
        }
    }
}