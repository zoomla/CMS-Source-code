using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZoomLa.BLL.Plat;
using ZoomLa.Model.Plat;
using ZoomLa.BLL;
using ZoomLa.Components.Mail;
using ZoomLa.Common;
using System.IO;

public partial class Plat_Mail_NetMessageDail : System.Web.UI.Page
{
    B_Plat_Mail mailBll = new B_Plat_Mail();
    public int MainID 
    {
        get 
        {
            return DataConverter.CLng(Request.QueryString["ID"]);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    void MyBind()
    {
        M_Plat_Mail mailMod = mailBll.SelReturnModel(MainID);
        LblSender.Text = "<a href='MailWrite.aspx?remail=" + mailMod.Sender + "'>" + mailMod.Sender + "</a>";
        LblIncept.Text = "<a href='MailWrite.aspx?remail=" + mailMod.Receiver + "'>" + mailMod.Receiver + "</a>";
        LblTitle.Text = mailMod.Title;
        txt_Content.Text = mailMod.Content;
        B_User buser = new B_User();
        LblSendTime.Text = mailMod.CDate.ToString();
        Attach_Hid.Value = mailMod.Attach;
        //string vpath = "";
        //foreach (string path in mailMod.Attach.Split('|'))
        //{
        //    if (string.IsNullOrEmpty(path)) continue;
        //    vpath += function.PToV(path) + "|";
        //}
        //Attach_Hid.Value = vpath.Trim('|');
    }
    protected void BtnReply_Click(object sender, EventArgs e)
    {
        M_Plat_Mail mailMod = mailBll.SelReturnModel(MainID);
        Response.Redirect("MailWrite.aspx?reply=" + mailMod.ID);
    }
}