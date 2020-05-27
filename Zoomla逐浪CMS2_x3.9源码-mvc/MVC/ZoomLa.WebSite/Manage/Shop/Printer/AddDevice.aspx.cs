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

namespace ZoomLaCMS.Manage.Shop.Printer
{
    public partial class AddDevice : System.Web.UI.Page
    {
        B_Shop_PrintDevice devBll = new B_Shop_PrintDevice();
        public int Mid { get { return DataConvert.CLng(Request.QueryString["ID"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='ListDevice.aspx'>智能硬件</a></li><li><a href='ListDevice.aspx'>设备列表</a></li><li class='active'>设备管理</li>");
            }
        }
        private void MyBind()
        {
            if (Mid > 0)
            {
                M_Shop_PrintDevice devMod = devBll.SelReturnModel(Mid);
                Alias_T.Text = devMod.Alias;
                DeviceNo_T.Text = devMod.DeviceNo;
                MemberCode_T.Text = devMod.MemberCode;
                SecurityKey_T.Text = devMod.SecurityKey;
                Remind_T.Text = devMod.Remind;
                IsDefault_C.Checked = devMod.IsDefault == 1;
                ShopName_T.Text = devMod.ShopName;
                Since_L.Text = devMod.Since.ToShortDateString();
                addon_tb.Visible = true;
            }
        }
        protected void Save_Btn_Click(object sender, EventArgs e)
        {
            M_Shop_PrintDevice devMod = new M_Shop_PrintDevice();
            if (Mid > 0) { devMod = devBll.SelReturnModel(Mid); }
            devMod.Alias = Alias_T.Text.Trim();
            devMod.DeviceNo = DeviceNo_T.Text.Replace(" ", "");
            devMod.MemberCode = MemberCode_T.Text.Replace(" ", "");
            devMod.SecurityKey = SecurityKey_T.Text.Replace(" ", "");
            devMod.Remind = Remind_T.Text.Trim();
            devMod.IsDefault = IsDefault_C.Checked ? 1 : 0;
            devMod.ShopName = ShopName_T.Text.Trim();
            if (Mid > 0)//修改
            {
                devBll.UpdateByID(devMod);
                if (IsDefault_C.Checked) { devBll.SetDefault(Mid); }
            }
            else
            {
                int newMid = devBll.Insert(devMod);
                if (IsDefault_C.Checked) { devBll.SetDefault(newMid); }
            }
            function.WriteSuccessMsg("操作成功!", "ListDevice.aspx");
        }
    }
}