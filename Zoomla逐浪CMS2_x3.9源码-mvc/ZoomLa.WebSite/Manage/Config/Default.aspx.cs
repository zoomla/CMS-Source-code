using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Xml;
using System.Text;
using ZoomLa.Components;
using ZoomLa.Common;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.AppCode.cn.woxp.gateway;
namespace ZoomLaCMS.Manage.Config
{
    public partial class Default : CustomerPageAction
    {
        private string g_uid;//登录账户
        private string g_eid;//企业ID代码
        private string g_pwd; //登录密码.明文
        private string g_gate_id;//使用通道
        private string g_content; //短信内容

        protected void Page_Load(object sender, EventArgs e)
        {
            function.AccessRulo();
            B_Admin badmin = new B_Admin();
            badmin.CheckIsLogin();
            if (!B_ARoleAuth.Check(ZLEnum.Auth.other, "SiteConfig"))
            {
                function.WriteErrMsg("没有权限进行此项操作");
            }
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

                string strIdentity = wsm.GetIdentityMark(Int32.Parse(g_eid), g_uid, g_pwd, Int32.Parse(g_gate_id));
                this.Label1.Text = "当前余额:" + wsm.GetMoney(strIdentity).ToString("0.00");
                if (g_uid == "" || g_eid == "" || g_pwd == "" || g_gate_id == "")
                {
                    Response.Write("参数不全!");
                    return;
                }
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            WebSMS wsm = new WebSMS();
            g_uid = SiteConfig.SiteOption.G_uid;
            g_eid = SiteConfig.SiteOption.G_eid;
            g_pwd = SiteConfig.SiteOption.G_pwd;
            g_gate_id = SiteConfig.SiteOption.G_gate_id;
            this.x_uid.Value = g_uid.ToString();
            this.x_eid.Value = g_eid.ToString();
            this.x_pwd_md5.Value = g_pwd.ToString();
            this.h_gate_id.Value = g_gate_id.ToString();
            if (wsm == null)
            {
                function.WriteErrMsg("网关初始化失败!");
                return;
            }
            string strIdentity = wsm.GetIdentityMark(Int32.Parse(g_eid), g_uid, g_pwd, Int32.Parse(g_gate_id));
            if (strIdentity == null || strIdentity == "")
            {
                function.WriteErrMsg("获取标识串失败!");
                return;
            }
            if (this.t_sendNo.Text.ToString().Trim() == "" || this.t_sendMemo.Text.ToString().Trim() == "")
            {
                function.WriteErrMsg("接收号码或者短信内容没有输入!");
                return;
            }
            if (this.t_sendTime.Text.ToString().Trim() != "")
            {
                DateTime dt;
                if (!DateTime.TryParse(this.t_sendTime.Text.ToString().Trim(), out dt))
                {
                    function.WriteErrMsg("定时格式错误!");
                    return;
                }
                if (dt <= DateTime.Now)
                {
                    function.WriteErrMsg("定时时间必须大于当前时间!");
                    return;
                }
            }
            //快速发送.直接提交到运营商网关
            SendResult status = wsm.FastSend(strIdentity, this.t_sendNo.Text.ToString().Trim(), this.t_sendMemo.Text.ToString().Trim(), this.t_sendTime.Text.ToString().Trim(), "");
            //发送成功
            string js = "";
            if (status.RetCode > 0)
            {
                js = "发送成功,共发送:" + status.RetCode.ToString() + "条";
                this.Label1.Text = "当前余额:" + wsm.GetMoney(strIdentity).ToString("0.00");

            }
            else //发送失败
            {
                js = "发送失败,错误代码:" + status.RetCode.ToString() + ",原因:" + status.ErrorDesc;
            }
            js = js.Replace("\"", "\\\"");
            js = js.Replace("\'", "\\\'");
            // Response.Write("<script>alert('" + js + "');window.location='MessageInfo.aspx';</script>");
        }
        /// <summary>
        /// 收到的XML转成dataset型
        /// </summary>
        /// <param name="xmlData"></param>
        /// <returns></returns>
        public DataSet ConvertXMLToDataSet(string xmlData)
        {
            StringReader stream = null;
            XmlTextReader reader = null;
            try
            {
                DataSet xmlDS = new DataSet();
                stream = new StringReader(xmlData);
                reader = new XmlTextReader(stream);
                xmlDS.ReadXml(reader);
                return xmlDS;
            }
            catch (Exception ex)
            {
                string strTest = ex.Message;
                return null;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }
        //从服务上取短信.功能被放弃。接收短信修改PUSH下发给客户。
        private void ReadSMS()
        {
            WebSMS wsm = new WebSMS();
            g_uid = SiteConfig.SiteOption.G_uid;
            g_eid = SiteConfig.SiteOption.G_eid;
            g_pwd = SiteConfig.SiteOption.G_pwd;
            g_gate_id = SiteConfig.SiteOption.G_gate_id;

            this.x_uid.Value = g_uid.ToString();
            this.x_eid.Value = g_eid.ToString();
            this.x_pwd_md5.Value = g_pwd.ToString();
            this.h_gate_id.Value = g_gate_id.ToString();
            if (wsm == null)
            {
                function.WriteErrMsg("网关初始化失败!");
                return;
            }
            string strIdentity = wsm.GetIdentityMark(Int32.Parse(g_eid), g_uid, g_pwd, Int32.Parse(g_gate_id));
            if (strIdentity == null || strIdentity == "")
            {
                function.WriteErrMsg("获取标识串失败!");
                return;
            }
            try
            {
                string readxml = wsm.ReadXml(strIdentity);
                //如果是数字代表返回失败
                int code = 0;
                if (Int32.TryParse(readxml, out code))
                {
                    this.div_sms.InnerText = "接收失败,错误代码:" + code.ToString().Trim() + ",原因:" + wsm.GetErrorHint(code);
                    return;
                }
                if (readxml == null || readxml.ToString().Trim() == "")
                {
                    this.div_sms.InnerText = "没有收到短信回复!";
                    this.EGV.DataSource = null;
                    this.EGV.DataBind();
                    return;
                }
                this.div_sms.Visible = false;
                DataSet ds = ConvertXMLToDataSet(readxml);
                this.EGV.DataSource = ds.Tables[0];
                this.EGV.DataBind();
            }
            catch (Exception ex)
            {
                this.div_sms.InnerText = "接收异常:原因" + ex.Message.ToString();
            }
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            ReadSMS();
        }
        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }
    }
}