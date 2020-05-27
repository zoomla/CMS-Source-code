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
using ZoomLa.Web;
using ZoomLa.Common;
using ZoomLa.Model;

namespace ZoomLaCMS.Manage.Shop
{
    public partial class ModelManage :CustomerPageAction
    {
        private B_Model bll = new B_Model();
        protected int Stocktype;
        private void DataBaseList()
        {
            int CPage;
            int temppage;
            int pageid;
            pageid = DataConverter.CLng(Request.QueryString["id"]);
            Stocktype = DataConverter.CLng(base.Request.QueryString["Stocktype"]);
            if (Request.Form["DropDownList1"] != null)
            {
                temppage = DataConverter.CLng(Request.Form["DropDownList1"]);
            }
            else
            {
                temppage = DataConverter.CLng(Request.QueryString["CurrentPage"]);
            }
            CPage = temppage;
            if (CPage <= 0)
            {
                CPage = 1;
            }

            DataTable Cll = bll.GetListShop();
            PagedDataSource cc = new PagedDataSource();
            cc.DataSource = Cll.DefaultView;
            cc.PageSize = 20;
            if (Request.QueryString["txtPage"] != null && Request.QueryString["txtPage"] != "")
            {
                cc.PageSize = DataConverter.CLng(Request.QueryString["txtPage"]);
            }
            if (Request.Form["txtPage"] != null && Request.Form["txtPage"] != "")
            {
                cc.PageSize = DataConverter.CLng(Request.Form["txtPage"]);
            }
            cc.AllowPaging = true;
            cc.CurrentPageIndex = CPage - 1;
            Repeater1.DataSource = cc;
            Repeater1.DataBind();

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
            txtPage.Text = cc.PageSize.ToString();
            Toppage.Text = "<a href=?txtPage=" + txtPage.Text + "&Stocktype=" + this.Stocktype + "&Currentpage=0>首页</a>";
            Nextpage.Text = "<a href=?txtPage=" + txtPage.Text + "&Stocktype=" + this.Stocktype + "&Currentpage=" + nextpagenum.ToString() + ">上一页</a>";
            Downpage.Text = "<a href=?txtPage=" + txtPage.Text + "&Stocktype=" + this.Stocktype + "&Currentpage=" + downpagenum.ToString() + ">下一页</a>";
            Endpage.Text = "<a href=?txtPage=" + txtPage.Text + "&Stocktype=" + this.Stocktype + "&Currentpage=" + Endpagenum.ToString() + ">尾页</a>";
            Nowpage.Text = CPage.ToString();
            this.txtPage.Text = cc.PageSize.ToString();
            if (!this.Page.IsPostBack)
            {
                for (int i = 1; i <= thispagenull; i++)
                {
                    DropDownList1.Items.Add(i.ToString());
                }

            }
        }
        public string GetIcon(string IconPath)
        {
            return "../../Images/ModelIcon/" + (string.IsNullOrEmpty(IconPath) ? "Default.gif" : IconPath);
        }
        protected void Repeater1_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                string Id = e.CommandArgument.ToString();
                Response.Redirect("Model.aspx?ModelID=" + Id);
            }
            if (e.CommandName == "Del")
            {
                string Id = e.CommandArgument.ToString();
                this.bll.DelModel(int.Parse(Id));
                DataBaseList();
            }
            if (e.CommandName == "Field")
            {
                string Id = e.CommandArgument.ToString();
                Response.Redirect("ModelField.aspx?ModelID=" + Id);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            ZoomLa.Common.function.AccessRulo();
            if (!this.Page.IsPostBack)
            {
                B_Admin badmin = new B_Admin();

                if (!B_ARoleAuth.Check(ZLEnum.Auth.shop, "ShopModelManage"))
                {
                    function.WriteErrMsg("没有权限进行此项操作");
                }
                DataBaseList();
            }
        }
        protected void txtPage_TextChanged(object sender, EventArgs e)
        {
            ViewState["page"] = "1";
            this.DataBaseList();
        }
    }
}