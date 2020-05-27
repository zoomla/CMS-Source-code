using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using ZoomLa.Model;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.API;
using ZoomLa.DZNT;
using System.Net;
using ZoomLa.SQLDAL;

public partial class User_Message_MessageDraftbox : System.Web.UI.Page
{
    private B_User buser = new B_User();
    private M_UserInfo muser = new M_UserInfo();
    private B_Message bll = new B_Message();

    private void DataBind(string key = "")
    {
        DataTable dt = new DataTable();
        string UserID = buser.GetLogin().UserID.ToString();
        dt = B_Message.SeachSendMessage(UserID, 0, 1);
        if (!string.IsNullOrEmpty(key.Trim()))
        {
            dt.DefaultView.RowFilter = "Title Like '%" + key + "%'";
        }
        EGV.DataSource = dt;
        EGV.DataBind();
        Session["UserDT"] = null;
    }
    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        DataBind();
    }
    protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleteMsg")
        {
            bll.RealDelByIDS(e.CommandArgument.ToString(), buser.GetLogin().UserID);
            DataBind();
        }
        if (e.CommandName == "ReadMsg")
        {
            Response.Redirect("MessageRead.aspx?id=" + e.CommandArgument.ToString());
        }
        if (e.CommandName == "EditeMsg")
        {
            Response.Redirect("MessageSend.aspx?Drafid=" + e.CommandArgument.ToString());
        }
    }
    protected void searchBtn_Click(object sender, EventArgs e)
    {
        DataBind(searchText.Text.Trim());
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        B_User.CheckIsLogged();
        M_UserInfo info = buser.GetLogin();
        if (info.IsNull)
        {
            Response.Redirect("Login.aspx");
        }
       if (!Page.IsPostBack)
        {
            DataBind();
        }
    }
    public string getStatus(string str)
    {
        string restr = DataConverter.CBool(str) ? "已读" : "未读";
        return restr;
    }
    //batDel
    protected void Button2_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Request.Form["idChk"]))
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('请先选择需要操作的邮件!!!');", true);
        }
        else
        {
            bll.RealDelByIDS(Request.Form["idChk"], buser.GetLogin().UserID);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('操作成功!!');", true);
            DataBind();
        }
    }
    public string GetUserName(string ids)
    {
        string un = buser.GetUserNameByIDS(ids, UserDT);
        return un.Length > 13 ? un.Substring(0, 12) + "..." : un;
    }
    public DataTable UserDT
    {
        get
        {
            if (Session["UserDT"] == null)
            {
                Session["UserDT"] = buser.Sel();
            }
            return Session["UserDT"] as DataTable;
        }
        set
        {
            Session["UserDT"] = value;
        }
    }
}
