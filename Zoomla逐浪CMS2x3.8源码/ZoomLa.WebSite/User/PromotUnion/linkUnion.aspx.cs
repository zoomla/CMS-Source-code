using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.Components;

public partial class User_PromotUnion_linkUnion : System.Web.UI.Page
{
    B_User buser = new B_User();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            buser.CheckIsLogin(GetPath());
            M_UserInfo info = buser.GetLogin();
            if (info != null && info.UserID > 0)
            {
                string url = SiteConfig.SiteInfo.SiteUrl;
                int index = url.Length;
                if (!string.IsNullOrEmpty(url) && url.Substring(index - 1, 1) != "/")
                {
                    url += "/";
                }
                regurl.Text = url + "User/Register.aspx?u=" + info.UserID;
                regtext.Value = "<a href='" + regurl.Text + "' target='_blank'>" + SiteConfig.SiteInfo.SiteName + "</a>";
            }
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
