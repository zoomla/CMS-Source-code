using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.Components;
using System.IO;

public partial class User_UserTalk_TalkLog : System.Web.UI.Page
{
    private B_Chat chat = new B_Chat();
    B_User buser = new B_User();
    B_ChatMsg msgbll = new B_ChatMsg();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    public void MyBind()
    {
        int reuid=buser.GetUserByName(ReUser_T.Text).UserID;
        DataTable dt = msgbll.SelByWhere(buser.GetLogin().UserID, reuid, SDate_T.Text, EDate_T.Text);
        EGV.DataSource = dt;
        EGV.DataBind();
    }
    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        MyBind();
    }

    public string GetReceUser()
    {
        string[] ids = Eval("ReceUser").ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        return "<a href='javascript:;' onclick='ShowUInfo(" + ids[0] + ")'>" + buser.GetUserByUserID(DataConverter.CLng(ids[0])).UserName + "</a>";
    }
    public string GetSender()
    {
        int uid = DataConverter.CLng(Eval("UserID"));
        return buser.GetUserByUserID(uid).UserName;
    }
    protected void Find_B_Click(object sender, EventArgs e)
    {
        MyBind();
    }
    protected void DownFile_B_Click(object sender, EventArgs e)
    {
        DataTable dt = msgbll.SelByWhere(buser.GetLogin().UserID, buser.GetUserByName(ReUser_T.Text).UserID, SDate_T.Text, EDate_T.Text);
        if (dt.Rows.Count < 1) { function.WriteErrMsg("没有聊天记录,无法导出"); }
        StringBuilder sb = new StringBuilder();
        foreach (DataRow dr in dt.Rows)
        {
            sb.Append(dr["UserName"] + dr["CDate"].ToString() + ":\r\n");
            sb.Append(dr["Content"].ToString() + "\r\n");
            sb.Append("---------------------------------------------------------------\r\n");
        }
        string vpath = "/Temp/ChatHis/";
        string filename = function.GetRandomString(8) + ".txt";
        SafeSC.WriteFile(vpath + filename, sb.ToString());
        SafeSC.DownFile(vpath + filename);
        SafeSC.DelFile(vpath + filename);
        Response.End();
    }
}