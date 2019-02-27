using Newtonsoft.Json;
using System;
using System.Data;
using ZoomLa.BLL;
using ZoomLa.BLL.Shop;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.Shop;
using ZoomLa.SQLDAL;


namespace ZoomLaCMS.Manage.Shop.IDC
{
    public partial class IDCOrder_Add : System.Web.UI.Page
    {
        B_Product proBll = new B_Product();
        B_Order_IDC idcBll = new B_Order_IDC();
        B_OrderList orderBll = new B_OrderList();
        B_CartPro cartProBll = new B_CartPro();
        B_User buser = new B_User();
        public int OrderType { get { return DataConvert.CLng(Request.QueryString["OrderType"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                STime_T.Text = DateTime.Now.ToString("yyyy/MM/dd");
                Call.SetBreadCrumb(Master, "<li><a href='../ProductManage.aspx'>" + Resources.L.商城管理 + "</a></li><li><a href='IDCOrder.aspx'>" + Resources.L.订单管理 + "</a></li><li class='active'><a href='IDCOrder_Add.aspx'>  <i class='fa fa fa-pencil'></i>补订单</a></li>");
            }
        }

        protected void Submit_B_Click(object sender, EventArgs e)
        {
            M_UserInfo mu = buser.SelReturnModel(DataConvert.CLng(UserID_Hid.Value));
            if (mu.IsNull) { function.WriteErrMsg("订单所绑定的用户[" + UserID_T.Text + "]不存在"); }
            if (string.IsNullOrEmpty(ProID_Hid.Value) || string.IsNullOrEmpty(ProTime_Hid.Value)) { function.WriteErrMsg("请先选择需要绑定的商品"); }
            M_Product proMod = proBll.GetproductByid(Convert.ToInt32(ProID_Hid.Value));
            if (proMod == null) { function.WriteErrMsg("商品不存在"); }
            DataRow timeMod = idcBll.GetSelTime(proMod, ProTime_Hid.Value);

            M_OrderList Odata = orderBll.NewOrder(mu, M_OrderList.OrderEnum.IDC);
            Odata.Ordermessage = Ordermessage_T.Text;//订货留言
            Odata.Ordersamount = DataConvert.CFloat(Price_T.Text);
            if (Odata.Ordersamount <= 0) { Odata.Ordersamount = Convert.ToDouble(timeMod["price"]); }
            Odata.Balance_price = Odata.Ordersamount;
            Odata.Specifiedprice = Odata.Ordersamount;
            if (Odata.Ordersamount <= 0) { function.WriteErrMsg("未为商品指定价格"); }
            Odata.id = orderBll.Adds(Odata);
            //-----------------------------
            M_Order_IDC idcMod = new M_Order_IDC();
            idcMod.ProID = proMod.ID;
            idcMod.OrderNo = Odata.OrderNo;
            idcMod.ProInfo = idcBll.ToProInfoStr(timeMod);
            idcMod.Domain = Domain_T.Text;
            idcMod.AdminName = "webmater@" + idcMod.Domain;
            idcMod.STime = Convert.ToDateTime(STime_T.Text);
            idcMod.ETime = idcMod.STime;
            idcMod.ID = idcBll.Insert(idcMod);
            //-----------------------------
            DataTable prodt = new DataTable();
            prodt.Columns.Add(new DataColumn("proid", typeof(int)));
            prodt.Columns.Add(new DataColumn("pronum", typeof(int)));
            DataRow dr = prodt.NewRow();
            dr["proid"] = proMod.ID;
            dr["pronum"] = 1;
            prodt.Rows.Add(dr);
            cartProBll.CopyToCartPro(mu, prodt, Odata.id);
            function.WriteSuccessMsg("订单添加成功!", "IDCOrder.aspx?OrderType=" + OrderType);
        }
    }
}