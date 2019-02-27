using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Components;
using ZoomLa.Model;
public partial class rss_ArtRss : System.Web.UI.Page
{

    #region 业务逻辑
    protected B_Content bcbll = new B_Content();
    protected B_Model mll = new B_Model();
    protected B_ModelField fll = new B_ModelField();
    protected B_Comment cll = new B_Comment();
    B_Node bnbll = new B_Node();

    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["ModeID"] = base.Request.QueryString["mid"];
            GetInit();
        }
    }

    private int Modeid
    {
        get
        {
            if (ViewState["ModeID"] != null)
                return int.Parse(ViewState["ModeID"].ToString());
            else
                return 0;
        }
        set
        {
            ViewState["ModeID"] = value;
        }
    }
    //初始化
    private void GetInit()
    {
        string message = string.Empty;
        string siteurl = SiteConfig.SiteInfo.SiteUrl + "/rss/rss.aspx";
        string sitename = SiteConfig.SiteInfo.SiteName;
        siteurl += "?mid=" + Modeid.ToString();
        B_Model bmod = new B_Model();
        sitename += bmod.GetModelById(Modeid).ModelName;
        string tableName = bmod.GetModelById(Modeid).TableName;
        // M_CommonData cd = bcbll.GetCommonData(Modeid);
        //sitename += cd.Title; 
        //初始化头部
        message += "<?xml version=\"1.0\"?><rss version=\"2.0\" xmlns:dc=\"http://purl.org/dc/elements/1.1/\" xmlns:itunes=\"http://www.itunes.com/dtds/podcast-1.0.dtd\"><channel><title>" + sitename + "</title><link>" + siteurl + "</link><description>" + SiteConfig.SiteInfo.MetaDescription + " </description><language>zh-cn</language>";
      
        if (tableName.IndexOf("ZL_P_") >= 0)
        {
            message = ReadShop(message);
        }
        else
        {
            message = ReadArticle(message);
        }
        //初始化底部
        message += "</channel></rss>";

        //打印出来
        Response.Write(message);
        Response.ContentType = "text/xml";



    }
    //遍历文章
    private string ReadArticle(string message)
    {
        return "";
    }
    //遍历商品
    private string ReadShop(string message)
    {
        return "";
    }
}
