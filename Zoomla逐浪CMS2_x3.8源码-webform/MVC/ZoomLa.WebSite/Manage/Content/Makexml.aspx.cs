using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using System.Xml;

namespace ZoomLaCMS.Manage.Content
{
    public partial class Makexml : CustomerPageAction
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            function.AccessRulo();
            B_Admin badmin = new B_Admin();

            string type = "";

            if (Request.QueryString["type"] != null)
            {
                type = Request.QueryString["type"].ToString();
            }

            if (type == "google")
            {
                MakeGoogleMap();
            }
            else if (type == "baidu")
            {
                MakeBaiduMap();
            }

        }
        // 生成Google地图
        public void MakeGoogleMap()
        {
            int num = 0;

            if (Request.QueryString["num"] != null)
            {
                num = DataConverter.CLng(Request.QueryString["num"].ToString());
            }

            DataTable dd = null;

            int changefreqs = 0;

            if (Request.QueryString["changefreqs"] != null)
            {
                changefreqs = DataConverter.CLng(Request.QueryString["changefreqs"].ToString());
            }
            int prioritys = 0;

            if (Request.QueryString["prioritys"] != null)
            {
                prioritys = DataConverter.CLng(Request.QueryString["prioritys"].ToString());
            }
            Response.Write("<script>");
            StringBuilder maptxt = new StringBuilder("");
            maptxt.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            maptxt.AppendLine("<urlset xmlns=\"http://www.google.com/schemas/sitemap/0.84\">");

            for (int i = 0; i < dd.Rows.Count; i++)
            {
                maptxt.AppendLine("<url>");
                maptxt.AppendLine("<loc>" + SiteConfig.SiteInfo.SiteUrl + "/Content.aspx?ItemID= " + dd.Rows[i]["GeneralID"] + "</loc>");
                maptxt.AppendLine("<lastmod>" + dd.Rows[i]["CreateTime"] + "</lastmod>");
                maptxt.AppendLine("<changefreq>" + changefreqs + "</changefreq>");
                maptxt.AppendLine("<priority>" + prioritys + "</priority>");
                maptxt.AppendLine("</url>");
                Response.Write("document.write(\"生成地图...\");");
                Response.Write("document.write(\"" + dd.Rows[i]["title"].ToString() + "\");");
                Response.Write("document.write(\"　<font color=blue><b>完成</b></font><br>\");");
            }
            maptxt.AppendLine("</urlset>");
            XmlDocument xdon = new XmlDocument();//定义一个对象为xdon,继承XmlDocument的方法
            xdon.LoadXml(maptxt.ToString());
            xdon.Save(Server.MapPath("/SiteMap.xml"));
            Response.Write("</script>");
            Response.Write("<br>");
            Response.Write("<a href=\"/SiteMap.xml\" target=\"_blank\">SiteMap.xml</a>");
            Response.Write("<br>");
            Response.Write("<br>一共 " + dd.Rows.Count + " 篇内容<br><input type=\"button\" name=\"Button2\" value=\"返回\" id=\"Button2\" onclick=\"location.href='SiteMap.aspx';\" />");
        }
        // 生成百度地图
        public void MakeBaiduMap()
        {
            int num = 0;
            if (Request.QueryString["num"] != null)
            {
                num = DataConverter.CLng(Request.QueryString["num"].ToString());
            }
            int updatePeris = 0;
            if (Request.QueryString["updatePeri"] != null)
            {
                updatePeris = DataConverter.CLng(Request.QueryString["updatePeri"].ToString());
            }
            B_Node nll = new B_Node();//定义一个对象 nll,继承 B_Node 的方法
            DataTable dt =null;
            Response.Write("<script>");
            StringBuilder maptxt = new StringBuilder("");
            //分类栏目
            maptxt.AppendLine("<?xml version=\"1.0\" encoding=\"gb2312\" ?>");
            maptxt.AppendLine("<document>");
            for (int i = 0; i < dt.Rows.Count && i < num; i++)
            {
                maptxt.AppendLine("<item>");
                maptxt.AppendLine("<webSite>" + SiteConfig.SiteInfo.SiteUrl + "</webSite>");
                maptxt.AppendLine("<webMaster>" + SiteConfig.SiteInfo.WebmasterEmail + "</webMaster>");
                maptxt.AppendLine("<updatePeri>" + updatePeris + "</updatePeri>");
                //maptxt.AppendLine("<items xmlns=\"" + SiteConfig.SiteInfo.SiteUrl + "ColumnList.aspx?Nodeid=" + dt.Rows[o]["NodeID"].ToString() + "\"/>");
                maptxt.AppendLine("<title>" + dt.Rows[i]["Title"] + "</title>");
                maptxt.AppendLine("<link>" + SiteConfig.SiteInfo.SiteUrl + "/Item/" + dt.Rows[i]["GeneralID"] + ".aspx</link>");
                //maptxt.AppendLine("<description>sfsff</description>");
                //maptxt.AppendLine("<text>sfsfsf</text>");
                //maptxt.AppendLine("<image>/images/nopic.gif</image>");
                //maptxt.AppendLine("<source>wtwtwt</source>");
                maptxt.AppendLine("<author>" + dt.Rows[i]["Inputer"] + "</author>");
                maptxt.AppendLine("<pubDate>" + dt.Rows[i]["CreateTime"] + "</pubDate>");
                maptxt.AppendLine("</item>");
                Response.Write("document.write(\"生成地图...\");");
                Response.Write("document.write(\"" + dt.Rows[i]["title"].ToString() + "\");");
                Response.Write("document.write(\"　<font color=blue><b>完成</b></font><br>\");");
                //Response.Write(dd.Rows[i]["GeneralID"].ToString() + "<br>");
            }
            maptxt.AppendLine("</document>");
            XmlDocument xdon = new XmlDocument();
            xdon.LoadXml(maptxt.ToString());
            xdon.Save(Server.MapPath("/Mapfile.xml"));
            Response.Write("</script>");
            //Response.Write("<script>alert('创建成功!');location.href='SiteMap.aspx';</script>");
            //Label1.Text = "<div align=center style=\"padding-top:10px; padding-bottom:10px\">恭喜,SiteMap.xml生成完毕！<br><br><a href=/Mapfile.xml target=_blank>点击查看生成好的 Mapfile.xml 文件</a></div>";
            Response.Write("<br>");
            Response.Write("<a href=\"/Mapfile.xml\" target=\"_blank\">Mapfile.xml</a>");
            Response.Write("<br>");
            Response.Write("<br>一共 " + dt.Rows.Count + " 篇内容<br><input type=\"button\" name=\"Button2\" value=\"返回\" id=\"Button2\" onclick=\"location.href='SiteMap.aspx';\" />");
        }
    }
}