using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL.Exam;

public partial class Manage_Exam_VersionList : CustomerPageAction
{
    B_Exam_Version verBll = new B_Exam_Version();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
            Call.SetBreadCrumb(Master, "<li><a href='" + customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='Papers_System_Manage.aspx'>教育模块</a></li><li><a href='" + Request.RawUrl + "'>教材版本</a> [<a href='AddVersion.aspx'>添加版本</a>]</li></li>");
        }
    }
    private void MyBind() 
    {
        DataTable dt = verBll.SelAll();
        EGV.DataSource = dt;
        EGV.DataBind();
    }
    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        MyBind();
    }
    protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "del2":
                verBll.Del(Convert.ToInt32(e.CommandArgument));
                break;
        }
        MyBind();
    }
}