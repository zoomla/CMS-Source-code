using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;

public partial class Manage_User_Mail_MailSysTlp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!B_ARoleAuth.Check(ZLEnum.Auth.user, "MessManage"))
            {
                function.WriteErrMsg("没有权限进行此项操作");
            }
            MyBind();
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + CustomerPageAction.customPath2 + "User/UserManage.aspx'>用户管理</a><li class='active'>系统邮件</li>");
        }
    }
    private void MyBind()
    {
        DataTable dt = FileSystemObject.GetFileinfos(HttpRuntime.AppDomainAppPath.ToString()+ "/Common/MailTlp");
        EGV.DataSource = dt;
        EGV.DataBind();
    }
    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        MyBind();
    }
    protected void EGV_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("ondblclick", "window.location.href = 'MailSysTlpEdit.aspx?TlpName=" + HttpUtility.UrlEncode((e.Row.DataItem as DataRowView)["Name"].ToString()) + "';");
            e.Row.Attributes.Add("title", "双击查看模板");
        }
    }
}