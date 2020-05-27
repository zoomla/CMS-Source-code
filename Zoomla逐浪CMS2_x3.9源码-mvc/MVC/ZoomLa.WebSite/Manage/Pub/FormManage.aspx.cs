using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;

namespace ZoomLaCMS.Manage.Pub
{
    public partial class FormManage : System.Web.UI.Page
    {
        B_Pub pubBll = new B_Pub();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>" + Resources.L.工作台 + "</a></li><li><a href='" + CustomerPageAction.customPath2 + "Content/ContentManage.aspx'>" + Resources.L.内容管理 + "</a></li><li class='active'><a href='" + Request.RawUrl + "'>" + Resources.L.互动表单管理 + "</a></li><li>[<a href='FormDesign.aspx' target='_blank'>" + Resources.L.添加互动表单 + "</a>]</li>");
                MyBind();
            }
        }
        public void MyBind()
        {
            DataTable dt = pubBll.SelALLForm();
            EGV.DataSource = dt;
            EGV.DataBind();
        }
        protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToLower())
            {
                case "del2":
                    int id = Convert.ToInt32(e.CommandArgument);
                    pubBll.Del(id);
                    break;
            }
            MyBind();
        }
        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            MyBind();
        }
    }
}