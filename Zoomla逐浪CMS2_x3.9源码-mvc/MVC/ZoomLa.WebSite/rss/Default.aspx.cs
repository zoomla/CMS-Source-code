using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ZoomLa.BLL;
using ZoomLa.Components;

namespace ZoomLaCMS.rss
{
    public partial class Default : System.Web.UI.Page
    {
        #region 业务逻辑
        protected B_Content bcbll = new B_Content();
        protected B_Model mll = new B_Model();
        protected B_ModelField fll = new B_ModelField();
        protected B_Comment cll = new B_Comment();
        protected B_Node bnbll = new B_Node();

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["NodeID"] = base.Request.QueryString["NId"];
                ViewState["action"] = base.Request.QueryString["action"];

                GetInit();
            }
        }

        #region 页面方法
        private int Nodeid
        {
            get
            {
                if (ViewState["NodeID"] != null)
                    return int.Parse(ViewState["NodeID"].ToString());
                else
                    return 0;
            }
            set
            {
                ViewState["NodeID"] = value;
            }
        }

        //初始化
        private void GetInit()
        {
            string message = string.Empty;
            string siteurl = SiteConfig.SiteInfo.SiteUrl + "/rss/Default.aspx";
            string sitename = SiteConfig.SiteInfo.SiteName;
            //throw new Exception(":"+siteurl + "=" + sitename);
            siteurl += "?NId=" + Nodeid.ToString();
            sitename += bnbll.GetNodeXML(Nodeid).NodeName;


            //初始化头部
            message += "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\" ?><rss version=\"2.0\"><channel visittype=\"xml\" isapiwrite=\"0\" forum_url=\"" + siteurl + "\" boardid=\"0\" pageurl=\"" + siteurl + "\" sqlquerynum=\"0\" runtime=\".01563\"><title>" + sitename + "</title><link>" + siteurl + "</link><description>" + SiteConfig.SiteInfo.MetaDescription + " </description><language>zh-cn</language>";
            //读取栏目
            message = ReadNote(message);
            //读取文章
            message = ReadArticle(message);
            //初始化底部
            message += "</channel></rss>";
            //打印出来
            Response.Write(message);
            Response.ContentType = "text/xml";
        }

        //遍历栏目
        private string ReadNote(string message)
        {
            DataTable ds = bnbll.SelByPid(Nodeid);
            foreach (DataRow drs in ds.Rows)
            {
                message += "<item type=\"board\" depth=\"0\" bid=\"111\" child=\"10\"><title>" + drs["NodeName"] + "</title><category>栏目</category><author></author><pubDate></pubDate><description>点击标题查看--" + drs["NodeName"] + "--子栏目         </description><link>" + SiteConfig.SiteInfo.SiteUrl + "/rss/Default.aspx?NId=" + drs["NodeID"] + "</link></item>";
            }
            if (ds != null)
                ds.Clear();
            return message;
        }

        //遍历文章
        private string ReadArticle(string message)
        {
            return "";
        }

        #endregion
    }
}