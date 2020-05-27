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
    public partial class LogDetail : CustomerPageAction
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ZoomLa.Common.function.AccessRulo();
            if (!this.Page.IsPostBack)
            {
                //int LogID = string.IsNullOrEmpty(Request.QueryString["LogID"]) ? 0 : DataConverter.CLng(Request.QueryString["LogID"]);
                //if (LogID <= 0)
                //    function.WriteErrMsg("请指定日志ID", "../Plus/LogManage.aspx");
                //M_LogInfo Log = B_Log.GetLog(LogID);
                //if (Log.IsNull)
                //    function.WriteErrMsg("ID为" + LogID.ToString() + "的日志不存在，可能已被删除", "../Plus/LogManage.aspx");
                //else
                //{
                //    this.LitLogID.Text = Log.LogId.ToString();
                //    this.LitLogPage.Text = Log.ScriptName;
                //    this.LitLogTime.Text = Log.Timestamp.ToString();
                //    this.LitUserName.Text = Log.UserName;
                //    this.LitUserIP.Text = Log.UserIP;
                //    this.LitTitle.Text = Log.Title;
                //    this.LitPost.Text = Log.PostString;
                //    this.LitMessage.Text = Log.Message;
                //    this.LitSource.Text = Log.Source;
                //}
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='LogManage.aspx'>日志管理</a></li><li class='active'>日志详情</li>");
        }
    }
}