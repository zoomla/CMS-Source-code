using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Content;
using ZoomLa.Model.Content;

public partial class Manage_Template_ExternDS_DSList :CustomerPageAction
{
    M_DataSource dsModel = new M_DataSource();
    B_DataSource dsBll = new B_DataSource();
    B_Admin badmin = new B_Admin();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
            Call.SetBreadCrumb(Master, "<li><a href='" + customPath2 + "Main.aspx'>工作台</a></li><li><a href='../../Config/SiteOption.aspx'>系统设置</a></li><li><a href='../TemplateSet.aspx'>模板风格</a></li><li class='active'>外部数据源<a href=\"DSAdd.aspx\">[添加新数据源]</a></li>");
        }
    }
    private void MyBind() 
    {
        EGV.DataSource = dsBll.Sel();
        EGV.DataBind();
    }
    protected void EGV_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView dr = e.Row.DataItem as DataRowView;
            e.Row.Attributes.Add("ondblclick", "location='DSAdd.aspx?ID=" + dr["ID"] + "'");
        }
    }
    protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "del2":
                dsBll.DeleteByID(Convert.ToInt32(e.CommandArgument));
                break;
            default:
                break;
        }
        MyBind();
    }
}