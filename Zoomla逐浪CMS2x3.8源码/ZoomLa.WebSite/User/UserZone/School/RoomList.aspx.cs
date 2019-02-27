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

public partial class User_UserZone_School_RoomList : System.Web.UI.Page
{
    B_User ubll = new B_User();
    B_ClassRoom bcr = new B_ClassRoom();
    B_School bs = new B_School();
    public string sid = "";
    protected string SchoolName;
    protected int SchoolID
    {
        get
        {
            if (ViewState["SchoolID"] != null)
                return int.Parse(ViewState["SchoolID"].ToString());
            else
                return 0;
        }
        set { ViewState["SchoolID"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        ubll.CheckIsLogin();
        if (!IsPostBack)
        {
            if (Request.QueryString["schoolid"]!=null)
            {
                SchoolID =int.Parse(Request.QueryString["schoolid"].ToString());
                SchoolName = bs.GetSelect(SchoolID).SchoolName;
                Bind(bcr.SelBySchID(SchoolID));
            }
        }
    }
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
        cc.PageSize = 30;
        cc.CurrentPageIndex = CPage - 1;
        this.dlClassRoom.DataSource = cc;
        this.dlClassRoom.DataBind();

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

        Toppage.Text = "<a href=?schoolid=" + SchoolID + "&Currentpage=0>首页</a>";
        Nextpage.Text = "<a href=?schoolid=" + SchoolID + "&Currentpage=" + nextpagenum.ToString() + ">上一页</a>";
        Downpage.Text = "<a href=?schoolid=" + SchoolID + "&Currentpage=" + downpagenum.ToString() + ">下一页</a>";
        Endpage.Text = "<a href=?schoolid=" + SchoolID + "&Currentpage=" + Endpagenum.ToString() + ">尾页</a>";
        Nowpage.Text = CPage.ToString();
        PageSize.Text = thispagenull.ToString();
        pagess.Text = cc.PageSize.ToString();
        for (int i = 1; i <= thispagenull; i++)
        {
            DropDownList1.Items.Add(i.ToString());
        }
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Response.Redirect("?schoolid=" + SchoolID + "&Currentpage=" + this.DropDownList1.Text);
    }
    protected void HiddenField1_ValueChanged(object sender, EventArgs e)
    {
        Response.Write("<script>location.href='MySchoolList.aspx'</script>");
    }
}
