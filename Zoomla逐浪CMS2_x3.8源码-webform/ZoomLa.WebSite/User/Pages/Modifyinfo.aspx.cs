using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.BLL;
using ZoomLa.Model;
using System.Data;
using ZoomLa.BLL.Page;
using ZoomLa.Model.Page;

public partial class User_Pages_Modifyinfo : System.Web.UI.Page
{
    protected B_PageReg pll = new B_PageReg();
    protected B_User ull = new B_User();
    protected void Page_Load(object sender, EventArgs e)
    {
      
        if (!IsPostBack)
        {
            M_UserInfo mu = ull.GetLogin();
            M_PageReg regMod = pll.GetSelectByUserID(mu.UserID);
            if (regMod == null) { function.WriteErrMsg("您还未注册黄页！"); }
            else if (regMod.Status != (int)ZLEnum.ConStatus.Audited) { function.WriteErrMsg("您的黄页还未通过审核！"); }
            KeyWords.Text = regMod.Keyword;
            Title.Text = regMod.PageTitle;
            Description.Text = regMod.Description;
            HeadColor.Text = regMod.NavColor;
            HeadBackGround.Text = regMod.NavBackground;
            HeadHeight.Text = regMod.NavHeight.ToString();
            TopWords.Text = regMod.TopWords;
            BottonWords.Text = regMod.BottonWords;
        }
    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        M_UserInfo uinfo = ull.GetLogin();
        M_PageReg pinfo = pll.GetSelectByUserID(uinfo.UserID);
        pinfo.Keyword = KeyWords.Text;
        pinfo.PageTitle = Title.Text;
        pinfo.Description = Description.Text;
        pinfo.NavColor = HeadColor.Text;
        pinfo.NavBackground = HeadBackGround.Text;
        pinfo.NavHeight = HeadHeight.Text;
        pinfo.TopWords = TopWords.Text;
        pinfo.BottonWords = BottonWords.Text;
        pll.Update(pinfo);
        function.WriteSuccessMsg("修改成功", "Modifyinfo.aspx");
    }
    protected void BtnCancle_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }
}
