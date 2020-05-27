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
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.Manage.Shop.Printer
{
    public partial class TestPrint : System.Web.UI.Page
    {
        B_Shop_PrintDevice devBll = new B_Shop_PrintDevice();
        B_Shop_PrintMessage msgBll = new B_Shop_PrintMessage();
        public int DevId { get { return DataConvert.CLng(Request.QueryString["DevId"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='ListDevice.aspx'>智能硬件</a></li><li class='active'>模拟打印</li>");
            }
        }
        public void MyBind()
        {


            DataTable devs = devBll.Sel();
            RPT.DataSource = devs;
            RPT.DataBind();
        }

        protected void Print_Btn_Click(object sender, EventArgs e)
        {
            int devID = DataConvert.CLng(Request.Form["Dev_R"]);
            if (devID < 1) { function.WriteErrMsg("你还没有选择打印设备!"); }
            int Num = DataConvert.CLng(Num_T.Text.Replace(" ", ""));
            if (Num < 1) { function.WriteErrMsg("请输入正确的打印数量！"); }
            M_Shop_PrintDevice devMod = devBll.SelReturnModel(devID);
            string msg = Msg_T.Text;
            msgBll.Insert(msg, -1, devMod, Num);
            function.WriteSuccessMsg("信息已发送", "MessageList.aspx");
        }
    }
}