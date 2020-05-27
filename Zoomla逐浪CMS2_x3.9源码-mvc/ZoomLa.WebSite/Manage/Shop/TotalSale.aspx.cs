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
    public partial class TotalSale : CustomerPageAction
    {
        protected B_ModelField bll = new B_ModelField();
        protected B_OrderList oll = new B_OrderList();
        protected void Page_Load(object sender, EventArgs e)
        {
            B_Admin badmin = new B_Admin();
            if (!B_ARoleAuth.Check(ZLEnum.Auth.shop, "TotalSale"))
                function.WriteErrMsg("没有权限进行此项操作");
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='ProductManage.aspx'>商城管理</a></li><li><a href='ProductSale.aspx'>销售统计</a></li><li>总体销售统计</li>");
            string stime = this.toptime.Text;
            string etime = this.endtime.Text;

            #region 客户平均订单
            DataTable tmp = oll.SelByTime(stime, etime, 1);
            this.Label1.Text = string.Format("{0:c}", DataConverter.CDouble(tmp.Compute("sum(Ordersamount)", "")));
            this.Label2.Text = tmp.Rows.Count.ToString();
            double dds = 0;
            if (DataConverter.CDouble(tmp.Compute("sum(Ordersamount)", "")) > 0)
            {
                dds = DataConverter.CDouble(tmp.Compute("sum(Ordersamount)", "")) / tmp.Rows.Count;
            }
            this.Label3.Text = string.Format("{0:c3}", dds);
            #endregion

            #region 每次访问订单
            //DataTable tos = oll.GetOrderListAll();
            DataTable tos = oll.SelByTime(stime, etime, 2);
            this.Label4.Text = string.Format("{0:c}", DataConverter.CDouble(tos.Compute("sum(Ordersamount)", "")));
            this.Label5.Text = tos.Rows.Count.ToString();
            double ddss = 0;
            if (DataConverter.CDouble(tos.Compute("sum(Ordersamount)", "")) > 0)
            {
                ddss = DataConverter.CDouble(tos.Compute("sum(Ordersamount)", "")) / tos.Rows.Count;
            }
            this.Label6.Text = string.Format("{0:c3}", ddss);
            #endregion

            #region 匿名购买率
            DataTable tmps = oll.SelByTime(stime, etime, 3);
            this.Label7.Text = string.Format("{0:c2}", DataConverter.CDouble(tmps.Compute("sum(Ordersamount)", "")));
            this.Label8.Text = tmps.Rows.Count.ToString();
            double userss = 0;
            if (DataConverter.CDouble(tmps.Compute("sum(Ordersamount)", "")) > 0)
            {
                userss = DataConverter.CDouble(tmps.Compute("sum(Ordersamount)", "")) / tmps.Rows.Count;
            }
            this.Label9.Text = string.Format("{0:c3}", userss);
            #endregion

            #region 会员购买率
            tmps = oll.SelByTime(stime, etime, 4);
            this.Label10.Text = string.Format("{0:c2}", DataConverter.CDouble(tmps.Compute("sum(Ordersamount)", "")));
            this.Label11.Text = tmps.Rows.Count.ToString();
            userss = 0;
            if (DataConverter.CDouble(tmps.Compute("sum(Ordersamount)", "")) > 0)
            {
                userss = DataConverter.CDouble(tmps.Compute("sum(Ordersamount)", "")) / tmps.Rows.Count;
            }
            this.Label12.Text = string.Format("{0:c3}", userss);
            #endregion

        }
    }
}