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

public partial class User_survey_SurveyAll : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            B_User buser = new B_User();
            buser.CheckIsLogin();
            dBind();
        }
    }
    private void dBind()
    {
        DataTable dt = new DataTable();
        if (string.IsNullOrEmpty(this.Hidd.Value))
            dt = B_Survey.GetSurveyList();
        else
            dt = B_Survey.SearchSur(this.Hidd.Value.Trim());
        Bind(dt);
    }
    private void Bind(DataTable dd)
    {
        int CPage, temppage;

        if (Request.Form["DropDownList1"] != null)
        {
            temppage = Convert.ToInt32(Request.Form["DropDownList1"]);
        }
        else
        {
            temppage = Convert.ToInt32(Request.QueryString["CurrentPage"]);
        }
        CPage = temppage;
        if (CPage <= 0)
        {
            CPage = 1;
        }

        DataTable Cll = dd;

        PagedDataSource cc = new PagedDataSource();

        cc.DataSource = Cll.DefaultView;
        cc.AllowPaging = true;
        EGV.DataSource = cc;
        EGV.DataBind();
    }

    protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.EGV.PageIndex = e.NewPageIndex;
        dBind();
    }
}
