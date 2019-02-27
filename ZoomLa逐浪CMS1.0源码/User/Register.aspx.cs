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
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.BLL;
using System.IO;

namespace ZoomLa.WebSite.User
{
    public partial class User_Register : System.Web.UI.Page, ICallbackEventHandler
    {
        protected string callBackReference;
        protected string result;
        private B_User buser = new B_User();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (!SiteConfig.UserConfig.EnableUserReg)
                {
                    this.PnlRegStep0.Visible = true;
                    this.PnlRegStep1.Visible = false;
                    this.PnlRegStep2.Visible = false;
                }
                else
                {
                    this.PnlRegStep0.Visible = false;
                    this.PnlRegStep1.Visible = true;
                    this.PnlRegStep2.Visible = false;
                    this.callBackReference = this.Page.ClientScript.GetCallbackEventReference(this, "arg", "ReceiveServerData", "context");
                    //String callbackScript = "function CallServer(arg, context)" + "{ " + this.callBackReference + "} ;";
                    //this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(),"CallServer", callbackScript, true);
                    InitProtocol();
                }
            }
        }
        protected void BtnRegStep1_Click(object sender, EventArgs e)
        {
            //第二步
            this.PnlRegStep1.Visible = false;
            this.PnlRegStep2.Visible = true;
            this.callBackReference = this.Page.ClientScript.GetCallbackEventReference(this, "arg", "ReceiveServerData", "context");
        }
        protected void BtnRegStep1NotApprove_Click(object sender, EventArgs e)
        {
            //不同意
            base.Response.Redirect("~/default.aspx");
        }
        //提交注册
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                M_UserInfo info = new M_UserInfo();
                info.UserName = this.TxtUserName.Text;
                info.UserPwd = this.TxtPassword.Text;
                info.Question = this.TxtQuestion.Text;
                info.Answer = this.TxtAnswer.Text;
                info.Email = this.TxtEmail.Text;
                info.CheckNum = DataSecurity.RandomNum(10);
                info.UserFace = "";
                info.FaceHeight = 16;
                info.FaceWidth = 16;
                info.GroupID = 0;
                info.LastLoginIP = HttpContext.Current.Request.UserHostAddress;
                info.LastLockTime = DateTime.Now;
                info.LastLoginTimes = DateTime.Now;
                info.LastPwdChangeTime = DateTime.Now;
                info.LoginTimes = 1;
                info.PrivacySetting = 0;
                info.RegTime = DateTime.Now;
                info.Sign = "";
                info.Status = 4;
                info.UserPwd = StringHelper.MD5(info.UserPwd);
                info.Answer = StringHelper.MD5(info.Answer);
                buser.Add(info);
                buser.SetLoginState(info);
                base.Response.Redirect("Default.aspx");
            }
        }

        private void InitProtocol()
        {
            try
            {
                this.LitProtocol.Text = FileSystemObject.ReadFile(base.Request.MapPath("~/User/Protocol.txt"));
            }
            catch (FileNotFoundException)
            {
                function.WriteErrMsg("<li>Protocol.txt文件不存在</li>", "../Index.aspx");
            }
            catch (UnauthorizedAccessException)
            {
                function.WriteErrMsg("<li>没有权限读取Protocol.txt文件</li>", "../Index.aspx");
            }
        }

        public string CallBackReference
        {
            get { return this.callBackReference; }
        }
        #region ICallbackEventHandler 成员

        string ICallbackEventHandler.GetCallbackResult()
        {
            return this.result;
        }

        void ICallbackEventHandler.RaiseCallbackEvent(string eventArgument)
        {
            if (string.IsNullOrEmpty(eventArgument))
            {
                this.result = "empty";
            }
            else if (StringHelper.FoundInArr(SiteConfig.UserConfig.UserNameRegDisabled, eventArgument, "|"))
            {
                this.result = "disabled";
            }
            else if (buser.IsExit(eventArgument))
            {
                this.result = "true";
            }
            else
            {
                this.result = "false";
            }
        }

        #endregion
    }
}