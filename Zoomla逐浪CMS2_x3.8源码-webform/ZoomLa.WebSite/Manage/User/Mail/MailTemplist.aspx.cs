using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Web;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

public partial class manage_User_MailTemplist : CustomerPageAction
{
    B_MailTemp tlpBll = new B_MailTemp();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!B_ARoleAuth.Check(ZLEnum.Auth.user, "MessManage"))
            {
                function.WriteErrMsg("没有权限进行此项操作");
            }
            MyBind();
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + customPath2 + "User/UserManage.aspx'>用户管理</a><li class='active'>邮件模板<a href='AddMailTemp.aspx'>[创建邮件模板]</a></li>");
        }
    }
    private void MyBind()
    {
        DataTable dt = tlpBll.Sel();
        EGV.DataSource = dt;
        EGV.DataKeyNames = new string[] { "ID" };
        EGV.DataBind();
    }
    protected void txtPage_TextChanged(object sender, EventArgs e)
    {

    }

    protected void gvHuaTee_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[3].Text == new DateTime().ToString())
            {
                e.Row.Cells[3].Text = "";
            }
        }
    }


    //绑定分页
    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        EGV.PageIndex = e.NewPageIndex;
        MyBind();
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void Row_Command(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleteMsg")
        {
            int ID = DataConverter.CLng(e.CommandArgument.ToString());
            tlpBll.Del(ID);
            MyBind();
        }
    }

    protected string GetType(int type)
    {
        switch (type)
        {
            case 1:
                return "普通信件";
            case 2:
                return "邀请函";
            case 3:
                return "明信片";
            default:
                return "普通信件";
        }
    }
    protected void EGV_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("ondblclick", "window.location.href = 'AddMailTemp.aspx?id=" + (e.Row.DataItem as DataRowView)["ID"] + "';");
            e.Row.Attributes.Add("title", "双击阅读信息");
        }
    }
}