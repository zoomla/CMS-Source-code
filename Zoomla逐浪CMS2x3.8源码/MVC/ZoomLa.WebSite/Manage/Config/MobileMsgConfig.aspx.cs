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
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Components;
using System.IO;
using System.Xml;
using System.Text;
using System.Net;
using ZoomLa.Model;

namespace ZoomLaCMS.Manage.Config
{
    public partial class MobileMsgConfig : CustomerPageAction
    {
        protected B_Admin badmin = new B_Admin();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!B_ARoleAuth.Check(ZLEnum.Auth.other, "SiteConfig"))
            {
                function.WriteErrMsg("没有权限进行此项操作");
            }
            if (!IsPostBack)
            {
                TxtMssUser.Attributes.Add("value", SiteConfig.SiteOption.MssUser);
                TxtMssPsw.Attributes.Add("value", SiteConfig.SiteOption.MssPsw);

                txtg_eid.Attributes.Add("value", SiteConfig.SiteOption.G_eid);
                txtg_uid.Attributes.Add("value", SiteConfig.SiteOption.G_uid);
                txtg_pwd.Attributes.Add("value", SiteConfig.SiteOption.G_pwd);
                txt_h_gate_id.Attributes.Add("value", SiteConfig.SiteOption.G_gate_id);
                userMobilAuth.SelectedValue = SiteConfig.UserConfig.UserMobilAuth;
                smskeyT.Text = SiteConfig.SiteOption.sms_key;
                smspwdT.Attributes.Add("value", SiteConfig.SiteOption.sms_pwd);
                blackList.Text = SiteConfig.SiteOption.G_blackList;
                MaxIpMsg.Text = SiteConfig.SiteOption.MaxIpMsg.ToString();
                MaxPhoneMsg.Text = SiteConfig.SiteOption.MaxMobileMsg.ToString();

                CCAppID_T.Text = SiteConfig.SiteOption.CCPAppID;
                CCAccount_T.Text = SiteConfig.SiteOption.CCPAccount_SID;
                CCToken_T.Text = SiteConfig.SiteOption.CCPToken;
                CCTemplate_T.Text = SiteConfig.SiteOption.CCPMsgTempID.ToString();

                ddlMessageCheck_DP.SelectedValue = SiteConfig.SiteOption.DefaultSMS;
                ChangeConfig();
                string bread = "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='SiteInfo.aspx'>系统设置</a></li><li><a href='SiteInfo.aspx'>网站配置</a></li><li class=\"active\">短信配置</li>";
                bread += " [<a href='http://www.z01.com/server' target='_blank'>联系官方购买短信授权</a>]";
                bread += Call.GetHelp(4);
                Call.SetBreadCrumb(Master, bread);
            }
        }

        protected void ddlMessageCheck_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeConfig();
        }
        public void ChangeConfig()
        {
            ShenZhen_P.Visible = false;
            East_P.Visible = false;
            YiMei_P.Visible = false;
            CCPRest_Div.Visible = false;
            switch (ddlMessageCheck_DP.SelectedValue)
            {
                case "1"://深圳
                    ShenZhen_P.Visible = true;
                    break;
                case "2"://北京网通
                    East_P.Visible = true;
                    break;
                case "3"://北京亿美
                    YiMei_P.Visible = true;
                    break;
                case "4"://云通讯
                    CCPRest_Div.Visible = true;
                    break;
                default://关闭
                    break;
            }
        }
        public void SaveConfig()
        {
            SiteConfig.SiteOption.DefaultSMS = ddlMessageCheck_DP.SelectedValue;
            SiteConfig.SiteOption.MaxMobileMsg = DataConverter.CLng(MaxPhoneMsg.Text);
            SiteConfig.SiteOption.MaxIpMsg = DataConverter.CLng(MaxIpMsg.Text);
            SiteConfig.UserConfig.UserMobilAuth = userMobilAuth.SelectedValue;
            SiteConfig.SiteOption.G_blackList = blackList.Text;
        }
        protected void Save_Btn_Click(object sender, EventArgs e)
        {
            SaveConfig();
            //北京网通
            SiteConfig.SiteOption.G_eid = txtg_eid.Text.Trim();
            SiteConfig.SiteOption.G_uid = txtg_uid.Text.Trim();
            SiteConfig.SiteOption.G_pwd = txtg_pwd.Text.Trim();
            SiteConfig.SiteOption.G_gate_id = txt_h_gate_id.Text.Trim();
            //深圳电信
            SiteConfig.SiteOption.MssUser = TxtMssUser.Text.Trim();
            SiteConfig.SiteOption.MssPsw = TxtMssPsw.Text.Trim();
            SiteConfig.SiteOption.G_blackList = blackList.Text.Trim();
            SiteConfig.UserConfig.UserMobilAuth = userMobilAuth.SelectedValue;
            //北京亿美
            SiteConfig.SiteOption.sms_key = smskeyT.Text.Trim();
            SiteConfig.SiteOption.sms_pwd = smspwdT.Text.Trim();
            //云通讯
            SiteConfig.SiteOption.CCPAppID = CCAppID_T.Text;
            SiteConfig.SiteOption.CCPAccount_SID = CCAccount_T.Text;
            SiteConfig.SiteOption.CCPToken = CCToken_T.Text;
            SiteConfig.SiteOption.CCPMsgTempID = DataConverter.CLng(CCTemplate_T.Text);
            SiteConfig.Update();
            function.WriteSuccessMsg("配置保存成功", Request.RawUrl);
        }
    }
}