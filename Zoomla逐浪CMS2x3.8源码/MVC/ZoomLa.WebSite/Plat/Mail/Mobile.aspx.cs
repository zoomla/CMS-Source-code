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

namespace ZoomLaCMS.Plat.Mail
{
    public partial class Mobile : System.Web.UI.Page
    {
        B_User buser = new B_User();
        protected void Page_Load(object sender, EventArgs e)
        {
            function.WriteErrMsg("账号余额不足！！");
            if (InBlackList(buser.GetLogin().UserID.ToString()))
            {
                function.WriteErrMsg("您没有权限发送短信，请与管理员联系。");
                BtnSend.Enabled = false;
            }
            if (!Page.IsPostBack)
            {

                if (buser.GetLogin().UserID == 0)
                {
                    function.WriteErrMsg("没有权限进行此项操作");
                }
                if (Request["Mb"] != null && Request["Mb"] != "")
                {
                    TxtInceptUser.Text = Request["Mb"].ToString();
                }
                if (Request["txt"] != null && Request["txt"] != "")
                {
                    EditorContent.Text = Request["txt"];
                }
                string uid = SiteConfig.SiteOption.MssUser;
                string psw = SiteConfig.SiteOption.MssPsw;
                if (string.IsNullOrEmpty(uid) || string.IsNullOrEmpty(psw))
                    this.LblMobile.Text = "没有设置网站的短信通账号和密码";
                else
                {
                    string balance = GetBalance(uid, psw);
                    string res = "";
                    switch (balance)
                    {
                        case "-01":
                            res = "当前账号余额不足！";
                            break;
                        case "-02":
                            //res = "当前用户ID错误！";
                            res = "警告：当前系统未开放此功能,请联系站长!<br/><br/>";
                            break;
                        case "-03":
                            res = "当前密码错误！";
                            break;
                        case "-04":
                            res = "参数不够或参数内容的类型错误！";
                            break;
                        case "-05":
                            res = "手机号码格式不对！";
                            break;
                        case "-06":
                            res = "短信内容编码不对！";
                            break;
                        case "-07":
                            res = "短信内容含有敏感字符！";
                            break;
                        case "-8":
                            res = "无接收数据";
                            break;
                        case "-09":
                            res = "系统维护中..";
                            break;
                        case "-10":
                            res = "手机号码数量超长！（100个/次 超100个请自行做循环）";
                            break;
                        case "-11":
                            res = "短信内容超长！（70个字符）";
                            break;
                        case "-12":
                            res = "其它错误！";
                            break;
                    }
                    if (DataConverter.CDouble(balance) > 0)
                    {
                        this.LblMobile.Text = "";
                    }
                    else
                    {
                        this.LblMobile.Text = res;
                        BtnSend.Text = "余额不足";
                        BtnSend.Attributes.Add("disabled", "disabled");
                    }
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
            //string Flag = System.Text.ASCIIEncoding.UTF8.GetString(b, 0, b.Length);
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
            //return "000";
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
        protected void BtnSend_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string str = Islimited();
                if (str == "")
                {
                    string mob = this.TxtInceptUser.Text.Trim();
                    if (string.IsNullOrEmpty(mob))
                    {
                        function.WriteErrMsg("请输入手机号码");
                    }
                    string msg = this.EditorContent.Text.Trim();
                    if (string.IsNullOrEmpty(msg))
                    {
                        function.WriteErrMsg("短信内容不能为空");
                    }
                    if (DataSecurity.Len(msg) > 70)
                    {
                        function.WriteErrMsg("短信内容不能超过70个字");
                    }
                    string[] mobarr = mob.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    if (mobarr.Length == 0)
                    {
                        function.WriteErrMsg("请输入手机号码");
                    }
                    else
                    {
                        //if (mobarr.Length > 100)
                        //{
                        //    function.WriteErrMsg("每次只能发送100条短信");
                        //}
                        if (mobarr.Length > 1)
                        {
                            function.WriteErrMsg("每次只能发送一条短信");
                        }
                        else
                        {
                            string req = this.SendMsg(SiteConfig.SiteOption.MssUser, SiteConfig.SiteOption.MssPsw, mob, msg);
                            string[] reqs = req.Split(new char[] { '/' });
                            string res = "";
                            switch (reqs[0])
                            {
                                case "000":
                                    res = "发送成功！";
                                    addMessage();
                                    //res += "发送条数:" + reqs[1].Split(new char[] { ':' })[1] + "<br/>";
                                    //res += "当次消费金额" + reqs[2].Split(new char[] { ':' })[1] + "<br/>";
                                    //res += "总体余额:" + reqs[3].Split(new char[] { ':' })[1] + "<br/>";
                                    //res += "短信编号:" + reqs[4];
                                    break;
                                case "-01":
                                    res = "当前账号余额不足！";
                                    break;
                                case "-02":
                                    res = "当前用户ID错误！";
                                    break;
                                case "-03":
                                    res = "当前密码错误！";
                                    break;
                                case "-04":
                                    res = "参数不够或参数内容的类型错误！";
                                    break;
                                case "-05":
                                    res = "手机号码格式不对！";
                                    break;
                                case "-06":
                                    res = "短信内容编码不对！";
                                    break;
                                case "-07":
                                    res = "短信内容含有敏感字符！";
                                    break;
                                case "-8":
                                    res = "无接收数据";
                                    break;
                                case "-09":
                                    res = "系统维护中..";
                                    break;
                                case "-10":
                                    res = "手机号码数量超长！（100个/次 超100个请自行做循环）";
                                    break;
                                case "-11":
                                    res = "短信内容超长！（70个字符）";
                                    break;
                                case "-12":
                                    res = "其它错误！";
                                    break;
                            }
                            Response.Write("<script>alert('" + res + "')</script>");
                        }
                    }
                }
                else
                {
                    Response.Write(str);
                }
            }
        }
        protected void BtnReset_Click(object sender, EventArgs e)
        {
            this.TxtInceptUser.Text = "";
            this.EditorContent.Text = "";
        }
        protected string Islimited()
        {
            string str = "";
            if (HttpContext.Current.Request.Cookies["postTime"] == null)
            {
                HttpContext.Current.Response.Cookies["postTime"].Expires = DateTime.Now.AddMinutes(1);
            }
            else
            {
                str = "<script>alert('一分钟内只能发送一条信息，请稍后！');</script>";
            }
            return str;

        }
        protected void addMessage()
        {
            M_Message messInfo = new M_Message();
            B_User bll = new B_User();
            messInfo.Sender = buser.GetLogin().UserName;
            messInfo.Title = "[" + this.TxtInceptUser.Text.Trim() + "]的手机短信";
            messInfo.PostDate = DataConverter.CDate(DateTime.Now.ToShortDateString());
            messInfo.Content = this.EditorContent.Text.Trim();
            messInfo.Receipt = "";
            messInfo.MsgType = 2;
            messInfo.Incept = buser.GetIdsByUserName(TxtInceptUser.Text.Trim());
            B_Message.Add(messInfo);
        }
        /// <summary>
        /// 判断是否在黑名单中,在则返回true,不在则返回false
        /// </summary>
        protected bool InBlackList(string UserCode)
        {
            return false;
            //bool flag = false;
            ////初始化XML文档操作类
            //XmlDocument myDoc = new XmlDocument();
            ////加载XML文件
            //myDoc.Load(Server.MapPath(FileName));

            ////搜索指定某列，一般是主键列
            //XmlNodeList myNode = myDoc.SelectNodes("//UserCode");

            ////判断是否有这个节点

            //if (!(myNode == null))
            //{
            //    //遍历节点，找到符合条件的元素

            //    foreach (XmlNode xn in myNode)
            //    {
            //        if (!flag&&xn.InnerXml == UserCode)
            //        {
            //            //删除元素的父节点
            //            //xn.ParentNode.ParentNode.RemoveChild(xn.ParentNode);
            //            flag = true;
            //        }
            //    }
            //    //保存
            //    //myDoc.Save(FileName);
            //}
            //return flag;
        }
    }
}