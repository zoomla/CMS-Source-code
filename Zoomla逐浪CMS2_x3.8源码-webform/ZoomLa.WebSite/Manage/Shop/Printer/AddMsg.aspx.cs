using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL.Shop;
using ZoomLa.Common;
using ZoomLa.Model.Shop;
using ZoomLa.SQLDAL;

public partial class Manage_Shop_Printer_AddMsg : System.Web.UI.Page
{
    B_Shop_PrintMessage msgBll = new B_Shop_PrintMessage();
    B_Shop_PrintDevice devBll = new B_Shop_PrintDevice();
    public int Mid { get { return DataConvert.CLng(Request.QueryString["ID"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Mid < 1) { function.WriteErrMsg("未指定信息"); }
            MyBind();
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='ListDevice.aspx'>智能硬件</a></li><li><a href='MessageList.aspx'>打印流水</a></li><li class='active'>流水详情</li>");
        }
    }
    private void MyBind()
    {
        M_Shop_PrintMessage msgMod = msgBll.SelReturnModel(Mid);
        M_Shop_PrintDevice devMod = devBll.SelReturnModel(msgMod.DevID);
        ReqTime_L.Text = msgMod.ReqTime.ToString();
        Alias_L.Text = devMod.Alias;
        ShopName_L.Text = devMod.ShopName;
        ReqStatus_L.Text = msgBll.DealReqStatus(msgMod.ReqStatus);
        Detail_T.Text = msgMod.Detail;
    }
}