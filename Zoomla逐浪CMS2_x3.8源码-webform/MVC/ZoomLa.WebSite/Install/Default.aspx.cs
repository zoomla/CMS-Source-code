using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using ZoomLa.BLL;
using ZoomLa.BLL.Helper;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.Safe;
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.Install
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            IsInStall();
            if (!IsPostBack)
            {
                TxtSiteUrl.Text = "http://" + Request.ServerVariables["HTTP_HOST"].ToString();
                switch (System.Globalization.CultureInfo.CurrentCulture.Name.ToLower())
                {
                    case "en-us":
                        Licence_Lit.Text = SafeSC.ReadFileStr("/Config/ZL_licence_en.txt", true);
                        break;
                    case "zh-cn":
                    default:
                        Licence_Lit.Text = SafeSC.ReadFileStr("/Config/ZL_licence.txt", true);
                        break;
                }

            }
        }
        //检测环境
        protected void Stpe1_Next_Btn_Click(object sender, EventArgs e)
        {
            CurStep_Hid.Value = "2";
            CheckEnviorment();
        }
        protected void Step2_Pre_Btn_Click(object sender, EventArgs e)
        {
            CurStep_Hid.Value = "1";
            Install_Wzd.MoveTo(WizardStep1);
        }
        protected void Step2_Next_Btn_Click(object sender, EventArgs e)
        {
            CurStep_Hid.Value = "3";
        }
        protected void Step3_Pre_Btn_Click(object sender, EventArgs e)
        {
            CurStep_Hid.Value = "2";
            Install_Wzd.MoveTo(WizardStep2);
            CheckEnviorment();
        }
        //检测配置是否正确,开始执行SQL脚本
        protected void Step3_Next_Btn_Click(object sender, EventArgs e)
        {
            string dbname = TxtDataBase.Text.Trim();
            TxtPassword_Hid.Value = TxtPassword.Text.Trim();
            string connstr = GetConnstr(dbname);
            try
            {
                switch (SqlVersion_DP.SelectedValue.ToLower())
                {
                    case "local":
                        //DBHelper.DB_Clear(connstr);
                        break;
                    case "mssql":
                        //if (!DBHelper.DB_Exist(connstr, dbname)) { function.Script(this, "alert('数据库[" + dbname + "]不存在,请先创建好数据库,再执行该步!');"); return; }
                        break;
                    case "oracle":
                        function.Script(this, "alert(\"" + Resources.L.该版本仅对商业用户开放 + "\");");
                        return;
                }
                WriteConnstr(connstr);
                if (!ignoreSql_chk.Checked)
                {
                    function.Script(this, "installDB();");
                }
                else
                {
                    Install_Wzd.MoveTo(WizardStep4);
                    CurStep_Hid.Value = "4";
                }
            }
            catch (Exception ex) { ZLLog.L("安装时出错,原因:" + ex.Message); function.Script(this, "showAlert(\"" + HttpUtility.UrlEncode(ex.Message.Replace(" ", "")) + "\");"); return; }
        }
        protected void Step4_Pre_Btn_Click(object sender, EventArgs e)
        {
            CurStep_Hid.Value = "3";
            Install_Wzd.MoveTo(WizardStep3);
        }
        //写入配置信息,并创建用户
        protected void Step4_Next_Btn_Click(object sender, EventArgs e)
        {
            B_User buser = new B_User();
            string message = string.Empty;
            if (TxtCustomPath.Text.Replace(" ", "").Length < 3)
            {
                function.WriteErrMsg(Resources.L.后台路径不能少于三位);
                return;
            }
            //URLRewriter.Config.RewriteConfigUpdate rupdate = new URLRewriter.Config.RewriteConfigUpdate();
            //rupdate.Update(HttpContext.Current, this.TxtCustomPath.Text.Replace(" ", ""));
            SiteConfig.SiteOption.ManageDir = this.TxtCustomPath.Text.Replace(" ", "");
            SiteConfig.SiteInfo.SiteTitle = TxtSiteTitle.Text.Trim();
            SiteConfig.SiteInfo.SiteName = TxtSiteName.Text.Trim();
            SiteConfig.SiteInfo.SiteUrl = TxtSiteUrl.Text.Trim();
            SiteConfig.SiteInfo.WebmasterEmail = TxtEmail.Text.Trim();
            SiteConfig.SiteOption.SiteManageCode = TxtSiteManageCode.Text.Trim();
            SiteConfig.SiteOption.IsFlashPaper = false;
            SiteConfig.Update();
            //----------------------------------------------------
            XmlDocument xmlDoc2 = new XmlDocument();
            xmlDoc2.Load(Server.MapPath("/Config/AppSettings.config"));
            XmlNodeList amde = xmlDoc2.SelectSingleNode("appSettings").ChildNodes;
            foreach (XmlNode xn in amde)
            {
                XmlElement xe = (XmlElement)xn;
                if (xe.GetAttribute("key").ToString() == "Installed")
                    xe.SetAttribute("value", "true");
            }
            xmlDoc2.Save(Server.MapPath("/Config/AppSettings.config"));
            M_AdminInfo adminMod = new M_AdminInfo()
            {
                AdminName = "admin",
                AdminPassword = TxtAdminPassword.Text.ToString().Trim()
            };
            //---添加用户
            bool isupdate = false;
            M_UserInfo muser = buser.GetUserByName("admin");
            if (!muser.IsNull) { isupdate = true; }
            else { muser = new M_UserInfo() { UserName = "admin" }; }
            muser.UserPwd = StringHelper.MD5(adminMod.AdminPassword);
            muser.RegTime = DateTime.Now;
            muser.RegTime = DateTime.Now;
            muser.LastLockTime = DateTime.Now;
            muser.LastLoginTimes = DateTime.Now;
            muser.LastPwdChangeTime = DateTime.Now;
            muser.Email = TxtEmail.Text;
            muser.Question = "admin";
            muser.Answer = function.GetRandomString(8);
            muser.GroupID = 1;
            muser.UserRole = ",1,";
            muser.SiteID = 1;
            muser.VIP = 0;
            muser.LastLoginIP = IPScaner.GetUserIP();
            muser.CheckNum = function.GetRandomString(6);
            if (isupdate) { buser.UpDateUser(muser); }
            else { muser.UserID = buser.Add(muser); }
            adminMod.AddUserID = muser.UserID;

            ZoomLa.BLL.Install.Add(adminMod);
            Install_Wzd.MoveTo(WizardStep5);
            CurStep_Hid.Value = "5";
        }
        protected void ReConfig_Btn_Click(object sender, EventArgs e)
        {
            CurStep_Hid.Value = "4";
            Install_Wzd.MoveTo(WizardStep4);
        }
        //执行SQL脚本
        protected void InstallDB_Btn_Click(object sender, EventArgs e)
        {
            try
            {
                string sqlPath = Server.MapPath("/App_Data/Data.sql");
                DBHelper.ExecuteSqlScript(GetConnstr(TxtDataBase.Text.Trim()), sqlPath);
                Install_Wzd.MoveTo(WizardStep4);
                CurStep_Hid.Value = "4";
            }
            catch (Exception ex) { ZLLog.L("安装时出错,原因:" + ex.Message); function.Script(this, "showAlert(\"" + HttpUtility.UrlEncode(ex.Message.Replace(" ", "")) + "\");"); }
        }
        #region Tools
        //是否安装过
        public void IsInStall()
        {
            string str = WebConfigurationManager.AppSettings["Installed"].ToLower();
            if (Convert.ToBoolean(str)) { Response.Redirect("/Default.aspx"); }
        }
        //环境检测,看权限是否给予,dll是否都存在
        public void CheckEnviorment()
        {
            string html = "";
            string rightTlp = "<div class='check_item_div'><i class='fa fa-check'></i><span>{0}</span></div>";
            string errorTlp = "<div class='check_item_div'><i class='fa fa-remove'></i><span>{0}</span></div>";
            //需要检测的文件
            string[] dirs = "/Config/,/JS/,/UploadFiles/,/Template/".Split(',');
            //string[] configs = "".Split(',');
            string[] dlls = "Zoomla.Safe,ZoomLa.BLL,ZoomLa.Components,ZoomLa.Common,ZoomLa.Web,ZoomLa.SQLDAL,ZoomLa.Model".Split(',');
            string[] files = new string[] { "AppSettings.config", "ConnectionStrings.config", "CRM_Dictionary.xml", "MakeSendList.config", "log.config", "Pages.config" };
            string[] files2 = new string[] { "URLRoute.config", "URLRewrite.config","globalization.config", "Guest.config",  "Site.config", "OrderStatus.xml", "StationGroup.config", "WaterConfig.config" };
            //检测环境
            double version = DataConverter.CDouble(Environment.Version.ToString().Substring(0, 3));
            if (version >= 4.0)
            {
                html += string.Format(rightTlp, Resources.L.当前点NET版本 + Environment.Version.ToString() + "," + Resources.L.验证通过);
            }
            else { html += string.Format(rightTlp, Resources.L.当前点NET版本 + Environment.Version.ToString() + "," + Resources.L.版本过低验证失败); }
            //---------------------------------
            if (File.Exists(Server.MapPath("/App_Data/data.sql"))) { html += string.Format(rightTlp, Resources.L.数据库脚本文件存在); }
            else { html += string.Format(errorTlp, Resources.L.数据库脚本文件不存在); }
            //---------------------------------
            string dllhtml = string.Format(rightTlp, Resources.L.程序集文件完整性验证通过);
            foreach (string dll in dlls)
            {
                if (!File.Exists(Server.MapPath("/Bin/" + dll + ".dll")))
                {
                    dllhtml = string.Format(errorTlp, "" + dll + Resources.L.程序集文件不完整性是否下载);
                    break;
                }
            }
            html += dllhtml;
            //---------------------------------
            foreach (string dir in dirs)
            {
                if (CheckDirAuth(Server.MapPath(dir)))
                {
                    html += string.Format(rightTlp, dir.Replace("/", "") + Resources.L.目录权限验证通过);
                }
                else { html += string.Format(errorTlp, dir.Replace("/", "") + Resources.L.目录权限验证未通过); }
            }
            Check_Basic_L.Text = html;
            //---------------------------------文件检测
            string filehtml = "", filehtml2 = "";
            foreach (string file in files)
            {
                if (File.Exists(Server.MapPath("/Config/" + file)))
                {
                    filehtml += string.Format(rightTlp, file + Resources.L.文件验证通过);
                }
                else { filehtml += string.Format(rightTlp, file + Resources.L.文件不存在); }
            }
            Check_File_L.Text = filehtml;
            foreach (string file in files2)
            {
                if (File.Exists(Server.MapPath("/Config/" + file)))
                {
                    filehtml2 += string.Format(rightTlp, file + Resources.L.文件验证通过);
                }
                else { filehtml2 += string.Format(rightTlp, file + Resources.L.文件不存在); }
            }
            Check_File2_L.Text = filehtml2;
        }
        public bool CheckDirAuth(string ppath)
        {
            try
            {
                if (File.Exists(ppath + "\\a.txt"))
                {
                    return true;
                }
                using (FileStream fs = new FileStream(ppath + "\\a.txt", FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    fs.Close();
                    fs.Dispose();
                }
                if (File.Exists(ppath + "\\a.txt"))
                {
                    System.IO.File.Delete(ppath + "\\a.txt");
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        public string GetConnstr(string dbname = "master")
        {
            switch (SqlVersion_DP.SelectedValue)
            {
                case "Local":
                    return @"Data Source=(localdb)\v11.0;Integrated Security=true;AttachDbFileName =" + Server.MapPath("/App_Data/ZoomlaDB.mdf") + ";";
                default:
                    string datasourcesa = TxtDataSource.Text.Trim();//数据库源sql_ip
                    string uname = TxtUserID.Text.Trim();//数据库用户名称sql_username
                    string upwd = TxtPassword_Hid.Value.Trim();//数据库用户口令sql_password
                    return string.Format(@"Data Source={0};Initial Catalog={1};User ID={2};Password={3};", datasourcesa, dbname, uname, upwd);
            }
        }
        //生成证书,并写入连接字符串
        private void WriteConnstr(string connstr)
        {
            SafeC.Cert_Update();
            connstr = SafeC.Cert_Encry(connstr);
            ZoomLa.BLL.Install.ChangeConnStr(connstr);
        }
        #endregion
    }
}