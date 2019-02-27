using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL.Plat;
using ZoomLa.Common;
using ZoomLa.Model.Plat;
using ZoomLa.BLL;
using ZoomLa.Model;

public partial class Manage_Plat_AddComp : System.Web.UI.Page
{
    B_Plat_Comp compBll = new B_Plat_Comp();
    B_User buser = new B_User();
    public int CompID { get { return DataConverter.CLng(Request.QueryString["id"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='" + Request.RawUrl + "'>能力中心</a></li><li><a href='PlatInfoManage.aspx'>信息管理</a></li><li><a href='" + Request.RawUrl + "'>添加公司</a></li>");
        }
    }
    public void MyBind()
    {
        CUser_T.Text = buser.SelReturnModel(1).UserName;//固定初始管理员
        CDate_T.Text = DateTime.Now.ToString();
        if (CompID > 0)
        {
            M_Plat_Comp compMod = compBll.SelReturnModel(CompID);
            CUser_T.Text = buser.SelReturnModel(compMod.CreateUser).UserName;
            CDate_T.Text = compMod.CreateTime.ToString();
            CompName_T.Text = compMod.CompName;
            CompShort_T.Text = compMod.CompShort;
            Logo_SFile.FileUrl = compMod.CompLogo;
            CompHref_T.Text = compMod.CompHref;
            CompDesc_T.Text = compMod.CompDesc;
            EMail_T.Text = compMod.Mails;
            Telephone_T.Text = compMod.Telephone;
            Mobile_T.Text = compMod.Mobile;
        }
    }

    protected void Save_Btn_Click(object sender, EventArgs e)
    {
        M_Plat_Comp compMod = new M_Plat_Comp();
        if (CompID > 0) { compMod = compBll.SelReturnModel(CompID); }
        string path = Logo_SFile.SaveFile();
        compMod.CompLogo = string.IsNullOrEmpty(path) ? compMod.CompLogo : path;
        compMod.CompName = CompName_T.Text;
        compMod.CreateTime = DataConverter.CDate(CDate_T.Text);
        M_UserInfo mu = buser.GetUserByName(CUser_T.Text);
        compMod.CreateUser = mu.IsNull ? 1 : mu.UserID;//后台固定为管理员添加
        compMod.Telephone = Telephone_T.Text;
        compMod.Mobile = Mobile_T.Text;
        compMod.CompHref = "http://" + CompHref_T.Text.Replace("http://", "");
        compMod.CompDesc = CompDesc_T.Text;
        compMod.Mails = EMail_T.Text;
        compMod.CompShort = CompShort_T.Text;
        if (CompID > 0) { compBll.UpdateByID(compMod); function.WriteSuccessMsg("修改成功!", "CompList.aspx"); }
        compBll.Insert(compMod);
        function.WriteSuccessMsg("添加成功!", "CompList.aspx");
    }
}