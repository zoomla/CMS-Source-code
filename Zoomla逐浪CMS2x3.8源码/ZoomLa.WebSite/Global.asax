<%@ Application Language="C#" %>
<%@ Import Namespace="System" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Web.Routing" %>
<%@ Import Namespace="System.Configuration" %>
<%@ Import Namespace="ZoomLa.Common" %> 
<%@ Import Namespace="ZoomLa.Components" %>
<%@ Import Namespace="ZoomLa.BLL" %>
<%@ Import Namespace="ZoomLa.Model" %>
<%@ Import Namespace="ZoomLa.HtmlLabel" %>
<script RunAt="server">
    //public override void Init()
    //{
    //    PostAuthenticateRequest +=  MvcApplication_PostAuthenticateRequest;
    //    base.Init();
    //}
    //void MvcApplication_PostAuthenticateRequest(object sender, EventArgs e)
    //{
    //    HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);
    //}
    public static void RegisterRoutes(RouteCollection routes)
    {
        if (SiteConfig.SiteOption.DomainRoute.Equals("1"))
        {
            routes.Add("SecondRoute", new SecondRoute(""));
        }
        B_Route.RegisterRoutes(routes);
    }
    void Application_OnStart(object sender, EventArgs e)
    {
        RegisterRoutes(RouteTable.Routes);
        log4net.Config.XmlConfigurator.Configure(new FileInfo(Server.MapPath("/Config/log.config")));
        //Application["needCheckRefer"] = ZoomLa.Components.SiteConfig.SiteOption.NeedCheckRefer;
        //Application["allSiteJS"] = ZoomLa.Components.SiteConfig.SiteInfo.AllSiteJS;
        //SchTask_Timer.GetInstance().Interval = 10 * 60 * 1000;//10Minute
        //SchTask_Timer.GetInstance().Start();
        //--------
        try
        {
            TaskCenter.Init();
            TaskCenter.Start();
        }
        catch (Exception ex) { ZLLog.L("TaskCenter" + ex.Message); }
    }
    void Application_End(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(5000);
        string url = SiteConfig.SiteInfo.SiteUrl;
        System.Net.HttpWebRequest req = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
        System.Net.HttpWebResponse rsp = (System.Net.HttpWebResponse)req.GetResponse();
        string tmp = rsp.StatusDescription;
    }

    void Application_Error(object sender, EventArgs e)
    {
        try
        {
            Exception objErr = Server.GetLastError().GetBaseException();
            if (objErr.Message.Contains("不存在")) { return; }
            ZLLog.L(ZLEnum.Log.exception, new M_Log()
            {
                Source = Request.Url.ToString(),
                Message = "来源：Application_Error,原因：" + objErr.Message + "异常源：" + objErr.Source
            });
        }
        catch { }
    }
    void Application_AcquireRequestState(object sender, EventArgs e)
    {

    }
    void Application_BeginRequest(object sender, EventArgs e)
    {
        if (Request.RequestType.ToUpper() == "GET")
        {
            if (ZoomlaSecurityCenter.GetData())
            {
                ZLLog.L(ZLEnum.Log.safe, "安全:已拦截SQL注入");
                function.WriteMessage("产生错误的可能原因：你提交的参数不正确,包含恶意字符串,或检查系统是否开启了SQL防注入功能!", "", "非法SQL注入或存储!");
            }
        }
        if (Request.HttpMethod.ToUpper() == "POST")
        {
            //if (SiteConfig.SiteOption.SqlInjectOption == "1")//开启Post与Cookies防注入(不推荐开启,耗用资源过多)
            //{
            //    if (ZoomlaSecurityCenter.PostData() || ZoomlaSecurityCenter.CookieData())
            //        function.WriteMessage("产生错误的可能原因：你提交的参数不正确,包含恶意字符串,或检查系统是否开启了SQL防注入功能!", "", "非法SQL注入或存储!");
            //}
            if (System.Web.HttpContext.Current.Request.Files.Count > 0)
            {
                ZoomlaSecurityCenter.CheckUpladFiles();
            }
        }
        // if (Application["safeDomain"] != null && !string.IsNullOrEmpty(Application["safeDomain"].ToString()) && Request.RawUrl.ToLower().Contains(".aspx"))
        //{
        //   if (!ZoomlaSecurityCenter.IsSafeDomain(Application["safeDomain"].ToString().ToLower()))
        //  {
        //     function.WriteErrMsg("非安全域名,无法访问管理目录!!!");
        //  }
        // }
    }

    void Application_EndRequest(Object sender, EventArgs e)
    {
        
    }
    void Session_Start(object sender, EventArgs e)
    { }
    void Session_End(object sender, EventArgs e)
    {
        try
        {
            string name = Session["M_LoginName"] as string;
            if (!string.IsNullOrEmpty(name))
            {
                B_Admin.UpdateField("LastLogoutTime", DateTime.Now.AddMinutes(-10).ToString(), name);
            }
        }
        catch (Exception ex) { ZLLog.L(ZLEnum.Log.exception, "更新管理员最后登录时间失败,原因:" + ex.Message + "," + (Session["M_LoginName"] as string)); }
    }
</script>
