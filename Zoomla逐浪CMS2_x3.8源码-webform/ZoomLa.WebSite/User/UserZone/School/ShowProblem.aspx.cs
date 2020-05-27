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

public partial class User_UserZone_School_ShowProblem : System.Web.UI.Page
{
    #region 业务对象
    B_User ubll = new B_User();
    //UserTableBLL utbll = new UserTableBLL();
    //B_Interlocution ibll = new B_Interlocution();
    B_Result brbll = new B_Result();
    B_Student bs = new B_Student();
    B_ClassRoom cll = new B_ClassRoom();
    protected string RoomName = "";
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        ubll.CheckIsLogin();
        if (!IsPostBack)
        {
            M_UserInfo uinfo = ubll.GetLogin();
            //初始化接收ID
            if (Request.QueryString["Pid"] != null)
            {
                Pid = int.Parse(Request.QueryString["Pid"].ToString());
                //M_Interlocution i = ibll.GetSelect(Pid);
                //Roomid=i.RoomID;
                //RoomName = cll.GetSelect(Roomid).RoomName;
                //DataTable dt = bs.SelByURid(i.RoomID,1, uinfo.UserID);
                //if (dt.Rows.Count > 0)
                //{
                //    if (dt.Rows[0]["StatusType"].ToString() != "1" && dt.Rows[0]["StatusType"].ToString() != "3")
                //    {
                //        this.Panel1.Visible = true;
                //    }
                //    else
                //    {
                //        this.Panel1.Visible = false;
                //    }

                //    tdtitle.InnerHtml = i.ProblemTitle;
                //    tdcontext.InnerHtml = i.ProblemContext;
                //    tdtimename.InnerHtml = "[" + getusername(i.UserID.ToString()) + "]      " + i.AddTime.ToString();
                //    GetMessage();
                //}

            }

        }

    }
    //接收ID
    private int Pid
    {
        get
        {
            if (ViewState["Pid"] != null)
                return int.Parse(ViewState["Pid"].ToString());
            else return 0;
        }
        set
        {
            ViewState["Pid"] = value;
        }
    }

    protected  int Roomid
    {
        get
        {
            if (ViewState["Roomid"] != null)
                return int.Parse(ViewState["Roomid"].ToString());
            else return 0;
        }
        set
        {
            ViewState["Roomid"] = value;
        }
    }
    private void GetMessage()
    {
        int CPage;
        int temppage;
        DataTable dt = brbll.SelByProID(Pid);
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
        this.Repeater1.DataSource = cc;
        this.Repeater1.DataBind();

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

    //显示用户昵称
    protected string getusername(string uid)
    {
        return ubll.GetUserByUserID(int.Parse(uid)).UserName;

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        M_Result mr = new M_Result();
        mr.ProblemID = Pid;
        mr.Result = this.txtResult.Text.Replace ("\n","<br/>");
        mr.ResultTime = DateTime.Now;
        mr.UserID = ubll.GetLogin().UserID;
        brbll.GetInsert(mr);
        Response.Write("<script>location.href='ShowProblem.aspx?Pid="+Pid+"'</script>");
    }
}
