using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;

public partial class Design_ask_Default : System.Web.UI.Page
{
    public string Ver = "20";
    protected void Page_Load(object sender, EventArgs e)
    {
        WxAPI.AutoSync(Request.RawUrl);
    }
}