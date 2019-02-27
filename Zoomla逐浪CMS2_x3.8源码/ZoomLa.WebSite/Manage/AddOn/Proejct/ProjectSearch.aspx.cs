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
public partial class manage_AddOn_ProjectSearch : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        function.AccessRulo();
        Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='ProjectManage.aspx'>项目列表</a></li><li>项目检索</li>");
        B_Admin badmin = new B_Admin();
        badmin.CheckIsLogin();
        if (!Page.IsPostBack)
        {
            MyBind();
            SearchBind();
        }
    }
    private void MyBind()
    {     
        //DataView dv = bproject.GetProjectAll().DefaultView;
        //this.Egv.DataSource = dv;
        //this.Egv.DataKeyNames = new string[] { "ProjectID" };
        //this.Egv.DataBind();  

    }
    protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Egv.PageIndex = e.NewPageIndex;
        MyBind();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void Search_Click(object sender, EventArgs e)
    {
        string keyvalue = SearchValue.Text.Trim();
        int typeid = DataConverter.CLng(DLType.SelectedValue);
        Page.Response.Redirect("ProjectSearch.aspx?action=Search&searchtype=" + typeid + "&searchvalue=" + keyvalue);
    }
    public void SearchBind()
    {
        string searchvalue = string.Empty;
        int searchtype = 0;
        if ((Request.QueryString["action"] != null) && (Request.QueryString["searchtype"] != null) && (Request.QueryString["searchvalue"] != null))
        {
            searchtype = DataConverter.CLng(Request.QueryString["searchtype"].Trim());
            searchvalue = Request.QueryString["searchvalue"].Trim();
            //DataView dv = this.bproject.ProjectSearch(searchtype, searchvalue).DefaultView;

            //if (dv.Table.Rows.Count == 0)
            //{
            //    this.nocontent.Visible = true;
            //    this.Egv.Visible = false;
            //}
            //else
            //{
            //    this.Egv.DataSource = dv;
            //    this.Egv.DataKeyNames = new string[] { "ProjectID" };
            //    this.Egv.DataBind();
            //    this.nocontent.Visible = false;
            //}
        }
        else if (Request.QueryString["cateid"] != null)
        {
            //DataView dv = this.bproject.ProjectCategory(Convert.ToInt32(Request.QueryString["cateid"].Trim())).DefaultView;
            //this.Egv.DataSource = dv;
            //this.Egv.DataBind();
        }

    }
}
