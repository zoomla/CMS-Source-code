using Newtonsoft.Json;
using System;
using System.Data;
using ZoomLa.BLL;
using ZoomLa.BLL.Shop;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.Manage.Shop
{
    public partial class AddOrder : System.Web.UI.Page
    {
        B_Product proBll = new B_Product();
        B_OrderList orderBll = new B_OrderList();
        B_CartPro cartProBll = new B_CartPro();
        B_User buser = new B_User();
        public int OrderType { get { return DataConvert.CLng(Request.QueryString["OrderType"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
                Call.SetBreadCrumb(Master, "<li><a href='ProductManage.aspx'>" + Resources.L.商城管理 + "</a></li><li><a href='OrderList.aspx'>" + Resources.L.订单管理 + "</a></li><li class='active'><a href='AddOrder.aspx'>添加订单</a></li>");
            }
        }
        private void MyBind()
        {
            OrderType_DP.SelectedValue = OrderType.ToString();
        }

        protected void Submit_B_Click(object sender, EventArgs e)
        {
            M_OrderList Odata = new M_OrderList();

            //int uid = DataConvert.CLng(UserID_Hid.Value);
            M_UserInfo mu = buser.SelReturnModel(DataConvert.CLng(UserID_Hid.Value));
            if (mu.IsNull) { function.WriteErrMsg("订单所绑定的用户[" + UserID_T.Text + "]不存在"); }
            Odata.Ordertype = DataConvert.CLng(OrderType_DP.SelectedValue);
            Odata.OrderNo = B_OrderList.CreateOrderNo((M_OrderList.OrderEnum)Odata.Ordertype);
            Odata.StoreID = DataConvert.CLng(StoreID_T.Text);
            Odata.Receiver = ReceUser_T.Text;
            Odata.Reuser = ReceUser_T.Text;
            Odata.Phone = Mobile_T.Text;
            Odata.MobileNum = Mobile_T.Text;
            Odata.Shengfen = Request.Form["province_dp"];
            Odata.Chengshi = Request.Form["city_dp"];
            Odata.Diqu = Request.Form["county_dp"];
            Odata.Jiedao = Address_T.Text;
            Odata.ZipCode = ZipCode_T.Text;
            Odata.Invoiceneeds = Invoiceneeds.Checked ? 1 : 0;
            Odata.Rename = mu.UserName;
            Odata.Outstock = 0;//缺货处理
            Odata.Ordermessage = Ordermessage_T.Text;//订货留言
            Odata.Balance_price = DataConvert.CFloat(Price_T.Text);
            Odata.Freight = DataConvert.CFloat(Freight_T.Text);
            Odata.Ordersamount = Odata.Balance_price + Odata.Freight;//订单金额
            Odata.Specifiedprice = Odata.Ordersamount;
            Odata.Receivablesamount = 0;//收款金额
            Odata.Developedvotes = 0;
            Odata.OrderStatus = DataConvert.CLng(OrderStatus_DP.SelectedValue);
            Odata.PayType = DataConvert.CLng(PayType_Rad.SelectedValue);
            Odata.Paymentstatus = DataConvert.CLng(Pay_Rad.SelectedValue);
            Odata.StateLogistics = DataConvert.CLng(Exp_Rad.SelectedValue);
            Odata.AddTime = DateTime.Now;
            Odata.AddUser = mu.UserName; ;
            Odata.Userid = mu.UserID;
            Odata.Merchandiser = "";//跟单员
            Odata.Internalrecords = ""; //内部记录
            Odata.id = orderBll.Adds(Odata);//添加到订单表
            Odata.IsCount = false;
            Odata.Freight_remark = " ";
            Odata.Balance_remark = "";
            Odata.Promoter = 0;
            //将数据添加至zl_cartpro永久保存
            CopyToCartPro(Odata);
            string rurl = "";
            if (OrderType == (int)M_OrderList.OrderEnum.IDC || OrderType == (int)M_OrderList.OrderEnum.IDCRen)
            {
                rurl = "OtherOrder/IDCOrder.aspx?OrderType=" + OrderType;
            }
            else { rurl = "OrderList.aspx?OrderType=" + OrderType; }
            function.WriteSuccessMsg("订单添加成功!", rurl);
        }
        public void CopyToCartPro(M_OrderList Odata)
        {
            DataTable proDt = JsonConvert.DeserializeObject<DataTable>(Pro_Hid.Value);
            if (proDt == null) { return; }
            foreach (DataRow row in proDt.Rows)
            {
                M_CartPro cartMod = new M_CartPro();
                M_Product proMod = proBll.GetproductByid(DataConvert.CLng(row["id"]));
                int num = DataConvert.CLng(row["pronum"]);
                cartMod.Orderlistid = Odata.id;
                cartMod.ProID = proMod.ID;
                cartMod.Pronum = num;
                cartMod.Proname = proMod.Proname;
                cartMod.Username = Odata.Reuser;
                cartMod.Shijia = proMod.LinPrice;
                cartMod.AllMoney = DataConvert.CDouble(Odata.Balance_price);
                cartMod.Danwei = proMod.ProUnit;
                cartMod.Addtime = DateTime.Now;
                cartMod.StoreID = DataConvert.CLng(Odata.StoreID);
                cartProBll.GetInsert(cartMod);
            }
        }
    }
}