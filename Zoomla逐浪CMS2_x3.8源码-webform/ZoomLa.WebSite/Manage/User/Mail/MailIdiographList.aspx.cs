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
using ZoomLa.Model;
using ZoomLa.Common;

public partial class manage_Qmail_MailIdiographList : CustomerPageAction
{
    B_MailIdiograph mibll = new B_MailIdiograph();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='SendMailList.aspx'>订阅管理</a></li><li>签名管理<a href='AddMailIdiograph.aspx'>[添加签名]</a></li>");
        }
    }

    private void MyBind()
    {
        DataTable dt = mibll.Select_All();
        this.EGV.DataSource = dt;
        this.EGV.DataBind();
    }

    protected string GetState(string st)
    {
        if (st == "True")
        {
            return "<font COLOR='#33cc00'>启用</font>";
        }
        else
        {
            return "<font COLOR='#cc0000'>禁用</font>";
        }
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        mibll.GetDelete(int.Parse(EGV.DataKeys[e.RowIndex].Value.ToString()));
        MyBind();
    }

    protected void btn_DeleteRecords_Click(object sender, EventArgs e)
    {
        string idst = Request.Form["pidCheckbox"];
        if (idst != "")
        {
            mibll.DelByIDS(idst);
        }
        MyBind();
    }

    protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Del"))
        {
            mibll.GetDelete(DataConverter.CLng(e.CommandArgument));
        }
        MyBind();
    }


    protected void Dels_Btn_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.Form["idchk"]))
        {
            mibll.DelByIDS(Request.Form["idchk"]);
        }
        MyBind();
    }
}
