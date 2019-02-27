namespace ZoomLa.WebSite.User.Content
{
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
    using ZoomLa.Common;
    
    using ZoomLa.Model;

    public partial class AddToFav : System.Web.UI.Page
    {
        private B_User buser = new B_User();
        private B_Favorite bfav = new B_Favorite();        
        public M_UserInfo UserInfo;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                buser.CheckIsLogin();
                string uname = HttpContext.Current.Request.Cookies["UserState"]["LoginName"];
                this.UserInfo = buser.GetUserByName(uname);
                if (string.IsNullOrEmpty(base.Request.QueryString["InfoID"]))
                {
                    function.WriteErrMsg("没有指定要添加到收藏夹的内容ID!");
                }
                else
                {
                    int InfoID = DataConverter.CLng(base.Request.QueryString["InfoID"]);
                    M_Favorite fav = new M_Favorite();
                    fav.FavoriteID = 0;
                    fav.InfoID = InfoID;
                    fav.Owner = this.UserInfo.UserID;
                    fav.AddDate = DateTime.Now;
                    this.bfav.AddFavorite(fav);
                    function.WriteSuccessMsg("该内容成功添加到你的收藏夹了！<br/> 10秒后该页自动关闭...也可手工【<a href=\"javascript:window.close();\">关闭</a>】");
                }
            }
        }
    }
}