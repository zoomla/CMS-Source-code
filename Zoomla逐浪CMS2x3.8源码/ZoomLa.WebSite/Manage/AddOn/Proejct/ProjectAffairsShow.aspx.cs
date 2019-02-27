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
public partial class manage_AddOn_ProjectAffairsShow : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        function.AccessRulo();
        B_Admin badmin = new B_Admin();
        badmin.CheckIsLogin();
        if (!Page.IsPostBack)
        {
            ShowPage();
        }

    }
    public void ShowPage()
    {
        if (Request.QueryString["PAid"]!=null)
        {
            //mpa=this.bpa.GetProjectAffairsByID(Convert.ToInt32(Request.QueryString["PAid"].Trim()));
            //Lbl.Text ="<div class=\"title\">"+ mpa.AuthorName + "</div><hr/>" + mpa.Content.ToString();
        }
    }

}
