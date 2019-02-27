namespace ZoomLaCMS.Plugins.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using ZoomLa.BLL;
    using ZoomLa.Common;
    using ZoomLa.Model;
    using ZoomLa.SQLDAL;
    public partial class ChargePurse : System.Web.UI.Page
    {
        protected B_User buser = new B_User();
        protected B_OrderList listBll = new B_OrderList();
        protected string orderNo, result;
        protected void Page_Load(object sender, EventArgs e)
        {
            B_User.CheckIsLogged();
            orderNo = Request.QueryString["OrderNo"];
            if (!IsPaySuccess(orderNo, ref result)) { function.WriteErrMsg(result); }

            if (!IsPostBack)
            {
                AddPurse();
            }
        }
        public bool IsPaySuccess(string orderNo, ref string result)
        {
            bool flag = false;
            DataTable dt = listBll.GetOrderbyOrderNo(orderNo);
            if (dt == null || dt.Rows.Count < 1)
                result = "提示:订单不存在";
            else if (Convert.ToInt32(dt.Rows[0]["paymentstatus"]) != 1)//orderstatus在处理完后再加1吧
                result = "提示:订单未付款";
            else if (Convert.ToInt32(dt.Rows[0]["orderstatus"]) == 99)
                result = "提示:已处理完成的订单,请勿重复访问";
            else if (Convert.ToInt32(dt.Rows[0]["ordertype"]) != 6)
                result = "提示:不是充值订单";
            else//检测成功
            {
                flag = true;
            }
            return flag;
        }
        public void AddPurse()
        {
            DataRow dr = listBll.GetOrderbyOrderNo(orderNo).Rows[0];
            info.Text = "余额充值成功:" + Convert.ToDouble(dr["OrdersAmount"]) + ",你现有" + buser.GetLogin().Purse;
        }
    }
}