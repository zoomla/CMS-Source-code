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
    public partial class AddQuickPrint : System.Web.UI.Page
    {
        B_Shop_PrintTlp tlpBll = new B_Shop_PrintTlp();
        OrderCommon orderCOM = new OrderCommon();
        private int Mid { get { return DataConvert.CLng(Request.QueryString["ID"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='ListDevice.aspx'>智能硬件</a></li><li><a href='QuickPrint.aspx'>快速指令</a></li><li class='active'>模板管理</li>");
            }
        }
        private void MyBind()
        {
            if (Mid > 0)
            {
                M_Shop_PrintTlp tlpMod = tlpBll.SelReturnModel(Mid);
                Alias_T.Text = tlpMod.Alias;
                Content_T.Text = tlpMod.Content;
                Remind_T.Text = tlpMod.Remind;
            }
        }
        protected void Save_Btn_Click(object sender, EventArgs e)
        {
            M_Shop_PrintTlp tlpMod = new M_Shop_PrintTlp();
            if (Mid > 0) { tlpMod = tlpBll.SelReturnModel(Mid); }
            tlpMod.Alias = Alias_T.Text;
            tlpMod.Content = Content_T.Text;
            tlpMod.Remind = Remind_T.Text;
            if (Mid > 0) { tlpBll.UpdateByID(tlpMod); }
            else { tlpBll.Insert(tlpMod); }
            function.WriteSuccessMsg("操作成功", "QuickPrint.aspx");
        }
    }
}