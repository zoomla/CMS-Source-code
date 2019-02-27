using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Helper;
using ZoomLa.BLL.Plat;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.Plat;
using ZoomLa.SQLDAL;

public partial class Plat_Addon_SetPrompt : System.Web.UI.Page
{
    M_Blog_Msg msgMod = new M_Blog_Msg();
    B_Blog_Msg msgBll = new B_Blog_Msg();
    B_User_Plat upBll = new B_User_Plat();
    B_Common_Notify comBll = new B_Common_Notify();
    B_User buser = new B_User();
    public int Mid { get { return DataConvert.CLng(Request.QueryString["ID"]); } }
    public int MsgID { get { return DataConvert.CLng(ViewState["MsgID"]); }
        set { ViewState["MsgID"] = value; } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MsgID = DataConvert.CLng(Request.QueryString["MsgID"]);
            MyBind();
        }
    }
    private void MyBind()
    {
        M_User_Plat upMod = B_User_Plat.GetLogin();
        if (Mid > 0)
        {
            M_Common_Notify comMod = comBll.SelReturnModel(Mid);
            if (comMod.CUser != upMod.UserID) { function.WriteErrMsg("你无权修改该信息"); }
            MsgID = comMod.InfoID;
            Title_T.Text = comMod.Title;
            Content_T.Text = comMod.Content;
            BeginDate_T.Text = comMod.BeginDate.ToString("yyyy-MM-dd HH:mm:ss");
            manage_hid.Value = buser.SelByIDS(comMod.ReceOrgin);
            function.Script(this,"SetRadVal('zstatus_rad',"+comMod.ZStatus+");");
        }
        else
        {
            msgMod = msgBll.SelReturnModel(MsgID);
            Title_T.Text = msgMod.Title;
            Content_T.Text = msgMod.MsgContent;
            BeginDate_T.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            manage_hid.Value = buser.SelByIDS(upMod.UserID.ToString());
        }

        RPT.DataSource = comBll.Blog_Sel(MsgID, -100);
        RPT.DataBind();
    }
    protected void Save_Btn_Click(object sender, EventArgs e)
    {
        M_User_Plat upMod = B_User_Plat.GetLogin();
        M_Common_Notify comMod = new M_Common_Notify();
        if (Mid > 0) { comMod = comBll.SelReturnModel(Mid); }
        comMod.Title = Title_T.Text;
        comMod.Content = Content_T.Text;
        comMod.BeginDate = Convert.ToDateTime(BeginDate_T.Text);
        if (!comMod.ReceOrgin.Equals(manage_hid.Value))
        {
            comMod.ReceOrgin = StrHelper.IdsFormat(manage_hid.Value);
            comMod.ReceUsers = StrHelper.IdsFormat(StrHelper.RemoveRepeat(comMod.ReceOrgin.Split(','), comMod.ReadedUsers.Split(',')));
        }
        if (Mid > 0)
        {
            comBll.UpdateByID(comMod);
        }
        else
        {
            comMod.NType = 2;
            comMod.InfoID = MsgID;
            comMod.CUser = upMod.UserID;
            comMod.CUName = upMod.UserName;
            comBll.Insert(comMod);
        }
        function.WriteSuccessMsg("操作成功");
    }

    protected void RPT_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "del":
                comBll.DelByIDS(e.CommandArgument.ToString(),buser.GetLogin().UserID);
                break;
        }
        Response.Redirect("SetPrompt.aspx?MsgID="+MsgID);//有可能删除了当前元素
    }
    public string GetStatus()
    {
        switch (DataConvert.CLng(Eval("ZStatus")))
        {
            case 0:
                return "待处理";
            case 99:
                return "已通知";
            default:
                return Eval("ZStatus", "");
       }
    }
    public string GetUser()
    {
        return buser.GetUserNameByIDS(Eval("ReceOrgin", ""));
    }
}