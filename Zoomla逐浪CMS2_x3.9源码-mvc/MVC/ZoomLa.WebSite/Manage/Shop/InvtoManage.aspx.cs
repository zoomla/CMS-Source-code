using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Components;
using System.Data;

namespace ZoomLaCMS.Manage.Shop
{
    public partial class InvtoManage : CustomerPageAction
    {
        B_InvtoType bll = new B_InvtoType();
        B_Admin badmin = new B_Admin();

        protected void Page_Load(object sender, EventArgs e)
        {
            ZoomLa.Common.function.AccessRulo();
            B_Admin badmin = new B_Admin();


            if (!IsPostBack)
            {
                bind();
            }
            string menu = Request.QueryString["menu"];
            int id = DataConverter.CLng(Request.QueryString["id"]);
            if (menu == "del")
            {
                if (bll.DeleteByGroupID(id))
                {
                    Response.Write("<script language=javascript>alert('删除成功!');location.href='InvtoManage.aspx';</script>");
                }
                else
                {
                    Response.Write("<script language=javascript>alert('删除失败!');location.href='InvtoManage.aspx';</script>");
                }
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='ProductManage.aspx'>商城管理</a></li> <li><a href='DeliverType.aspx'>商城设置</a></li><li>发票类型管理  [<a href='AddInvoType.aspx'>添加发票类型</a>] </li>");
        }


        private void bind()
        {
            int CPage, temppage = 0;

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
            DataTable cc = bll.Select_All();

            PagedDataSource pag = new PagedDataSource();

            pag.DataSource = cc.DefaultView;
            pag.AllowPaging = true;
            if (Request.QueryString["txtPage"] != null && Request.QueryString["txtPage"] != "")
            {
                pag.PageSize = Convert.ToInt32(Request.QueryString["txtPage"]);
            }
            if (Request.Form["txtPage"] != null && Request.Form["txtPage"] != "")
            {
                pag.PageSize = Convert.ToInt32(Request.Form["txtPage"]);
            }
            pag.CurrentPageIndex = CPage - 1;
            if (cc != null && pag.PageSize >= cc.Rows.Count)
            {
                pag.CurrentPageIndex = 0;
                CPage = 1;
            }
            Repeater1.DataSource = pag;
            Repeater1.DataBind();

            int thispagenull = pag.PageCount;//总页数
            int CurrentPage = pag.CurrentPageIndex;
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

            Allnum.Text = cc.DefaultView.Count.ToString();
            Toppage.Text = "<a href=?Currentpage=0&txtPage=" + pag.PageSize + "> 首页</a>";
            Nextpage.Text = "<a href=?Currentpage=" + nextpagenum.ToString() + "&txtPage=" + pag.PageSize + ">上一页</a>";
            Downpage.Text = "<a href=?Currentpage=" + downpagenum.ToString() + "&txtPage=" + pag.PageSize + ">下一页</a>";
            Endpage.Text = "<a href=?Currentpage=" + Endpagenum.ToString() + "&txtPage=" + pag.PageSize + ">尾页</a>";
            Nowpage.Text = CPage.ToString();
            PageSize.Text = thispagenull.ToString();
            txtPage.Text = pag.PageSize.ToString();
            pagess.Text = pag.PageSize.ToString();
            DropDownList1.Items.Clear();
            for (int i = 1; i <= thispagenull; i++)
            {
                this.DropDownList1.Items.Add(i.ToString());
            }
            for (int j = 0; j < DropDownList1.Items.Count; j++)
            {
                if (DropDownList1.Items[j].Value == Nowpage.Text)
                {
                    DropDownList1.SelectedValue = Nowpage.Text;
                    break;
                }
            }

        }

        protected void txtPage_TextChanged(object sender, EventArgs e)
        {
            ViewState["page"] = "1";
            bind();
        }
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            bind();
        }
    }
}