namespace ZoomLaCMS.Design.album
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using ZoomLa.BLL;
    using ZoomLa.BLL.Design;
    using ZoomLa.BLL.User.Addon;
    using ZoomLa.Model;
    using ZoomLa.Model.Design;
    using ZoomLa.Model.User;
    using ZoomLa.SQLDAL;
    public partial class mbview : System.Web.UI.Page
    {
        B_Design_Album albumBll = new B_Design_Album();
        B_User buser = new B_User();
        M_WX_User wxMod = new M_WX_User();
        B_WX_User wxBll = new B_WX_User();
        Appinfo appMod = new Appinfo();
        B_UserAPP appBll = new B_UserAPP();
        public int Mid { get { return DataConvert.CLng(Request.QueryString["ID"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                M_Design_Album albumMod = albumBll.SelReturnModel(Mid);
                M_UserInfo mu = buser.GetSelect(albumMod.UserID);
                string username = mu.UserName;
                appMod = appBll.SelModelByUid(mu.UserID, "wechat");
                if (appMod != null)
                {
                    wxMod = wxBll.SelForOpenid(1, appMod.OpenID);
                    if (wxMod != null)
                    {
                        username = wxMod.Name;
                    }
                }
                albumMod.AlbumName += "-来自[" + username + "]的动力逐浪微相册";
                string html = SafeSC.ReadFileStr("/design/album/tlps/" + albumMod.UseTlp + "/photo.html");
                html = html.Replace("\"{album}\"", JsonConvert.SerializeObject(albumMod));
                Response.Clear(); Response.Write(html); Response.End();
            }
        }
    }
}