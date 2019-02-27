using Newtonsoft.Json;
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
using ZoomLa.Model;
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.Manage.Shop.IDC
{
    public partial class SelIDCPro : System.Web.UI.Page
    {
        B_Admin badmin = new B_Admin();
        B_User buser = new B_User();
        B_Product proBll = new B_Product();
        B_Content conBll = new B_Content();
        B_Order_IDC idcBll = new B_Order_IDC();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
                Call.HideBread(Master);
            }
        }
        public void MyBind()
        {
            DataTable dt = proBll.Sel("6", Skey_T.Text.Trim());
            RPT.DataSource = dt;
            RPT.DataBind();
        }
        public string getproimg(string type)
        {
            string restring;
            restring = "";
            if (!string.IsNullOrEmpty(type)) { type = type.ToLower(); }
            if (!string.IsNullOrEmpty(type) && (type.IndexOf(".gif") > -1 || type.IndexOf(".jpg") > -1 || type.IndexOf(".png") > -1))
            {
                restring = "<img src=/" + type + " border=0 width=60 height=45>";
            }
            else
            {
                restring = "<img src=/UploadFiles/nopic.gif border=0 width=60 height=45>";
            }
            return restring;

        }
        public string GetIDCPrice(string price)
        {

            DataTable dt = idcBll.P_GetValid(price);
            if (dt == null || dt.Rows.Count < 1) { return "未定义期限价格"; }
            string result = "<select class='idcprice form-control text_150'>@options</select>";
            string options = "";
            foreach (DataRow dr in dt.Rows)
            {
                options += "<option value='" + dr["price"] + "' data-time=" + dr["time"] + ">" + dr["name"] + "</option>";
            }
            return result.Replace("@options", options);
        }

        protected void Skey_Btn_Click(object sender, EventArgs e)
        {
            MyBind();
        }
    }
}