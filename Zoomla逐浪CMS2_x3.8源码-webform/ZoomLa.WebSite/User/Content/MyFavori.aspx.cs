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
    using ZoomLa.Components;

    public partial class MyFavori : System.Web.UI.Page
    {
        B_Content bll = new B_Content();
        B_User buser = new B_User();
        B_Favorite favBll = new B_Favorite();

        public int Type { get { return DataConverter.CLng(Request.QueryString["type"]); } }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
            }
        }
        public void MyBind()
        {
            Egv.DataSource = favBll.SelByType(buser.GetLogin().UserID);
            Egv.DataBind();
        }
        protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Egv.PageIndex = e.NewPageIndex;
            MyBind();
        }
        public string GetTitle()
        {
            string title = Eval("Title").ToString();
            return title.Length > 30 ? title.Substring(0, 30) + "..." : title;
        }
        protected void Egv_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Del":
                    favBll.Del(Convert.ToInt32(e.CommandArgument));
                    MyBind();
                    break;
                default:
                    break;
            }
        }
        protected void BtnDel_B_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.Form["idchk"]))
            {
                favBll.DelByIDS(Request.Form["idchk"]);
                MyBind();
            }
        }
    }
}