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
using ZoomLa.Model;
using ZoomLa.Common;
public partial class User_Project_ProjectList : System.Web.UI.Page
{
    private int m_uid =0;//测试用
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (HttpContext.Current.Request.Cookies["UserState"] != null)
            {
                this.m_uid = DataConverter.CLng(HttpContext.Current.Request.Cookies["UserState"]["UserID"]);
                
            }
            MyBind();
        }
    }
    public void MyBind()
    {
        //if (this.m_uid > 0)
        //{
             B_Project bproject = new B_Project();
             DataView dv = bproject.GetProjectByUid(this.m_uid);
             this.Egv.DataSource = dv;
             this.Egv.DataKeyNames = new string[] { "ProjectID" };
             this.Egv.DataBind();
        //}
       
    }
    protected void delete_Click(object sender, EventArgs e)
    {
    }
    protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Egv.PageIndex = e.NewPageIndex;
        MyBind();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        
        switch (e.CommandName)
        {
            case "ShowDetail":
                Response.Redirect("Detail.aspx?pid=" + Convert.ToInt32(e.CommandArgument) + "");
                break;
        }
    }
}
