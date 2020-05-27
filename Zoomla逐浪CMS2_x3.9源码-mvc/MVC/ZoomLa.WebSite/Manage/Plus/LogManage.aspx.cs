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
using System.Text;

namespace ZoomLaCMS.Manage.Plus
{
    public partial class LogManage :CustomerPageAction
    {
        ZLLog logBll = new ZLLog();
        private int LogType { get { return DataConverter.CLng(Request.QueryString["LogType"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            B_ARoleAuth.CheckEx(ZLEnum.Auth.content, "ContentManage");
            if (!IsPostBack)
            {
                MyBind();
                string curLog = "内容管理";
                switch (LogType)
                {
                    case 4:
                        curLog = "管理员登录日志";
                        break;
                    case 0:
                    default:
                        break;
                }
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='LogManage.aspx'>日志管理</a></li><li class='active'>" + curLog + "</li>");
            }
        }
        public void MyBind()
        {
            DataTable dt = logBll.SelLog((ZLEnum.Log)LogType);
            EGV.DataSource = dt;
            EGV.DataBind();
        }
        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            MyBind();
        }
    }
}