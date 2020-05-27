using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Shop;
using ZoomLa.Common;

namespace ZoomLaCMS.Manage.Shop
{
    public partial class OrderlistToExcel : System.Web.UI.Page
    {
        B_OrderList orderBll = new B_OrderList();
        B_CartPro cartBll = new B_CartPro();
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dt = Session["Temp_OrderList"] as DataTable;
            RPT.DataSource = dt;
            RPT.DataBind();
            Session["Temp_OrderList"] = null;
        }
        public string GetComm(string type)
        {
            int oid = DataConverter.CLng(Eval("ID"));
            DataTable dt = cartBll.SelByOrderID(oid);
            string ids = "";
            string names = "";
            string nums = "";
            foreach (DataRow dr in dt.Rows)
            {
                ids += "<div style='margin-bottom:3px;'>" + dr["ProID"] + "</div>";
                names += "<div style='margin-bottom:3px;'>" + dr["Proname"] + "</div>";
                nums += "<div style='margin-bottom:3px;color:green;'>" + dr["Pronum"] + "</div>";
            }
            if (type == "id") return ids;
            else if (type == "name") return names;
            else return nums;
        }
        public string formatzt(string zt, string selects)
        {
            string result = "";
            int status = DataConverter.CLng(zt);
            int type = DataConverter.CLng(selects);
            switch (type)
            {
                case 0:
                    result = OrderHelper.GetOrderStatus(status);
                    break;
                case 1:
                    result = OrderHelper.GetPayStatus(status);
                    break;
                case 2:
                    result = OrderHelper.GetExpStatus(status);
                    break;
                default:
                    result = "";
                    break;
            }
            return result;
        }
    }
}