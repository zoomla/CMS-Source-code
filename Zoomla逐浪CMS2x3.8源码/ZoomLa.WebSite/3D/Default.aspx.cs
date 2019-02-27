using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;

public partial class test2_3d_Default : System.Web.UI.Page
{
    private string xmlPath = AppDomain.CurrentDomain.BaseDirectory+@"\Config\3DShop.config";
    private DataTableHelper dtHelper = new DataTableHelper();
    private B_3DShop shopBll = new B_3DShop();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            JsonHelper jsonHelper = new JsonHelper();
            DataTable dt = shopBll.Sel();
            string json = JsonHelper.JsonSerialDataTable(dt);
            //dt.TableName = "ZL_3DShop";
            //DataTable targetDT=dtHelper.GetTaleStruct("ZL_3DShop");
            //dtHelper.WriteDataToDB(dt, targetDT);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "ShopInit", "ShopInit('" + json + "');", true);
        }
    }
}