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
namespace ZoomLaCMS.Manage.Shop
{
    public partial class OrderStateManage : CustomerPageAction
    {

        B_OrderList orderBll = new B_OrderList();
        B_User buser = new B_User();

        protected void Page_Load(object sender, EventArgs e)
        {
            int type = DataConverter.CLng(Request.QueryString["type"]);  //类型：0为网站管理员,1为店铺管理员,2为客户
            if (type == 0)
            {
                ZoomLa.Common.function.AccessRulo();
                B_Admin badmin = new B_Admin();

                if (!B_ARoleAuth.Check(ZLEnum.Auth.shop, "OrderList"))
                {
                    function.WriteErrMsg("没有权限进行此项操作");
                }
            }
            else
            {
                B_User.CheckIsLogged();
            }
            int id = DataConverter.CLng(Request.QueryString["id"]);
            hforderid.Value = id.ToString();
            M_OrderList orderinfo = orderBll.GetOrderListByid(id);
            if (!IsPostBack)
            {
                if (orderinfo != null && orderinfo.id > 0)
                {
                    ddstate.SelectedValue = orderinfo.Paymentstatus.ToString();
                    ReadXml(orderinfo.Paymentstatus);
                }
                ViewState["type"] = type;
                if (type == 0 || type == 1)//类型：0为网站管理员,1为店铺管理员,2为客户
                {
                    if (orderinfo.Aside != 1 && orderinfo.Settle != 1)
                    {
                        ddstate.Enabled = true;
                        txtBank.Enabled = true;
                        txtContent.Enabled = true;
                        txtMoney.Enabled = true;
                        txtDate.Enabled = true;
                        txtNumber.Enabled = true;
                        btn.Enabled = true;
                    }
                    else
                    {
                        txtBank.Enabled = false;
                        txtContent.Enabled = false;
                        txtMoney.Enabled = false;
                        txtDate.Enabled = false;
                        txtNumber.Enabled = false;
                        btn.Enabled = false;
                    }
                }
                else
                {
                    ddstate.SelectedValue = "5";
                    ddstate.Enabled = false;
                    txtBank.Enabled = true;
                    txtContent.Enabled = true;
                    txtMoney.Enabled = true;
                    txtDate.Enabled = true;
                    txtNumber.Enabled = true;
                    btn.Enabled = true;
                    duanxin.Visible = false;
                }
                if (ddstate.SelectedValue == "5")
                {
                    lblbank.Text = "开户人：";
                    lblmon.Text = "开户行：";
                    lblC.Text = "银行帐号：";
                    bodybank.Visible = false;
                }
                else
                {
                    lblbank.Text = "银行：";
                    lblmon.Text = "金额：";
                    lblC.Text = "到帐日期：";
                }
                Call.SetBreadCrumb(Master, "<li>商城管理</li><li><a href='UserOrderlist.aspx'>店铺订单</a></li><li>订单状态处理</li>");
            }
        }

        //保存
        protected void btn_Click(object sender, EventArgs e)
        {
            int id = DataConverter.CLng(hforderid.Value);
            M_OrderList orderinfo = orderBll.GetOrderListByid(id);//GetCartProOrderID
            B_CartPro cartproBll = new B_CartPro();
            DataTable dts = cartproBll.GetCartProOrderID(id);
        }
        protected void ddstate_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = DataConverter.CLng(hforderid.Value);
            M_OrderList userorder = orderBll.SelReturnModel(id);
            if (userorder != null && userorder.id > 0)
            {

                if (ddstate.SelectedValue == "4" || ddstate.SelectedValue == "5")
                {
                    if (userorder.OrderStatus != 0)
                    {
                        tips.InnerHtml = "订单未取消，无法退款";
                        ddstate.SelectedValue = userorder.Paymentstatus.ToString();
                    }
                    else
                    {
                        tips.InnerHtml = "";
                    }
                }
                else
                {
                    tips.InnerHtml = "";
                }
            }
            if (ddstate.SelectedValue == "5")
            {
                lblbank.Text = "开户人：";
                lblmon.Text = "开户行：";
                lblC.Text = "银行帐号：";
                bodybank.Visible = false;
            }
            else
            {
                lblbank.Text = "银行：";
                lblmon.Text = "金额：";
                lblC.Text = "到帐日期：";
            }
            ReadXml(DataConverter.CLng(ddstate.SelectedValue));
        }


        //确认支付 同时发送站内短信,email,手机短信
        private string SendMessage(int userid, DateTime orderData, string username, string orderno, int deid, string MobieNum)
        {
            ReadXml(DataConverter.CLng(ddstate.SelectedValue));
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

        private void ReadXml(int type)
        {
   
        }
    }
}