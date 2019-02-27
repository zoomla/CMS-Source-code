using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Model;
using ZoomLa.BLL;
using ZoomLa.Common;
using System.Data;
using System.Net.Mail;
using ZoomLa.Components;
using Newtonsoft.Json;

public partial class Manage_User_SendSubMail : System.Web.UI.Page
{
    B_Mail_BookRead readBll = new B_Mail_BookRead();
    B_Content contentBll = new B_Content();
    OrderCommon common = new OrderCommon();
    B_MailManage mailBll = new B_MailManage();
    B_MailIdiograph graphBll = new B_MailIdiograph();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (function.isAjax())
        {
            string action = Request.Form["action"];
            string result = "";
            switch (action)
            {
                case "getcon":
                    {
                        DataTable dt = contentBll.GetContentByItems(DataConverter.CLng(Request.Form["cid"]));
                        if (dt == null || dt.Rows.Count <= 0) { result = "无内容!"; }

                        result = JsonConvert.SerializeObject(dt);
                    }
                    break;
                default:
                    break;
            }
            Response.Write(result);Response.Flush();Response.End();
        }
        if (!IsPostBack)
        {
            MyBind();
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href=\"SubscriptListManage.aspx?menu=all\">订阅管理</a></li><li>发送邮件</li>");
        }
    }
    public void MyBind()
    {
        Graph_Drop.DataSource = graphBll.Select_All(1);
        Graph_Drop.DataBind();
        Graph_Drop.Items.Insert(0, new ListItem("无签名", "0"));
    }

    protected void Send_Btn_Click(object sender, EventArgs e)
    {
        string emailcontent = mailBll.SelByType(B_MailManage.MailType.SubMailContent);
        emailcontent = common.TlpDeal(emailcontent, GetSubEmailDt(Content_T.Text));
        if (DataConverter.CLng(Graph_Drop.SelectedValue) > 0)//邮件签名
        {
            M_MailIdiograph graphMod = graphBll.SelReturnModel(DataConverter.CLng(Graph_Drop.SelectedValue));
            emailcontent += graphMod.Context;
        }
        if (SubUsers_Radio.SelectedValue.Equals("alluser"))//全部订阅用户发送
        {
            DataTable allUserDT = readBll.SelectAll(1);
            foreach (DataRow dr in allUserDT.Rows)
            {
                SendSubEMail(dr["EMail"].ToString(), emailcontent);
            }
        }
        function.WriteSuccessMsg("发送成功!");
    }
    public DataTable GetSubEmailDt(string content)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Content");
        dt.Rows.Add(dt.NewRow());
        dt.Rows[0]["Content"] = content;
        return dt;
    }
    public void SendSubEMail(string email,string content)
    {
        MailAddress adMod = new MailAddress(email);
        MailInfo mailInfo = new MailInfo() { ToAddress = adMod, IsBodyHtml = true };
        mailInfo.Subject = Subject_T.Text;
        mailInfo.FromName = SiteConfig.SiteInfo.SiteName;
        mailInfo.MailBody = content;
        SendMail.Send(mailInfo);
    }
}