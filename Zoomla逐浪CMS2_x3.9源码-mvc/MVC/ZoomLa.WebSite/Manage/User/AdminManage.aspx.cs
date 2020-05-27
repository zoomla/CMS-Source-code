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
using ZoomLa.Web;
using ZoomLa.Common;
using ZoomLa.BLL;
using System.Net.Mail;
using ZoomLa.BLL.Helper;

namespace ZoomLaCMS.Manage.User
{
    public partial class AdminManage : CustomerPageAction
    {
        M_AdminInfo manager = new M_AdminInfo();
        M_AdminInfo m = new M_AdminInfo();
        B_Admin badmin = new B_Admin();
        B_User buser = new B_User();
        public int IsLock { get { return string.IsNullOrEmpty(Request.QueryString["islock"]) ? -1 : DataConverter.CLng(Request.QueryString["islock"]); } }
        public int RoleID { get { return DataConverter.CLng(Request.QueryString["id"]); } }
        private string SearchKey
        {
            get { return Search_T.Text; }
            set { Search_T.Text = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            B_Admin.IsSuperManage();
            if (!B_ARoleAuth.Check(ZLEnum.Auth.user, "AdminManage"))
            {
                function.WriteErrMsg("没有权限进行此项操作");
            }
            if (!IsPostBack)
            {
                MyBind();
                SearchKey = Request.QueryString["keyWordss"];
                if (IsLock != 1) { Auit_B.Attributes.Add("disabled", "disabled"); }
                Call.HideBread(Master);
            }
        }
        // 变量说明：flag判断记录是否选中，chkCount选种记录数量
        protected void Lnk_Click(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteAdmin")
            {
                B_Admin.DelAdminById(DataConverter.CLng(e.CommandArgument.ToString()));
                MyBind();
            }
        }
        // 批量删除
        protected void Button2_Click(object sender, EventArgs e)
        {
            string[] ids = Request.Form["idchk"].Split(',');
            foreach (string id in ids)
            {
                B_Admin.DelAdminById(Convert.ToInt32(id));
            }
            MyBind();
        }
        public string Getroleid()
        {
            return RoleID > 0 ? "?roleid=" + RoleID : "";
        }
        public void MyBind()
        {
            B_Role brole = new B_Role();
            ViewState["RoleList"] = brole.GetRoleAll();
            DataTable dt = new DataTable();
            if (RoleID == 0)
            {
                dt = badmin.Sel();
            }
            else
            {
                dt = badmin.SelByRole(RoleID);
            }
            EGV.Visible = (dt != null && dt.Rows.Count > 0);
            string strwhere = "1=1 ";
            if (!string.IsNullOrEmpty(SearchKey))
                strwhere += "AND AdminName Like '%" + SearchKey + "%'";
            if (IsLock > -1)
                strwhere += "AND islock=" + IsLock;
            dt.DefaultView.RowFilter = strwhere;
            EGV.DataSource = dt;
            EGV.DataKeyNames = new string[] { "AdminID" };
            EGV.DataBind();
        }
        public string GetRoleName()
        {
            string ids = Eval("AdminRole").ToString();
            DataTable dt = ViewState["RoleList"] as DataTable;
            DataRow[] dr = dt.Select("'" + ids + "' LIKE '%,'+RoleID+',%'");
            ids = "";
            foreach (DataRow item in dr)
            {
                ids += item["RoleName"].ToString() + ",";
            }
            return ids.Trim(',');
        }
        protected void Egv_RowDataBound(Object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[1].Text = "<i>" + e.Row.Cells[1].Text + "</i>";
            }
        }
        protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.EGV.PageIndex = e.NewPageIndex;
            MyBind();
        }
        protected override void Render(HtmlTextWriter writer)
        {
            foreach (GridViewRow row in this.EGV.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    row.Attributes["ondblclick"] = String.Format("javascript:location.href='AdminDetail.aspx?id={0}'", this.EGV.DataKeys[row.RowIndex].Value.ToString());
                    row.Attributes["style"] = "cursor:pointer";
                    row.Attributes["title"] = "双击查看管理员信息";
                }
            }
            base.Render(writer);
        }
        protected void Search_Btn_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminManage.aspx?keyWordss=" + Search_T.Text);
        }
        protected void EGV_PreRender(object sender, EventArgs e)
        {
            ViewState["RoleName"] = null;
        }
        protected void Lock_B_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.Form["idchk"]))
                badmin.LockAdmin(Request.Form["idchk"], true);
            MyBind();
        }
        protected void UnLock_B_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.Form["idchk"]))
                badmin.LockAdmin(Request.Form["idchk"], false);
            MyBind();
        }
        //string MailTlp = "<img src='http://demo.z01.com/Images/logo.png' /><br /><p>亲爱的{$UserName}:</p> <p>您在{$SiteName}网站申请测试账号资格已审核通过!以下是您的账号信息:</p><p>  用户名:{$UserName}<br />密码:{$UserPwd}</p>请妥善保管好您的用户名和密码!"
        //                + " <a href='{$url}'>点击登录{$SiteName}后台管理系统</a><br />如果链接不能点击，请将以下链接地址复制到浏览器，然后直接打开。<br /><a href='{$url}' target='_blank'>{$url}</a><p>致此!</p><p>此为自动发送邮件，请勿直接回复。</p>{$SiteName}";
        protected void Auit_B_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request.Form["idchk"])) { function.WriteErrMsg("未选定需要操作的管理员"); }
            DataTable dt = badmin.SelByIds(Request.Form["idchk"], 1);
            if (IsEmail_Hid.Value.Equals("1"))//如开启邮件通知,修改密码并发送邮件
            {
                string emailtlp = SafeSC.ReadFileStr("/manage/Common/MailTlp/GetManageMail.html");
                foreach (string id in Request.Form["idchk"].Split(','))
                {
                    dt.DefaultView.RowFilter = "AdminID='" + id + "'";
                    DataTable infoDT = dt.DefaultView.ToTable();
                    //非锁定的管理员,超管,不会触发密码修改和邮件通知
                    if (infoDT.Rows.Count < 1 || id.Equals("1")) { continue; }
                    if (infoDT.Rows[0]["IsLock"].ToString().Equals("0")) { continue; }
                    string adminpwd = function.GetRandomString(8).ToLower();
                    if (infoDT.Rows.Count > 0)
                    {
                        infoDT.Columns.Add("SiteName");
                        infoDT.Columns.Add("url");
                        infoDT.Columns.Add("UserPwd");
                        infoDT.Rows[0]["SiteName"] = SiteConfig.SiteInfo.SiteName;
                        infoDT.Rows[0]["url"] = "http://" + Request.Url.Authority + "/" + SiteConfig.SiteOption.ManageDir + "/Login.aspx?HasAccount=1";
                        infoDT.Rows[0]["UserPwd"] = adminpwd;
                        SendAdminMail(emailtlp, infoDT);
                    }
                    badmin.UpdatePwdByIDS(id, adminpwd);
                }
            }
            badmin.LockAdmin(Request.Form["idchk"], false);//解锁申请用户
            function.WriteSuccessMsg("审核成功!");
        }
        public void SendAdminMail(string mailTlp, DataTable dt)
        {
            DataRow dr = dt.Rows[0];
            M_UserInfo mu = buser.GetUserByName(dr["UserName"].ToString());
            if (string.IsNullOrEmpty(mu.Email))
                function.WriteErrMsg("这个账号未绑定邮件！");
            MailAddress adMod = new MailAddress(mu.Email);
            MailInfo mailInfo = new MailInfo() { ToAddress = adMod, IsBodyHtml = true };
            mailInfo.FromName = SiteConfig.SiteInfo.SiteName;
            mailInfo.Subject = "恭喜您已获得" + SiteConfig.SiteInfo.SiteName + "申请测试帐号资格";
            mailInfo.MailBody = new OrderCommon().TlpDeal(mailTlp, dt);
            SendMail.Send(mailInfo);
        }
        public string GetUserName()
        {
            DataTable users = buser.GetUserName(Eval("UserName").ToString());
            if (users.Rows.Count > 0)
                return "<a href='UserInfo.aspx?id=" + users.Rows[0]["UserID"] + "'>" + Eval("UserName") + "</a>";
            return "<span style='color:gray;'>无</span>";
        }
        public string GetIpLocation(string ip)
        {
            return IPScaner.IPLocation(ip);
        }
    }
}