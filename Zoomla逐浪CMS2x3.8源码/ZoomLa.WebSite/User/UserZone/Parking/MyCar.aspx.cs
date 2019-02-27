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
using ZoomLa.Sns;
using System.Collections.Generic;
using ZoomLa.BLL;

public partial class User_UserZone_Parking_MyCar : Page
{
    Parking_BLL pbll = new Parking_BLL();
    B_User buser = new B_User();
    int currentUser = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        currentUser = buser.GetLogin().UserID;
        if (currentUser == 0)
            Page.Response.Redirect(@"~/user/login.aspx");
        if (!IsPostBack)
        {
            Bind();
            ViewState["index"] = Request.QueryString["index"].ToString ();
        }
    }

    private void Bind()
    {
        int CPage;
        int temppage;
        List<ZL_Sns_MyCar> list = pbll.GetMyCarList(currentUser);
        temppage = Convert.ToInt32(Request.QueryString["CurrentPage"]);
        CPage = temppage;
        if (CPage <= 0)
        {
            CPage = 1;
        }
        PagedDataSource cc = new PagedDataSource();
        cc.DataSource = list;
        cc.AllowPaging = true;
        cc.PageSize = 2;
        cc.CurrentPageIndex = CPage - 1;
        DataList1.DataSource = cc;
        DataList1.DataBind();

        Allnum.Text = list.Count.ToString();
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

        Toppage.Text = "<a href=MyCar.aspx?Currentpage=0>首页</a>";
        Nextpage.Text = "<a href=MyCar.aspx?Currentpage=" + nextpagenum.ToString() + ">上一页</a>";
        Downpage.Text = "<a href=MyCar.aspx?Currentpage=" + downpagenum.ToString() + ">下一页</a>";
        Endpage.Text = "<a href=MyCar.aspx?Currentpage=" + Endpagenum.ToString() + ">尾页</a>";
        Nowpage.Text = CPage.ToString();
        PageSize.Text = thispagenull.ToString();
        pagess.Text = cc.PageSize.ToString();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Request.Form["car"] != null)
        {
            Page.ClientScript.RegisterStartupScript(typeof(string), "TabJs", "<script language='javascript'>window.returnVal='" + Request.Form["car"] + "," + ViewState["index"].ToString () + ",';window.parent.hidePopWin(true);</script>");
        }
    }
}
