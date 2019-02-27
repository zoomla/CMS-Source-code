using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Components;
using ZoomLa.Common;
using ZoomLa.API;
using ZoomLa.DZNT;
using System.Data;
using ZoomLa.SQLDAL;

public partial class User_SetSecondPwd : Page
{
    private B_User buser = new B_User();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            M_UserInfo mu = buser.GetLogin();
            if (mu.PayPassWord.Length != 0)
            {
                DV_Set.Visible = false;
                DV_show.Visible = true;
            }
            else//设置二级密码
            {
                DV_Set.Visible = true;
                DV_show.Visible = false;
            }
        }
    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        M_UserInfo info = buser.GetLogin();
        if (!string.IsNullOrEmpty(TxtPassword.Text.Trim()))
        {
            if (info.PayPassWord != StringHelper.MD5(TxtOldPassword.Text.Trim()))
            {
                function.WriteErrMsg("原密码错误,请重新输入！");
                return;
            }
            info.PayPassWord = StringHelper.MD5(TxtPassword.Text.Trim());
            buser.UpDateUser(info);
            function.WriteSuccessMsg("操作成功", "SetPayPwd.aspx");
        }
    }

    private bool checkPws()
    {
        if (!string.IsNullOrEmpty(TxtPassword.Text.Trim()) && string.IsNullOrEmpty(TxtPassword2.Text.Trim()))
        {
            function.WriteErrMsg("请输入确认密码!");
            return false;
        }
        else
        {
            return true;
        }
    }
   
    protected void BtnCancle_Click(object sender, EventArgs e)
    {
        this.TxtOldPassword.Text = "";
        this.TxtPassword.Text = "";
        this.TxtPassword2.Text = "";
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        M_UserInfo info = buser.GetLogin();
        info.PayPassWord = StringHelper.MD5(this.TextBox1.Text.Trim());
        buser.UpDateUser(info);
        function.WriteSuccessMsg("操作成功", "SetPayPwd.aspx");
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        this.TextBox1.Text = "";
        this.TextBox2.Text = "";
    }
}
