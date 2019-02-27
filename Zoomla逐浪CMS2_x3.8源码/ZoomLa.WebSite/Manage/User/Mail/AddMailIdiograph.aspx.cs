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
using ZoomLa.Model;
using ZoomLa.Common;

public partial class manage_Qmail_AddMailIdiograph : CustomerPageAction
{
    B_MailIdiograph mibll = new B_MailIdiograph();
    M_MailIdiograph mm = new M_MailIdiograph();
    public string type = "添加";
    public int GID { get { return DataConverter.CLng(Request.QueryString["id"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack )
        {
            if (GID>0)
            {
                mm = mibll.GetSelect(int.Parse(Request.QueryString["ID"].ToString()));
                txtName.Text = mm.Name;
                txtContext.Text = mm.Context;
                rblState.SelectedValue = mm.State.ToString();
                type = "修改";
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='" + CustomerPageAction.customPath2 + "User/UserManage.aspx'>用户管理</a></li><li><a href='" + CustomerPageAction.customPath2 + "User/SubscriptListManage.aspx?menu=all'>订阅管理</a></li><li><a href='MailIdiographList.aspx'>签名列表</a></li><li>添加签名</li>"); 
        }
    }
    protected void Save_Btn_Click(object sender, EventArgs e)
    {
        if (GID > 0)
        {
            mm = mibll.GetSelect(GID);
            mm.Name = txtName.Text;
            mm.Context = txtContext.Text;
            mm.State = bool.Parse(rblState.SelectedValue);
            mibll.GetUpdate(mm);
        }
        else
        {
            mm.Name = txtName.Text;
            mm.Context = txtContext.Text;
            mm.State = bool.Parse(rblState.SelectedValue);
            mm.AddTime = DateTime.Now;
            mibll.GetInsert(mm);
        }
        function.WriteSuccessMsg("保存成功!", "MailIdiographList.aspx");
    }
}
