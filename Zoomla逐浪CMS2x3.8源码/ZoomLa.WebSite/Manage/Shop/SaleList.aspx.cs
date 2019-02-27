namespace Zoomla.Website.manage.Shop
{
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
    using System.Data.SqlClient;

    public partial class SaleList : CustomerPageAction
    {
        protected B_CartPro pro = new B_CartPro();
        protected B_OrderList oll = new B_OrderList();
        protected B_ModelField mll = new B_ModelField();
        protected B_Product pll = new B_Product();
        protected B_User ull = new B_User();
        protected void Page_Load(object sender, EventArgs e)
        {
            B_Admin badmin = new B_Admin();
            
            if (!B_ARoleAuth.Check(ZLEnum.Auth.shop, "SaleList"))
            {
                function.WriteErrMsg("没有权限进行此项操作");
            }
            if(!IsPostBack)
            {
                DataBind();
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='" + customPath2 + "I/Shop/ProductManage.aspx'>商城管理</a></li><li><a href='BankRollList.aspx'>明细记录</a></li><li class='active'>销售明细</li>");
        }
        public void DataBind(string key="")
        {
            DataTable dt = new DataTable();
            int qtype = DataConverter.CLng(quicksouch.SelectedValue);
            string souchtables = souchtable.SelectedValue;
            string keywork = souchkey.Text;
            string str = "";
            switch (qtype)
            {
                case 0:
                    str = "";
                    break;
                case 1:
                    str = "";
                    break;
                case 2:
                    str = "and datediff(day,Addtime,'" + DateTime.Now + "')<1";
                    break;
                case 3:
                    str = "and datediff(day,Addtime,'" + DateTime.Now + "')<7";
                    break;
                case 4:
                    str = "and Addtime>dateadd(month,-0,convert(varchar(6),cast(getdate() as datetime),112)+'01') and Addtime<dateadd(month,+1,convert(varchar(6),cast(getdate() as datetime),112)+'01')";
                    break;
                default :
                    str = "";
                    break;
            }
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("lkey", "%" + keywork + "%"), new SqlParameter("lkey2", "%" + keywork.Replace("-", "%") + "%"), new SqlParameter("key", keywork) };
            if (!string.IsNullOrEmpty(keywork))
            {
                switch (souchtables)
                {
                    case "Reuser":
                        str = str + "and Orderlistid in (select id from ZL_Orderinfo where Reuser like @lkey)";
                        break;
                    case "Username":
                        str = str + "and Username=@key";
                        break;
                    case "Proname":
                        str = str + "and Username=@key";
                        break;
                    case "AddTime":
                        str = str + "and Orderlistid in (select id from ZL_Orderinfo where CONVERT(char, AddTime, 20) like  @lkey2)";
                        break;
                    case "OrderNo":
                        str = str + "and Orderlistid in (select id from ZL_Orderinfo where OrderNo like @lkey)";
                        break;
                }
            }
            dt = mll.SelectTableName("ZL_CartPro", "Orderlistid>0 " + str, sp);//Checked
            AllMonkey_HF.Value = string.Format("{0:c}", dt.Compute("sum(AllMoney)", "")).ToString();
            Egv.DataSource = dt;
            Egv.DataBind();
        }
        protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Egv.PageIndex = e.NewPageIndex;
            DataBind();
        }
        protected void Egv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(e.Row!=null&&e.Row.RowType==DataControlRowType.DataRow)
            {
                e.Row.Attributes["ondblclick"] = "javascript:getinfo('" + Egv.DataKeys[e.Row.RowIndex].Value + "');";
                e.Row.Attributes["style"] = "cursor:pointer;";
            }
        }
        protected string GetOrder(string orderid)
        {
            int orid = DataConverter.CLng(orderid);
            if (orid > 0)
            {
                M_OrderList olist = oll.GetOrderListByid(orid);
                return olist.OrderNo;
            }
            else { return ""; }
        }
        protected string getordertime(string orderid)
        {
            int oid = DataConverter.CLng(orderid);
            return oll.GetOrderListByid(oid).AddTime.ToString();

        }

        protected string GetOrderiuser(string orderid)
        {

            int orid = DataConverter.CLng(orderid);
            if (orid > 0)
            {
                M_OrderList olist = oll.GetOrderListByid(orid);
                return olist.Reuser;
            }
            else { return ""; }
        }

        protected double getpromoney(string proid)
        {
            int pid = DataConverter.CLng(proid);
            return pll.GetproductByid(pid).ShiPrice;
        }
        protected void souchok_Click(object sender, EventArgs e)
        {
            DataBind();
        }
    }
}