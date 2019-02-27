using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
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
using System.Xml;
using System.IO;
using ZoomLa.BLL.User.Develop;
using ZoomLa.Model.User.Develop;
public partial class OrderOver : System.Web.UI.Page
{
    B_User buser = new B_User();
    B_OrderList oll = new B_OrderList();
    B_Stock bstock = new B_Stock();
    B_CartPro bcarpro = new B_CartPro();
    B_Product bll = new B_Product();
    DataSet fileset = new DataSet();
    DataTable infotable = new DataTable();
    public string OrderNo { get { return Request.QueryString["OrderCode"]; } }
    protected void Page_Load(object sender, EventArgs e)
    {
        B_User.CheckIsLogged(Request.RawUrl);
        if (!IsPostBack)
        {
            return;
            //Readcartinfo();
            //M_UserInfo infouser = buser.GetLogin(false);
            //DataTable orderinfo = oll.GetOrderbyOrderNo(OrderNo);
            //if (orderinfo != null && orderinfo.Rows.Count > 0)
            //{
            //    int ordertype = DataConverter.CLng(orderinfo.Rows[0]["orderType"]);
            //    if (ordertype == 4)//积分订单
            //    {
            //        Score_Btn.Visible = true;
            //        Button1.Visible = false;
            //        SiverCoin.Visible = false;
            //        UserPurseBTN.Visible = false;
            //        Button3.Visible = false;
            //    }
            //    else if (ordertype == 5 || ordertype == 7)//域名订单
            //    {
            //        SiverCoin.Visible = false;
            //        Button3.Visible = false;
            //    }
            //}
            //DataTable Unews = bcarpro.GetCartProOrderID(DataConverter.CLng(orderinfo.Rows[0]["id"]));
            //int jifen = 0;
            //for (int i = 0; i < Unews.Rows.Count; i++)
            //{
            //    int Onum = DataConverter.CLng(Unews.Rows[i]["Pronum"].ToString());
            //    int Opid = DataConverter.CLng(Unews.Rows[i]["ProID"].ToString());
            //    M_Product pdin = bll.GetproductByid(Opid);
            //    jifen += pdin.Integral;
            //}
            //DataTable ordertable = oll.GetOrderbyOrderNo(OrderNo);
            //if (ordertable != null && ordertable.Rows.Count != 0)
            //{
            //    int Deliverys = DataConverter.CLng(ordertable.Rows[0]["Delivery"]);
                
            //    double dingdanjiage = DataConverter.CDouble(ordertable.Rows[0]["Ordersamount"]);
            //    double tempjiage = System.Math.Round(dingdanjiage, 2);
            //    string jiage = tempjiage.ToString();
            //    if (jiage.IndexOf(".") == -1)
            //    {
            //        jiage = jiage + ".00";
            //    }
            //    double alljiage = dingdanjiage;
            //    string JG = jiage.ToString() + " = " + alljiage.ToString();
            //    string addr = "收货人地址：";
            //    if (!string.IsNullOrEmpty(ordertable.Rows[0]["Shengfen"].ToString())) { addr += ordertable.Rows[0]["Shengfen"] + "省"; }
            //    if (!string.IsNullOrEmpty(ordertable.Rows[0]["Chengshi"].ToString())) { addr += ordertable.Rows[0]["Chengshi"] + "市"; }
            //    addr += ordertable.Rows[0]["Jiedao"] + "<span style='margin-left:15px;'></span>" + ordertable.Rows[0]["Receiver"];

            //    JG = JG + "　<font color='#4A708B'>应付金额=商品总价(含发票税额)+运费</font>";
            //    Label1.Text = this.infotable.Rows[0]["message28"].ToString() + OrderNo + "<br /> " + addr + "<br />应付金额：" + JG + "<br />此订单获得积分：" + jifen + "<br />您的积分总计：" + infouser.UserExp + "<br />";// +this.infotable.Rows[0]["message29"].ToString();
            //}
        }
    }
    private void Readcartinfo()
    {
        fileset.ReadXml(Server.MapPath("/config/Cartinfo.xml"));
        this.infotable = fileset.Tables[0].DefaultView.Table;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        DataTable orderinfo = oll.GetOrderbyOrderNo(OrderNo);
        int orderamount = 0;
        if (orderinfo != null && orderinfo.Rows.Count > 0)
        {
            M_UserInfo info = buser.GetLogin();
            if (info != null)
            {
                if (orderinfo.Rows[0]["ordertype"].ToString() == "4")
                {
                    orderamount = DataConverter.CLng(DataConverter.CDouble(orderinfo.Rows[0]["Ordersamount"].ToString()));
                }
                else
                {
                    Response.Redirect("PayOnline/Orderpay.aspx?OrderCode=" + OrderNo);
                }
            }
        }
        else
        {
            function.WriteErrMsg("此订单不存在!");
        }
    }
    protected void UserPurseBTN_Click(object sender, EventArgs e)
    {
       
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        DataTable orderinfo = oll.GetOrderbyOrderNo(OrderNo);
        if (orderinfo != null && orderinfo.Rows.Count > 0)
        {
            Response.Redirect("User/Shopfee/PaypalDefray.aspx?OrderCode=" + OrderNo);
        }
        else
        {
            function.WriteErrMsg("此订单不存在!");
        }
    }
    /// <summary>
    ///  添加出库记录
    /// </summary>
    /// <param name="id">订单ID</param>
    /// <param name="user">添加用户</param>
    private void AddStoc(int id, string user)
    {
        
        DataTable cplist = bcarpro.GetCartProOrderID(id);
        if (cplist != null && cplist.Rows.Count > 0)
        {
            foreach (DataRow dr in cplist.Rows)
            {
                M_Stock CData = new M_Stock();
                CData.proid = DataConverter.CLng(dr["ProID"].ToString());
                CData.stocktype = 1;
                CData.proname = dr["Proname"].ToString();
                CData.danju = "CK" + Convert.ToString(DateTime.Now.Year) + Convert.ToString(DateTime.Now.Month) + Convert.ToString(DateTime.Now.Day) + Convert.ToString(DateTime.Now.Hour) + Convert.ToString(DateTime.Now.Minute) + Convert.ToString(DateTime.Now.Second) + Convert.ToString(DateTime.Now.Millisecond);
                CData.adduser = user;
                CData.addtime = DateTime.Now;
                CData.content = "订单:" + user + "发货";
                CData.Pronum = DataConverter.CLng(dr["pronum"]);
                bstock.AddStock(CData);
            }
        }
    }
    protected void SiverCoin_Click(object sender, EventArgs e)
    {
        Response.Redirect(string.Concat(new object[] { "/PayOnline/OrderPay.aspx?Method=SilverCoin&OrderNo=" + Request["OrderCode"] }));
    }
    protected void Score_Btn_Click(object sender, EventArgs e)
    {
        Response.Redirect(string.Concat(new object[] { "/PayOnline/OrderPay.aspx?Method=Score&OrderNo=" + OrderNo }));
    }
}