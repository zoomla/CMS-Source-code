using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
/*
 * OA签章生成页面
 */
public partial class Manage_WorkFlow_AddSign : CustomerPageAction
{
    protected B_Admin badmin = new B_Admin();
    protected B_User buser = new B_User();
    protected B_OA_Sign signBll = new B_OA_Sign();
    protected M_OA_Sign signMod = new M_OA_Sign();
    public int Mid { get { return DataConverter.CLng(Request.QueryString["ID"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Mid > 0)
            {
                editBtn.Visible = true;
                saveBtn.Visible = false;
                signMod = signBll.SelReturnModel(DataConverter.CLng(Request.QueryString["ID"]));
                signNameT.Text = signMod.SignName;
                SFile_Up.FileUrl = signMod.VPath;
                SignPwd_T.Attributes.Add("value", signMod.SignPwd);
                signKeyT.Text = signMod.SignKey;
                BindUser_T.Text = buser.GetUserNameByIDS(signMod.OwnUserID.ToString());
                BindUser_Hid.Value = signMod.OwnUserID.ToString();
                statusChk.Checked = signMod.Status == 1 ? true : false;
                remindT.Text = signMod.Remind;
            }
            else
            {
                SignPwd_T.Attributes.Add("value", "111111");
            }
        }
        Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='OAConfig.aspx'>OA配置</a></li><li><a href='SignManage.aspx'>签章管理</a></li><li class='active'>添加签章</a></li>");
    }
    //保存
    protected void saveBtn_Click(object sender, EventArgs e)
    {
        if (DataConverter.CLng(BindUser_Hid.Value) == 0)
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('用户不存在!!');", true);
        else
        {
            string filePath = OAConfig.SignPath + "/" + BindUser_T.Text + "/";
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            SFile_Up.SaveUrl = filePath;
            signMod.VPath = SFile_Up.SaveFile();
            signMod.SignName = signNameT.Text.Trim();
            signMod.SignPwd = SignPwd_T.Text;
            signMod.SignKey = function.GetRandomString(12);
            signMod.OwnUserID = DataConverter.CLng(BindUser_Hid.Value);
            signMod.Status = statusChk.Checked ? 1 : 0;
            signMod.CreateMan = badmin.GetAdminLogin().AdminId;
            signMod.CreateTime = DateTime.Now;
            signMod.Remind = remindT.Text.Trim();
            signBll.Insert(signMod);
            function.WriteSuccessMsg("添加成功", "SignManage.aspx");
        }
    }
    //修改
    protected void editBtn_Click(object sender, EventArgs e)
    {
        signMod = signBll.SelReturnModel(Mid);
        int userid = DataConverter.CLng(BindUser_Hid.Value);
        if (userid == 0)
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('用户不存在!!');", true);
        else
        {
            signMod.SignName = signNameT.Text.Trim();
            signMod.SignPwd = SignPwd_T.Text;
            signMod.OwnUserID = userid;
            signMod.Status = statusChk.Checked ? 1 : 0;
            signMod.CreateMan = badmin.GetAdminLogin().AdminId;
            signMod.CreateTime = DateTime.Now;
            signMod.Remind = remindT.Text.Trim();
            string filePath = OAConfig.SignPath + "/" + BindUser_T.Text + "/";
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            SFile_Up.SaveUrl = filePath;
            signMod.VPath = SFile_Up.SaveFile();
            signBll.UpdateByID(signMod);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('修改成功!!');window.location.href='SignManage.aspx';", true);
            signBll.UpdateByID(signMod);
            function.WriteSuccessMsg("修改成功", "SignManage.aspx");

        }
    }
}