using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZoomLa.BLL;
using ZoomLa.BLL.Exam;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.Model.Exam;
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.Controllers
{
    public class RSSController : Controller
    {
        //
        // GET: /RSS/
        B_Node nodeBll = new B_Node();
        B_Pub pubBll = new B_Pub();
        B_Content conBll = new B_Content();
        B_Content_Publish cpBll = new B_Content_Publish();
        public int Mid {
            get { return DataConverter.CLng(Request["ID"]); } }
        public void Default()
        {
            int NodeID = DataConverter.CLng(Request["nid"]);
            string message = string.Empty;
            string siteurl = SiteConfig.SiteInfo.SiteUrl + "/RSS/Default?NID=" + NodeID;
            string sitename = SiteConfig.SiteInfo.SiteName + nodeBll.GetNodeXML(NodeID).NodeName;
            //初始化头部
            message += "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\" ?><rss version=\"2.0\"><channel visittype=\"xml\" isapiwrite=\"0\" forum_url=\"" + siteurl + "\" boardid=\"0\" pageurl=\"" + siteurl + "\" sqlquerynum=\"0\" runtime=\".01563\"><title>" + sitename + "</title><link>" + siteurl + "</link><description>" + SiteConfig.SiteInfo.MetaDescription + " </description><language>zh-cn</language>";
            //读取栏目
            DataTable dt = nodeBll.SelByPid(NodeID);
            foreach (DataRow drs in dt.Rows)
            {
                message += "<item type=\"board\" depth=\"0\" bid=\"111\" child=\"10\"><title>" + drs["NodeName"] + "</title><category>栏目</category><author></author><pubDate></pubDate><description>点击标题查看--" + drs["NodeName"] + "--子栏目         </description><link>" + SiteConfig.SiteInfo.SiteUrl + "/rss/Default.aspx?NId=" + drs["NodeID"] + "</link></item>";
            }
            //读取文章
            message += "";//未实现
            //初始化底部
            message += "</channel></rss>";
            //打印出来
            Response.ContentType = "text/xml";
            Response.Write(message);
        }
        public ActionResult FormView()
        {
            int Mid = DataConverter.CLng(Request["pid"]);
            if (Mid < 1) { function.WriteErrMsg("未指定模型ID");return Content(""); }
            M_Pub pubMod = pubBll.SelReturnModel(Mid);
            ViewBag.PubName = pubMod.PubName;
            ViewBag.PubTemplate = pubMod.PubTemplate;
            ViewBag.PubContent = pubMod.Pubinfo;
            return View();
        }
        public ActionResult News()
        {
            int ID = Convert.ToInt32(Request.QueryString["ID"]);
            M_CommonData conMod = conBll.GetCommonData(ID);
            DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, "Select * From ZL_C_Article Where ID=" + conMod.ItemID);
            ViewBag.Title = conMod.Title;
            ViewBag.CreateTime = conMod.CreateTime.ToString("yyyy年MM月dd日");
            ViewBag.Content = dt.Rows[0]["Content"].ToString();
            return View();
        }
        public ActionResult ViewPublish()
        {
            int Pid = DataConverter.CLng(Request["pid"]);
            M_Content_Publish curMod = new M_Content_Publish();
            M_Content_Publish pubMod = cpBll.SelReturnModel(Pid);
            if (pubMod == null) { function.WriteErrMsg("报纸不存在");return Content(""); }
            if (Mid > 0) { curMod = cpBll.SelReturnModel(Mid); }
            else { curMod = cpBll.SelModel(Mid, Pid, "n"); }
            if (curMod == null) { function.WriteErrMsg("[" + pubMod.NewsName + "]下没有任何文章!!"); return Content("");}

            ViewBag.PID = pubMod.ID;
            ViewBag.ShowDown = !string.IsNullOrEmpty(curMod.AttachFile);
            M_Content_Publish prMod = new M_Content_Publish();
            prMod.NewsName = pubMod.NewsName;
            prMod.ImgPath = curMod.ImgPath;
            prMod.ID = curMod.ID;
            prMod.Title = curMod.Title;
            prMod.Json = curMod.Json;
            return View(prMod);
        }
        public void ViewPublish_Down()
        {
            M_Content_Publish cpMod = cpBll.SelReturnModel(1);
            SafeSC.DownFile(cpMod.AttachFile);
        }
    }
}
