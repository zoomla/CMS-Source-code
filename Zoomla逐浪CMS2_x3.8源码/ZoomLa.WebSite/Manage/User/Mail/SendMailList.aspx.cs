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

public partial class manage_Qmail_SendMailList : CustomerPageAction
{
    B_User bull = new B_User();
    B_MailInfo mibll = new B_MailInfo();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='"+ CustomerPageAction.customPath2+ "User/UserManage.aspx'>用户管理</a></li><li><a href='" + CustomerPageAction.customPath2 + "User/AddSubscriptionCount.aspx'>邮件订阅</a></li><li>邮件列表 [<a href='AddMail.aspx'>订阅群发</a>]</li>");
        }
    }
    private void MyBind() 
    {
        DataTable dt = mibll.Sel();
        EGV.DataSource = dt;
        EGV.DataBind();
    }
    protected string GetState(string str)
    {
        if (str == "True")
        {
            return "已发送";
        }
        else
        {
            return "待发送";
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        mibll.GetDelete(int.Parse(EGV.DataKeys[e.RowIndex].Value.ToString()));
        Response.Write("<script>location.href='SendMailList.aspx'</script>");
    }

    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        DataBind();
    }
}
