using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Components;
using MSXML2;
using System.Net.Mail;

using System.Xml;
using System.Data;

public partial class manage_Shop_Ordertic : CustomerPageAction
{
    private B_OrderList oll = new B_OrderList();
    private B_User buser = new B_User();
    private B_CartPro cartproBll = new B_CartPro();
    private B_Product pll = new B_Product();
    private B_Stock Sll = new B_Stock();

    protected void Page_Load(object sender, EventArgs e)
    {
        int type = DataConverter.CLng(Request.QueryString["type"]);  //类型：0为网站管理员,1为店铺管理员,2为客户
        if (type == 0)
        {
            B_Admin badmin = new B_Admin();
            if (!B_ARoleAuth.Check(ZLEnum.Auth.shop, "OrderList"))
            {
                function.WriteErrMsg("没有权限进行此项操作");
            }
        }
        else
        {
            buser.CheckIsLogin();
        }
        int id = DataConverter.CLng(Request.QueryString["id"]);
        hforderid.Value = id.ToString();
        M_OrderList orderinfo = oll.GetOrderListByid(id);
        if (!IsPostBack)
        {
            if (orderinfo != null && orderinfo.id > 0)
            {
                ddstate.SelectedValue = orderinfo.Developedvotes.ToString();
                if (ddstate.SelectedValue == "1")
                {
                    fahuo.Visible = true;
                }
                else
                {
                    fahuo.Visible = false;
                }
            }
            //类型：0为网站管理员,1为店铺管理员,2为客户
            if (type == 0||type == 1)
            {
                if (orderinfo.Aside != 1 && orderinfo.Settle != 1)
                {
                    ddstate.Enabled = true;
                    txtContent.Enabled = true;
                    btn.Enabled = true;
                }
                else
                {
                    ddstate.Enabled = false;
                    txtContent.Enabled = false;
                    btn.Enabled = false;
                }
            }
            else
            {
                ddstate.Enabled = false;
                txtContent.Enabled = false;
                btn.Enabled = false;
            }
        }
        Call.SetBreadCrumb(Master, "<li>商城管理</li><li><a href='UserOrderlist.aspx'>店铺订单</a></li><li>发票信息处理</li>");
    }

    //保存
    protected void btn_Click(object sender, EventArgs e)
    {
    }
    protected void ddstate_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddstate.SelectedValue == "1")
        {
            fahuo.Visible = true;
        }
        else
        {
            fahuo.Visible = false;
        }
    }


    //确认支付 同时发送站内短信,email,手机短信
    private string SendMessage(int userid, DateTime orderData, string username, string orderno, int deid, string mobilenum)
    {
        string message = "";
        M_UserInfo info = buser.GetUserByUserID(userid);
        if (info != null && info.UserID > 0)
        {
            //Email发送
            MailInfo mailInfo = new MailInfo();
            mailInfo.IsBodyHtml = true;
            mailInfo.FromName = SiteConfig.SiteInfo.SiteName;
            MailAddress address = new MailAddress(info.Email);
            mailInfo.ToAddress = address;

            string EmailContent = txtContent.Text.ToString();
        }
        return message;
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
    private string MobileMess(string mob, string msg)
    {

        string req = this.SendMsg(SiteConfig.SiteOption.MssUser, SiteConfig.SiteOption.MssPsw, mob, msg);
        string[] reqs = req.Split(new char[] { '/' });
        string res = "";
        switch (reqs[0])
        {
            case "000":
                res = "成送成功！<br/>";
                res += "发送条数:" + reqs[1].Split(new char[] { ':' })[1] + "<br/>";
                res += "当次消费金额" + reqs[2].Split(new char[] { ':' })[1] + "<br/>";
                res += "总体余额:" + reqs[3].Split(new char[] { ':' })[1] + "<br/>";
                res += "短信编号:" + reqs[4];
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
        return res;
    }

}