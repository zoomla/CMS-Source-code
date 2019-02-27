using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using ZoomLa.BLL;
using ZoomLa.BLL.Design;
using ZoomLa.BLL.Helper;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.Design;
using ZoomLa.SQLDAL;
public partial class Design_scence_preview : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        switch (DeviceHelper.GetAgent())
        {
            case DeviceHelper.Agent.PC:
                Server.Transfer("/design/h5/pcview.aspx?"+Request.QueryString);
                break;
            default:
                 Server.Transfer("/design/h5/mbview.aspx?"+Request.QueryString);
                break;
        }
    }
}