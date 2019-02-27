using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZoomLa.BLL;

public partial class manage_User_ServiceInfo : CustomerPageAction
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ZoomLa.Common.function.AccessRulo();
        B_Admin badmin = new B_Admin();
        badmin.CheckIsLogin();
        Call.SetBreadCrumb(Master,"<li>后台管理</li><li>客服管理</li><li><a href='CustomerService.aspx'>客服信息</a></li><li>查询客服记录</li>");
    }
    //分页绑定数据
    private void Bind(DataTable dt)
    {
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
        PagedDataSource cc = new PagedDataSource();
        cc.DataSource = dt.DefaultView;
        cc.AllowPaging = true;
        cc.PageSize = 20;
        cc.CurrentPageIndex = CPage - 1;
        this.rpServiceInfo.DataSource = cc;
        this.rpServiceInfo.DataBind();

        Allnum.Text = dt.DefaultView.Count.ToString();
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
        for (int i = 1; i <= thispagenull; i++)
        {
            DropDownList1.Items.Add(i.ToString());
        }

    }
}
