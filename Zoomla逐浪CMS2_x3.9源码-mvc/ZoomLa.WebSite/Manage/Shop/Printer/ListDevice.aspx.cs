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
using ZoomLa.Model.Shop;
namespace ZoomLaCMS.Manage.Shop.Printer
{
    public partial class ListDevice : System.Web.UI.Page
    {
        private B_Shop_APIPrinter prtBll = new B_Shop_APIPrinter();
        private B_Shop_PrintDevice devBll = new B_Shop_PrintDevice();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='ListDevice.aspx'>智能硬件</a></li><li class='active'>设备列表<a href='AddDevice.aspx'>[添加设备]</a></li>");
            }
        }
        public void MyBind()
        {
            DataView dv = new DataView(devBll.Sel());
            dv.Sort = "IsDefault DESC";
            EGV.DataSource = dv;
            EGV.DataBind();
        }
        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            MyBind();
        }

        protected void Dels_Btn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.Form["idchk"]))
            {
                devBll.DelByIDS(Request.Form["idchk"]);
                MyBind();
            }
        }
        protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "del":
                    {
                        devBll.Del(DataConverter.CLng(e.CommandArgument));
                    }
                    break;
                case "setDef":
                    {
                        devBll.SetDefault(DataConverter.CLng(e.CommandArgument));
                    }
                    break;
            }
            MyBind();
        }
        protected void EGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dr = e.Row.DataItem as DataRowView;
                e.Row.Attributes.Add("ondblclick", "location='AddDevice.aspx?ID=" + dr["id"] + "'");
            }
        }
    }
}