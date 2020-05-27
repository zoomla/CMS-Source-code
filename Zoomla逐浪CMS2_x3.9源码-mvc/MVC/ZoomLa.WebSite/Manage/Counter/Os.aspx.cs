using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZoomLa.BLL;
using ZoomLa.BLL.User;
using ZoomLa.Model.User;

namespace ZoomLaCMS.Manage.Counter
{
    public partial class Os :CustomerPageAction
    {

        B_Com_VisitCount visitBll = new B_Com_VisitCount();
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li> <li><a href='Counter.aspx'>访问统计</a></li><li><a href='Os.aspx'>操作系统统计报表</a></li>");
        }
        public void MyBind()
        {
            DataTable dt = visitBll.SelBySys();
            CountRPT.DataSource = dt;
            CountRPT.DataBind();
            dt = visitBll.SelectAll();
            EGV.DataSource = dt;
            EGV.DataBind();
        }
        public string GetOSInfo()
        {
            string OS = Eval("OSVersion").ToString();
            return VisitCounter.User.SystemIcon(OS);
        }

        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            MyBind();
        }
    }
}