using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
// 添加命名空间
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Net.Sockets;
using System.IO;
using jmail;
using System.Text;
using System.Data;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.SQLDAL;
using ZoomLa.Common;
using System.Data.SqlClient;

public partial class MIS_Target_mailQuote : System.Web.UI.Page
{
    protected SmtpClient smtpClient;
    protected TcpClient tcpClient;
    protected NetworkStream networkStream;
    protected StreamReader streamReader;
    protected StreamWriter streamWriter;

    // 定义接收邮件对象
    protected POP3Class popClient;

    // 定义邮件信息接口
    protected jmail.Message messageMail;

    // 定义邮件附件集合接口
    protected Attachments attachments;

    // 定义邮件附件接口
    protected jmail.Attachment attachment;
    B_MailSet bll = new B_MailSet();
    M_MailSet model = new M_MailSet();
    B_User buser = new B_User();
    B_MailInfo bminfo = new B_MailInfo();
    int Eid = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        buser.CheckIsLogin("/Mis/Target/");
        Eid = DataConvert.CLng(Request["id"]);
        M_UserInfo info = buser.GetLogin();
        if (!IsPostBack)
        {
            DataTable dt = bminfo.SelByUid(info.UserID);
            tbxMailboxInfo.Text = dt.Rows.Count.ToString();
            Page_list(dt);

        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string PlanID = this.ProID.Value;
        string ids = string.Empty;
        if (!string.IsNullOrEmpty(Request["types"]) && Request["types"] == "8")
        {
            if (!string.IsNullOrEmpty(Request["ID"]))
            {
                ids = Request["id"].ToString();
            }
        }
    }
    #region 通用分页过程
    /// <summary>
    /// 通用分页过程　by h.
    /// </summary>
    /// <param name="Cll"></param>
    public void Page_list(DataTable Cll)
    {
        RPT.DataSource = Cll;
        RPT.DataBind();
    }
    #endregion

}