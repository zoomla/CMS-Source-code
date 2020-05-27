using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Design;
using ZoomLa.Model.Design;
using ZoomLa.SQLDAL;

public partial class Design_SPage_PageDesign : System.Web.UI.Page
{
    B_SPage_Page pageBll = new B_SPage_Page();
    public M_SPage_Page pageMod = null;
    public int Mid { get { return DataConvert.CLng(Request.QueryString["ID"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        B_Admin.CheckIsLogged(Request.RawUrl);
        if (!IsPostBack)
        {
            pageMod = pageBll.SelReturnModel(Mid);
        }
    }
}