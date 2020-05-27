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
using ZoomLa.Web;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Components;
using MSXML2;
namespace ZoomLa.Manage.User
{
    public partial class MobileMsg : CustomerPageAction
    {
        B_Admin badmin = new B_Admin();
        B_User buser = new B_User();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!B_ARoleAuth.Check(ZoomLa.Model.ZLEnum.Auth.user, "MessManage"))
                {
                    function.WriteErrMsg("没有权限进行此项操作");
                }
                string uid = SiteConfig.SiteOption.MssUser;
                string psw = SiteConfig.SiteOption.MssPsw;
                if (string.IsNullOrEmpty(uid) || string.IsNullOrEmpty(psw))
                    this.LblMobile.Text = "没有设置网站的短信通账号和密码";
                else
                {
                    string balance = SendWebSMS.GetBalance();
                    string res = "";
                    switch (balance)
                    {
                        case "-01":
                            res = "短信API账号余额不足！";
                            break;
                        case "-02":
                            res = "短信API用户ID错误！";
                            break;
                        case "-03":
                            res = "短信API密码错误！";
                            break;
                        case "-04":
                            res = "短信API配置参数不够或参数内容的类型错误！";
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
                        default:
                            res = balance;
                            break;
                    }
                    if (DataConverter.CDouble(balance) > 0)
                    {
                        this.LblMobile.Text = DataConverter.CDouble(balance).ToString();
                        this.BtnSend.Enabled = true;
                    }
                    else
                    {
                        this.LblMobile.Text = res;
                        this.BtnSend.Enabled = false;
                    }
                }
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='UserManage.aspx'>用户管理</a></li><li><a href='MessageSend.aspx'>信息发送</a></li><li>手机短信</li>" + Call.GetHelp(109));
        }
        ///// <summary>
        ///// 获取余额接口
        ///// </summary>
        ///// <param name="uid"></param>
        ///// <param name="pwd"></param>
        ///// <returns></returns>
        //private string GetBalance(string uid, string pwd)
        //{
        //    string Send_URL = "http://service.winic.org/webservice/public/remoney.asp?uid=" + uid + "&pwd=" + pwd + "";
        //    MSXML2.XMLHTTP xmlhttp = new MSXML2.XMLHTTP();
        //    xmlhttp.open("GET", Send_URL, false, null, null);
        //    xmlhttp.send("");
        //    MSXML2.XMLDocument dom = new XMLDocument();
        //    Byte[] b = (Byte[])xmlhttp.responseBody;
        //    //string Flag = System.Text.ASCIIEncoding.UTF8.GetString(b, 0, b.Length);
        //    string andy = System.Text.Encoding.GetEncoding("GB2312").GetString(b).Trim();
        //    return andy;
        //}
        ///// <summary>
        ///// 发送短信调用接口
        ///// </summary>
        ///// <param name="uid"></param>
        ///// <param name="pwd"></param>
        ///// <param name="mob"></param>
        ///// <param name="msg"></param>
        ///// <returns></returns>
        //private string SendMsg(string uid, string pwd, string mob, string msg)
        //{
        //    string Send_URL = "http://service.winic.org/sys_port/gateway/?id=" + uid + "&pwd=" + pwd + "&to=" + mob + "&content=" + msg + "&time=";
        //    MSXML2.XMLHTTP xmlhttp = new MSXML2.XMLHTTP();
        //    xmlhttp.open("GET", Send_URL, false, null, null);
        //    xmlhttp.send("");
        //    MSXML2.XMLDocument dom = new XMLDocument();
        //    Byte[] b = (Byte[])xmlhttp.responseBody;
        //    //string Flag = System.Text.ASCIIEncoding.UTF8.GetString(b, 0, b.Length);
        //    string andy = System.Text.Encoding.GetEncoding("GB2312").GetString(b).Trim();
        //    return andy;
        //}
        protected void BtnSend_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string msg = Content_T.Text.Trim();
                if (string.IsNullOrEmpty(msg))
                {
                    function.WriteErrMsg("短信内容不能为空");
                }
                if (DataSecurity.Len(msg) > 70)
                {
                    function.WriteErrMsg("短信内容不能超过70个字");
                }
                string[] UserIds = InceptUser_Hid.Value.Trim(',').Split(',');
                if (UserIds.Length <= 0) { function.WriteErrMsg("请选择用户"); }
                if (UserIds.Length > 100) { function.WriteErrMsg("每次最多只能发送100条短信!"); }
                string mobs = "", ids = "";
                //获取用户信息中的手机号
                foreach (string uid in UserIds)
                {
                    M_Uinfo mu = buser.GetUserBaseByuserid(DataConverter.CLng(uid));
                    if (mu.IsNull) { continue; }
                    if (!string.IsNullOrEmpty(mu.Mobile))
                    {
                        mobs += mu.Mobile + ",";
                        ids += mu.UserId + ",";
                    }
                }
                mobs = mobs.Trim(',');
                ids = ids.Trim(',');
                if (string.IsNullOrEmpty(ids)) { function.WriteErrMsg("没有要发送的号码"); }
                string req = SendWebSMS.SendMessage(mobs, msg);
                string[] reqs = req.Split(new char[] { '/' });
                string res = "发送成功";
                switch (reqs[0])
                {
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
                M_Message messInfo = new M_Message();
                messInfo.Sender = badmin.GetAdminLogin().AdminId.ToString();
                messInfo.Title = "手机信息";
                messInfo.PostDate = DataConverter.CDate(DateTime.Now.ToLocalTime().ToString());
                messInfo.Content = Content_T.Text;
                messInfo.Receipt = "";
                messInfo.MsgType = 2;
                messInfo.status = 1;
                messInfo.Incept = ids;
                B_Message.Add(messInfo);
                if (res.Equals("发送成功")) { function.WriteSuccessMsg(res, "Message.aspx"); }
                else { function.WriteErrMsg(res); }
            }
        }
        protected void BtnReset_Click(object sender, EventArgs e)
        {
            InceptUser_T.Text = "";
            InceptUser_Hid.Value = "";
            Content_T.Text = "";
        }
    }
}