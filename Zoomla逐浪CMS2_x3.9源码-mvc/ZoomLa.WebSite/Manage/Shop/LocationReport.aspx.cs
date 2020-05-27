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
using System.Collections.Generic;
using System.Text;
using ZoomLa.Components;
using ZoomLa.SQLDAL;
using System.Data.SqlClient;

namespace ZoomLaCMS.Manage.Shop
{
    public partial class LocationReport :CustomerPageAction
    {
        private B_OrderList bll = new B_OrderList();

        protected void Page_Load(object sender, EventArgs e)
        {
            B_Admin badmin = new B_Admin();
            string bar = "";
            if (!this.Page.IsPostBack)
            {
                string province = Request.QueryString["province"];

                if (string.IsNullOrEmpty(Request.QueryString["code"]))
                {
                    this.Button1.Attributes.Add("disabled", "disabled");
                    bar = "省份列表";
                }
                else {
                    this.Button1.Enabled = true;
                    bar = province + "省城市列表";
                }
                dBind();
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + customPath2 + "Shop/ProductManage.aspx'>商城管理</a></li><li><a href='LocationReport.aspx'>省市报表</a></li><li class='active'>" + bar + "</li>");
        }

        private void dBind()
        {


        }

        protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.EGV.PageIndex = e.NewPageIndex;
            dBind();
        }
        protected void Gdv_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lbtn = (LinkButton)e.Row.FindControl("LinkButton1");
                if (!string.IsNullOrEmpty(Request.QueryString["code"]))
                {
                    lbtn.Enabled = false;
                }
            }

        }
        protected void txtPage_TextChanged(object sender, EventArgs e)
        {
            ViewState["page"] = "1";
            dBind();
        }
        protected void Gdv_Editing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void Lnk_Click(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DicList")
            {
                string code = e.CommandArgument.ToString();
                //string name = EGV.Rows[Convert.ToInt32("code")].Cells[0].Text;
                //string pcode = Request.QueryString["code"];
                string name;
                if (EGV.PageIndex == 0)
                {
                    name = EGV.Rows[(Convert.ToInt32(code) - 1)].Cells[0].Text;
                }
                else
                {
                    name = EGV.Rows[(Convert.ToInt32(code) - 1) - EGV.PageIndex * 15].Cells[0].Text;
                }
                Response.Redirect("LocationReport.aspx?code=" + code + "&province=" + name);
            }

            if (e.CommandName == "CheckList")
            {
                string name = e.CommandArgument.ToString();
                if (string.IsNullOrEmpty(Request.QueryString["province"]))
                {
                    Response.Redirect("OrderList.aspx?province=" + name);
                }
                else {
                    string prov = Request.QueryString["province"];
                    Response.Redirect("OrderList.aspx?province=" + prov + "&city=" + name);
                }
            }

        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("LocationReport.aspx");
        }
        protected string GetOrderNum(object dataItem)
        {
            //int units = Int32.Parse(DataBinder.Eval(dataItem, "name").ToString());
            string name = DataBinder.Eval(dataItem, "name").ToString();
            DataTable location;
            string province = Request.QueryString["province"];
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("province", province), new SqlParameter("name", name) };
            if (string.IsNullOrEmpty(province))
            {
                location = SqlHelper.ExecuteTable(CommandType.Text, "SELECT * FROM ZL_Orderinfo Where Shengfen=@name", sp);
            }
            else
            {
                location = SqlHelper.ExecuteTable(CommandType.Text, "SELECT * FROM ZL_Orderinfo Where Shengfen=@province AND chengshi=@name", sp);
            }
            int OrderNum = location.Rows.Count;
            return Convert.ToString(OrderNum);
        }

        protected string GetOrderAmount(object dataItem)
        {
            string name = DataBinder.Eval(dataItem, "name").ToString();
            string province = Request.QueryString["province"];
            DataTable location;
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("province", province), new SqlParameter("name", name) };
            if (string.IsNullOrEmpty(province))
            {
                location = SqlHelper.ExecuteTable(CommandType.Text, "SELECT * FROM ZL_Orderinfo Where Shengfen=@name", sp);
            }
            else
            {
                location = SqlHelper.ExecuteTable(CommandType.Text, "SELECT * FROM ZL_Orderinfo Where Shengfen=@province AND chengshi=@name", sp);
            }
            int num = 0;
            double ddmoney = 0.00;
            num = location.Rows.Count;
            for (int i = 0; i < num; i++)
            {
                ddmoney = ddmoney + Convert.ToDouble(getshijiage(location.Rows[i]["id"].ToString()));
            }
            string OrderAmount = "0";
            OrderAmount = ddmoney + ".00";
            return OrderAmount;
        }

        public string getshijiage(string id)
        {
            int Sid = DataConverter.CLng(id);
            //string shijiage = "";
            M_OrderList orders = bll.GetOrderListByid(Sid);
            DataTable tb = bll.GetOrderbyOrderNo(orders.OrderNo, "0,4");
            object s = tb.Compute("sum(ordersamount)", "orderno='" + orders.OrderNo + "'");
            if (orders.Ordertype != 4)
            {
                s = DataConverter.CDouble(s).ToString("0.00");
            }
            else
            {
                s = DataConverter.CLng(DataConverter.CDouble(s)).ToString();
            }
            return s.ToString();
        }
    }
}