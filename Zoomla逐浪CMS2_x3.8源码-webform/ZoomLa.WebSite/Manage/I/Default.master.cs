using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;

public partial class V_Master_Default : System.Web.UI.MasterPage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        B_Admin.CheckIsLogged(Request.RawUrl);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
    }
}