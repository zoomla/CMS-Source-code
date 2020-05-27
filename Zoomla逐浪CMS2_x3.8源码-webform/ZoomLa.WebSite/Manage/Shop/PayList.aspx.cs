namespace Zoomla.Website.manage.Shop
{
    using System;
    using System.Data;
    using System.Web.UI.WebControls;
    using ZoomLa.BLL;
    using ZoomLa.Common;
    using ZoomLa.Model;

    public partial class PayList : CustomerPageAction
    {
        protected B_Payment payll = new B_Payment();
        protected B_User ull = new B_User();
        protected B_PayPlat prell = new B_PayPlat();
        protected B_ModelField mll = new B_ModelField();
        public string UName { get { return Request.QueryString["uname"] ?? ""; } }
        public int Status { get { return DataConverter.CLng(Request.QueryString["status"]); } }
        public string order
        {
            get { if (ViewState["order"] == null) { ViewState["order"] = "id desc"; } return ViewState["order"] as string; }
            set { ViewState["order"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            B_Admin badmin = new B_Admin();

            if (!B_ARoleAuth.Check(ZLEnum.Auth.shop, "PayList"))
            {
                function.WriteErrMsg("没有权限进行此项操作");
            }
            if (!IsPostBack)
            {
                if (Status == -99)
                {
                    SwitchUrl_A.HRef = customPath2 + "Shop/PayList.aspx";
                    SwitchUrl_A.InnerText = "返回列表";
                }
                else
                {
                    SwitchUrl_A.HRef = customPath2 + "Shop/PayList.aspx?status=-99";
                    SwitchUrl_A.InnerText = "回收站";
                }
                MyBind();
            }
        }
        public void MyBind()
        {
            string skeetype = SkeeType_DR.SelectedValue;
            string skey = Skey_T.Text.Trim(' ');
            string addon = Addon_DR.SelectedValue;
            if (!string.IsNullOrEmpty(UName))
            {
                skeetype = "username";
                skey = UName;
            }
            DataTable dt = payll.GetPay("", skeetype, skey, order, addon);
            if (Status == -99) { dt.DefaultView.RowFilter = "isdel = -99"; }
            RPT.DataSource = dt.DefaultView.ToTable();
            RPT.DataBind();
        }
        public string Getclickbotton(string id)
        {
            int tempid = DataConverter.CLng(id);
            string tempstr = "";
            tempstr = "<input name=\"Item\" type=\"checkbox\" value=\"" + id + "\"/>";
            return tempstr;
        }

        protected string GetStatus(string status)
        {
            if (status == "3")
                return "<font color=green>√</font>";
            else
                return "<font color=red>×</font>";
        }
        protected string getusername(string userid)
        {
            M_UserInfo uinfo = ull.GetUserByUserID(DataConverter.CLng(userid));
            return uinfo.UserName;
        }
        protected string getPayPlat(string id)
        {
            var payplat = prell.GetPayPlatByid(DataConverter.CLng(id));
            if (payplat == null) { return ""; }
            return payplat.PayPlatName;
        }
        protected void souchok_Click(object sender, EventArgs e)
        {
            MyBind();
            if (!string.IsNullOrEmpty(Skey_T.Text)) { sel_box.Attributes.Add("style", "display:inline;"); template.Attributes.Add("style", "margin-top:44px;"); }
            else { sel_box.Attributes.Add("style", "display:none;"); }
        }

        protected void RPT_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName.ToLower())
            {
                case "del":
                    payll.DelByIDS(e.CommandArgument.ToString(), M_OrderList.StatusEnum.Recycle);
                    break;
                case "recdel":
                    payll.RealDelByIDS(e.CommandArgument.ToString());
                    break;
                case "rec":
                    payll.DelByIDS(e.CommandArgument.ToString(), M_OrderList.StatusEnum.Normal);
                    break;
            }
            MyBind();
        }

        protected void quicksouch_SelectedIndexChanged(object sender, EventArgs e)
        {
            MyBind();
        }

        protected void ID_Order_Click(object sender, EventArgs e)
        {
            SetOrder("id");
        }

        protected void PayTime_Order_Click(object sender, EventArgs e)
        {
            SetOrder("paytime");
        }

        protected void MoneyPay_Order_Click(object sender, EventArgs e)
        {
            SetOrder("moneypay");
        }

        protected void MoneyTrue_Order_Click(object sender, EventArgs e)
        {
            SetOrder("moneytrue");
        }
        public void SetOrder(string field)
        {
            var reflink = ID_Order;
            string icon = "";
            string orderby = "";
            switch (field)
            {
                case "id":
                    break;
                case "paytime":
                    reflink = PayTime_Order;
                    break;
                case "moneypay":
                    reflink = MoneyPay_Order;
                    break;
                case "moneytrue":
                    reflink = MoneyTrue_Order;
                    break;
            }
            string text = reflink.Text;
            if (text.Contains("fa-long-arrow-up"))
            {
                icon = "<i class='fa fa-long-arrow-down'></i>";
                orderby = " desc";
            }
            else if (text.Contains("fa-long-arrow-down"))
            {
                icon = "";
                orderby = "";
            }
            else
            {
                icon = "<i class='fa fa-long-arrow-up'></i>";
                orderby = " asc";
            }
            ID_Order.Text = "ID";
            PayTime_Order.Text = "交易时间";
            MoneyPay_Order.Text = "汇款金额";
            MoneyTrue_Order.Text = "实际转账金额";
            reflink.Text += " " + icon;
            order = field + orderby;
            MyBind();
        }
    }
}
