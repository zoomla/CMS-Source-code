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
using ZoomLa.Components;
using ZoomLa.BLL;

public partial class User_UserZone_School_MySchool : System.Web.UI.Page
{
    B_User ubll = new B_User();
    B_School bs = new B_School();
    B_ClassRoom bcr = new B_ClassRoom();

    protected void Page_Load(object sender, EventArgs e)
    {
        ubll.CheckIsLogin();
        if (!IsPostBack)
        {
            Bind();
        }
    }

    private void Bind()
    {
        int CPage;
        int temppage;
        DataTable dt = bcr.SelectStudentRoom(ubll.GetLogin().UserID.ToString());
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
        cc.PageSize = 5;
        cc.CurrentPageIndex = CPage - 1;
        this.DataList1.DataSource = cc;
        this.DataList1.DataBind();

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

    protected string GetSchool(int id)
    {
        return bs.GetSelect(id).SchoolName;
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Response.Redirect("?Currentpage=" + this.DropDownList1.Text);
    }
}
