using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using System.Data;
using System.Threading;
using System.Net.Mail;

using MSXML2;
namespace ZoomLaCMS.Manage.User
{
    public partial class UserDay : CustomerPageAction
    {
        protected B_UserDay dll = new B_UserDay();
        protected B_User ull = new B_User();
        protected string selectday = "";
        protected string Sendtotxt = "";
        protected DateTime selectdate = DateTime.Now;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.SendDate.Text = DateTime.Now.ToShortDateString();
            this.Label2.Text = SiteConfig.SiteOption.SendNum.ToString() + " 次";
            string uid = SiteConfig.SiteOption.MssUser;
            string psw = SiteConfig.SiteOption.MssPsw;

            if (string.IsNullOrEmpty(uid) || string.IsNullOrEmpty(psw))
            {
                //this.LblMobile.Text = "没有设置网站的短信通账号和密码";
                this.Label1.Text = "";
            }
            else
            {
                string balance = GetBalance(uid, psw);//余额查询
                if (DataConverter.CDouble(balance) > 0)
                {
                    this.Label1.Text = DataConverter.CDouble(balance).ToString() + " 元";
                }
                else
                {
                    this.Label1.Text = "0 元";
                }
            }

            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>" + Resources.L.工作台 + "</a></li><li><a href='UserManage.aspx'>" + Resources.L.用户管理 + "</a></li><li><a href='UserManage.aspx'>" + Resources.L.会员管理 + "</a></li><li>" + Resources.L.手机节日提醒 + "</li>");

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //选择时间
            this.selectdate = DataConverter.CDate(SendDate.Text);
            //发送时间
            for (int ii = 0; ii < this.SendDay.Items.Count; ii++)
            {
                if (this.SendDay.Items[ii].Selected)
                {
                    this.selectday = selectday + this.SendDay.Items[ii].Value;
                }
                if (ii < this.SendDay.Items.Count - 1)
                {
                    this.selectday = selectday + ",";
                }
            }
            //发送目标
            for (int ii = 0; ii < this.Sendto.Items.Count; ii++)
            {
                if (this.Sendto.Items[ii].Selected)
                {
                    this.Sendtotxt = Sendtotxt + this.Sendto.Items[ii].Value;
                }
                if (ii < this.Sendto.Items.Count - 1)
                {
                    this.Sendtotxt = Sendtotxt + ",";
                }
            }
            //Work();
            Thread readread = new System.Threading.Thread(new System.Threading.ThreadStart(this.Work));
            readread.IsBackground = true;
            readread.Start();
            function.WriteSuccessMsg(Resources.L.操作成功 + "!", "UserDay.aspx");
        }

        /// <summary>
        /// 线程工作
        /// </summary>
        protected void Work()
        {
            DataTable allinfo = dll.Select_All(this.selectdate, selectday);
            if (allinfo != null)
                for (int i = 0; i < allinfo.Rows.Count; i++)
                {
                    int id = DataConverter.CLng(allinfo.Rows[i]["id"].ToString());
                    int userID = DataConverter.CLng(allinfo.Rows[i]["D_UserID"].ToString());
                    M_UserInfo uinfo = ull.GetUserByUserID(userID);


                    if (uinfo.UserCreit >= 70)
                    {
                        M_UserDay dayinfo = dll.GetSelect(id);
                        if (Sendtotxt != "")
                        {
                            if (Sendtotxt.IndexOf(',') > -1)
                            {
                                string[] sendarr = Sendtotxt.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);


                            }
                            //发送邮件
                            if (uinfo.Email != null)
                            {
                                string mailaddstr = uinfo.Email;//邮件地址

                                MailInfo minfo = Getmail(allinfo.Rows[i]["D_name"].ToString(), SiteConfig.SiteInfo.SiteName + "\n\r" + SiteConfig.SiteInfo.SiteUrl + Resources.L.提醒您 + "：\n\r" + allinfo.Rows[i]["D_Content"].ToString() + "\n\r " + Resources.L.网站自动发送请勿回复);

                                if (DataValidator.IsEmail(uinfo.Email))
                                {
                                    minfo.ToAddress = new MailAddress(uinfo.Email, "");

                                    if (SendMail.Send(minfo) == SendMail.MailState.Ok)
                                    {
                                        //发送成功
                                        dayinfo.D_mail = 1;
                                        //Response.Write("邮件发送成功！");
                                    }
                                    else
                                    {
                                        dayinfo.D_mail = 0;
                                        //Response.Write("邮件发送失败！");
                                        //发送失败
                                    }
                                }
                            }


                            //发送手机短信
                            M_Uinfo ubaseinfo = ull.GetUserBaseByuserid(userID);
                            string UserMobile = ubaseinfo.Mobile;

                            if (UserMobile.Length == 11)
                            {
                                string uid = SiteConfig.SiteOption.MssUser;
                                string psw = SiteConfig.SiteOption.MssPsw;

                                if (string.IsNullOrEmpty(uid) || string.IsNullOrEmpty(psw))
                                {
                                    //this.LblMobile.Text = "没有设置网站的短信通账号和密码";
                                }
                                else
                                {
                                    string balance = GetBalance(uid, psw);//余额查询
                                    if (DataConverter.CDouble(balance) > 0)
                                    {
                                        string sendtxt = SiteConfig.SiteInfo.SiteName + SiteConfig.SiteInfo.SiteUrl + Resources.L.提醒您 + "：" + dayinfo.D_Content + Resources.L.网站自动发送请勿回复;
                                        string req = this.SendMsg(SiteConfig.SiteOption.MssUser, SiteConfig.SiteOption.MssPsw, UserMobile, sendtxt);

                                        string[] reqs = req.Split(new char[] { '/' });


                                    }
                                }
                            }
                        }
                        dayinfo.D_SendNum += 1;
                        dll.GetUpdate(dayinfo);
                    }
                }
        }

        /// <summary>
        /// 获取余额接口
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        private string GetBalance(string uid, string pwd)
        {
            string Send_URL = "http://service.winic.org/webservice/public/remoney.asp?uid=" + uid + "&pwd=" + pwd + "";
            MSXML2.XMLHTTP xmlhttp = new MSXML2.XMLHTTP();
            xmlhttp.open("GET", Send_URL, false, null, null);
            xmlhttp.send("");
            MSXML2.XMLDocument dom = new XMLDocument();
            Byte[] b = (Byte[])xmlhttp.responseBody;
            string andy = System.Text.Encoding.GetEncoding("GB2312").GetString(b).Trim();
            return andy;
        }

        /// <summary>
        /// 发送短信调用接口
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="pwd"></param>
        /// <param name="mob"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        private string SendMsg(string uid, string pwd, string mob, string msg)
        {
            //string Send_URL = "http://service.winic.org/sys_port/gateway/?id=" + uid + "&pwd=" + pwd + "&to=" + mob + "&content=" + msg + "&time=";
            //MSXML2.XMLHTTP xmlhttp = new MSXML2.XMLHTTP();
            //xmlhttp.open("GET", Send_URL, false, null, null);
            //xmlhttp.send("");
            //MSXML2.XMLDocument dom = new XMLDocument();
            //Byte[] b = (Byte[])xmlhttp.responseBody;
            ////string Flag = System.Text.ASCIIEncoding.UTF8.GetString(b, 0, b.Length);
            //string andy = System.Text.Encoding.GetEncoding("GB2312").GetString(b).Trim();
            //return andy;
            return "";
        }

        private MailInfo Getmail(string tit, string con)
        {
            MailInfo info = new MailInfo();
            info.Subject = tit;
            info.MailBody = con;
            info.ReplyTo = new MailAddress(SiteConfig.SiteInfo.WebmasterEmail);
            info.FromName = SiteConfig.SiteInfo.SiteName;
            info.Priority = MailPriority.Normal;
            info.IsBodyHtml = true;
            return info;
        }
    }
}