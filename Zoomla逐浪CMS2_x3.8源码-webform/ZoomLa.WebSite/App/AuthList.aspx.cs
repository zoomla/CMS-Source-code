using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.CreateJS;
using ZoomLa.BLL.User;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.Model.CreateJS;
using ZoomLa.SQLDAL;

//管理端处理用户的授权申请
public partial class App_AuthList : CustomerPageAction
{
    B_APP_Auth authBll = new B_APP_Auth();
    public int Filter { get { return DataConvert.CLng(Request.QueryString["Filter"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
            Call.SetBreadCrumb(Master, "<li><a href='" + customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + CustomerPageAction.customPath2 + "WeiXin/Home.aspx'>移动微信</a></li><li class='active'>授权审核</li>");
        }
    }
    private void MyBind()
    {
        DataTable dt = authBll.SelByFilter(Filter);
        EGV.DataSource = dt;
        EGV.DataBind();
    }
    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        MyBind();
    }
    protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int id = Convert.ToInt32(e.CommandArgument);
        switch (e.CommandName)
        {
            case "del2":
                authBll.Del(id);
                break;
            case "audit":
                string ids = authBll.AuditApply(id.ToString());
                SendMailByIDS(ids);
                break;
            case "unaudit":
                authBll.UnAuditApply(id.ToString());
                break;
        }
        MyBind();
    }
    protected void BatDel_Btn_Click(object sender, EventArgs e)
    {
        authBll.DelByIDS(Request.Form["idchk"]);
        MyBind();
    }
    //审核并发送邮件
    protected void BatAudit_Btn_Click(object sender, EventArgs e)
    {
        string ids = authBll.AuditApply(Request.Form["idchk"]);
        SendMailByIDS(ids);
        MyBind();
    }
    //仅审核不发送邮件
    protected void BatAudit2_Btn_Click(object sender, EventArgs e)
    {
        authBll.AuditApply(Request.Form["idchk"]);
        MyBind();
    }
    protected void BatUnAudit_Btn_Click(object sender, EventArgs e)
    {
        authBll.UnAuditApply(Request.Form["idchk"]);
        MyBind();
    }
    //-----邮件发送
    private void SendMailByIDS(string ids)
    {
        if (!string.IsNullOrEmpty(ids))//发送邮件
        {
            string emailTlp = SafeSC.ReadFileStr("/APP/Other/mail.html");
            DataTable dt = authBll.SelByIDS(ids);
            foreach (string tid in ids.Split(','))
            {
                dt.DefaultView.RowFilter = "ID=" + tid;
                SendAuthedMail(emailTlp, dt.DefaultView.ToTable());
            }
        }
    }
    private void SendAuthedMail(string emailTlp,DataTable dt)
    {
        if (dt.Rows.Count < 1) { function.WriteErrMsg("授权申请不存在"); }
        MailAddress adMod = new MailAddress(dt.Rows[0]["Email"].ToString());
        MailInfo mailInfo = new MailInfo() { ToAddress = adMod, IsBodyHtml = true };
        mailInfo.FromName = SiteConfig.SiteInfo.SiteName;
        mailInfo.Subject = SiteConfig.SiteInfo.SiteName + "APP授权审核邮件";
        mailInfo.MailBody = new OrderCommon().TlpDeal(emailTlp, dt);
        if (SendMail.Send(mailInfo) == ZoomLa.Components.SendMail.MailState.Ok)//发送成功,生成用户,显示下一步提示
        {
            function.WriteErrMsg("发送成功!");
        }
        else
        {
            ZLLog.L(ZLEnum.Log.exception, adMod.Address + "的邮件发送失败,请检测邮箱地址是否正确!");
        }
    }
    protected void EGV_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView dr = e.Row.DataItem as DataRowView;
            if (string.IsNullOrEmpty(DataConvert.CStr(dr["AuthKey"])))
            {
                e.Row.FindControl("unaudit_btn").Visible = false;
            }
            else
            {
                e.Row.FindControl("audit_btn").Visible = false;
            }
        }
    }
}