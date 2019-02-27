using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;
using MSXML2;
using ZoomLa.BLL;
using ZoomLa.Components;
using ZoomLa.Model;
using System.Xml;
public partial class User_Message_Mobile : System.Web.UI.Page
{
    B_User buser = new B_User();
    RegexHelper regHelper = new RegexHelper();
    public string Mobile { get { return Request.QueryString["mb"] ?? ""; } }
    public string Txt { get { return Request.QueryString["txt"] ?? ""; } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TxtInceptUser.Text = Mobile;
            EditorContent.Text = Txt;
            string uid = SiteConfig.SiteOption.MssUser;
            string psw = SiteConfig.SiteOption.MssPsw;
            if (string.IsNullOrEmpty(uid) || string.IsNullOrEmpty(psw)) { LblMobile.Text = "没有设置网站的短信通账号和密码"; }
        }
    }

    protected void BtnSend_Click(object sender, EventArgs e)
    {
        string mob = TxtInceptUser.Text.Trim();
        string msg = EditorContent.Text.Trim();
        if (string.IsNullOrEmpty(mob))
        {
            function.WriteErrMsg("请输入手机号码");
        }
        if (string.IsNullOrEmpty(msg))
        {
            function.WriteErrMsg("短信内容不能为空");
        }
        if (msg.Length > 70)
        {
            function.WriteErrMsg("短信内容不能超过70个字");
        }
        Result_L.Text = SendWebSMS.SendMessage(mob, msg);
    }
    protected void BtnReset_Click(object sender, EventArgs e)
    {
        TxtInceptUser.Text = "";
        EditorContent.Text = "";
    }
}