using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;

namespace ZoomLaCMS.Manage.Config
{
    public partial class WxPayConfig : System.Web.UI.Page
    {
        B_WX_APPID appBll = new B_WX_APPID();
        public int AppId { get { return DataConverter.CLng(Request.QueryString["appid"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
            }
        }
        private void MyBind()
        {
            if (AppId <= 0) { function.WriteErrMsg("没有指定公众号ID"); }
            M_WX_APPID appmod = appBll.SelReturnModel(AppId);
            if (appmod == null) { function.WriteErrMsg("公众号不存在"); }
            APPID_T.Text = appmod.Pay_APPID;
            AccountID_T.Text = appmod.Pay_AccountID;
            Key_T.Text = appmod.Pay_Key;
            SSLPath_T.Text = appmod.Pay_SSLPath;
            SSLPassword_T.Text = appmod.Pay_SSLPassword;
            string alias = " [公众号:" + appmod.Alias + "]";
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + CustomerPageAction.customPath2 + "WeiXin/WxAppManage.aspx'>公众号管理</a></li><li class='active'>支付配置" + alias + "</li>");
        }

        protected void Save_B_Click(object sender, EventArgs e)
        {
            M_WX_APPID appmod = appBll.SelReturnModel(AppId);
            appmod.Pay_APPID = APPID_T.Text;
            appmod.Pay_AccountID = AccountID_T.Text;
            appmod.Pay_Key = Key_T.Text;
            appmod.Pay_SSLPath = SSLPath_T.Text.Trim();
            appmod.Pay_SSLPassword = SSLPassword_T.Text.Trim();
            appBll.UpdateByID(appmod);
            function.WriteSuccessMsg("操作成功", "WxAppManage.aspx");
        }
    }
}