using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL.Mobile;
using ZoomLa.Common;
using ZoomLa.Model.Mobile;
using ZoomLa.SQLDAL;
namespace ZoomLaCMS.Manage.Mobile.Push
{
    public partial class AddAPI : System.Web.UI.Page
    {
        B_Mobile_PushAPI apiBll = new B_Mobile_PushAPI();
        M_Mobile_PushAPI apiMod = new M_Mobile_PushAPI();
        private int Mid { get { return DataConvert.CLng(Request.QueryString["ID"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='Default.aspx'>消息推送</a></li><li class='active'>API设置</li>");
            }
        }
        private void MyBind()
        {
            if (Mid > 0)
            {
                apiMod = apiBll.SelReturnModel(Mid);
                Alias_T.Text = apiMod.Alias;
                APPKey_T.Text = apiMod.APPKey;
                APPSecret_T.Text = apiMod.APPSecret;
            }
        }
        protected void Save_Btn_Click(object sender, EventArgs e)
        {
            if (Mid > 0) { apiMod = apiBll.SelReturnModel(Mid); }
            apiMod.Alias = Alias_T.Text;
            apiMod.APPKey = APPKey_T.Text.Replace(" ", "");
            apiMod.APPSecret = APPSecret_T.Text.Replace(" ", "");
            apiMod.Plat = 1;
            if (Mid > 0) { apiBll.UpdateByID(apiMod); }
            else { apiBll.Insert(apiMod); }
            function.WriteSuccessMsg("操作成功", "APIList.aspx");
        }
    }
}