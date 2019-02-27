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
using ZoomLa.Model;
using ZoomLa.BLL;
using ZoomLa.Common;

public partial class manage_AddOn_ProjectRequireList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            myBind();
        }
        Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='Projects.aspx'>项目管理</a></li><li class='active'>需求列表</a></li>" + Call.GetHelp(43));
    }
    private void myBind()
    {
    }
    //绑定分页
    protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Egv.PageIndex = e.NewPageIndex;
        myBind();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {      
        switch (e.CommandName)
        {
            case "CreateProject":
                Response.Redirect("AddProject.aspx?rid=" + Convert.ToInt32(e.CommandArgument) + "");
                break; 
            case"DeleteRequest":
                string Id = e.CommandArgument.ToString();
                myBind();
                break;
        }
    }   
    //批量删除
    protected void btnDel_Click(object sender, EventArgs e)
    {
        myBind();
    }
    public int CountProjectNumByRid(int rid)
    {
        return 0;
    }
}
