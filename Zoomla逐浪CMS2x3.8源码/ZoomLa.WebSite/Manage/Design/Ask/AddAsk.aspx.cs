using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Design;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.Model.Design;

public partial class Manage_Design_Ask_AddAsk : CustomerPageAction
{
    B_Design_Ask askBll = new B_Design_Ask();
    B_User buser = new B_User();
    public int AskID { get { return DataConverter.CLng(Request.QueryString["id"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
            Call.SetBreadCrumb(Master, "<li><a href='" + customPath2 + "Main.aspx'>工作台</a></li><li><a href='../Default.aspx'>动力模块</a></li><li><a href='AskManage.aspx'>问卷调查</a></li><li class='active'>编辑问卷</li>");
        }
    }
    public void MyBind()
    {
        if (AskID <= 0) { function.WriteErrMsg("没有指定问卷模型ID"); }
        M_Design_Ask askMod = askBll.SelReturnModel(AskID);
        if (askMod == null) { function.WriteErrMsg("问卷模型不存在"); }
        Title_T.Text = askMod.Title;
        var mu = buser.SelReturnModel(askMod.CUser);
        CUser_L.Text = "<a href='javascript:;' onclick='showuser(" + mu.UserID + ")'>" + mu.UserName + "</a>";
        //ZType_L.Text = askMod.ZType.ToString();
        //Status_L.Text = askMod.ZStatus.ToString();
        SFile_Up.FileUrl = askMod.PreViewImg;
        CDate_T.Text = askMod.CDate.ToString("yyyy-MM-dd HH:mm:ss");
        EndDate_L.Text = askMod.EndDate.ToString("yyyy-MM-dd HH:mm:ss");
        Remind_T.Text = askMod.Remind;
    }

    protected void Save_B_Click(object sender, EventArgs e)
    {
        M_Design_Ask askMod = askBll.SelReturnModel(AskID);
        askMod.Title = Title_T.Text;
        askMod.Remind = Remind_T.Text;
        askMod.CDate = DataConverter.CDate(CDate_T.Text);
        askMod.EndDate = DataConverter.CDate(EndDate_L.Text);
        M_UserInfo mu = buser.GetLogin();
        SFile_Up.SaveUrl = SiteConfig.SiteOption.UploadDir + "User/" + mu.UserName + mu.UserID + "/";
        askMod.PreViewImg = SFile_Up.SaveFile();
        askBll.UpdateByID(askMod);
        function.WriteSuccessMsg("操作成功", "AskManage.aspx");
    }
}