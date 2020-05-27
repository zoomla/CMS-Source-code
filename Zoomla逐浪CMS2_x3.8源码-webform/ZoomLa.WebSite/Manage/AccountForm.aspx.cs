using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.BLL.Helper;

public partial class Manage_AccountForm : System.Web.UI.Page
{
    B_User buser = new B_User();
    B_Group groupBll = new B_Group();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (SiteConfig.SiteOption.RegManager != 1) { function.WriteErrMsg("未开启管理员申请功能"); }
        if (!IsPostBack)
        {
            IPCity cityMod = IPScaner.FindCity(IPScaner.GetUserIP());
            if (cityMod.IsValid)
            {
                function.Script(this, "pcc.SetDef('" + cityMod.Province + "','" + cityMod.City + "','" + cityMod.County + "');");
            }
            function.Script(this, "pcc.ProvinceInit();");
        }
    }
    protected void Submit_B_Click(object sender, EventArgs e)
    {
        AddAdmin();
    }
    public void AddAdmin()
    {
        if (B_Admin.IsExist(UserName_T.Text.Trim()))
        {
            function.WriteErrMsg("该账号已存在!");
        }
        M_AdminInfo admin = new M_AdminInfo();
        admin.UserName = UserName_T.Text.Trim();
        admin.AdminName = UserName_T.Text.Trim();
        admin.AdminTrueName = UserName_T.Text.Trim();
        admin.AdminPassword = function.GetRandomString(8);
        admin.EnableModifyPassword = true;
        admin.LastLoginTime = DateTime.Now;
        admin.LastLogoutTime = DateTime.Now;
        admin.LastModifyPasswordTime = DateTime.Now;
        admin.CDate = DateTime.Now;
        admin.IsLock = true;
        admin.RoleList = ",2,4,6,7,8,";
        B_Admin.Add(admin);
        AddUser(admin.AdminPassword);
        Tip_Div.Visible = true;
        Form_Div.Visible = false;
    }
    public void AddUser(string password)
    {
        M_UserInfo uinfo = buser.GetUserByName(UserName_T.Text.Trim());
        if (uinfo == null || uinfo.UserID <= 0) { uinfo = new M_UserInfo(); }
        uinfo.UserName = UserName_T.Text.Trim();
        uinfo.UserPwd = password;
        uinfo.Email = UserEmail_T.Text;
        if (buser.IsExistMail(uinfo.Email)){ uinfo.Email = function.GetRandomString(6) + "@random.com"; }
        uinfo.GroupID = groupBll.DefaultGroupID();
        uinfo.LastLoginIP = IPScaner.GetUserIP();
        M_Uinfo binfo = new M_Uinfo();
        binfo.Province = Request.Form["tbProvince"];
        binfo.City = Request.Form["tbCity"];
        binfo.County = Request.Form["tbCounty"];
        binfo.Mobile = UserPhone_T.Text;
        binfo.Position = Compay_T.Text;
        binfo.QQ = QQ_T.Text;
        if (uinfo == null || uinfo.UserID <= 0)//如果未注册会员则添加会员信息
        {
            int uid = buser.AddModel(uinfo);
            binfo.UserId = uid;
            buser.AddBase(binfo);
        }
        else
        {
            buser.UpDateUser(uinfo);
            binfo.UserId = uinfo.UserID;
            buser.UpdateBase(binfo);
        }
    }
}