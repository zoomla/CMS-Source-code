using System;
using System.Data;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;


namespace ZoomLaCMS.Manage.Shop
{
    public partial class PresentManage : CustomerPageAction
    {
        private B_Model bmode = new B_Model();

        protected void Page_Load(object sender, EventArgs e)
        {
            B_Admin badmin = new B_Admin();

            Call.SetBreadCrumb(Master, "<li>商城管理</li><li>促销管理</li><li>促销商品管理</li>");
            int CPage;
            int temppage;

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


            DataTable Cll = null;
            PagedDataSource cc = new PagedDataSource();
            cc.DataSource = Cll.DefaultView;
            cc.AllowPaging = true;
            cc.PageSize = 10;
            cc.CurrentPageIndex = CPage - 1;
            Presetlist.DataSource = cc;
            Presetlist.DataBind();

            Allnum.Text = Cll.DefaultView.Count.ToString();
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
            Toppage.Text = "<a href=?Currentpage=0>首页</a>";
            Nextpage.Text = "<a href=?Currentpage=" + nextpagenum.ToString() + ">上一页</a>";
            Downpage.Text = "<a href=?Currentpage=" + downpagenum.ToString() + ">下一页</a>";
            Endpage.Text = "<a href=?Currentpage=" + Endpagenum.ToString() + ">尾页</a>";
            Nowpage.Text = CPage.ToString();
            PageSize.Text = thispagenull.ToString();
            pagess.Text = cc.PageSize.ToString();

            if (!this.Page.IsPostBack)
            {
                for (int i = 1; i <= thispagenull; i++)
                {
                    DropDownList1.Items.Add(i.ToString());
                }
            }

            string menu = Request.QueryString["menu"];
            if (menu == "del")
            {
                int id = DataConverter.CLng(Request.QueryString["id"]);
            }

        }

        public string getformat(string ShiPrice, string Linprice)
        {
            double shi, lin;
            string strshi, strlin, tempstr;
            shi = System.Math.Round(DataConverter.CDouble(ShiPrice), 2);
            lin = System.Math.Round(DataConverter.CDouble(Linprice), 2);
            strshi = shi.ToString();
            strlin = lin.ToString();
            tempstr = "原价：" + strshi + "<br />现价：" + strlin;
            return tempstr;
        }
        protected void Button1_Click(object sender, EventArgs e)
        {

            string itemlist = Request.Form["Item"];
        }
    }
}