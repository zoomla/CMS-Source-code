namespace ZoomLa.Web
{
    using System;
    using System.Data;
    using System.Configuration;
    using System.Web;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.WebControls.WebParts;
    using System.Web.UI.HtmlControls;
    using ZoomLa.DALFactory;
    using ZoomLa.IDAL;
    using ZoomLa.Model;
    using System.Web.Configuration;
    using ZoomLa.Components;
    using System.Globalization;
    using ZoomLa.BLL;

    /// <summary>
    /// 后台管理页基类
    /// </summary>
    public class AdminPage : System.Web.UI.Page
    {
        private const string StyleSheetThemeSessionName = "AdminPage_StyleSheetTheme";
        private const string ThemesDirectoryName = "App_Themes";        
        public AdminPage()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// 重写初始化页面
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            CheckMultLogin();
            //检测页面操作权限
            CheckPagePermission();
        }
        /// <summary>
        /// 检验是否多重登录
        /// </summary>
        private void CheckMultLogin()
        {
            B_Admin bll = new B_Admin();
            bll.CheckMulitLogin();
        }
        private void CheckPagePermission()
        {

        }
        public string GetManagePath()
        {
            return SiteConfig.SiteOption.ManageDir.ToLower(CultureInfo.CurrentCulture);
        }

        public static void WriteErrMsg(string errorMessage)
        {
            WriteErrMsg(errorMessage, string.Empty);
        }

        public static void WriteErrMsg(string errorMessage, string returnurl)
        {
            Utility.WriteErrMsg(errorMessage, returnurl);
        }

        public static void WriteSuccessMsg(string successMessage)
        {
            WriteSuccessMsg(successMessage, string.Empty);
        }

        public static void WriteSuccessMsg(string successMessage, string returnurl)
        {
            Utility.WriteSuccessMsg(successMessage, returnurl);
        }

        public string StyleSheetPath
        {
            get
            {
                return (this.BasePath + "App_Themes/" + this.StyleSheetTheme + "/");
            }
        }

        public string BasePath
        {
            get
            {
                return Utility.GetBasePath(base.Request);
            }
        }

        public override string StyleSheetTheme
        {
            get
            {
                if (HttpContext.Current.Session == null)
                {
                    return "AdminDefaultTheme";
                }
                //if (this.Session["AdminPage_StyleSheetTheme"] == null)
                //{
                //    ID_Admin IAdmin = IDal.CreateAdmin();
                //    M_AdminInfo admininfo = IAdmin.GetAdminByName(MYContext.Current.AdminPrincipal.AdminName);
                //    if (admininfo == null || string.IsNullOrEmpty(admininfo.Theme))
                //    {
                //        return "AdminDefaultTheme";
                //    }
                //    this.Session.Add("AdminPage_StyleSheetTheme", admininfo.Theme);
                //}

                //浏览页面时报错（找不到 MYContext），所以暂时注释掉

                return (string)this.Session["AdminPage_StyleSheetTheme"];
            }
        }
    }
}