using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL.Content;
using ZoomLa.BLL.Sentiment;
using ZoomLa.Model.Content;

namespace ZoomLaCMS.Manage.Sentiment
{
    public partial class TaskList : System.Web.UI.Page
    {
        B_Sen_Task senBll = new B_Sen_Task();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + CustomerPageAction.customPath2 + "Default.aspx'>企业办公</a></li><li><a href='Default.aspx'>舆情监测</a></li><li class='active'>任务列表</li> <a href='AddTask.aspx'>[新建任务]</a>");
                MyBind();
            }
        }
        public void MyBind()
        {
            DataTable dt = senBll.Sel();
            EGV.DataSource = dt;
            EGV.DataBind();
        }
        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            EGV.DataBind();
        }
        protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToLower())
            {
                case "del":
                    senBll.Del(Convert.ToInt32(e.CommandArgument));
                    break;
            }
            MyBind();
        }
        protected void BatDel_Btn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.Form["idchk"]))
            {
                senBll.DelByIDS(Request.Form["idchk"]);
                MyBind();
            }

        }
    }
}