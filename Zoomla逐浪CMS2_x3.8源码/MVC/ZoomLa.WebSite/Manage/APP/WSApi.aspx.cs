using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;

namespace ZoomLaCMS.Manage.APP
{
    public partial class WSApi : System.Web.UI.Page
    {
        B_Ucenter ucBll = new B_Ucenter();
        protected void Page_Load(object sender, EventArgs e)
        {
            B_Admin.IsSuperManage();
            if (!IsPostBack)
            {
                MyBind();
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + CustomerPageAction.customPath2 + "user/UserManage.aspx'>用户管理</a></li><li><a href='" + CustomerPageAction.customPath2 + "user/UserManage.aspx'>会员管理</a></li><li>跨域网站接入[<a href='AddUcenter.aspx'>添加授权网站</a>]</li>");
            }
        }
        public void MyBind()
        {
            EGV.DataSource = ucBll.Sel();
            EGV.DataBind();
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
                    ucBll.Del(id);
                    break;
            }
            MyBind();
        }
    }
}