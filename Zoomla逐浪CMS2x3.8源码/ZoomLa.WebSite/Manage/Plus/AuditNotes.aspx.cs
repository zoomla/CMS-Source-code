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
using ZoomLa.Common;
using ZoomLa.Model;
using System.Text;

public partial class manage_Plus_AuditNotes : CustomerPageAction
{
	//DisusePage
    //无用的页面,无记录
    B_Content bcontent = new B_Content();
    protected void Page_Load(object sender, EventArgs e)
    {
        Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='LogManage.aspx'>日志管理</a></li><li class='active'>内容审核记录</li>");
        if (!this.Page.IsPostBack)
        {
            DataBind();
        }
    }
    public string GetStatus(string status)
    {
        return ZLEnum.GetConStatus(DataConverter.CLng(status));
    }

    /// <summary>
    /// 绑定数据
    /// </summary>
    public new void DataBind()
    {
        DataTable dt = bcontent.GetContentAll();
        Egv.DataSource = dt;
        Egv.DataBind();
    }
    protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Egv.PageIndex = e.NewPageIndex;
        DataBind();
    }
}
