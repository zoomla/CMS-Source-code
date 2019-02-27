using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Model.Chat;
using ZoomLa.Common;

public partial class Manage_User_MsgInfo : System.Web.UI.Page
{
    B_User userbll = new B_User();
    B_ChatMsg msgbll = new B_ChatMsg();
    public int MID
    {
        get
        {
            return DataConverter.CLng(Request.QueryString["id"]);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Call.SetBreadCrumb(this.Master, "<li><a href='../Main.aspx'>工作台</a></li><li><a href='ServiceSeat.aspx'>客服通</a></li><li><a href='MsgEx.aspx'>聊天记录</a></li><li><a href='" + Request.RawUrl + "'>内容详情</a></li>");
            MyBind();
        }
    }

    public void MyBind()
    {
        M_ChatMsg msgMod= msgbll.SelReturnModel(MID);
        ID_L.Text = msgMod.ID.ToString();
        CUser_LB.Text = "<a href='javascript:;' onclick='ShowUInfo(" + msgMod.UserID + ")'>" + userbll.GetUserByUserID(DataConverter.CLng(msgMod.UserID)).UserName+"</a>";
        ReceUser_LB.Text = GetReceUser(msgMod.ReceUser);
        MsgContent_L.Text = msgMod.Content;
        MsgContent_T.Text = msgMod.Content;
        CDate_L.Text = msgMod.CDate.ToString();
        CDate_T.Text = msgMod.CDate.ToString();
    }

    public string GetReceUser(string users)
    {
        string[] ids = users.ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        return "<a href='javascript:;' onclick='ShowUInfo(" + ids[0] + ")'>" + userbll.GetUserByUserID(DataConverter.CLng(ids[0])).UserName + "</a>";
    }

    protected void Save_Btn_Click(object sender, EventArgs e)
    {
        M_ChatMsg msgMod = msgbll.SelReturnModel(MID);
        msgMod.Content = MsgContent_T.Text;
        msgMod.CDate = DateTime.Parse(CDate_T.Text);
        msgbll.UpdateByID(msgMod);
        Response.Redirect(Request.RawUrl);
    }
}