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
        B_User buser = new B_User();
        M_Favorite mfave = new M_Favorite();
        B_Favorite bfave = new B_Favorite();
        public string Url { get { return Request["Url"] ?? ""; } }
        public int type { get { return  DataConverter.CLng(Request["type"]); } }//收藏类型
        public int itemid { get { return DataConverter.CLng(Request["itemid"]); } }//收藏内容ID
        public string Content { get { return Request["Content"] ?? ""; } } //收藏内容相关ID
        new public string Title { get { return Request["Title"] ?? ""; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            B_User.CheckIsLogged(Url);
            if (itemid < 1) { function.WriteErrMsg("没有指定要添加到收藏夹的内容ID"); }
            mfave.AddDate = DateTime.Now;
            mfave.Owner = buser.GetLogin().UserID;
            mfave.InfoID = itemid;
            mfave.FavItemID = Content;
            mfave.Title = Title;
            mfave.FavoriType = type;
            mfave.FavUrl = BaseClass.CheckInjection(Url);
            DataTable dt = bfave.SelBy(mfave);
            if (dt != null && dt.Rows.Count > 0)
            {
                function.WriteSuccessMsg("提示：该信息此前已被加入收藏夹! 本页面10秒后自动关闭...也可点击这里手工[<a href=\"javascript:window.close();\">关闭</a>]", Url);
            }
            else
            {
                bfave.AddFavorite(mfave);
                function.WriteSuccessMsg("恭喜！该内容成功添加到您的收藏夹了！本页面10秒后自动关闭...也可点击这里手工[<a href=\"javascript:window.close();\">关闭</a>]", Url);
            }
        }
    }
}