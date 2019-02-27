using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ZoomLa.Model;
using ZoomLa.BLL;
using ZoomLa.Common;

namespace ZoomLaCMS.Manage.Shop
{
    public partial class ProductsSelect : CustomerPageAction
    {
        B_Product proBll = new B_Product();
        B_Node nodeBll = new B_Node();
        private string KeyWord { get { return TxtKeyWord.Text; } set { TxtKeyWord.Text = value; } }
        private int Mid { get { return DataConverter.CLng(Request.QueryString["ID"]); } }
        public int NodeID { get { return DataConverter.CLng(Request.QueryString["NodeID"]); } }
        //指定需要由父页面哪个JS方法处理
        public string CallBack { get { { return Request.QueryString["callback"]; } } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
                DataTable dt = nodeBll.GetAllShopNode(0);
                NodeHtml_L.Text = nodeBll.CreateDP(dt);
                Call.HideBread(Master);
            }
        }
        private void MyBind()
        {
            DataTable dt = new DataTable();
            if (!string.IsNullOrEmpty(KeyWord))
            {
                dt = proBll.ProductSearch(NodeID, 0, KeyWord);
            }
            else { dt = proBll.GetProductAll(NodeID); }
            RPT.DataSource = dt;
            RPT.DataBind();
        }
        public string getproimg()
        {
            return function.GetImgUrl(Eval("Thumbnails"));
        }
        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            MyBind();
        }
    }
}