using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL.CreateJS;
using ZoomLa.BLL.Helper;
using ZoomLa.BLL.User;
using ZoomLa.Common;
using ZoomLa.Model.CreateJS;
using ZoomLa.Model.User;

public partial class App_AuthApply : System.Web.UI.Page
{
    B_APP_Auth authBll = new B_APP_Auth();
    private int Mid { get { return DataConverter.CLng(Request.QueryString["ID"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }
    protected void Sure_Btn_Click(object sender, EventArgs e)
    {
        M_APP_Auth authMod = new M_APP_Auth();
        authMod.SiteUrl = StrHelper.UrlDeal(SiteUrl_T.Text);
        authMod.Contact = Contact_T.Text;
        authMod.MPhone = MPhone_T.Text;
        authMod.Email = Email_T.Text;
        authMod.QQCode = QQCode_T.Text;
        authBll.Insert(authMod);
        function.WriteSuccessMsg("授权申请提交成功,请等待管理员审核", "AuthApply.aspx", 0);
    }
}