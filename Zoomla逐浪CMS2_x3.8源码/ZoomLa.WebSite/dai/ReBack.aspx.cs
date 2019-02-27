using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL.User;
using ZoomLa.BLL;
using ZoomLa.BLL.CreateJS;
using ZoomLa.Model.User;
using ZoomLa.Common;
using ZoomLa.BLL.Helper;
using Newtonsoft.Json;
public partial class try_ReBack : System.Web.UI.Page
{
    B_Temp tempBll = new B_Temp();
    B_Product proBll = new B_Product();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    private void MyBind()
    {
        DataTable dt = JsonConvert.DeserializeObject<DataTable>(TempData_Hid.Value);
        RPT.DataSource = dt;
        RPT.DataBind();
    }
}