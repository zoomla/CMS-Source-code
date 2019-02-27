using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL.Shop;
using ZoomLa.Common;
using ZoomLa.Model.Shop;

namespace ZoomLaCMS.Manage.Shop.Printer
{
    public partial class MessageList : System.Web.UI.Page
    {
        public B_Shop_PrintMessage msgBll = new B_Shop_PrintMessage();
        B_Shop_PrintDevice devBll = new B_Shop_PrintDevice();
        B_Shop_APIPrinter printBll = new B_Shop_APIPrinter();
        private int DevID { get { return DataConverter.CLng(Request.QueryString["DevID"]); } }
        private string skey { get { return Request.QueryString["skey"] ?? ""; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='ListDevice.aspx'>智能硬件</a></li><li class='active'>流水详情</li>");
            }
        }
        private void MyBind()
        {
            EGV.DataSource = msgBll.Search(DevID, skey);
            EGV.DataBind();
        }
        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            MyBind();
        }
        protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = DataConverter.CLng(e.CommandArgument);
            switch (e.CommandName)
            {
                case "reprint":
                    M_Shop_PrintMessage msgMod = msgBll.SelReturnModel(id);
                    M_Shop_PrintDevice devMod = devBll.SelReturnModel(msgMod.DevID);
                    msgMod.ReqStatus = printBll.SendFreeMessage(msgMod, devMod.DeviceNo);
                    msgBll.Insert(msgMod);
                    function.WriteSuccessMsg("已重新发送");
                    break;
            }
            MyBind();
        }
        protected void EGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dr = e.Row.DataItem as DataRowView;
                e.Row.Attributes.Add("ondblclick", "location='AddMsg.aspx?id=" + dr["id"] + "'");
            }
        }
        public string GetContent()
        {
            return StringHelper.SubStr(Eval("Detail"), 30);
        }
    }
}