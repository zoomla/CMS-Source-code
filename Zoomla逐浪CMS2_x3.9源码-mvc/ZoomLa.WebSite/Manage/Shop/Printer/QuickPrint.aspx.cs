using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL.Shop;
using ZoomLa.Common;
namespace ZoomLaCMS.Manage.Shop.Printer
{
    public partial class QuickPrint : System.Web.UI.Page
    {
        B_Shop_PrintTlp tlpBll = new B_Shop_PrintTlp();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='ListDevice.aspx'>智能硬件</a></li><li class='active'>模板管理 [<a href='AddQuickPrint.aspx'>添加模板</a>]</li>");
            }
        }
        private void MyBind()
        {
            EGV.DataSource = tlpBll.Sel();
            EGV.DataBind();
        }
        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            MyBind();
        }
        protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "del2":
                    int id = Convert.ToInt32(e.CommandArgument);
                    tlpBll.Del(id);
                    break;
            }
            MyBind();
        }
        protected void EGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dr = e.Row.DataItem as DataRowView;
                e.Row.Attributes.Add("ondblclick", "location='AddQuickPrint.aspx?id=" + dr["id"] + "'");
            }
        }
        public string GetContent()
        {
            return StringHelper.SubStr(Eval("Content"));
        }
    }
}