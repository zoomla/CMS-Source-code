using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Model;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Components;
using System.Text.RegularExpressions;

public partial class User_PromotUnion_PromoRecord : System.Web.UI.Page
{
    //B_Shopsite bshop = new B_Shopsite();
    B_PromoCount bpromo = new B_PromoCount();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Request.QueryString["u"]))
        {
            int sid = DataConverter.CLng(Request.QueryString["sid"]);
            int uid = DataConverter.CLng(Request.QueryString["uid"]);
            string turl = Request.QueryString["th"];
            //M_Shopsite mshop = bshop.GetSelectById(sid);
            //if (mshop != null && mshop.id > 0)
            //{
            //    string reg = "&(" + mshop.lablename + "|" + mshop.ToParaname + ")=(.*)";
            //    string sendurl = Regex.Replace(mshop.aurl, reg, "");
            //    if (!string.IsNullOrEmpty(turl))
            //    {
            //        sendurl += "&" + mshop.lablename + "=" + uid + "&" + mshop.ToParaname + "=" + turl;
            //    }
            //    else
            //    {
            //        if (!string.IsNullOrEmpty(mshop.ToParaname))
            //        {
            //            sendurl += "&" + mshop.lablename + "=" + uid + "&" + mshop.ToParaname + "=" + mshop.url;
            //        }
            //        else
            //        {
            //            sendurl += "&" + mshop.lablename + "=" + uid;
            //        }
            //    }
            //    M_PromoCount promo = bpromo.GetSelectBysid(mshop.id);
            //    promo.sid = mshop.id;
            //    promo.PromotionUrl = GetPath();
            //    if (promo != null && promo.id > 0)
            //    {
            //        promo.linkCount = promo.linkCount + 1;
            //        bpromo.GetUpdate(promo);
            //    }
            //    else
            //    {
            //        promo.linkCount = 1;
            //        bpromo.GetInsert(promo);
            //    }
            //    Response.Redirect(sendurl);
            //}
            //else
            //{
            string url = SiteConfig.SiteInfo.SiteUrl;

            if (url.Substring(url.Length - 1) == "/")
            {
                url = url.Substring(0, url.Length - 1);
            }
            if (url.LastIndexOf('\\') > -1)
            {
                url = url.Substring(0, url.Length - 1);
            }
            function.WriteErrMsg("对不起,该内容不存在", url);
            //}
        }
        else
        {
            HttpContext.Current.Response.Cookies["users"]["u"] = Request.QueryString["u"];
            Response.Redirect(SiteConfig.SiteInfo.SiteUrl);
        }
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
}
