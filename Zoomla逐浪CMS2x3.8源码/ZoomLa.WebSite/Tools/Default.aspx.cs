using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.Components;
using System.IO;
using System.Data;
using ZoomLa.SQLDAL;
/*
 * 提供config文件的维护功能
 */ 
public partial class Tools_Default : System.Web.UI.Page
{
    public int LoginCount
    {
        get
        {
            if (HttpContext.Current.Session["ValidateCount"] == null)
            {
                HttpContext.Current.Session["ValidateCount"] = 0;
            }
            return Convert.ToInt32(HttpContext.Current.Session["ValidateCount"]);
        }
        set
        {
            HttpContext.Current.Session["ValidateCount"] = value;
        }
    }
    B_Admin badmin = new B_Admin();
    protected void Page_Load(object sender, EventArgs e)
    {
        //B_Admin.CheckIsLogged();
        if(!badmin.CheckLogin())
        {
            SupperGavel.Visible = true;
            SupperGavelCon.Visible = false;
        }
        if (!IsPostBack)
        {
            if (LoginCount >= 3) { function.Script(this, "EnableCode();"); }   
        }
    }

    protected void Update_Btn_Click(object sender, EventArgs e)
    {
        B_Admin.IsSuperManage();
        //删除文件
        //if (Directory.Exists(Server.MapPath("/Template/V3/System/")))
        //{
        //    Directory.Delete(Server.MapPath("/Template/V3/System/"), true);
        //}
        //-------
        SiteConfig.SiteInfo.SiteName = "逐浪CMS";
        SiteConfig.SiteInfo.SiteTitle = "逐浪CMS";
        SiteConfig.SiteInfo.LogoUrl = "/images/logo.svg";
        SiteConfig.SiteInfo.LogoAdmin = "";
        SiteConfig.SiteInfo.LogoPlatName = "";
        SiteConfig.SiteInfo.AllSiteJS = "";
        SiteConfig.SiteOption.Language = "ZH-CN";
        SiteConfig.SiteOption.ManageDir = "admin";
        SiteConfig.SiteOption.IsOpenHelp = "1";
        SiteConfig.SiteOption.RegManager = 0;
        SiteConfig.SiteOption.GeneratedDirectory = "html";
        SiteConfig.SiteOption.ProjectServer = "http://update.z01.com";
        SiteConfig.SiteOption.RegPageStart = true;
        SiteConfig.SiteOption.UploadDir = "/UploadFiles/";
        SiteConfig.SiteOption.UploadFileExts = "avi|gif|jpg|jpeg|bmp|png|flv|doc|ppt|pptx|xls|xlsx|docx|txt|accdb|sql|mp3|f4v|mp4|wmv|csv|tmp|pdf|zip|rar|rtf";
        SiteConfig.SiteOption.UploadPicExts = "avi|gif|jpg|jpeg|bmp|png|flv|doc|ppt|pptx|xls|xlsx|docx|txt|accdb|sql|mp3|f4v|mp4|wmv|tmp|pdf|zip|rar";
        SiteConfig.SiteOption.UploadMdaExts = "avi|rm|real|mpge|mpg|swf|flv|doc|ppt|pptx|xls|xlsx|docx|txt|accdb|sql|jpg|doc|xls|f4v|wmv|mp3|mp4|tmp|pdf|zip|rar|rtf";
        SiteConfig.SiteOption.SiteID = "purse,sicon,point";
        SiteConfig.SiteOption.SafeDomain = "";
        SiteConfig.SiteOption.DomainRoute = "0";
        SiteConfig.SiteOption.SiteManageMode = 0;
        SiteConfig.SiteOption.IsSensitivity = 0;
        SiteConfig.SiteInfo.SiteName = "逐浪CMS";
        SiteConfig.SiteInfo.Webmaster = "Zoomla";
        SiteConfig.SiteInfo.BannerUrl = "/Images/Qrcode.gif";//能力中心下载
        SiteConfig.SiteOption.DomainMerge = false;
        SiteConfig.SiteOption.AdminKey = "";
        //短信配置
        SiteConfig.SiteOption.DefaultSMS = "0";
        //商城配置
        SiteConfig.ShopConfig.OrderExpired = 72;
        //用户配置
        SiteConfig.UserConfig.EnableUserReg = true;
        SiteConfig.UserConfig.UserNameLimit = 4;
        SiteConfig.UserConfig.Agreement = "2";
        SiteConfig.UserConfig.EmailTell = false;
        SiteConfig.UserConfig.UserNameRegDisabled = "admin|administrator|system|operator|support|root|postmaster|webmaster|security";
        SiteConfig.UserConfig.RegFieldsSelectFill = "TrueName,UserSex,Address,OfficePhone,Birthday,Province,ParentUserID";
        SiteConfig.UserConfig.RegFieldsMustFill = "";
        //虚拟币
        SiteConfig.UserConfig.PointMoney = 0;
        SiteConfig.UserConfig.PointSilverCoin = 0;
        //--------------------
        SiteConfig.Update();
        //-----其他修改
        {
            string webPath = Server.MapPath("/Web.config");
            XmlDocument web = new XmlDocument();
            web.Load(webPath);
            web.SelectSingleNode("/configuration/system.web/compilation").Attributes["debug"].Value = "false";
            //web.SelectSingleNode("/configuration/system.web/customErrors").Attributes["mode"].Value = "On"; 
            web.Save(webPath);
        }
        function.WriteSuccessMsg("恢复初始配置成功!");
    }
    protected void Develop_Btn_Click(object sender, EventArgs e)
    {
        {
            string webPath = Server.MapPath("/Web.config");
            XmlDocument web = new XmlDocument();
            web.Load(webPath);
            web.SelectSingleNode("/configuration/system.web/compilation").Attributes["debug"].Value = "true";
            web.SelectSingleNode("/configuration/system.web/customErrors").Attributes["mode"].Value = "Off";
            web.Save(webPath);
        }
        function.WriteSuccessMsg("开启成功!");
    }
    protected void Check_Btn_Click(object sender, EventArgs e)
    {
        FileInfo_Div.Visible = true;
        string[] files = new string[] {"AppSettings.config","ConnectionStrings.config","Guest.config","oa.config","Plat.config","Pages.config","URLRoute.config","URLRewrite.config"};
        string filehtml = "";
        foreach (string filename in files)
        {
            string existIcon = "";
            if (!FileSystemObject.IsExist(function.VToP("/Config/" + filename), FsoMethod.File))
            {
                existIcon = "fa fa-remove";
            }else
            {
                existIcon = "fa fa-check";
            }
            filehtml += "<tr><td>"+filename+"</td><td><span class='"+existIcon+"'></span></td></tr>";
            Files_Li.Text = filehtml;
        }
    }
    protected void Login_Btn_Click(object sender, EventArgs e)
    {
        if (LoginCount >= 3)
        {
            if (!ZoomlaSecurityCenter.VCodeCheck(VCode_hid.Value, VCode.Text))
            {
                function.WriteErrMsg("验证码不正确!");
            }
        }
        M_AdminInfo admininfo= B_Admin.AuthenticateAdmin(UserName_T.Text, UserPwd_T.Text);
        if (admininfo == null || admininfo.AdminId < 1)
        {
            LoginCount++;
            function.WriteErrMsg("用户名或密码错误!");
        }
        badmin.SetLoginState(admininfo);
        LoginCount = 0;
        Response.Redirect(Request.RawUrl);
    }
    protected void Close_Btn_Click(object sender, EventArgs e)
    {
        SiteConfig.SiteOption.SafeDomain = "";
        SiteConfig.Update();
        function.WriteSuccessMsg("操作成功");
    }
    //------------------------------------辅助方法
    /// <summary>
    /// 检测用户或节点是否拥有循环
    /// </summary>
    /// <param name="tbname">表名:ZL_User</param>
    /// <param name="PK">主键:UserID</param>
    /// <param name="pname">父键名:ParentUserID</param>
    public void CheckLoopNodes(string tbname, string PK, string pname)
    {
        string fields = " " + PK + "," + pname + " ";
        DataTable dt = SqlHelper.ExecuteTable("SELECT " + fields + " FROM " + tbname);
        foreach (DataRow dr in dt.Rows)
        {
            DataTable nodedt = SqlHelper.ExecuteTable("with Tree as(SELECT * FROM " + tbname + " WHERE " + pname + "=" + dr[PK] + " UNION ALL SELECT a.* FROM " + tbname + " a JOIN Tree b on a." + pname + "=b." + PK + ") SELECT * FROM Tree AS A");
        }
    }
    protected void Close_Code_Btn_Click(object sender, EventArgs e)
    {
        SiteConfig.SiteOption.AdminKey = "";
        SiteConfig.Update();
        function.WriteSuccessMsg("关闭动态口令成功");
    }
}