using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.Model.Edit;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLaCMS.Controllers
{
    public class EditController : Controller
    {
        B_User buser = new B_User();
        B_Group gpBll = new B_Group();
        B_Node nodeBll = new B_Node();
        public ActionResult Default()
        {
            ViewBag.uptp = Request["uptp"] ?? "";
            ViewBag.doctitle = Request["doctitle"] ?? "";
            ViewBag.doctype = Request["doctype"] ?? "";
            ViewBag.IsLogin = buser.CheckLogin();
            return View();
        }

        public ActionResult MastDefault()
        {
            ViewBag.uptp = Request["uptp"] ?? "";
            ViewBag.doctitle = Request["doctitle"] ?? "";
            ViewBag.doctype = Request["doctype"] ?? "";
            return View();
        }
        [ChildActionOnly]
        public PartialViewResult Edit()
        {
            string doctype = Request["doctype"] ?? "";
            if (Request["Tg"] == "true")
            {
                ViewBag.tgid = Server.HtmlEncode(Request.QueryString["ID"]);
                if (Request.QueryString["Tgid"] != "0" && Request.QueryString["Tgid"] != "")
                {
                    if (Request.QueryString["title"] == "" || Request.QueryString["title"] == null)
                    {
                        Session["Tgid"] = Server.HtmlEncode(Request.QueryString["Tg"]);
                    }
                    else {
                        Session["Tgid"] = Server.HtmlEncode(Request.QueryString["Tg"]);
                    }
                    Response.End(); return null;
                }
            }
            string url = "http://" + Request.ServerVariables["HTTP_HOST"].ToString() + Request.ServerVariables["PATH_INFO"].ToString();  //获得URL的值
            int i = url.LastIndexOf("/");
            url = url.Substring(0, i);
            Session["URL"] = url;
            string tagkey = Request.Form["TagKey_T"];
            if (!string.IsNullOrEmpty(Request.Form["need"]))
            {
                //获取文件名-读取xml-再依据xml-获取表A-表B
                XmlDocument doc = new XmlDocument();
                string docName = tagkey.Split('/')[0];
                Response.Write(tagkey);
                Response.Flush();
                Response.End(); return null;
            }
            ViewBag.doctype = doctype;
            return PartialView();
        }
        public void BatUpload()
        {
            string Case = Request["case"] ?? "";
            if (!string.IsNullOrEmpty(Case))
            {
                string myCase = Server.UrlDecode(Case);
                SafeSC.CreateDir("~/uploadFiles/DocTemp/", myCase);
                string path = Server.MapPath("~/uploadFiles/DocTemp/" + myCase + "/");
                //目录建好了，然后我们开始存文档
                Response.Clear();
                //ID为文档的主键，如果ID不为空，则更新数据，否则新建一条记录
                string ID = Request.Params["ID"];
                string DocTitle, content;
                DocTitle = "test";
                if (!string.IsNullOrEmpty(ID))
                {
                    DocTitle = Server.UrlDecode(Request.Params["DocTitle"]);
                }
                DocTitle = Server.UrlDecode(Request.Params["DocTitle"]);
                content = Server.UrlDecode(Request.Params["content"]);
                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase upPhoto = Request.Files[0];
                    int upPhotoLength = upPhoto.ContentLength;
                    byte[] PhotoArray = new Byte[upPhotoLength];
                    Stream PhotoStream = upPhoto.InputStream;
                    PhotoStream.Read(PhotoArray, 0, upPhotoLength); //这些编码是把文件转换成二进制的文件
                    if (DocTitle.ToLower().Contains(".cshtml") || DocTitle.ToLower().Contains(".aspx") || DocTitle.ToLower().Contains(".exe")) { return; }
                    SafeSC.SaveFile(path, DocTitle, PhotoArray);
                }
                Response.ContentType = "text/plain";
                Response.Write("Complete"); Response.Flush(); Response.End(); return;
            }
        }
        public ActionResult EditList()
        {
            PageSetting setting = new PageSetting() { itemCount = 0 };
            return View(setting);
        }
        public PartialViewResult Edit_Data()
        {
            PageSetting setting = new PageSetting() { itemCount = 0 };
            return PartialView(setting);
        }
        public void S_Word()
        {
            Response.Clear();
            Response.ContentType = "Application/msword";
            Response.Charset = "utf-8";
            string DocType = Request.QueryString["DocType"];
            string ID = Request.QueryString["ID"];
            if (ID == null || ID == "")
            {
                Response.End(); return;
            }
        }
        public ActionResult ShowEdit()
        {
            string str = base.Request.QueryString["Dir"];
            string m_UserInput = base.Request.QueryString["OpenWords"];
            string m_ConfigUploadDir = SiteConfig.SiteOption.UploadDir;
            string m_ParentDir = "";
            DataTable dt = new DataTable();
            if (!string.IsNullOrEmpty(m_ConfigUploadDir))
            {
                string configUploadDir = m_ConfigUploadDir;
                if (!string.IsNullOrEmpty(str) && (str.LastIndexOf("/") > 0))
                {
                    m_ParentDir = str.Remove(str.LastIndexOf("/"), str.Length - str.LastIndexOf("/"));
                }
                if (!string.IsNullOrEmpty(str)) { str = configUploadDir + "/DocTemp/" + str; }
                else { str = configUploadDir + "/DocTemp"; }
                string dirPath = Server.MapPath(str);
                //回朔Bug处理,1,只能访问上传目录 2,替换../
                if (!dirPath.ToLower().Contains(configUploadDir.ToLower().Replace("/", @"\"))) { function.WriteErrMsg("路径错误!"); return Content(""); }
                DirectoryInfo info = new DirectoryInfo(dirPath);
                if (info.Exists)
                {
                    string str2 = "";
                    try
                    {
                        if (!string.IsNullOrEmpty(base.Request.QueryString["Dir"])) { str2 = m_ConfigUploadDir + "/DocTemp/" + base.Request.QueryString["Dir"]; }
                        else
                        {
                            str2 = m_ConfigUploadDir + "/DocTemp";
                        }
                        if (string.IsNullOrEmpty(Request.Form["TxtSearchKeyword"]))
                        {
                            //为空并且显示目录

                            dt = FileSystemObject.GetDirectoryInfos(base.Request.PhysicalApplicationPath + str2, FsoMethod.All);
                            dt.DefaultView.RowFilter = "content_type not in ('xml')";
                        }
                        else
                        {
                            //如果不为空则搜索
                            dt = FileSystemObject.SearchFiles(base.Request.PhysicalApplicationPath + str2 + "/DocTemp", Request.Form["TxtSearchKeyword"]);
                            dt.DefaultView.RowFilter = "content_type not in ('xml')";
                        }
                    }
                    catch { function.WriteErrMsg(str2);return Content(""); }
                }
                else { function.WriteErrMsg(info.FullName);return Content(""); }
                ViewBag.currentdir = str;
            }
            ViewBag.m_userinput = m_UserInput;
            ViewBag.dir = Request["dir"] ?? "";
            return View(dt.DefaultView.ToTable());
        }
        public ActionResult SourceList()
        {
            return View();
        }
        public ActionResult Statistics()
        {
            M_UserInfo mu = buser.GetLogin();
            int Articles = 0, BEcount = 0;
            string GroupName = gpBll.GetByID(mu.GroupID).GroupName;
            int gid = DataConverter.CLng(Request["GID"]);
            if (gid > 0)
            {
                int nid = DataConverter.CLng(Request.QueryString["NodeID"]);
                //节点配置文件信息
                M_Node node = nodeBll.GetNodeXML(DataConverter.CLng(DataConverter.CLng(Request.QueryString["NodeID"])));
                XmlDocument Xml = new XmlDocument();
                try
                {
                    Xml.Load(Server.MapPath("/Config/Payment.xml"));
                }
                catch (Exception)
                {
                    function.WriteErrMsg("出现错误");return Content("");
                }
                //读取用户组的可浏览篇数
                string GroupN = function.GetChineseFirstChar(GroupName);
                XmlNode Xn = Xml.SelectSingleNode("UserGroups/" + GroupN + "/Manner");
                //该会员组可查看篇数
                Articles = DataConverter.CLng(Xn.Attributes["Articles"].Value);
                if (BEcount <= Articles) { }
                else {
                    Response.Write("2");
                    Response.End(); return null;
                }
            }
            return View();
        }
        public ActionResult Submission()
        {
            if (!B_User.CheckIsLogged()) { return null; }
            return View();
        }
        public ActionResult Doc()
        {
            ViewBag.islogin = buser.CheckLogin();
            return View();
        }
        public string LoadDoc()
        {
            long version = long.Parse(Request["version"]);
            if (version < DocCache.verMod.version)
            {
                return JsonConvert.SerializeObject(DocCache.verMod);
            }
            else { return "0"; }
        }
        public void UpdateDoc()
        {
            string html = Request.Form["html"];
            DocCache.verMod.html = html;
            DocCache.verMod.sessionID = Session.SessionID;
            DocCache.verMod.version = long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));
            Response.Write(DocCache.verMod.version); Response.Flush(); Response.End();
        }
    }
}
