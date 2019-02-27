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

namespace ZoomLaCMS.Manage.Shop.IDC
{
    public partial class IDC_Ren : System.Web.UI.Page
    {
        B_OrderList orderBll = new B_OrderList();
        B_Order_IDC idcBll = new B_Order_IDC();
        B_CartPro cpBll = new B_CartPro();
        B_Product probll = new B_Product();
        B_User buser = new B_User();
        public string OrderNo { get { return Request.QueryString["ID"] ?? ""; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                M_OrderList orderMod = orderBll.SelModelByOrderNo(OrderNo);
                M_Order_IDC idcMod = idcBll.SelModelByOrderNo(orderMod.OrderNo);
                if (orderMod == null || idcMod == null) { function.WriteErrMsg("订单不存在或并非IDC订单"); }
                M_Product proMod = probll.GetproductByid(idcMod.ProID);
                OrderNo_L.Text = orderMod.OrderNo;
                ProName_L.Text = proMod.Proname;
                IDCTime_DP.DataSource = idcBll.P_GetValid(proMod.IDCPrice);
                IDCTime_DP.DataBind();
                STime_L.Text = idcMod.STime.ToString("yyyy-MM-dd");
                ETime_L.Text = idcMod.ETime.ToString("yyyy-MM-dd");
                ETime_T.Text = idcMod.ETime.ToString("yyyy-MM-dd");
                Call.HideBread(Master);
            }
        }
        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            //续费订单只生成订单,不生成商品与IDC信息
            //创建一张新订单，完毕后更新老订单时间
            M_OrderList orderMod = orderBll.SelModelByOrderNo(OrderNo);
            M_Order_IDC idcMod = idcBll.SelModelByOrderNo(OrderNo);
            M_Product proMod = probll.GetproductByid(idcMod.ProID);
            M_UserInfo mu = buser.SelReturnModel(orderMod.Userid);
            if (mu.IsNull) { function.WriteErrMsg("订单未绑定用户[" + orderMod.Userid + "],或用户不存在"); }
            //根据所选,生成新的ID充值订单
            DataRow timeMod = idcBll.GetSelTime(proMod, IDCTime_DP.SelectedValue);
            M_OrderList newMod = orderBll.NewOrder(mu, M_OrderList.OrderEnum.IDCRen);
            newMod.Ordersamount = Convert.ToDouble(timeMod["price"]);
            newMod.Promoter = idcMod.ID;
            newMod.Ordermessage = idcBll.ToProInfoStr(timeMod);
            newMod.id = orderBll.insert(newMod);

            buser.SetLoginState(mu);
            string url = "/PayOnline/Orderpay.aspx?OrderCode=" + newMod.OrderNo;
            function.Script(this, "window.open('" + url + "');");
            ////普通续费产生无用IDC订单,以用户身份续费,产生了新订单
            //switch (Request.Form["usertype"])
            //{
            //    case "1":

            //        break;
            //    case "0"://管理员
            //        OrderHelper.FinalStep(newMod);
            //        function.WriteSuccessMsg("续费成功");
            //        break;
            //    default:
            //        break;
            //}
        }
        protected void logonBtn_Click(object sender, EventArgs e)
        {
            M_OrderList orderMod = orderBll.SelModelByOrderNo(OrderNo);
            M_Order_IDC idcMod = idcBll.SelModelByOrderNo(OrderNo);
            M_UserInfo mu = buser.SelReturnModel(orderMod.Userid);
            buser.SetLoginState(mu, "Day");
            function.Script(this, "window.open('/Cart/FillIDCInfo.aspx?ProID=" + idcMod.ProID + "')");
        }
        protected void UpdateETime_Btn_Click(object sender, EventArgs e)
        {
            M_Order_IDC idcMod = idcBll.SelModelByOrderNo(OrderNo);
            idcMod.ETime = Convert.ToDateTime(ETime_T.Text);
            idcBll.UpdateByID(idcMod);
            function.WriteSuccessMsg("修改到期时间成功");
        }
    }
}