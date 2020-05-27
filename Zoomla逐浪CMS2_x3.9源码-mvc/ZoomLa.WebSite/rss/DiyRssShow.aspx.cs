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
using ZoomLa.Model;

namespace ZoomLaCMS.rss
{
    public partial class DiyRssShow : System.Web.UI.Page
    {
        #region 业务逻辑
        protected B_Content bcbll = new B_Content();
        protected B_Model mll = new B_Model();
        protected B_ModelField fll = new B_ModelField();
        B_Node bnbll = new B_Node();

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["NodeID"] = base.Request.QueryString["gid"];
                GetInit();
            }
        }

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
            string siteurl = SiteConfig.SiteInfo.SiteUrl + "/rss/rss.aspx";
            string sitename = SiteConfig.SiteInfo.SiteName;

            M_CommonData cd = bcbll.GetCommonData(Nodeid);
            sitename += cd.Title;
            DataTable dt = fll.GetModelFieldList(cd.ModelID);


            //初始化头部
            message += "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\" ?><rss version=\"2.0\"><channel visittype=\"xml\" isapiwrite=\"0\" forum_url=\"" + siteurl + "\" boardid=\"0\" pageurl=\"" + siteurl + "\" sqlquerynum=\"0\" runtime=\".01563\"><title>" + sitename + "</title><link>" + siteurl + "</link><description>" + SiteConfig.SiteInfo.MetaDescription + " </description><language>zh-cn</language>";

            #region 项目初始化

            //项目顶部
            message += "<item><title>" + cd.Title + "</title><author>" + cd.Inputer + "</author><pubDate></pubDate><description>";

            //项目中部
            foreach (DataRow drs in dt.Rows)
            {
                DataTable contenttable = bcbll.GetContent(Nodeid);
                //drs["FieldName"]
                if (contenttable.Rows.Count > 0)
                {
                    message += drs["FieldAlias"] + ":" + contenttable.Rows[0]["" + drs["FieldName"] + ""] + "\n";
                }
            }

            //项目底部
            message += "</description><link>" + SiteConfig.SiteInfo.SiteUrl + "/rss/diyrssshow.aspx?gid=" + Nodeid + "</link></item>";
            #endregion

            //初始化底部
            message += "</channel></rss>";

            if (dt != null)
                dt.Dispose();
            //打印出来
            Response.Write(message);
            Response.ContentType = "text/xml";



        }
    }
}