using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL.Third;
using ZoomLa.Common;
using ZoomLa.Model.Third;
using ZoomLa.SQLDAL;

//不允许添加,只可修改,如需要添加指定信息,则直接执行脚本后再修改
public partial class Manage_Config_AddPlatInfo : System.Web.UI.Page
{
    B_Third_PlatInfo infoBll = new B_Third_PlatInfo();
    M_Third_PlatInfo infoMod = new M_Third_PlatInfo();
    public int Mid { get { return DataConvert.CLng(Request.QueryString["ID"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
            Call.SetBreadCrumb(Master, "<li>扩展功能</li><li><a href='RunSql.aspx'>开发中心</a></li><li><a href='PlatInfoList.aspx'>平台信息</a></li><li class='active'>信息管理</i>");
        }
    }
    private void MyBind()
    {
        if (Mid > 0)
        {
            infoMod = infoBll.SelReturnModel(Mid);
            if (infoMod == null) { function.WriteErrMsg("接口信息不存在"); }
            Name_T.Text = infoMod.Name;
            APPID_T.Text = infoMod.APPID;
            Flag_T.Text = infoMod.Flag;
            APPKey_T.Text = infoMod.APPKey;
            APPSecret_T.Text = infoMod.APPSecret;
            CallBack_T.Text = infoMod.CallBack;
            UserName_T.Text = infoMod.UserName;
            //UserPwd_T.Text = infoMod.UserPwd;
            UserPwd_T.Attributes.Add("value", infoMod.UserPwd);
            Remind_T.Text = infoMod.Remind;
        }
    }
    protected void Save_Btn_Click(object sender, EventArgs e)
    {
        if (Mid > 0) { infoMod = infoBll.SelReturnModel(Mid); }
        infoMod.Name = Name_T.Text;
        infoMod.Flag = Flag_T.Text;
        infoMod.APPID = APPID_T.Text.Trim(' ');
        infoMod.APPKey = APPKey_T.Text;
        infoMod.APPSecret = APPSecret_T.Text;
        infoMod.CallBack = CallBack_T.Text;
        infoMod.UserName = UserName_T.Text;
        infoMod.UserPwd = UserPwd_T.Text;
        infoMod.Remind = Remind_T.Text;
        if (Mid > 0) { infoBll.UpdateByID(infoMod); }
        else { infoBll.Insert(infoMod); }
        function.WriteSuccessMsg("操作完成", "PlatInfoList.aspx");
    }
}