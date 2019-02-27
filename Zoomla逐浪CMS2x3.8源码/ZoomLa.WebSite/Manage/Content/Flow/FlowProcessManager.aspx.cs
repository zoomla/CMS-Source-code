namespace ZoomLa.WebSite.Manage.Content
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using ZoomLa.Common;
    using ZoomLa.BLL;

    public partial class FlowProcessManager : CustomerPageAction
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='../Config/SiteOption.aspx'>系统设置</a></li><li><a href='FlowManager.aspx'>流程管理</a></li><li><a href='FlowProcessManager.aspx?id=" + Request.QueryString["id"] + "&name=" + Request.QueryString["name"] + "'>" + Request.QueryString["name"] + "</a></li><li class='active'>步骤列表<a href='FlowProcess.aspx?id=" + Request.QueryString["id"] + "&name=" + Request.QueryString["name"] + "'>[添加步骤]</a></li>");
        }

        protected void gwFlowProcess_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Attributes.Add("onmouseover", "color=this.style.backgroundColor;this.style.backgroundColor='#DBF9D9';");//获得焦点
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=color;");//失去焦点
        }
        protected void button1_Click(object sender, EventArgs e)
        {
            //Response.Redirect("FlowProcess.aspx?id=<%# Eval(id) %>&name=<%# Eval(flowName) %>");        
        }
    }
}