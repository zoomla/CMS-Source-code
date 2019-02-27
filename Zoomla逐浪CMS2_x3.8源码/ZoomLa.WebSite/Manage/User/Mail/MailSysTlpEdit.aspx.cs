using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;

public partial class Manage_User_Mail_MailSysTlpEdit : System.Web.UI.Page
{
    string vdir = "/Common/MailTlp/";
    private String TlpName { get { return HttpUtility.UrlDecode(Request.QueryString["TlpName"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            String content = SafeSC.ReadFileStr(vdir + TlpName);
            TxtTempName.Text = TlpName;
            TxtContent.Value = content;
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + CustomerPageAction.customPath2 + "User/UserManage.aspx'>用户管理</a></li><li><a href='MailSysTlp.aspx'>系统模板</a></li><li class='active'>修改系统模板</li>");
        }
    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        SafeSC.WriteFile(vdir + TlpName, TxtContent.Value);
        function.WriteSuccessMsg("操作成功", "MailSysTlp.aspx");
    }


}