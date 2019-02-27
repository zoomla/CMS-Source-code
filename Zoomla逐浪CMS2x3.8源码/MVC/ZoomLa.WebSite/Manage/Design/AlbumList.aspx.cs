namespace ZoomLaCMS.Manage.Design
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using ZoomLa.BLL;
    using ZoomLa.BLL.Design;
    using ZoomLa.Common;
    using ZoomLa.Model;
    public partial class AlbumList : CustomerPageAction
    {
        B_Design_Album albumBll = new B_Design_Album();
        B_User buser = new B_User();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
                Call.SetBreadCrumb(Master, "<li><a href='" + customPath2 + "Main.aspx'>工作台</a></li><li><a href='Default.aspx'>动力模块</a></li><li class='actice'>智能相册</li>");
            }
        }
        public void MyBind()
        {
            EGV.DataSource = albumBll.Sel();
            EGV.DataBind();
        }
        public string GetUser()
        {
            int userid = DataConverter.CLng(Eval("UserID"));
            M_UserInfo mu = buser.SelReturnModel(userid);
            if (mu.IsNull) return "";
            return "<a href='../User/Userinfo.aspx?id=" + mu.UserID + "'>" + mu.UserName + "</a>";
        }
        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            MyBind();
        }

        protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "del2":
                    int id = Convert.ToInt32(e.CommandArgument);
                    albumBll.Del(id);
                    break;
            }
            MyBind();
        }
    }
}