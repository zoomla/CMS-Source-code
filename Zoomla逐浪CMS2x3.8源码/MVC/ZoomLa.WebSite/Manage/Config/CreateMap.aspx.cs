using System;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.SQLDAL;

/*
 * 重新生成SiteMap，用于支持全站搜索
 */

namespace ZoomLaCMS.Manage.Config
{
    public partial class CreateMap : System.Web.UI.Page
    {
        private string xmlPath = "/Config/SiteMap.config";
        protected void Page_Load(object sender, EventArgs e)
        {
            B_Admin.CheckIsLogged();
            if (!IsPostBack)
            {
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='SiteInfo.aspx'>系统设置</a></li><li><a href='SiteInfo.aspx'>网站配置</a></li><li class='active'>页面索引(用于主页右上方的快速搜索功能)</li>");
            }
        }
        protected void Sure_Btn_Click(object sender, EventArgs e)
        {
            CreateSiteMap(Server.MapPath("/Manage"), Server.MapPath(xmlPath));
            function.WriteSuccessMsg("生成后台索引成功");
        }
        protected void SureUser_Btn_Click(object sender, EventArgs e)
        {
            CreateMvcSiteMap();
            function.WriteSuccessMsg("生成MVC用户索成功");
            //CreateSiteMap(Server.MapPath("/Areas/User/Views/"), Server.MapPath("/Config/UserMap.config"));
            //function.WriteSuccessMsg("生成用户索引成功");
        }
        #region 站点搜索索引
        ///// <summary>
        ///// 生成搜索索引文件
        ///// </summary>
        ///// <param name="path">文件目录</param>
        ///// <param name="xmlPath">配置文件名</param>
        //public static DataTable CreateSiteMap(string path, string xmlPath)
        //{
        //    //忽略目录
        //    //忽略页面
        //    Regex reg = new Regex(@"(?<=<title>).*(?=</title>)", RegexOptions.IgnoreCase);
        //    string[] dirs = Directory.GetDirectories(path, "*", SearchOption.AllDirectories);
        //    DataTable dt = new DataTable();
        //    dt.Columns.Add(new DataColumn("Title", typeof(string)));
        //    dt.Columns.Add(new DataColumn("Url", typeof(string)));
        //    //dt.Columns.Add(new DataColumn("PPath", typeof(string)));
        //    dt.Columns.Add(new DataColumn("Desc", typeof(string)));
        //    for (int d = 0; d < dirs.Length; d++)
        //    {
        //        string[] files = Directory.GetFiles(dirs[d]);
        //        for (int i = 0; i < files.Length; i++)
        //        {
        //            if (files[i].EndsWith(".aspx") || files[i].EndsWith(".cshtml") || files[i].EndsWith(".htm"))
        //            {
        //                DataRow dr = dt.NewRow();
        //                dr["Title"] = DecreateTitle(reg.Match(File.ReadAllText(files[i])).Value);
        //                dr["Url"] = function.PToV(files[i]);
        //                //dr["PPath"] = files[i];
        //                dt.Rows.Add(dr);
        //            }
        //        }
        //    }
        //    dt.TableName = "SiteMap";
        //    dt.WriteXml(xmlPath);
        //    return dt;
        //}
        public void CreateMvcSiteMap() 
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Title", typeof(string)));
            dt.Columns.Add(new DataColumn("Url", typeof(string)));
            dt.Columns.Add(new DataColumn("Desc", typeof(string)));
            Assembly models = Assembly.Load("ZoomLaCMS");//装载程序集,通常是以dll为单位
            foreach (Type t in models.GetTypes())
            {
                if (!t.IsClass) { continue; }
                string assembleName = t.Assembly.GetName().Name;
                string fullName = t.Namespace + "." + t.Name;
                //Controller  只处理Controller 
                if (t.Name.EndsWith("Controller")) { 
                   //Controller classMod = ReflectionHelper.CreateInstance<Controller>(fullName, assembleName);
                    System.Reflection.MethodInfo[] methods = t.GetMethods();
                    foreach (var method in t.GetMethods())
                    {
                        if (method.ReturnType.ToString().Equals("System.Web.Mvc.ActionResult"))
                        {
                            //失效模块或不开放页面不加入搜索
                            if (fullName.StartsWith("ZoomLaCMS.Areas.Mis")) {continue; }
                            DataRow dr=dt.NewRow();
                            dr["Url"] = ControllerToRoute(fullName + "." + method.Name);
                            //文件和标题不存在则不添加
                            dr["Title"] = DecreateTitle(ControllerToCSHtml(fullName + "." + method.Name));
                            if (string.IsNullOrEmpty(DataConvert.CStr(dr["Title"]))) { continue; }
                            dt.Rows.Add(dr);
                        }
                    }//method end;
                }
            }//type end;
            dt.TableName = "SiteMap";
            dt.WriteXml(Server.MapPath("/Config/UserMap.config"));
        }
        /// <summary>
        /// 生成搜索索引文件
        /// </summary>
        /// <param name="path">文件目录</param>
        /// <param name="xmlPath">配置文件名</param>
        public DataTable CreateSiteMap(string path, string xmlPath)
        {
            string[] dirs = Directory.GetDirectories(path, "*", SearchOption.AllDirectories);
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Title", typeof(string)));
            dt.Columns.Add(new DataColumn("Url", typeof(string)));
            //dt.Columns.Add(new DataColumn("PPath", typeof(string)));
            dt.Columns.Add(new DataColumn("Desc", typeof(string)));
            for (int d = 0; d < dirs.Length; d++)
            {
                string[] files = Directory.GetFiles(dirs[d]);
                for (int i = 0; i < files.Length; i++)
                {
                    if (files[i].EndsWith(".aspx"))
                    {
                        DataRow dr = dt.NewRow();
                        dr["Title"] = DecreateTitle(files[i]);
                        dr["Url"] = function.PToV(files[i]);
                        if (string.IsNullOrEmpty(DataConverter.CStr(dr["Title"]))) { continue; }
                        //dr["PPath"] = files[i];
                        dt.Rows.Add(dr);
                    }
                }
            }
            dt.TableName = "SiteMap";
            dt.WriteXml(xmlPath);
            return dt;
        }
        /// <summary>
        /// 根据路径,获取页面标题,支持MVC和WebForm
        /// </summary>
        /// <param name="vpath">页面aspx或cshtml的虚拟路径</param>
        private string DecreateTitle(string vpath)
        {
            if (string.IsNullOrEmpty(vpath)) { return ""; }
            string site = SiteConfig.SiteInfo.SiteName;
            Regex reg = new Regex(@"(?<=<title>).*(?=</title>)", RegexOptions.IgnoreCase);
            Regex reg2 = new Regex("(?<=<%=.*\").*(?=\".*%>)", RegexOptions.IgnoreCase);
            string title = reg.Match(File.ReadAllText(function.VToP(vpath))).Value;
            title = title.Replace("<%=Call.SiteName%>", site).Replace("<%:Call.SiteName%>", site);
            title = title.Replace("@Call.SiteName", site).Replace("@(Call.SiteName)", site);
            return title;
        }
        private string ControllerToCSHtml(string fullName)
        {
            //string[] ignore = { "Home/AddMis_Add","Change/Mobile_1","Change/Email_1","Change/Email_2","Change/Email_SendEmail","Change/Email_SendNewEmail","Change/GetPassword_Answer","Change/GetPassWord_Mobile"
            //                      ,"Index/Register_","Money/GetRedPacket","DrawBack_Add","UserShop/Apply_Add","App/TlpShow","FrontBaike/AddRef","FrontBaike/SelClass"
            //                      ,"D3/Test.cshtml","Site/"
            //                  };
            //    ZoomLaCMS.Areas.Mis.Controllers.ApprovalController.Index
            //   /Areas/Mis/Approval/Index ||/Controllers/App/Default.cshtml
            fullName = fullName.Replace("ZoomLaCMS.", "/").Replace(".Controllers.", "/Views/").Replace("Controller.", "/").Replace(".", "/");
            //  /Controllers/App/Default.cshtml -->
            fullName = fullName.Replace("/Controllers/", "/Views/");
            fullName += ".cshtml";
            if (!File.Exists(function.VToP(fullName))) { return ""; }
            return fullName;
            //if (!File.Exists(function.VToP(fullName)))
            //{
            //    if (ignore.FirstOrDefault(p => fullName.Contains(p)) != null) { return; }
            //    throw new Exception(fullName+"|||"+function.VToP(fullName));
            //}
        }
        //将其转化为路由路径
        private string ControllerToRoute(string fullName)
        {
            //ZoomLaCMS.Areas.Mis.Controllers.ApprovalController.Index
            fullName = fullName.Replace("ZoomLaCMS.Areas.", "/").Replace(".Controllers.", "/").Replace("Controller.", "/");
            //	ZoomLaCMS/PostBar/EditContent
            fullName = fullName.Replace("ZoomLaCMS/", "/");
            fullName = fullName.Replace("/FrontBaike/", "/Baike/");
            return fullName;
        }
        #endregion 
    }
}