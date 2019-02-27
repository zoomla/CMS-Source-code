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
using ZoomLa.Model;

public partial class User_UserZone_School_RoomNotifyList : System.Web.UI.Page
{
    B_RoomNotify brn = new B_RoomNotify();
    B_User bu = new B_User();
    B_ClassRoom cll = new B_ClassRoom();
    B_Student bs = new B_Student();
    protected string RoomName;
    protected int RoomID
    {
        get {
            if (ViewState["Roomid"] != null)
                return int.Parse(ViewState["Roomid"].ToString());
            else
                return 0;
        }
        set { ViewState["Roomid"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        bu.CheckIsLogin();
        if (!IsPostBack)
        {
            M_UserInfo mu=bu.GetLogin();
            

            if (Request.QueryString["RoomID"] != null)
            {
                RoomID = int.Parse(Request.QueryString["RoomID"].ToString ());
                //查询班级详细信息
                M_ClassRoom cr=cll.GetSelect(RoomID);
                RoomName = cr.RoomName;
                //查询用户在班级里的信息
                DataTable dt = bs.SelByURid(RoomID,1, mu.UserID);
                if (dt.Rows.Count > 0 ) 
                {
                    //判断用户是否是管理员或老师
                    if (dt.Rows[0]["StatusType"].ToString() == "2" || cr.CreateUser == mu.UserID)
                    {
                        //显示添加黑板报信息
                        trAdd.Visible = true;
                    }
                    else
                    {
                        //隐藏添加黑板报信息
                        trAdd.Visible = false;
                    }
                    Bind();
                }
                
            }
        }
    }

    private void Bind()
    {
        int CPage;
        int temppage;
        DataTable dt = brn.Sel(RoomID);
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
        this.DataList1.DataSource = cc;
        this.DataList1.DataBind();

        if (dt.Rows.Count <= 0)
            tdn.InnerHtml = "<br/>本班暂时没有任何黑板报！";
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

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Response.Redirect("?Currentpage=" + this.DropDownList1.Text);
    }
}
