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
using ZoomLa.BLL.Shop;
using ZoomLa.Model.Shop;
namespace ZoomLaCMS.Manage.Shop
{
    public partial class AddDeliverType : CustomerPageAction
    {
        B_Shop_FareTlp fareBll = new B_Shop_FareTlp();
        B_Admin badmin = new B_Admin();
        public int Mid { get { return DataConverter.CLng(Request.QueryString["ID"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!B_ARoleAuth.Check(ZLEnum.Auth.shop, "DeliverType"))
            {
                function.WriteErrMsg("没有权限进行此项操作");
            }
            if (!IsPostBack)
            {
                MyBind();
                Call.SetBreadCrumb(Master, "<li><a href='ProductManage.aspx'>商城管理</a></li> <li><a href='DeliverType.aspx'>商城设置</a></li><li><a href='DeliverType.aspx'>运费模板管理</a></li><li>添加运费模板</li>");
            }
        }
        public void MyBind()
        {
            if (Mid > 0)
            {
                M_Shop_FareTlp fareMod = fareBll.SelReturnModel(Mid);
                TlpName_T.Text = fareMod.TlpName;
                Fare_Hid.Value = fareMod.Express;
                function.Script(this, "ShowPrice();");
            }
        }
        protected void Save_Btn_Click(object sender, EventArgs e)
        {
            M_Shop_FareTlp fareMod = new M_Shop_FareTlp();
            if (Mid > 0) { fareMod = fareBll.SelReturnModel(Mid); }
            fareMod.TlpName = TlpName_T.Text;
            fareMod.PriceMode = Convert.ToInt32(Request.Form["pricemod_rad"]);
            fareMod.Express = Fare_Hid.Value;//运费Json,后期扩展支持地区,重量
                                             //fareMod.EMS = "";
                                             //fareMod.Mail = "";
            fareMod.Remind = Remind_T.Text;
            fareMod.Remind2 = Remind2_T.Text;
            if (Mid > 0)
            { fareBll.UpdateByID(fareMod); }
            else { fareBll.Insert(fareMod); }
            Response.Redirect("DeliverType.aspx");
        }
    }
}