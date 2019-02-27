using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.Components;

public partial class User_PromotUnion_Userunionactive : System.Web.UI.Page
{
    //B_Shopsite bshop = new B_Shopsite();
    B_User buser = new B_User();
    B_Promotion bpro = new B_Promotion();

    protected void Page_Load(object sender, EventArgs e)
    {
        buser.CheckIsLogin(GetPath());
    }


    private string GetPath()
    {
        string strPath = "http://" + Request.ServerVariables["HTTP_HOST"] + Request.ServerVariables["PATH_INFO"] + "?" + Request.ServerVariables["QUERY_STRING"];
        if (strPath.EndsWith("?"))
        {
            strPath = strPath.Substring(0, strPath.Length - 1);
        }
        return strPath;
    }

    //生成推广链接
    protected void btn_Click(object sender, EventArgs e)
    {
        string siteurls = SiteConfig.SiteInfo.SiteUrl;
        if (siteurls.Substring(siteurls.Length - 1) != "/")
        {
            siteurls = siteurls + "/";
        }
        string prolinks = siteurls + "redir.aspx?";
        string url = prolink.Text.Trim();
        string link = "";
        int index = url.IndexOf(':');
        int indexl = url.LastIndexOf('.');
        int length = indexl - index - 3;
        string proUrl = url.Substring(index + 3, length);
        string[] urls = proUrl.Split('.');
        if (urls != null && urls.Length > 1)
        {
            //M_Shopsite shop = bshop.GetSelByName(urls[1]);
            //if (shop != null && shop.id > 0)  //存在商家则找到链接地址
            //{
            //    link = urls[1];
            //    prolinks +=shop.id.ToString()+"?";
            //}
        }

        if (!string.IsNullOrEmpty(link))
        {
            url = System.Web.HttpUtility.UrlEncode(url, Encoding.BigEndianUnicode).Replace("%00", "");
            prolinks = prolinks+ buser.GetLogin().UserID + "?"+ url.Replace(".", "%2E");
        }
        else
        {
            prolinks = "";
        }
        unionprolink.Text = prolinks;
        M_Promotion mpromotion = new M_Promotion();
        mpromotion.userId = buser.GetLogin().UserID;
        mpromotion.PromoUrl = prolinks;
        mpromotion.LinkUrl = prolink.Text.Trim();
        mpromotion.type = "5";
        int id = bpro.GetInsert(mpromotion);
    }
}
