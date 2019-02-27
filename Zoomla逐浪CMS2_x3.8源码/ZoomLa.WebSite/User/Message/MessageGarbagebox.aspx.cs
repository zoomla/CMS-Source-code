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
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.API;
using ZoomLa.DZNT;
public partial class User_Message_MessageGarbagebox : System.Web.UI.Page
{
    B_User buser = new B_User();
    M_UserInfo muser = new M_UserInfo();
    B_Message msgBll = new B_Message();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    private void MyBind()
    {
        string UserID = buser.GetLogin().UserID.ToString();
        EGV.DataSource = msgBll.SelMyMail(buser.GetLogin().UserID, 3);
        EGV.DataKeyNames = new string[] { "MsgID" };
        EGV.DataBind();
        Session["UserDT"]=null;
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        MyBind();
    }
    public string getStatus(string str)
    {
        string restr = DataConverter.CBool(str) ? "已读" : "未读";           
        return restr;
    }
    protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName) { 
            case "DeleteMsg":
                msgBll.RealDelByIDS(e.CommandArgument.ToString(), buser.GetLogin().UserID);
                MyBind();
                break;
            case "ReadMsg":
                Response.Redirect("MessageRead.aspx?id=" + e.CommandArgument.ToString());
                break;
        
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
    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        DataBind();
    }

    protected void BatDel_Btn_Click(object sender, EventArgs e)
    {
        string ids = Request.Form["ids"];
        if (!string.IsNullOrEmpty(ids))
        {
            M_UserInfo mu = buser.GetLogin();
            msgBll.DelByIDS(ids, mu.UserID);
        }
        MyBind();
    }
}
