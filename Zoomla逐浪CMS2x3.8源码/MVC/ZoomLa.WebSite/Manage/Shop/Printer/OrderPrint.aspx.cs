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
using ZoomLa.Model.Shop;
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.Manage.Shop.Printer
{
    public partial class OrderPrint : System.Web.UI.Page
    {
        B_Shop_PrintTlp tlpBll = new B_Shop_PrintTlp();
        B_Shop_PrintDevice devBll = new B_Shop_PrintDevice();
        B_Shop_PrintMessage msgBll = new B_Shop_PrintMessage();
        B_OrderList orderBll = new B_OrderList();
        OrderCommon orderCOM = new OrderCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='ListDevice.aspx'>智能硬件</a></li><li class='active'>订单输出</li>");
            }
        }
        public void MyBind()
        {
            DataTable tlps = tlpBll.Sel();
            Tlp_DP.DataSource = tlps;
            Tlp_DP.DataTextField = "Alias";
            Tlp_DP.DataValueField = "ID";
            Tlp_DP.DataBind();

            DataTable orders = orderBll.Sel();
            Order_DP.DataSource = orders;
            Order_DP.DataTextField = "ID";
            Order_DP.DataValueField = "ID";
            Order_DP.DataBind();

            DataTable devs = devBll.Sel();
            RPT.DataSource = devs;
            RPT.DataBind();
        }

        protected void Print_Btn_Click(object sender, EventArgs e)
        {
            int devID = DataConvert.CLng(Request.Form["Dev_R"]);
            if (devID < 1) { function.WriteErrMsg("你还没有选择打印设备!"); }
            int Num = DataConvert.CLng(Num_T.Text.Replace(" ", ""));
            if (Num < 1) { function.WriteErrMsg("请输入正确的打印数量！"); }
            int orderID = DataConvert.CLng(Order_DP.SelectedValue);
            int tlpID = DataConvert.CLng(Tlp_DP.SelectedValue);
            //------------------------------
            DataTable orderDT = DBCenter.Sel("ZL_OrderInfo", "ID=" + orderID);
            M_Shop_PrintTlp tlpMod = tlpBll.SelReturnModel(tlpID);
            M_Shop_PrintDevice devMod = devBll.SelReturnModel(devID);
            string msg = orderCOM.TlpDeal(tlpMod.Content, orderDT);
            msgBll.Insert(msg, tlpMod.ID, devMod, Num);
            function.WriteSuccessMsg("信息已发送", "MessageList.aspx");
        }
    }
}