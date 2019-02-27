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
    using System.Text;
    using ZoomLa.Components;

    public partial class MoneyManage : CustomerPageAction
    {
        private B_MoneyManage moneyBll = new B_MoneyManage();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='ProductManage.aspx'>商城管理</a></li> <li><a href='DeliverType.aspx'>商城设置</a></li><li><a href='MoneyManage.aspx'>货币管理</a></li><li class='active'>货币列表<a href='AddMoney.aspx'>[添加货币]</a></li>");
            }
        }
        public void MyBind()
        {
            DataTable dt = moneyBll.Sel();
            Egv.DataSource = dt;
            Egv.DataBind();
        }
        protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Egv.PageIndex = e.NewPageIndex;
            MyBind();
        }
        protected void Egv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
            {

                e.Row.Attributes.Add("ondblclick", "location='AddMoney.aspx?ID=" + (e.Row.DataItem as DataRowView)["Flow"] + "';");
            }
        }
        protected void Egv_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToLower())
            {
                case "del1":
                    moneyBll.GetDelete(Convert.ToInt32(e.CommandArgument.ToString()));
                    break;
            }
            MyBind();
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.Form["idchk"]))
            {
                if (moneyBll.DeleteByIds(Request.Form["idchk"]))
                { 
                    function.WriteSuccessMsg("批量删除成功", "MoneyManage.aspx");
                }
            }
        }
    }
}