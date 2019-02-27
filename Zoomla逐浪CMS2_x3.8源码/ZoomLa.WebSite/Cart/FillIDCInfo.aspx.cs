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
using ZoomLa.Model.Shop;
using ZoomLa.SQLDAL;

public partial class Cart_fill_idc : System.Web.UI.Page
{
    B_CartPro cartProBll = new B_CartPro();
    B_OrderList orderBll = new B_OrderList();
    B_Order_IDC idcBll = new B_Order_IDC();
    B_Payment payBll = new B_Payment();
    B_Product proBll = new B_Product();
    B_User buser = new B_User();
    public int ProID { get { return DataConvert.CLng(Request.QueryString["ProID"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        B_User.CheckIsLogged(Request.RawUrl);
        if (!IsPostBack)
        {
            M_Product proMod = proBll.GetproductByid(ProID);
            if (proMod == null) { function.WriteErrMsg("你尚未选定需要购买的商品"); }
            DataTable idcDT = idcBll.P_GetValid(proMod.IDCPrice);
            if (idcDT == null || idcDT.Rows.Count < 1) { function.WriteErrMsg("未设置IDC商品价格"); }
            IDCTime_DP.DataSource = idcDT;
            IDCTime_DP.DataBind();
            Proname_L.Text = proMod.Proname;
            ProContent_L.Text = proMod.Procontent;
            Pwd_T.Attributes.Add("value", "123456");
            CPwd_T.Attributes.Add("value", "123456");
        }
    }
    protected void Submit_Btn_Click(object sender, EventArgs e)
    {
        M_UserInfo mu = buser.GetLogin();
        M_Product proMod = proBll.GetproductByid(ProID);
        DataRow timeMod = idcBll.GetSelTime(proMod, IDCTime_DP.SelectedValue);
        //-----------------------------------
        M_OrderList Odata = orderBll.NewOrder(mu,M_OrderList.OrderEnum.IDC);
        Odata.Balance_price = DataConvert.CDouble(timeMod["price"]);//需付金额
        Odata.Ordersamount = Odata.Balance_price;
        //-----------------------------
        M_Order_IDC idcMod = new M_Order_IDC();
        idcMod.OrderNo = Odata.OrderNo;
        idcMod.ProID = proMod.ID;
        idcMod.ProInfo = idcBll.ToProInfoStr(timeMod);
        idcMod.Domain = BindDomain_T.Text;
        idcMod.AdminName = "webmater@" + idcMod.Domain;
        idcMod.AdminPwd = Pwd_T.Text;
        idcMod.Remind = OPSys_DP.SelectedValue + "|" + ServerPos_DP.SelectedValue + "|" + Record_T.Text;
        idcMod.ID = idcBll.Insert(idcMod);
        Odata.id = orderBll.Adds(Odata);
        //-----------------------------
        M_CartPro cpMod = new M_CartPro();
        cpMod.Orderlistid = Odata.id;
        cpMod.ProID = proMod.ID;
        cpMod.Pronum = 1;
        cpMod.Proname = proMod.Proname;
        cpMod.Username = mu.UserName;
        cpMod.Shijia = Convert.ToDouble(timeMod["price"]);
        cpMod.AllMoney = Convert.ToDouble(timeMod["price"]);
        cpMod.code = timeMod["time"].ToString();
        cartProBll.GetInsert(cpMod);
        //-----------------------------
        M_Payment payMod = payBll.CreateByOrder(Odata);
        payBll.Add(payMod);
        Response.Redirect("/PayOnline/Orderpay.aspx?PayNo=" + payMod.PayNo);
    }
}