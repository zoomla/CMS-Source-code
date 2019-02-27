using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL.Mobile;
using ZoomLa.Common;
using ZoomLa.Model.Mobile;
using ZoomLa.PdoApi.JPush;

public partial class Manage_Mobile_Push_PushMsg : System.Web.UI.Page
{
    //private String app_key = "4273022a6f4eaea232ecb878";
    //private String master_secret = "c1be920354532e3e292f5008";
    B_Mobile_PushMsg msgBll = new B_Mobile_PushMsg();
    B_Mobile_PushAPI apiBll = new B_Mobile_PushAPI();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='Default.aspx'>消息推送</a></li><li class='active'>推送消息</li>");
        }
    }
    private void MyBind() 
    {
        APPList_DP.DataSource = apiBll.Sel();
        APPList_DP.DataBind();
    }
    protected void Push_Btn_Click(object sender, EventArgs e)
    {
        M_Mobile_PushAPI apiMod = apiBll.SelReturnModel(Convert.ToInt32(APPList_DP.SelectedValue));
        C_JPush jpush = new C_JPush(apiMod);
        M_Mobile_PushMsg msgMod = new M_Mobile_PushMsg();
        msgMod.MsgContent = MsgContent_T.Text.Trim();
        msgMod.MsgType = "手动发送";
        msgMod.PushPlat = 1;
        msgMod.PushType = Request.Form["pushtype_rad"];
        msgMod.Result = jpush.SendPush(msgMod).ToString();
        msgBll.Insert(msgMod);
        function.WriteSuccessMsg("发送完成", "Default.aspx");
    }
}