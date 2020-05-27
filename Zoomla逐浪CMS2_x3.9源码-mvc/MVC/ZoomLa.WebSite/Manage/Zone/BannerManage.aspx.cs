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
using ZoomLa.Common;
using ZoomLa.Web;
using ZoomLa.BLL;
using System.Collections.Generic;
using ZoomLa.Model;
using ZoomLa.Sns;

namespace ZoomLaCMS.Manage.Zone
{
    public partial class BannerManage : CustomerPageAction
    {
       
    #region 业务对象
    B_StoreStyleTable sstbll = new B_StoreStyleTable();
    B_Model mbll = new B_Model();
    B_Admin badmin = new B_Admin();
    B_Content cbll = new B_Content();
    BlogStyleTableBLL bstbll = new BlogStyleTableBLL();
    #endregion

    #region 初始化
    protected void Page_Load(object sender, EventArgs e)
    {
        ZoomLa.Common.function.AccessRulo();
        //if (!badmin.ChkPermissions("ZoneParking"))
        //{
        //    function.WriteErrMsg("没有权限进行此项操作");
        //}
        B_Admin badmin = new B_Admin();
        badmin.CheckIsLogin();
        Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='ContentManage.aspx'>内容管理</a></li><li>栏目管理</li>");
        if (!IsPostBack)
        {
            GetInit();
        }
    }
    #endregion

    #region 页面方法
    //初始化
    private void GetInit()
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
        cc.AllowPaging = true;
        cc.PageSize = 10;
        cc.CurrentPageIndex = CPage - 1;
        Productlist.DataSource = cc;
        Productlist.DataBind();

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


    protected string GetState(string gid)
    {
        switch (gid)
        {
            case "0": return "不显示";
            case "1": return "显示";
            default: return "";
        }
    }

    protected string GetIndex(string gid)
    {
        switch (gid)
        {
            case "0": return "分栏目";
            case "1": return "首页栏目";
            default: return "";
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        LinkButton lb = sender as LinkButton;
        GetInit();
    }
    protected void SaveBtn_Click(object sender, EventArgs e)
    {
        string list = Request.Form["Item"];
        if (!string.IsNullOrEmpty(list))
        {
            Button bt = sender as Button;
            if (bt.CommandName == "5")
            {
            }
            else
            {
            }
        }
        GetInit();
    }
    #endregion
    }
}