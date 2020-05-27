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
using ZoomLa.SQLDAL;

public partial class Manage_WorkFlow_FlowTypeList : System.Web.UI.Page
{
    protected B_MisType typeBll = new B_MisType();
    protected M_MisType typeMod = new M_MisType();
    protected void Page_Load(object sender, EventArgs e)
    {
        EGV.txtFunc = txtPageFunc;
        if (!IsPostBack)
        {
            DataBind();
        }
        Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='../Config/SiteOption.aspx'>系统设置</a></li><li class='active'>类型管理<a href='AddWorkFlowType.aspx'>[添加类型]</a></li>");
    }
    private void DataBind(string key = "")
    {
        DataTable dt = typeBll.Sels();
        if (!string.IsNullOrEmpty(key))
            dt.DefaultView.RowFilter = "TypeName like '%" + key + "%'";
        EGV.DataSource = dt;
        EGV.DataBind();
    }
    //处理页码
    public void txtPageFunc(string size)
    {
        int pageSize;
        if (!int.TryParse(size, out pageSize))//如果转换失败,即不是一个数字时
        {
            pageSize = EGV.PageSize;
        }
        else if (pageSize < 1)//小于1时,均恢复默认PageSize,默认PageSize是你给序的
        {
            pageSize = EGV.PageSize;
        }
        EGV.PageSize = pageSize;
        EGV.PageIndex = 0;//改变后回到首页
        size = pageSize.ToString();
        DataBind();
    }
    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        DataBind();
    }
    protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName.ToLower())
        {
            case "del":
                typeBll.Del(DataConvert.CLng(e.CommandArgument.ToString()));
                break;
        }
        DataBind();
    }
    protected void EGV_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Attributes["class"] = "tdbg";
        if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["ondblclick"] = String.Format("javascript:location.href='AddWorkFlowType.aspx?ID={0}'", this.EGV.DataKeys[e.Row.RowIndex].Value.ToString());
            e.Row.Attributes["onmouseover"] = "this.className='tdbgmouseover'";
            e.Row.Attributes["onmouseout"] = "this.className='tdbg'";
            e.Row.Attributes["style"] = "cursor:pointer;";
            e.Row.Attributes["title"] = "双击修改";
        }
    }
    protected void searchBtn_Click(object sender, EventArgs e)
    {
        DataBind(searchText.Text);
    }
}