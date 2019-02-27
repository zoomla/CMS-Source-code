using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.Common;

using System.Net.Mail;
using System.Data;

public partial class Common_EmailSender : System.Web.UI.Page
{
    B_User b_User = new B_User();
    M_UserInfo m_UserInfo = new M_UserInfo();
    B_IServer serverBll = new B_IServer();
    protected void Page_Load(object sender, EventArgs e)
    {

        m_UserInfo = b_User.GetLogin();
        bindSP();
        shopids.Value = Request["shops"];
        string address = SiteConfig.MailConfig.MailServerList;
        string[] s = address.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
        toEmail.Value = s[0];
        SeEmail.Value = m_UserInfo.Email;
        //m_UserInfo = b_User.GetLogin();
        ////是否登录
        //if (m_UserInfo.UserID > 0)
        //{
        //    SendEmail();
        //}
        //else
        //{
        //    //是否开放游客发送权限
        //    if (SiteConfig.SiteOption.MailPermission == "1")
        //    {
        //        SendEmail();
        //    }
        //    else
        //    {
        //        Response.Write(2);
        //        Response.Flush();
        //        Response.Close();
        //    }
        //}
    }
    protected void bindSP()
    {
        string str = "";
      
       if (!string.IsNullOrEmpty(Request["shops"]))
       {
           string shops = Request["shops"];
           string[] arr = shops.Split(new Char[] { '|' });
           for (int i = 0; i < arr.Length; i++)
           {
               if (arr[i] != "")
               {
                   B_Product pro = new B_Product();
                   M_Product model = pro.GetproductByid(Convert.ToInt32(arr[i]));
                   str += "<tr class='odd first'> <th class='inquiry-checkbox'> <input id='ckShop" + i + "' value='" + model.ID + "' checked='true' name='ckShop' type='checkbox' onclick='getval(" + i + "," + model.ID + ")'> </th><td class='inquiryCom'  ><label >" + model.AddUser + "</label></td> <td class='inquiryPro' id=''><img src='" + SiteConfig.SiteInfo.SiteUrl + "/" + model.Thumbnails + "' width='40' height='40'><a href='" + SiteConfig.SiteInfo.SiteUrl + "/shop/" + model.ID + ".aspx' id='" + model.ID + "' target='_blank' class='AE:overshow-images'>" + model.Proname + "</a><p><font color='#999999'>" + model.Proinfo + "</font></p></td></tr>";
               }
           }
           str = "<tr class=\"tdbg\"><td  class=\"tdbgleft\" align=\"right\">产品:</td><td><table  cellpadding=\"3\" cellspacing=\"5\" style=\" text-align:left; padding:5px;\">" + str + "</table> </td></tr>";
       }
       //Session["shopli"] =  str ;
       //string shopids= Request["shopids"];
        
    }

    protected void send_Click(object sender, EventArgs e)
    {
        m_UserInfo = b_User.GetLogin();
        //是否登录
        if (m_UserInfo.UserID > 0)
        {
            SendEmail();
        }
        else
        {
            //是否开放游客发送权限
            if (SiteConfig.SiteOption.MailPermission == "1")
            {
                SendEmail();
            }
            else
            {
                function.Script(this,"ActionSec(-1)");
                //val = "0";
                //showMsg.InnerHtml = "邮件发送失败！系统未开放匿名发送邮件，请登录后再试！";
                //Response.Write("邮件发送失败！系统未开放匿名发送邮件，请登录后再试");
                //Response.Flush();
                //Response.Close();
            }
        }
    } 
    //protected void pro_seld()
    //{
    //    shopids.Value = Request["ckShop"];
    //}
    //protected void iid()
    //{
    //    string shoptd = "";
    //    string shops=shopids.Value;
    //    string[] sps= shops.Split(new Char[]{','});
    //    if (sps.Length > 1)
    //    {
    //        for (int i=0; i < sps.Length; i++)
    //        { 
    //       B_Product pro=new B_Product();
    //       M_Product model = pro.GetproductByid(Convert.ToInt32(sps[i]));
    //       shoptd = "<td>	<span id=\"fixBand4\"><span id=\"fixBand6\"><a href=\"" + model.ID + "\" target=\"_blank\"><img border=\"0\" height=\"70\" id=\"_x0000_i1030\" src=\"" + model.Thumbnails + "\" style=\"border-width: 0px;\" width=\"70\" /></a></span></span> </td>";
    //        }
    //    }
    //    Session["shoptd"] = shoptd;
    //}
    protected void SendEmail()
    {
        string shoptd = "";
        string shops = Request["shopids"];
        string[] sps = shops.Split(new Char[] { '|' });
        if (sps.Length > 1)
        {
            shoptd = "<table>";
            for (int i = 0; i < sps.Length; i++)
            {
                try
                {
                    B_Product pro = new B_Product();
                    M_Product model = pro.GetproductByid(Convert.ToInt32(sps[i]));

                    shoptd += "<tr><td class='inquiryCom'  ><label for='select_" + model.ID + "'>" + model.AddUser + "</label></td> <td class='inquiryPro' id=''><img src='" + SiteConfig.SiteInfo.SiteUrl + "/" + model.Thumbnails + "' width='58' height='58'><a href='" + SiteConfig.SiteInfo.SiteUrl + "/shop/" + model.ID + ".aspx' id='" + model.ID + "' target='_blank' class='pname'>" + model.Proname + "</a><p><font color='#999999'>" + model.Proinfo + "</font></p></td></tr>";
                }
                catch {
                    shoptd += "";
                }
               }
            shoptd += "</table>";
        }
        M_IServer iserver = new M_IServer();
        string strUinfo = "";
        if (m_UserInfo.UserID < 1)
        {
            iserver.UserId = 0;
            iserver.Title = "游客";
            strUinfo = "<table  cellpadding=\"0\" cellspacing=\"0\" style=\"width: 100%;border:1px solid #D9D9D9;\" > <tbody> <tr> <td><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" style=\"width: 100%\"><tr><td style=\"width: 430px\">&nbsp;<a href=\"mailto:" + Request["SeEmail"] + "\">" + Request["SeEmail"] + "</a> &nbsp;&nbsp; [Unverified Email]</td></tr></table> </td> </tr> </tbody> </table>";
        }
        else
        {
            iserver.UserId = m_UserInfo.UserID;
            iserver.Title = m_UserInfo.UserName;
            strUinfo="<table  cellpadding=\"0\" cellspacing=\"0\" style=\"width: 100%;border:1px solid #D9D9D9;\" > <tbody> <tr> <td>"+sendUserInfo()+"</td> </tr> </tbody> </table>";
        }
        string lgourl=SiteConfig.SiteInfo.LogoUrl;
        string [] lurl=lgourl.Split(new char[]{'~'});
        if(lurl.Length>1)
        {
        lgourl=lurl[0];
        }

        string strhead = "<table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" style=\"width: 760px;padding:10px;\"  ><tbody> <tr> <td> <p align=\"left\"> <img height=\"39\" id=\"_x0000_i1025\" src=\"" + SiteConfig.SiteInfo.SiteUrl + "/" + lgourl + "\" width=\"206\" /></p> </td> <td> <p align=\"right\"> The following message was generated before" + DateTime.Now.ToUniversalTime() + "</p> </td> </tr> </tbody> </table>";//邮件头部
        string strTit="	<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\" color:#88889B;\"> <tbody> <tr> <td> <h1>" + Request["title"] + "</h1> </td> </tr> </tbody> </table>";//邮件标题
        string strcon = "<table cellpadding=\"0\" cellspacing=\"0\" style=\" border:1px solid #D9D9D9;\"> <tbody><tr><td>" + Request["con"] + "</td> </tr> </tbody> </table>";//填写内容

        string strs = "<table border=\"0\" cellpadding=\"0\" style=\"width: 100%; background: #efefef\"> <tbody> <tr> <td>" +strhead  + "<div style=\"clear: both\"> &nbsp;</div> <table align=\"center\" cellpadding=\"0\" cellspacing=\"0\" height=\"70\" style=\"width: 700px; background: #fff;padding:30px; border:1px solid #D9D9D9; font-size:12px;\" > <tbody> <tr> <td>"+ strTit + "	</td> </tr>"
						+"<tr> <td > <div align=\"center\"> <hr align=\"center\" size=\"2\" width=\"100%\" /></div>"
								+"<p> Dear Grace Wu,</p>"
								+"<p>I&#39;m interested in your product(s)</p> "
                               + shoptd +"<p align=\"left\"> &nbsp;</p> "
                               + strcon + "<p align=\"left\"> &nbsp;</p> "
                               + strUinfo
                                + "<div style=\"clear: both\"> &nbsp;</div>"
                                + "<p>" + SiteConfig.SiteInfo.SiteName + "shall not be liable for any lost profits or incidental, consequential or other damages arising out of or in connection with this message, our web site content, our services or the activities of any of the users of our web site. </p>"
                              +" </td><tr></tbody> </table></td> </tr> </tbody> </table>";
        iserver.Content = strs;//邮件内容
        iserver.Priority = "";
        iserver.Type = "";
        iserver.SubTime = DateTime.Now;
        iserver.Root = "网页表单";
        iserver.State = "未解决";
        
        if (string.IsNullOrEmpty(iserver.Title) || string.IsNullOrEmpty(iserver.Content))
        {
            function.WriteErrMsg("请输入标题!");
            return;
        }
        else
        {
            int QuestionID = serverBll.AddQuestion(iserver);
            if (QuestionID > 0)
            {
                SendEmailToAdmin(m_UserInfo.UserID, QuestionID);
                function.Script(this, "ActionSec(1)");
                //showMsg.InnerHtml = "发送成功！";
                //Response.Write("发送成功！");
                //Response.Flush();
                //Response.Close();
            }
            else
            {
                function.Script(this, "ActionSec(0)");
               // showMsg.InnerHtml = "发送失败！";
               // Response.Write("发送失败！");
                //Response.Flush();
                //Response.Close();
                return;
            }
        }
    }

    private void SendEmailToAdmin(int id, int QuestionID)
    {
        string address = SiteConfig.MailConfig.MailServerList;
        string[] s = address.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

        for (int i = 0; i < s.Length+1; i++)
        {
            M_IServer iserver = serverBll.SeachById(QuestionID);
           
            MailInfo mailInfo = new MailInfo();
            mailInfo.IsBodyHtml = true;

            mailInfo.MailBody = iserver.Content;//Request["con"];
            mailInfo.Subject = Request["title"];//iserver.Title Request["title"];
            if (i == s.Length)
            {
                mailInfo.ToAddress = new MailAddress(Request["toEmail"]);//new MailAddress(s[i])
            }
            else {
                mailInfo.ToAddress = new MailAddress(s[i]);
            }

            if (SendMail.Send(mailInfo) == SendMail.MailState.Ok)
            {
                //发送成功
            }
        }
    }
    protected string sendUserInfo()
    {

        M_Uinfo m_uinfo = b_User.GetUserBaseByuserid(m_UserInfo.UserID);
        string str = "";
        str +="<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" style=\"width: 100%\">"
	        +"<tbody> <tr> <td style=\"width: 42px\">"
            + "<img border=\"0\" height=\"32\" id=\"_x0000_i1034\" src=\"" + SiteConfig.SiteInfo.SiteUrl+"/" + m_uinfo.UserFace + "\" style=\"border-bottom: #cccccc 1px solid; border-left: #cccccc 1px solid; border-top: #cccccc 1px solid; border-right: #cccccc 1px solid\" width=\"32\" /></td>"
            +"<td style=\"width: 500px\">"
            + "<h4><a href=\""+SiteConfig.SiteInfo.SiteUrl+"/User/Login.aspx\">" + getusex("'"+m_uinfo.UserSex+"'") + "Mr. " + m_UserInfo.UserName + "</a></h4> </td> <td> &nbsp;</td> </tr>"
        +"</tbody> </table> "
        +"<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\"> <tbody>"
            +"<tr> <td> &nbsp;</td> <td> </td> </tr>"
            +"<tr> <td style=\"width: 148px\"> <strong>Company: </strong> </td>"
            + "<td style=\"width: 430px\">&nbsp;" + m_UserInfo.CompanyName +  "</td> </tr>"
            +"<tr> <td> <strong>Country/Region: </strong>  </td>"
            + "<td style=\"width: 430px\">&nbsp;" + m_uinfo.Country + "</td> </tr>"
            +"<tr> <td> <strong>Address: </strong>  </td>"
            + "<td style=\"width: 430px\">&nbsp;" + m_uinfo.Address + "</td> </tr>"
            +"<tr> <td><strong>Email:</strong> </td>"
            + "<td style=\"width: 430px\">&nbsp;<a href=\"mailto:" + Request["SeEmail"] + "\">" + Request["SeEmail"] + "</a> &nbsp;&nbsp;"
            +"</td> </tr>"
            +"<tr><td><strong>TEL: </strong></td>"
            + "<td style=\"width: 430px\">&nbsp;" + m_uinfo.Mobile + "</td>"
            +"</tr>"
            +"<tr> <td> <strong>FAX: </strong>  </td>"
            + "<td style=\"width: 430px\">&nbsp;" + m_uinfo.Fax + "</td></tr>"
        +"</tbody> </table>" ;
        return str;
    }

    protected string getusex(string sex)
    {
        if (sex == "false")
        {
            return "Mrs.";
        }
        else
        {
            return "Mr.";
        }
    }
}