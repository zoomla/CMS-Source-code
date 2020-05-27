using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Shop.Order;
using ZoomLa.Common;
using ZoomLa.Model.Shop.Order;

namespace ZoomLaCMS.Manage.Shop
{
    public partial class AddExpSender : System.Web.UI.Page
    {
        B_Order_ExpSender esBll = new B_Order_ExpSender();
        B_Admin badmin = new B_Admin();
        public int Mid { get { return DataConverter.CLng(Request.QueryString["ID"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='ProductManage.aspx'>商城管理</a></li> <li><a href='DeliverType.aspx'>商城设置</a></li><li><a href='ExpSenderManage.aspx'>发件信息</a></li><li class='active'>编辑发件信息</li>");
            }
        }
        private void MyBind()
        {
            M_Order_ExpSender esMod = esBll.SelReturnModel(Mid);
            if (esMod != null)
            {
                Name_T.Text = esMod.Name;
                CompName_T.Text = esMod.CompName;
                Mobile_T.Text = esMod.Mobile;
                IsDefault_C.Checked = esMod.IsDefault == 1;
                Address_T.Text = esMod.Address;
                Remind_T.Text = esMod.Remind;
            }
        }

        protected void Save_Btn_Click(object sender, EventArgs e)
        {
            M_Order_ExpSender esMod = esBll.SelReturnModel(Mid);
            if (esMod == null) { esMod = new M_Order_ExpSender(); }
            esMod.Name = Name_T.Text;
            esMod.CompName = CompName_T.Text;
            esMod.Mobile = Mobile_T.Text;
            esMod.Address = Address_T.Text;
            esMod.Remind = Remind_T.Text;
            if (esMod.ID > 0)
            {
                esBll.UpdateByID(esMod);
            }
            else
            {
                esMod.CDate = DateTime.Now;
                esMod.AdminID = badmin.GetAdminLogin().AdminId;
                esMod.ID = esBll.Insert(esMod);
            }
            if (IsDefault_C.Checked) { esBll.SetDefault(esMod.ID); }
            function.WriteSuccessMsg("操作成功!","ExpSenderManage.aspx");
        }
    }
}