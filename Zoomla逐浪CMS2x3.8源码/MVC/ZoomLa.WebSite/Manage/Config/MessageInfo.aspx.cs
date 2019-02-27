using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Xml;
using System.Text;
using ZoomLa.Components;
using ZoomLa.Model.Shop;
using ZoomLa.AppCode.cn.woxp.gateway;


namespace ZoomLaCMS.Manage.Config
{
    public partial class MessageInfo : CustomerPageAction
    {
        private string g_uid;//登录账户
        private string g_eid;//企业ID代码
        private string g_pwd; //登录密码.明文
        private string g_gate_id;//使用通道
        private string g_content; //短信内容
        private B_OrderList oll = new B_OrderList();
        M_OrderList modl = new M_OrderList();
        B_ModelField bmf = new B_ModelField();
        B_Admin badmin = new B_Admin();
        //private string g_tyep; //短信类型

        M_OrderList orderinfo;

        protected int oid = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            function.AccessRulo();
            if (!this.IsPostBack)
            {
                WebSMS wsm = new WebSMS();
                g_uid = SiteConfig.SiteOption.G_uid;
                g_eid = SiteConfig.SiteOption.G_eid;
                g_pwd = SiteConfig.SiteOption.G_pwd;
                g_gate_id = SiteConfig.SiteOption.G_gate_id;
                g_content = SiteConfig.SiteOption.G_content;
                this.x_uid.Value = g_uid.ToString();
                this.x_eid.Value = g_eid.ToString();
                this.x_pwd_md5.Value = g_pwd.ToString();
                this.h_gate_id.Value = g_gate_id.ToString();
                this.txtContent.Value = g_content.ToString();
                this.txtType.Value = "";


                string t_sendNos = Request.QueryString["t_sendNo"];
                string conetent = Request.QueryString["s_memo"];
                s_memo.Text = conetent;
                t_sendNo.Text = t_sendNos;

                string strIdentity = wsm.GetIdentityMark(Int32.Parse(g_eid), g_uid, g_pwd, Int32.Parse(g_gate_id));
                if (g_uid == "" || g_eid == "" || g_pwd == "" || g_gate_id == "")
                {
                    Response.Write("参数不全!");
                    return;
                }
            }

            int id = DataConverter.CLng(Request.QueryString["id"]);
            this.oid = id;
            if (oll.FondOrder(id) == true)
            {
                orderinfo = oll.GetOrderListByid(id);
                string OrderNo = orderinfo.OrderNo.ToString();
            }
        }
        //景点
        public void get_jdInfo()
        {
            string phoneNum = orderinfo.MobileNum;
            string messageContent = "";
            senderMesssage(phoneNum, messageContent);
        }
        //酒店
        public void get_HotelInfo()
        {
            string phoneNum = orderinfo.MobileNum;
            string messageContent = "";
            senderMesssage(phoneNum, messageContent);
        }
        //路线
        public void get_lxInfo()
        {
            string phoneNum = orderinfo.MobileNum;
            string messageContent = "";
            senderMesssage(phoneNum, messageContent);
        }
        //租车
        public void get_zclInfo()
        {
            string phoneNum = orderinfo.MobileNum;
            string messageContent = "";
            senderMesssage(phoneNum, messageContent);
        }
        //订餐
        public void get_dcInfo()
        {
            string phoneNum = orderinfo.MobileNum;
            string messageContent = "";
            senderMesssage(phoneNum, messageContent);
        }
        //会员
        public void get_hyInfo()
        {
            string phoneNum = orderinfo.MobileNum;
            string messageContent = "";
            senderMesssage(phoneNum, messageContent);
        }
        //通用短信
        public void senderMesssage(string phoneNum, string messageContent)
        {
            WebSMS wsm = new WebSMS();
            if (wsm == null)
            {
                function.WriteErrMsg("网关初始化失败!", "Default.aspx");
                return;
            }
            string strIdentity = wsm.GetIdentityMark(Int32.Parse(g_eid), g_uid, g_pwd, Int32.Parse(g_gate_id));
            if (strIdentity == null || strIdentity == "")
            {
                function.WriteErrMsg("获取标识串失败!", "Default.aspx");
                return;
            }
            if (phoneNum == "" || messageContent == "")
            {
                function.WriteErrMsg("接收号码或者短信内容没有输入!", "Default.aspx");
                return;
            }
            //快速发送.直接提交到运营商网关
            SendResult status = wsm.FastSend(strIdentity, phoneNum, messageContent, "", "");
            //发送成功
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (this.t_sendNo.Text != null && this.t_sendNo.Text.Trim() != "" && this.s_memo.Text.Trim() != "" && this.s_memo.Text != null)
            {
                SendWebSMS.SendMessage(this.t_sendNo.Text.Trim(), this.s_memo.Text.Trim());
                M_Message messInfo = new M_Message();
                messInfo.Sender = badmin.GetAdminLogin().AdminId.ToString();
                messInfo.Title = "手机信息";
                messInfo.PostDate = DataConverter.CDate(DateTime.Now.ToLocalTime().ToString());
                messInfo.Content = s_memo.Text;
                messInfo.Receipt = "";
                messInfo.MsgType = 2;
                messInfo.status = 1;
                B_Message.Add(messInfo);
                function.WriteSuccessMsg("发送成功!");

            }
        }
    }
}