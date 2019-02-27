using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.Components;

public partial class User_Info_CompanyInfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Mybind();
        }
    }
    public void Mybind()
    {
        M_UserInfo info = new M_UserInfo();
        B_User buser = new B_User();
        if (!string.IsNullOrEmpty(Request.QueryString["UserName"]))
        {
            info = buser.GetUserIDByUserName(Request.QueryString["UserName"]);
        }
        if (info == null || info.UserID < 0)
        {
            info = buser.GetLogin();
        }
        ViewState["user"] = info.UserID;
        if (info.State == 1)
        {
            function.WriteErrMsg("信息正在审核中.......");
        }
        else
        {
            if (info.State == 0)
            {
                lblState.Text = "未通过认证, 您将无法建立商铺站点";
                lblState_hid.Text = "0";
            }
            else if (info.State == 2)
            {
                lblState.Text = "已通过" + info.ApproveType.ToString() + "认证";
                lblState.ForeColor = System.Drawing.Color.Green;
                lblState_hid.Text = "2";
            }
            DropDownList1.SelectedValue = info.ApproveType;
            txtName.Text = info.CompanyName;
            txtCompanyDescribe.Text = info.CompanyDescribe;
            SFile_Up.FileUrl = info.CerificateLogo;
        }
    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        B_User buser = new B_User();
        M_UserInfo info = new M_UserInfo();
        int userID = DataConverter.CLng(ViewState["user"]);
        string CerificateLogo = getFilePath();
        string CompanyDescribe = txtCompanyDescribe.Text.ToString();
        string ApproveType = DropDownList1.SelectedValue.ToString();
        if (ApproveType == "已通过国际认证" && CerificateLogo.Length == 0)
        {
            Response.Write("<script>alert('请选择您要审核的证书！')</script>");
            return;
        }
        //if (buser.UpdateCerificateApproveInfo(userID, State, CerificateLogo, ApproveType, CompanyDescribe, DateTime.Now))
        //{
        //    Response.Write("<script>alert('信息已更新,更新后信息待审核！')</script>");
        //    Mybind();
        //}
        //else
        //{
        //    Response.Write("<script>alert('信息已更新失败！')</script>");
        //    return;
        //}
    }
    protected void BtnCancle_Click(object sender, EventArgs e)
    {
        txtCompanyDescribe.Text = "";
        Response.Redirect("../Main.aspx");
    }
    public string getFilePath()
    {
        string filepath = SiteConfig.SiteOption.ManageDir+"/iServer/Files/";
        SFile_Up.SaveUrl = filepath;
        return SFile_Up.SaveFile();
    }
}
