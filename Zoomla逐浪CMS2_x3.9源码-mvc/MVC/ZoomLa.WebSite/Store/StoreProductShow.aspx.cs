using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using System.Text;
using System;

namespace ZoomLaCMS.Store
{
    public partial class StoreProductShow : System.Web.UI.Page
    {
        private B_User buser = new B_User();
        private B_Product bll = new B_Product();
        private B_Node bNode = new B_Node();
        private B_Model bmode = new B_Model();
        B_UserStoreTable ustbll = new B_UserStoreTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            int uid = int.Parse(Request.QueryString["userid"].ToString());
            DataTable dt = bll.GetProductAll(uid);
            this.DataList1.DataSource = dt;
            this.DataList1.DataBind();
        }
        protected string GetPic(string pic)
        {
            return "~/" + pic;
        }
    }
}