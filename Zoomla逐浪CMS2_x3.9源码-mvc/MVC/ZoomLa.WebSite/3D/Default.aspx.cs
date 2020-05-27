using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;

namespace ZoomLaCMS._3D
{
    public partial class Default : System.Web.UI.Page
    {
        private string xmlPath = AppDomain.CurrentDomain.BaseDirectory + @"\Config\3DShop.config";
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

        public DataTable GetStruct()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("ID", typeof(int)));
            dt.Columns.Add(new DataColumn("ShopName", typeof(string)));
            dt.Columns.Add(new DataColumn("ShopImg", typeof(string)));
            dt.Columns.Add(new DataColumn("UserID", typeof(string)));
            dt.Columns.Add(new DataColumn("UserName", typeof(string)));
            dt.Columns.Add(new DataColumn("posX", typeof(string)));
            dt.Columns.Add(new DataColumn("posY", typeof(string)));
            return dt;
        }
        public DataTable GetFromXml(string path)
        {
            DataSet ds = new DataSet();
            ds.ReadXml(path);
            return ds.Tables[0];
        }
        public void WriteToXml(DataTable dt)
        {
            dt.WriteXml(xmlPath);
        }
        public void TempAdd()
        {
            DataTable dt = GetStruct();
            dt.TableName = "Shop";
            DataRow dr = dt.NewRow();
            dr["ID"] = 1;
            dr["ShopName"] = "日用百货";
            dr["ShopImg"] = "/Images/Style3D/c1.png";
            dr["UserID"] = "1";
            dr["UserName"] = "admin";
            dr["posX"] = "80";
            dr["posY"] = "120";
            dt.Rows.Add(dr);
            dr = null;
            dr = dt.NewRow();
            dr["ID"] = 2;
            dr["ShopName"] = "家居杂货";
            dr["ShopImg"] = "/Images/Style3D/node.gif";
            dr["UserID"] = "2";
            dr["UserName"] = "test";
            dr["posX"] = "160";
            dr["posY"] = "88";
            dt.Rows.Add(dr);
            dt.WriteXml(xmlPath);
        }
    }
}