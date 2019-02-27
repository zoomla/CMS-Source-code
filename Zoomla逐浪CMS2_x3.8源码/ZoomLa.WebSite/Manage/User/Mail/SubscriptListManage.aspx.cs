using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Model;
using System.Web.UI.HtmlControls;
using ZoomLa.BLL;
using System.Data;
using ZoomLa.Common;

public partial class manage_User_SubscriptListManage : CustomerPageAction
{
    private B_Mail_BookRead readBll = new B_Mail_BookRead();
    B_User buser = new B_User();
    public int Audit { get { return string.IsNullOrEmpty(Request.QueryString["audit"]) ? -10 : DataConverter.CLng(Request.QueryString["audit"]); } }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
        }
        Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href=\"SubscriptListManage.aspx?menu=all\">订阅管理</a></li><li>邮件订阅 </li>" + Call.GetHelp(105));//<a href='AddSubscriptionCount.aspx?menu=add'>[新增订阅]</a>
    }
    private void MyBind()
    {
        EGV.DataSource = readBll.SelectAll(Audit);
        EGV.DataBind();
        function.Script(this, "ShowTab(" + Audit + ");");
    }
    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        MyBind();
    }
    public string GetUserName()
    {
        return string.IsNullOrEmpty(Eval("UserName").ToString()) ? "匿名用户" : Eval("UserName").ToString();
    }

    protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Del"))
        {
            readBll.GetDelete(DataConverter.CLng(e.CommandArgument));
        }
        MyBind();
    }

    protected void Dels_Btn_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.Form["idchk"]))
        {
            readBll.DelByIDS(Request.Form["idchk"]);
        }
        MyBind();
    }
    public string GetStatus()
    {
        switch (Eval("isAudit").ToString())
        {
            case "1":
                return "正常";
            case "0":
                return "停用";
            case "-1":
                return "退订";
            default:
                return "停用";
        }
    }
}
