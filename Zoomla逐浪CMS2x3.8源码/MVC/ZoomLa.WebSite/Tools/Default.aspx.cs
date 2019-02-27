using System;
using System.Collections.Generic;
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
using ZoomLa.Safe;
using System.Configuration;

/*
 * 提供config文件的维护功能
 */
namespace ZoomLaCMS.Tools
{
    public partial class Default : System.Web.UI.Page
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
            if (!badmin.CheckLogin())
            {
                SupperGavel.Visible = true;
                SupperGavelCon.Visible = false;
            }
            else if (badmin.CheckLogin()) { B_Admin.IsSuperManage(); admintop.Visible = true; }
            if (!IsPostBack)
            {
                if (ConfigurationManager.ConnectionStrings["Connection String"].ConnectionString.Contains("Data Source"))
                {
                    SafeC.Cert_Update();
                    ZoomLa.BLL.Install.ChangeConnStr(SafeC.Cert_Encry(SqlHelper.ConnectionString));
                }
                if (LoginCount >= 3) { function.Script(this, "EnableCode();"); }
            }
        }

        protected void Update_Btn_Click(object sender, EventArgs e)
        {
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
            SiteConfig.SiteInfo.Webmaster = "Zoomla";
            SiteConfig.SiteInfo.BannerUrl = "/Images/Qrcode.gif";//能力中心下载
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
        protected void Repair_Btn_Click(object sender, EventArgs e)
        {
            SiteConfig.Update();
            function.WriteSuccessMsg("恢复完成");
        }
        //开启调试
        protected void Develop_Btn_Click(object sender, EventArgs e)
        {
            {
                string webPath = Server.MapPath("/Web.config");
                XmlDocument web = new XmlDocument();
                web.Load(webPath);

                XmlNode compNode = web.SelectSingleNode("/configuration/system.web/compilation");
                XmlNode custNode = web.SelectSingleNode("/configuration/system.web/customErrors");

                if (compNode == null)
                {
                    XmlElement compEle = web.CreateElement("compilation");
                    compEle.SetAttribute("debug", "true");
                    web.SelectSingleNode("/configuration/system.web").AppendChild(compEle);
                }
                else
                {
                    if (compNode.Attributes["debug"] == null) { compNode.Attributes.Prepend(web.CreateAttribute("debug")); }
                    compNode.Attributes["debug"].Value = "true";
                }

                if (custNode == null)
                {
                    XmlElement compEle = web.CreateElement("customErrors");
                    compEle.SetAttribute("mode", "Off");
                    compEle.SetAttribute("defaultRedirect", "~/Prompt/GenericError.html");
                    web.SelectSingleNode("/configuration/system.web").AppendChild(compEle);
                }
                else
                {
                    if (custNode.Attributes["mode"] == null) { custNode.Attributes.Prepend(web.CreateAttribute("mode")); }
                    custNode.Attributes["mode"].Value = "Off";
                }
                web.Save(webPath);
            }
            function.WriteSuccessMsg("开启成功!");
        }
        //关闭调试
        protected void ColseDevlop_Btn_Click(object sender, EventArgs e)
        {
            string webPath = Server.MapPath("/Web.config");
            XmlDocument web = new XmlDocument();
            web.Load(webPath);
            XmlNode compNode = web.SelectSingleNode("/configuration/system.web/compilation");
            if (compNode != null && compNode.Attributes["debug"] != null)
            {
                compNode.Attributes["debug"].Value = "false";
                web.Save(webPath);
            }
            function.WriteSuccessMsg("关闭成功!");
        }
        protected void Check_Btn_Click(object sender, EventArgs e)
        {
            FileInfo_Div.Visible = true;
            string[] files = new string[] { "AppSettings.config", "ConnectionStrings.config", "Guest.config", "oa.config", "Plat.config", "Pages.config", "URLRoute.config", "URLRewrite.config" };
            string filehtml = "";
            foreach (string filename in files)
            {
                string existIcon = "";
                if (!FileSystemObject.IsExist(function.VToP("/Config/" + filename), FsoMethod.File))
                {
                    existIcon = "fa fa-remove";
                }
                else
                {
                    existIcon = "fa fa-check";
                }
                filehtml += "<tr><td>" + filename + "</td><td><span class='" + existIcon + "'></span></td></tr>";
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
            M_AdminInfo admininfo = B_Admin.AuthenticateAdmin(UserName_T.Text, UserPwd_T.Text);
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
        protected void Encry_Btn_Click(object sender, EventArgs e)
        {
            try { After_T.Text = SafeC.Cert_Encry(Before_T.Text.Trim()); }
            catch (Exception ex) { After_T.Text = ex.Message; }
        }

        protected void Decry_Btn_Click(object sender, EventArgs e)
        {
            try { After_T.Text = SafeC.Cert_Decry(Before_T.Text.Trim()); }
            catch (Exception ex) { After_T.Text = ex.Message; }
        }

        protected void RepairData_Btn_Click(object sender, EventArgs e)
        {
            B_ModelField fieldBll = new B_ModelField();
            DataTable dt = fieldBll.Sel();
            foreach (DataRow dr in dt.Rows)
            {
                int fid = DataConverter.CLng(dr["FieldID"]);
                string content = DataConverter.CStr(dr["Content"]);
                if (!content.Contains("||") || !content.Contains("=")) continue;

                //1=是||否,Property=True,Default=
                string itemStr = content.Split(',')[0].Split('=')[1];
                M_ModelField fieldMod = fieldBll.SelReturnModel(fid);
                fieldMod.Content = content.Replace(itemStr, SortStr(itemStr));
                fieldBll.Update(fieldMod);
            }
            function.WriteSuccessMsg("操作成功");
        }
        private string SortStr(string str)
        {
            //合作网站||媒体推介||财经分享||学术研究
            //==>
            //合作网站|合作网站$0||媒体推介|媒体推介$0||财经分享|财经分享$0||学术研究|学术研究$0
            string context = "";
            string[] strx = str.Split(new string[] { "||" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string s in strx)
            {
                if (s.IndexOf("|") > 0)
                {
                    string[] ss = s.Split(new char[] { '|' });
                    if (ss.Length > 2)
                    {
                        context += SortStr(s.Replace("|", "||"));
                    }
                    else
                    {
                        if (s.IndexOf('$') == -1)//s|s
                        {
                            context += s + "$0";
                        }
                        else//s|s$0
                        {
                            context += s;
                        }
                    }
                }
                else
                {
                    if (s.IndexOf('$') == -1)//s
                    {
                        context += s + "|" + s + "$0";
                    }
                    else//s|$0
                    {
                        context += s.Split('$')[0] + "|" + s;
                    }
                }
                context += "||";
            }
            if (context.EndsWith("||"))
            {
                context = context.Remove(context.Length - 2);
            }
            return context;
        }
    }
}