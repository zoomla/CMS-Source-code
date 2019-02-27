using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL.Design;
using ZoomLa.Common;

public partial class Manage_Design_TlpClass : CustomerPageAction
{
    B_Design_TlpClass classBll = new B_Design_TlpClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
            Call.SetBreadCrumb(Master, "<li><a href='" + customPath2 + "Main.aspx'>工作台</a></li><li><a href='Default.aspx'>动力模块</a></li><li class='active'>分类列表 <a href='AddTlpClass.aspx'>[添加分类]</a></li>");
        }
    }
    private void MyBind() 
    {
        DataTable dt = classBll.Sel();
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
                int id = Convert.ToInt32(e.CommandArgument);
                classBll.Del(id);
                break;
        }
        MyBind();
    }
    protected void EGV_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView dr = e.Row.DataItem as DataRowView;
            e.Row.Attributes.Add("ondblclick", "location='AddTlpClass.aspx?ID=" + dr["ID"] + "'");
        }
    }
    protected void BatDel_Btn_Click(object sender, EventArgs e)
    {
        string ids = Request.Form["idchk"];
        if (!string.IsNullOrEmpty(ids))
        {
            classBll.DelByIDS(ids);
            function.WriteSuccessMsg("批量删除完成");
        }
        else { function.WriteErrMsg("未指定需要删除的分类"); }
    }
}