using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;

public partial class WebFont_t : System.Web.UI.Page
{
    //用于提供下载支持
    protected void Page_Load(object sender, EventArgs e)
    {
        string flow = B_Route.GetParam("Flow", this);
        string file = B_Route.GetParam("File", this);
        SafeSC.DownFile("/WebFont/Users/" + flow + "/" + file);
    }
}