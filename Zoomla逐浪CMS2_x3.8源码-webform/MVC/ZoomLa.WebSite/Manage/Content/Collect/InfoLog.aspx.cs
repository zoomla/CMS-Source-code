using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;

namespace ZoomLaCMS.Manage.Content.Collect
{
    public partial class InfoLog : System.Web.UI.Page
    {
        private string SearchKey
        {
            get { return ViewState["searchKey"] as string; }
            set { ViewState["searchKey"] = value; }
        }

        private string STime
        {
            get { return ViewState["bTime"] as string; }
            set { ViewState["bTime"] = value; }
        }
        private string ETime
        {
            get { return ViewState["eTime"] as string; }
            set { ViewState["eTime"] = value; }
        }

        B_Site_Log LogBll = new B_Site_Log();
        DataTable dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='CollSite.aspx'>内容管理</a></li><li><a href='CollSite.aspx'>采集检索</a></li><li>检索日志</li>" + Call.GetHelp(3));
                MyBind();
            }

        }
        private void MyBind()
        {
            dt = LogBll.SelByRemind(SearchKey, STime, ETime); ;
            EGV.DataSource = dt;
            EGV.DataBind();
        }
        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            EGV.DataBind();
            MyBind();
        }
        protected void souchok_Click(object sender, EventArgs e)
        {
            SearchKey = Search_T.Text.Trim();
            STime = BeginTime_T.Text.Trim();
            ETime = EndTime_T.Text.Trim();
            dt = LogBll.SelByRemind(SearchKey, STime, ETime);
            EGV.DataSource = dt;
            EGV.DataBind();
        }
    }
}