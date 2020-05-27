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

namespace ZoomLaCMS.Manage.Shop
{
    public partial class Delivery : System.Web.UI.Page
    {
        B_OrderList orderBll = new B_OrderList();
        B_Content conBll = new B_Content();
        B_Admin badmin = new B_Admin();
        B_Order_Exp expBll = new B_Order_Exp();
        //订单号
        private int Mid { get { return DataConvert.CLng(Request.QueryString["ID"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!badmin.CheckLogin())
            {
                function.WriteErrMsg("您没有管理员权限!");
            }
            if (!IsPostBack)
            {
                MyBind();
            }
        }
        private void MyBind()
        {
            ExpComp_DP.DataSource = expBll.ExpCompDT();
            ExpComp_DP.DataBind();
            //-------------------
            M_OrderList orderMod = orderBll.SelReturnModel(Mid);
            OrderNo_L.Text = orderMod.OrderNo;
            UName_L.Text = orderMod.Rename;
            CDate_L.Text = orderMod.AddTime.ToString("yyyy年MM月dd日 HH:mm");
            ProName_L.Text = GetProName(orderMod);//需要方法处理
            OrderMessage_L.Text = orderMod.Ordermessage;
            Address_L.Text = orderMod.Shengfen + " " + orderMod.Jiedao;
            ZipCode_L.Text = orderMod.ZipCode;
            Mobile_L.Text = orderMod.MobileNum;
            Phone_L.Text = orderMod.Phone;
            Reuser_L.Text = orderMod.Reuser;
            if (!string.IsNullOrEmpty(orderMod.ExpressNum))
            {
                M_Order_Exp expMod = expBll.SelReturnModel(Convert.ToInt32(orderMod.ExpressNum));
                ExpNo_T.Text = expMod.ExpNo;
                ExpComp_DP.SelectedValue = expMod.CompType;
                ExpOther_T.Text = expMod.ExpComp;
            }
        }
        protected void Save_Btn_Click(object sender, EventArgs e)
        {
            M_OrderList orderMod = orderBll.SelReturnModel(Mid);
            M_Order_Exp expMod = new M_Order_Exp();
            if (!string.IsNullOrEmpty(orderMod.ExpressNum))
            {
                expMod = expBll.SelReturnModel(Convert.ToInt32(orderMod.ExpressNum));
            }
            expMod.ExpNo = ExpNo_T.Text.Trim();
            expMod.CompType = ExpComp_DP.SelectedValue;
            if (expMod.CompType.Equals("其它 ")) { expMod.ExpComp = ExpOther_T.Text; }
            else { expMod.ExpComp = ExpComp_DP.SelectedValue; }
            if (expMod.ID > 0) { expBll.UpdateByID(expMod); }
            else
            {
                expMod.OrderID = orderMod.id;
                expMod.UserID = orderMod.Userid;
                expMod.ID = expBll.Insert(expMod);
            }
            orderMod.StateLogistics = 1;
            orderMod.ExpressNum = expMod.ID.ToString();
            orderBll.UpdateByID(orderMod);
            function.Script(this, "parent.window.location= parent.location;");
        }
        private string GetProName(M_OrderList orderMod)
        {
            B_CartPro cartBll = new B_CartPro();
            DataTable dt = cartBll.SelByOrderID(orderMod.id);
            string result = "";
            foreach (DataRow dr in dt.Rows)
            {
                result += "[" + dr["ProName"] + "(数量:" + dr["ProNum"] + ")]<br />";
            }
            return result;
        }
    }
}