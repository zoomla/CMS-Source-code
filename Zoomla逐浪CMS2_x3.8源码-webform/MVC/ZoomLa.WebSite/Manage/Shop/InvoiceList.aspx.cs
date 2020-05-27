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
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Components;

namespace ZoomLaCMS.Manage.Shop
{
    public partial class InvoiceList : CustomerPageAction
    {

        protected B_User ull = new B_User();
        protected B_ModelField mll = new B_ModelField();
        protected B_OrderList ss = new B_OrderList();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + customPath2 + "Shop/ProductManage.aspx'>商城管理</a></li><li><a href='BankRollList.aspx'>明细记录</a></li><li class='active'>开发票明细</li>");
            }
        }
        private void MyBind()
        {
            EGV.DataSource = ss.Sel();
            EGV.DataBind();
        }
        public string Getclickbotton(string id)
        {
            int tempid = DataConverter.CLng(id);
            string tempstr = "";
            tempstr = "<input name=\"Item\" type=\"checkbox\" value=\"" + id + "\"/>";
            return tempstr;
        }
        public string GetMoney(string id)
        {
            int osid = DataConverter.CLng(id);
            M_OrderList sds = ss.GetOrderListByid(osid);
            double mmd = sds.Ordersamount + sds.Freight;
            return string.Format("{0:c}", mmd).ToString();
        }

        public string Getdeli(string id)
        {
            int osid = DataConverter.CLng(id);
            M_OrderList sds = ss.GetOrderListByid(osid);
            double yunfei = sds.Freight;
            return string.Format("{0:c}", yunfei).ToString();
        }

        protected string GetDevelopedvotes(string status)
        {
            if (status == "1")
                return "<font color=green>√</font>";
            else
                return "<font color=red>×</font>";
        }
        protected string getusername(string userid)
        {
            M_UserInfo uinfo = ull.GetUserByUserID(DataConverter.CLng(userid));
            return uinfo.UserName;
        }

        protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            MyBind();
        }
    }
}