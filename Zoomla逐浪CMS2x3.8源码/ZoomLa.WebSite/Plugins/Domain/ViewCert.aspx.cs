using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Site;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

public partial class Manage_Site_ViewCert : System.Web.UI.Page
{
    protected B_IDC_DomainList domListBll = new B_IDC_DomainList();
    protected B_IDC_DomainTemp domTempBll = new B_IDC_DomainTemp();
    string id;
    protected void Page_Load(object sender, EventArgs e)
    {
        B_User.CheckIsLogged(Request.RawUrl);
        id = Request.QueryString["id"];
        DataTable dom = domListBll.SelByID(id);
        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "setValue('" + dom.Rows[0]["RegInfo"] + "');", true);
    }
    public string DomainName()
    {
        DataTable dom = domListBll.SelByID(id);
        return dom.Rows[0]["DomName"].ToString();
    }
    public string DomainEndDate()
    {
        DataTable dom = domListBll.SelByID(id);
        return DataConverter.CDate(dom.Rows[0]["EndDate"]).ToString("yyyy年MM月dd日");
    }
}