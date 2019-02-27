using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;

public partial class Manage_WorkFlow_SignManage : System.Web.UI.Page
{
    protected B_OA_Sign signBll = new B_OA_Sign();
    protected M_OA_Sign signMod = new M_OA_Sign();
    protected void Page_Load(object sender, EventArgs e)
    {
        EGV.txtFunc = txtPageFunc;
        if (!IsPostBack)
        {
            DataBind();
        }
        Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='OAConfig.aspx'>OA配置</a></li><li class='active'>签章管理<a href='AddSign.aspx'>[添加签章]</a></li>");
    }
    private void DataBind(string key = "")
    {
        DataTable dt = new DataTable();
        dt = signBll.SelAll();
        if (!string.IsNullOrEmpty(key))
        {
            key = "'%" + key + "%'";
            dt.DefaultView.RowFilter = "UserName like " + key + " or SignName Like " + key;
        }
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
            case "del2":
                signBll.DelByID(DataConverter.CLng(e.CommandArgument.ToString()));
                Page.ClientScript.RegisterStartupScript(this.GetType(),"","alert('操作成功');",true);
                break;
        }
        DataBind();
    }
    protected void searchBtn_Click(object sender, EventArgs e)
    {
        DataBind(searchText.Text.Trim());
    }
    protected void EGV_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Attributes["class"] = "tdbg";
        if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["ondblclick"] = String.Format("javascript:location.href='AddSign.aspx?ID={0}'", this.EGV.DataKeys[e.Row.RowIndex].Value.ToString());
            e.Row.Attributes["onmouseover"] = "this.className='tdbgmouseover'";
            e.Row.Attributes["onmouseout"] = "this.className='tdbg'";
            e.Row.Attributes["style"] = "cursor:pointer;";
            e.Row.Attributes["title"] = "双击修改";
        }
    }
    public string GetStatus(string status)
    {
        switch (status)
        {
            case "0":
                return "<font color='red'>不启用</font>";
            case "1":
                return "<font color='green'>启用</font>";
            default :
                return "";
        }
    }
}