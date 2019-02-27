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
using ZoomLa.API;
using ZoomLa.DZNT;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Components;
using ZoomLa.Common;

public partial class User_Message_MessageDraftbox : System.Web.UI.Page
{
    private B_User buser = new B_User();
    private M_UserInfo muser = new M_UserInfo();
    private B_Message msgBll = new B_Message();
    protected void Page_Load(object sender, EventArgs e)
    {
        M_UserInfo info = buser.GetLogin();
        if (info.IsNull)
        {
            Response.Redirect("Login.aspx");
        }
       if (!Page.IsPostBack)
        {
            MyBind();
        }
    }
    //绑定留言ID(草稿箱)
    private void MyBind()
    {
        string UserID = buser.GetLogin().UserID.ToString();
        this.EGV.DataSource = msgBll.SelMyMail(buser.GetLogin().UserID, 0);
        EGV.DataKeyNames = new string[] { "MsgID" };
        this.EGV.DataBind();
        Session["UserDT"] = null;
    }
    //绑定分页
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        MyBind();
    }
    // 获取选中记录，并绑定数据
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        EGV.EditIndex = e.NewEditIndex;
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
                int MsgID = DataConverter.CLng(e.CommandArgument.ToString());
                msgBll.RealDelByID(MsgID, buser.GetLogin().UserID);
                MyBind();
                break;
            case "ReadMsg":
                Response.Redirect("MessageRead.aspx?id=" + e.CommandArgument.ToString());
                break;
            case "EditeMsg":
                Response.Redirect("MessageSend.aspx?Drafid=" + e.CommandArgument.ToString());
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
        MyBind();
    }
    protected void BatDel_Btn_Click(object sender, EventArgs e)
    {
        string ids = Request.Form["idchk"];
        if (!string.IsNullOrEmpty(ids))
        {
            M_UserInfo mu = buser.GetLogin();
            msgBll.DelByIDS(ids, mu.UserID);
        }
        MyBind();
    }
}
