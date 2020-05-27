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
public partial class User_Message_MessageOutbox : System.Web.UI.Page
{
    B_User buser = new B_User();
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
        EGV.DataSource = msgBll.SelMyMail(buser.GetLogin().UserID, 1,Skey_T.Text.Trim());
        EGV.DataKeyNames = new string[] { "MsgID" };
        EGV.DataBind();
        UserDT = null;
        Session["UserDT"] = null;
    }

    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
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
        if (e.CommandName == "del2")
        {
            M_UserInfo mu = buser.GetLogin();
            msgBll.DelByID(Convert.ToInt32(e.CommandArgument), mu.UserID);
            MyBind();
        }
    }
    public string GetUserName(string userid)
    {
        string un = buser.GetUserNameByIDS(userid, UserDT);
        return un.Length > 13 ? un.Substring(0,12)+"..." : un;
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
    protected void EGV_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["ondblclick"] = "javascript:location.href='MessageRead.aspx?id=" + ((DataRowView)e.Row.DataItem)["MsgID"] + "';";
            e.Row.Attributes["title"] = "双击修改";
        }
    }
    protected void BatDel_Btn_Click(object sender, EventArgs e)
    {
        string ids = Request.Form["idchk"];
        msgBll.DelByIDS(ids, buser.GetLogin().UserID);
        MyBind();
    }
    protected void Skey_Btn_Click(object sender, EventArgs e)
    {
        MyBind();
    }
}

