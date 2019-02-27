using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Components;

public partial class User_Cloud_Jwplayer_MusicPlayer : System.Web.UI.Page
{
    protected B_User ull = new B_User();
    protected M_UserInfo uinfo;

    public string current=string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ull.CheckIsLogin();
            this.uinfo = ull.GetLogin();
            current = "http://" + HttpContext.Current.Request.Url.Authority.ToString() + SiteConfig.SiteOption.UploadDir + "/" + uinfo.UserName + "/" + uinfo.UserName + ".xml";
           //throw new Exception(current);
        }
    }
}