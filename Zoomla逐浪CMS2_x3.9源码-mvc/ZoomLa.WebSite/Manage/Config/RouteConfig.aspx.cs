using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL.Site;
using ZoomLa.Common;


namespace ZoomLaCMS.Manage.Config
{
    public partial class RouteConfig : System.Web.UI.Page
    {
        B_IDC_DomainList domBll = new B_IDC_DomainList();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
                Call.SetBreadCrumb(Master, "<li>工作台</li><li>网站配置</li><li class='active'><a href='" + Request.RawUrl + "'>网站路由器</a></li>[<a href='AddRoute.aspx'>添加路由</a>]");
            }
        }
        public void MyBind()
        {
            EGV.DataSource = domBll.SelWithUser();
            EGV.DataBind();
        }
        protected void RPT_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            switch (e.CommandName.ToLower())
            {
                case "edit2":
                    break;
                case "del2":
                    RouteHelper.RouteDT.Rows.RemoveAt(index);
                    break;
            }
            MyBind();
        }
        public string GetType(string type)
        {
            int stype = DataConverter.CLng(type);
            switch (stype)
            {
                case 2:
                    return "在线设计_域名路由";
                case 1:
                    return "CMS_站点路由";
                default:
                    return "未确定";
            }
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
                    domBll.Del(id.ToString());
                    RouteHelper.RouteDT = domBll.Sel();
                    break;
            }
            MyBind();
        }
        protected void BatDel_Btn_Click(object sender, EventArgs e)
        {
            string ids = Request.Form["idchk"];
            if (!string.IsNullOrEmpty(ids))
            {
                domBll.Del(ids);
                RouteHelper.RouteDT = domBll.Sel();
                function.WriteSuccessMsg("批量删除成功");
            }
            MyBind();
        }
    }
}