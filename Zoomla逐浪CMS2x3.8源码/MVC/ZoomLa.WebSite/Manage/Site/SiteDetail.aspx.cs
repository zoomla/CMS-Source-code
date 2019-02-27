using Microsoft.Web.Administration;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
namespace ZoomLaCMS.Manage.Site
{
    public partial class SiteDetail :CustomerPageAction
    {
        protected string siteName, appName, webPath, sitePort, siteDomain;
        protected ServerManager iis = new ServerManager();
        protected IISHelper iisHelper = new IISHelper();
        protected IISWebSite siteModel = new IISWebSite();
        protected B_Admin badmin = new B_Admin();
        protected B_User buser = new B_User();
        protected EnviorHelper enHelper = new EnviorHelper();
        protected B_Site_SiteList siteBll = new B_Site_SiteList();
        protected M_Site_SiteList siteM = new M_Site_SiteList();
        protected string siteID;
        public string piePercent;
        //long banSize = 1073741824;
        public delegate string AsyncInvokeFunc(string path);//与目标方法必须参数返回均相同.
        public string getFileSize(string path)
        {
            long size = FileSystemObject.getDirectorySize(path);
            string used = FileSystemObject.ConvertSizeToShow(size);
            return used;
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            //此处控件未实例化，不能对控件进行操作
            if (!string.IsNullOrEmpty(Request.QueryString["remote"]))
            {
                this.MasterPageFile = "~/manage/Site/OptionMaster.master";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            IdentityAnalogue ia = new IdentityAnalogue();
            ia.CheckEnableSA();
            siteName = Server.UrlDecode(Request.Params["siteName"]);
            siteID = Request.QueryString["siteID"];
            //SiteID无法获取到对应的，只能通过名字来访问了,ID需要通过循环遍历

            if ((string.IsNullOrEmpty(siteName) || iis.Sites[siteName] == null) && string.IsNullOrEmpty(siteID))
            {
                function.WriteErrMsg("未选择站点或要访问的站点不存在!!");
            }
            if (string.IsNullOrEmpty(siteID))
                siteID = iis.Sites[siteName].Id.ToString();
            else
            {
                siteName = iisHelper.GetNameBySiteID(siteID);
            }
            if (string.IsNullOrEmpty(siteName)) function.WriteErrMsg("未选择站 点或要访问的站点不存在!");

            appName = iis.Sites[siteName].Applications[0].ApplicationPoolName;
            webPath = iis.Sites[siteName].Applications[0].VirtualDirectories[0].PhysicalPath + "\\web.config";
            //按1G的量计算剩余空间,1G=1073741824
            //piePercent = "['已用空间" + used+ "'," + (double)size / (double)banSize + "],['剩余空间',"+((double)1-(double)size / (double)banSize)+"]";//  
            if (!IsPostBack)
            {
                AuthCheck();
                AsyncInvokeFunc getSize = getFileSize;//异步开始获取网站所占空间
                System.IAsyncResult asynResult = getSize.BeginInvoke(iis.Sites[siteName].Applications[0].VirtualDirectories[0].PhysicalPath, null, null);
                GetInfoLocal();
                piePercent = "['已用空间" + getSize.EndInvoke(asynResult) + "',1]";
            }
            Call.HideBread(Master);
        }
        //验证权限
        public void AuthCheck()
        {
            //---------------指定管理员权限管理
            if (badmin.CheckLogin())
            {
                //如果是管理员登录则不判断
            }
            else if (buser.CheckLogin())
            {
                //非管理员用户登录,开始判断
                string userID = buser.GetLogin().UserID.ToString();
                siteM = siteBll.SelByUserID(userID);//查找有无为该用户分配权限
                if (siteM == null || !siteBll.AuthCheck(siteID, userID)) function.WriteErrMsg("你没有管理站点的权限");
                rBtn.Visible = false;
                adminTR.Visible = false;
                userTR.Visible = true;
                siteList.DataSource = siteBll.SelAllByUserID(userID);
                siteList.DataTextField = "SiteName";
                siteList.DataValueField = "SiteID";
                siteList.DataBind();
                siteList.SelectedValue = siteID;
                this.Title = "独立控制台-站点详情";
            }
            else { function.WriteErrMsg("你无权限访问该站点!!"); }
        }
        //本地获取信息
        public void GetInfoLocal()
        {
            siteModel = iisHelper.GetSiteModel(siteName);
            Name.Text = siteModel.SiteName;
            siteAppPool.Text = siteModel.AppPool;

            lName.Text = siteModel.SiteName;
            lSitePool.Text = siteModel.AppPool;
            string bind = "";
            string lbind = "";
            for (int i = 0; i < siteModel.BindInfoList.Count; i++)
            {
                string[] temp = siteModel.BindInfoList[i].Split(':');
                if (i < 1)//第一行不显示移除，即最少要一行
                {
                    bind += "<tr><td>绑定信息：</td><td>";
                }
                else
                {
                    bind += "<tr><td>绑定信息：<a href='javascript:' onclick='remove(this)' title='移除'><i class='fa fa-remove'></i></a></td><td>";
                }

                bind += "<label style='float:left; display:block; width:50px;'>IP：</label><input type='text' name='siteIP'  class='form-control' style='width:150px;'   maxlength='15'  txt='IP不能为空' value='" + temp[0] + "' list='ipList'/>";
                bind += "<label style='float:left; display:block; width:50px;'>端口：</label>";
                bind += "<input type='text' name='sitePort' class='form-control'  maxlength='6'  txt='端口不能为空' style='width:150px;' value='" + temp[1] + "'/> ";
                bind += "<label style='float:left; display:block; width:50px;'>域名：</label><input type='text' name='Domain' style='width:150px;' class='form-control' txt='域名不能为空' value='" + temp[2] + "'/>";
                bind += "</td></tr>";

                //-----ReadOnly
                lbind += "<tr'><td>绑定信息：</td><td><label>IP：" + temp[0] + "</label><br/>";
                lbind += "<label >端口：" + temp[1] + "</label><br/>";
                lbind += "<label >域名：" + temp[2] + "</label>";
                lbind += "</td></tr>";
                sitePort = temp[1];
                siteDomain = temp[2];
            }
            string path = "";
            string lpath = "";
            foreach (string p in siteModel.PhysicalPathList)
            {
                path += "<tr><td>物理路径：</td><td>";
                path += "<input name='PPath' type='text' value='" + p + "' class='form-control text_md' maxlength='80' style='width:200px;' txt='物理路径为空'/>";
                path += "</td></tr>";

                lpath += "<tr><td>物理路径：</td><td>";
                lpath += "<label>" + p + "</label> ";
                lpath += "</td></tr>";
            }

            literalPath.Text = path;
            literalBind.Text = bind;
            labelPath.Text = lpath;
            labelBind.Text = lbind;

            siteM = siteBll.SelBySiteID(siteID);//如果无记录,则返回空
                                                //------EditMode
            opSystem.Text = enHelper.GetOSVersion();
            manageName.Text = siteM == null ? "" : siteM.Remind;
            iisVersion.Text = Request.ServerVariables["Server_SoftWare"].ToString();
            netVersion.SelectedValue = iis.ApplicationPools[appName].ManagedRuntimeVersion;
            runMode.SelectedValue = iis.ApplicationPools[appName].ManagedPipelineMode.ToString();
            serverIP.Text = enHelper.GetServerIP();
            //-----输出一个datalist服务于IP输入自动提示
            hideIPLiteral.Text = "<datalist id='ipList'><option>*</option>";
            foreach (string s in serverIP.Text.Split(','))
            {
                hideIPLiteral.Text += "<option>" + s + "</option>";
            }
            hideIPLiteral.Text += "</datalist>";
            //-----
            siteState.Text = iis.Sites[siteName].State == ObjectState.Started ? "运行中" : "已停止";
            ssBtn1.Text = iis.Sites[siteName].State == ObjectState.Started ? "停止" : "启动";

            //------ReadOnly
            lOPSystem.Text = enHelper.GetOSVersion();
            lManager.Text = (siteM == null || string.IsNullOrEmpty(siteM.SiteManager)) ? "尚未分配" : manageName.Text;
            //---颁发管理链接
            if (siteM != null)
            {
                string url = Request.Url.AbsoluteUri;
                string userName = Server.UrlEncode(buser.GetUserByUserID(DataConverter.CLng(siteM.SiteManager)).UserName);
                Regex re = new Regex("(?<=(://))[.\\s\\S]*?(?=(/))", RegexOptions.IgnoreCase);
                url = re.Match(url).Value;

                manageID.Value = "http://" + url + CustomerPageAction.customPath2 + "Site/Share.aspx?identity=" + userName;
                this.TextBox1.Text = manageID.Value;
                this.TextBox2.Text = manageID.Value;

            }

            lIISVersion.Text = Request.ServerVariables["Server_SoftWare"].ToString();
            lNetVersion.Text = iis.ApplicationPools[appName].ManagedRuntimeVersion;
            lRunMode.Text = iis.ApplicationPools[appName].ManagedPipelineMode == ManagedPipelineMode.Classic ? "经典模式" : "集成模式";
            lServerIP.Text = enHelper.GetServerIP();
            lSiteState.Text = iis.Sites[siteName].State == ObjectState.Started ? "运行中" : "已停止";
            ssBtn2.Text = iis.Sites[siteName].State == ObjectState.Started ? "停止" : "启动";

            try
            {
                //need Web.config 
                errorPage1.Text = enHelper.GetCustomError(webPath, 403).Redirect;
                errorPage2.Text = enHelper.GetCustomError(webPath, 404).Redirect;
                errorPage3.Text = enHelper.GetCustomError(webPath, 500).Redirect;
                errorPage4.Text = enHelper.GetCustomError(webPath, 503).Redirect;

                lErrorPage1.Text = enHelper.GetCustomError(webPath, 403).Redirect;
                lErrorPage2.Text = enHelper.GetCustomError(webPath, 404).Redirect;
                lErrorPage3.Text = enHelper.GetCustomError(webPath, 500).Redirect;
                lErrorPage4.Text = enHelper.GetCustomError(webPath, 503).Redirect;
            }
            catch { }

            //------------------高级设置
            //---只读
            ApplicationPool appPool = iis.ApplicationPools[appName];
            lmaxCPULimit.Text = appPool.Cpu.Limit.ToString();
            lconfigChange.Text = appPool.Recycling.DisallowRotationOnConfigChange ? "是" : "否";
            lTimeSpanRecy.Text = appPool.Recycling.PeriodicRestart.Time.TotalMinutes.ToString();
            lmaxMemory.Text = (appPool.Recycling.PeriodicRestart.Memory / 1024).ToString();
            lmaxPrivateMemory.Text = (appPool.Recycling.PeriodicRestart.PrivateMemory / 1024).ToString();
            string[] defaultDoc = iisHelper.GetDefaultDocBySiteName(siteName);
            for (int i = 0; i < defaultDoc.Length; i++)
            {
                ldefaultDoc.Text += defaultDoc[i] + "<br />";
                edefaultDoc.Text += defaultDoc[i] + ",\r";//编辑状态
            }
            //---编辑
            emaxCPULimit.Text = appPool.Cpu.Limit.ToString();
            econfigChange.SelectedValue = appPool.Recycling.DisallowRotationOnConfigChange.ToString();
            eTimeSpanRecy.Text = appPool.Recycling.PeriodicRestart.Time.TotalMinutes.ToString();
            emaxMemory.Text = (appPool.Recycling.PeriodicRestart.Memory / 1024).ToString();
            emaxPrivateMemory.Text = (appPool.Recycling.PeriodicRestart.PrivateMemory / 1024).ToString();
        }
        //Save Site(基础设置)
        protected void BasicSaveBtn_Click(object sender, EventArgs e)
        {
            IPDeal();
            M_UserInfo mu = buser.GetUserIDByUserName(manageName.Text.Trim());
            if (!string.IsNullOrEmpty(manageName.Text.Trim()))//保存时检测用户名
            {
                if (mu.IsNull)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('用户不存在');", true);
                    return;
                }
            }
            iisHelper.ChangeNetVersion(appName, netVersion.SelectedValue);
            iisHelper.ChangeMode(appName, runMode.SelectedValue);

            //-----Error Page,etc..

            //403 （禁止） 服务器拒绝请求。
            //404 （未找到） 服务器找不到请求的网页。
            //500 （服务器内部错误） 服务器遇到错误，无法完成请求。
            //503 （服务不可用)
            //enHelper.UpdateCustomError(webPath, 403, errorPage1.Text.Trim());
            //enHelper.UpdateCustomError(webPath, 404, errorPage2.Text.Trim());
            //enHelper.UpdateCustomError(webPath, 500, errorPage3.Text.Trim());
            //enHelper.UpdateCustomError(webPath, 503, errorPage4.Text.Trim());

            //Name change must be the last
            if (!siteName.Equals(Name.Text.Trim()))//名字最后改,不能为空不能小于三位，前台正则
            {
                iisHelper.ChangeSiteName(siteName, Name.Text.Trim());
            }
            //先检测目标SiteID是否存在，如果存在则更新，不存在则插入
            siteM = siteBll.SelBySiteID(siteID);
            if (siteM == null)
            {
                //Insert
                siteM = new M_Site_SiteList();
                siteM.SiteID = Convert.ToInt16(siteID);
                siteM.SiteManager = mu.UserID == 0 ? "" : mu.UserID.ToString();
                siteM.Remind = mu.UserName;
                siteM.SiteName = siteName;
                siteM.CreateDate = DateTime.Now;
                siteM.EndDate = DateTime.Now;
                siteBll.Insert(siteM);
            }
            else
            {
                //Update
                siteM.SiteManager = mu.UserID == 0 ? "" : mu.UserID.ToString();
                siteM.Remind = mu.UserName;
                siteBll.UpdateModel(siteM);
            }
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('修改成功');location='SiteDetail.aspx?siteName="+Server.UrlEncode(Name.Text.Trim())+"';", true);
            Response.Redirect(CustomerPageAction.customPath + "Site/SiteDetail.aspx" + Request.Url.Query);
        }
        //保存时IP与路径的逻辑
        private void IPDeal()
        {
            string[] siteIP = Request.Form["siteIP"].Split(',');
            string[] sitePort = Request.Form["sitePort"].Split(',');
            string[] domain = Request.Form["Domain"].Split(',');
            string[] ppath = Request.Form["PPath"].Split(',');

            if (siteIP.Length == 0) return;
            //-----Site Change
            if (sitePort.Length != domain.Length)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('端口与域名信息填写不完整');", true);
                return;
            }
            iisHelper.ClearSiteBindinfo(siteName);
            for (int i = 0; i < sitePort.Length; i++)
            {
                //*:86:www.
                iisHelper.AddSiteBindInfo(siteName, siteIP[i] + ":" + sitePort[i] + ":" + domain[i]);
            }
            for (int i = 0; i < ppath.Length; i++)
            {
                iisHelper.ChangePhysicalPath(siteName, ppath[i], i);
            }
        }
        //Start || Stop Site
        protected void SSSite_Click(object sender, EventArgs e)
        {
            if (iis.Sites[siteName].State == ObjectState.Started)
            {
                iis.Sites[siteName].Stop();
            }
            else
                iis.Sites[siteName].Start();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "location=location;", true);
        }
        protected void siteList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect("SiteDetail.aspx?siteID=" + siteList.SelectedValue);
        }
        //只用于保存高级设置,有修改才保存(检测未完成)
        protected void advSaveBtn_Click(object sender, EventArgs e)
        {
            ApplicationPool appPool = iis.ApplicationPools[appName];
            string cupLimit = emaxCPULimit.Text.Trim().ToLower();

            if (!cupLimit.Equals(appPool.Cpu.Limit.ToString()))
                iisHelper.ChangeCpuLimit(appName, DataConverter.CLng(cupLimit));
            iisHelper.ChangeRecycleOnConfigChange(appName, DataConverter.CBool(econfigChange.SelectedValue));//配置更改时回收

            iisHelper.ChangeRecycleTimeSpan(appName, DataConverter.CLng(eTimeSpanRecy.Text));
            appPool.Recycling.PeriodicRestart.Memory = (DataConverter.CLng(emaxMemory.Text) * 1024);
            appPool.Recycling.PeriodicRestart.PrivateMemory = (DataConverter.CLng(emaxPrivateMemory.Text) * 1024);
            iisHelper.AddDefaultDocBysiteName(siteName, edefaultDoc.Text.Split(','));

            Response.Redirect(CustomerPageAction.customPath + "Site/SiteDetail.aspx" + Request.Url.Query);
        }
        public string GetUrl()
        {
            if (siteDomain == "")
            {
                return lServerIP.Text + ":" + sitePort;
            }
            else
            {
                return siteDomain + ":" + sitePort;
            }
        }
    }
}