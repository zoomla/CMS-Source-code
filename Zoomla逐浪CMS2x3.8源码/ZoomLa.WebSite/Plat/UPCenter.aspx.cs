using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.IO;
using ZoomLa.BLL;
using ZoomLa.BLL.Helper;
using ZoomLa.BLL.Plat;
using ZoomLa.BLL.Third;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.Plat;
using ZoomLa.Model.Third;
using ZoomLa.PdoApi.SinaWeiBo;

/*
 * 是管理员，则允许传UserID,否则只显示本人信息
 */
public partial class Plat_RegisterDetail : System.Web.UI.Page
{
    B_User_Plat userBll = new B_User_Plat();
    B_User buser = new B_User();
    M_User_Plat userMod = new M_User_Plat();
    B_Plat_UserRole urBll = new B_Plat_UserRole();
    B_User_Token tokenbll = new B_User_Token();
    M_User_Token tokenMod = new M_User_Token();
    B_Third_Info thirdBll = new B_Third_Info();

    public int UserID
    {
        get
        {
            return DataConverter.CLng(Request.QueryString["uid"]);
        }
    }
    public string UserRole 
    {
        get { return ViewState["UserRole"] == null ? "" : ViewState["UserRole"].ToString(); }
        set { ViewState["UserRole"] = value; }
    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["remote"]))
        {
            this.MasterPageFile = "~/Plat/Empty.master";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (UserID > 0 && B_User_Plat.IsAdmin())
            {
                Role_Rep.Visible = true;
                AdminSave_Btn.Visible = true;
                MyBind(UserID);
            }
            else//非管理员，只允许查看自己
            {
                Role_View_Rep.Visible = true;
                Save_Btn.Visible = true;
                MyBind(B_User_Plat.GetLogin().UserID);
            }
            if (!string.IsNullOrEmpty(Request.QueryString["Set"]))
            { function.Script(this, "SetPlat();"); }
        }
    }
    private void MyBind(int uid)
    {
        showViboIcons(uid);
        M_UserInfo mu = buser.SelReturnModel(uid);
        userMod = userBll.SelReturnModel(uid);
        UserName_T2.Text = userMod.UserName;
        Plat_Group_T2.Text = string.IsNullOrEmpty(userMod.GroupName.Trim(',')) ? "未分组" : userMod.GroupName.Trim(',');
        Mobile_T2.Text = userMod.Mobile;
        Post_T2.Text = userMod.Post;
        TrueName_T2.Text = mu.HoneyName;
        SFile_Up.FileUrl = mu.UserFace;
        UserRole = userMod.Plat_Role;
        DataTable roledt = urBll.SelByCompID(userMod.CompID);
        Role_Rep.DataSource = roledt;
        Role_Rep.DataBind();
        if (!string.IsNullOrWhiteSpace(UserRole.Trim(',')))
        {
            roledt.DefaultView.RowFilter = "ID in(" + UserRole.Trim(',') + ")";
            Role_View_Rep.DataSource = roledt.DefaultView.ToTable();
            Role_View_Rep.DataBind();
        }
    }
    private void showViboIcons(int uid)
    {
        tokenMod = tokenbll.SelModelByUid(uid);
        if (tokenMod == null) return;
        if (!string.IsNullOrEmpty(tokenMod.SinaToken))
        {
            SinaHelper sina = new SinaHelper(tokenMod.SinaToken);
            if (sina.CheckToken())
            {
                JObject uinfo = sina.GetUserState(sina.GetUid());
                sinaStatu_D.InnerText = "(已绑定)";//" + uinfo["screen_name"].ToString() + "
                sinaimg.Style.Add("color", "rgb(10, 164, 231)");
                Sina_Btn.Text = "修改绑定";
            }
            else
            {
                sinaStatu_D.InnerText = "(已过期)";
            }
        }
        if (!string.IsNullOrEmpty(tokenMod.QQToken))
        {
            QQHelper qqhelper = new QQHelper(tokenMod.QQToken, tokenMod.QQOpenID);
            if (qqhelper.TokenIsValid())
            {
                QQStatus_Div.InnerText = "(已绑定:" + tokenMod.QQUName + ")";
                qqimg.Style.Add("color", "rgb(10, 164, 231)");
                QQSPan.InnerText = "修改绑定";
            }
            else
            {
                QQStatus_Div.InnerText = "(已过期:" + tokenMod.QQUName + ")";
            }
        }
    }
    protected void Save_Btn_Click(object sender, EventArgs e)
    {
        M_UserInfo mu = buser.GetLogin();
        mu.HoneyName = TrueName_T2.Text;
        mu.UserFace = SFile_Up.SaveFile();
        userMod = userBll.SelReturnModel(mu.UserID);
        userMod.Mobile = Mobile_T2.Text;
        userMod.Post = Post_T2.Text;
        userBll.UpdateByID(userMod);
        buser.UpdateByID(mu);
        function.WriteSuccessMsg("操作成功");
    }
    //修改其他人
    protected void AdminSave_Btn_Click(object sender, EventArgs e)
    {
        M_UserInfo mu = buser.SelReturnModel(UserID);
        mu.HoneyName = TrueName_T2.Text;
        userMod = userBll.SelReturnModel(UserID);
        userMod.Post = Post_T2.Text;
        userMod.Mobile = Mobile_T2.Text;
        userMod.Plat_Role = Request.Form["UserRole_Chk"];
        userBll.UpdateByID(userMod);
        buser.UpdateByID(mu);
        function.WriteSuccessMsg("操作成功");
    }
    protected void ImgUP_Btn_Click(object sender, EventArgs e)
    {
        string vpath = B_Plat_Common.GetDirPath(B_Plat_Common.SaveType.Person_P);
        string fname = function.GetRandomString(6) + Path.GetExtension(SFile_Up.FileUrl);
        SFile_Up.SaveUrl = vpath + fname;
        M_UserInfo mu = buser.GetLogin();
        buser.UpdateByID(mu);
    }
    public string IsChecked() 
    {
        string result="";
        if (UserRole.Contains("," + Eval("ID") + ","))
            result = "checked='checked'";
        return result;
        
    }
    protected void bindVibo_B_Click(object sender, EventArgs e)
    {
        M_Third_Info appmod = thirdBll.SelModelByName("Sina");
        M_User_Token tokenMod = tokenbll.SelModelByUid(buser.GetLogin().UserID);
        SinaHelper sinaBll = new SinaHelper(tokenMod.SinaToken);
        Response.Redirect(sinaBll.GetAuthUrl());
    }
}