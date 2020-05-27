using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Helper;
using ZoomLa.Common;
using ZoomLa.Model;

namespace ZoomLaCMS.Guest
{
    public partial class Default : System.Web.UI.Page
    {
        B_User buser = new B_User();
        B_GuestBook guestBll = new B_GuestBook();
        B_GuestBookCate cateBll = new B_GuestBookCate();
        public int CateID { get { return DataConverter.CLng(Request.QueryString["CateID"]); } }
        public string Skey { get { return ViewState["Skey"] as string; } set { ViewState["Skey"] = value; } }
        public int CPage { get { return PageCommon.GetCPage(); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (buser.CheckLogin()) { user.Text = buser.GetLogin().UserName; }
                userDiv.Visible = CateID > 0;
                addDiv.Visible = CateID <= 0;
                Skey = Request.QueryString["Skey"];
                txtName.Text = Skey;
                MyBind();
            }
        }
        private void MyBind()
        {
            M_GuestBookCate cateMod = cateBll.SelReturnModel(CateID);
            DataTable cateDT = cateBll.Cate_Sel(0);
            Cate_RPT.DataSource = cateDT;
            Cate_RPT.DataBind();
            Cate_DP.DataSource = cateDT;
            Cate_DP.DataBind();
            if (CateID > 0) { Cate_DP.SelectedValue = CateID.ToString(); LitCate.Text = Cate_DP.SelectedItem.Text; }
            else { LitCate.Text = "留言信息"; }
            //-----------------------------
            int itemCount = 0;
            DataTable dt = cateBll.SelPage(out itemCount, 1, CPage, CateID, 0, Skey);
            RPT.DataSource = dt;
            RPT.DataBind();
            Page_Lit.Text = PageCommon.CreatePageHtml(PageHelper.GetPageCount(itemCount, 10), CPage);
        }
        public string GetTitle()
        {
            if (Eval("Status").ToString().Equals("0"))
            {
                return "<span style='color:gray;'>" + Eval("Title") + "[未审核]</span>";
            }
            else
            {
                return Eval("Title").ToString();
            }
        }
        public string GetCate(string CateID)
        {
            return "";
        }
        public string GetUName()
        {
            string name = B_User.GetUserName(Eval("HoneyName", ""), Eval("UserName", ""));
            return string.IsNullOrEmpty(name) ? "游客" : name;
        }
        //发表新帖
        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            if (!ZoomlaSecurityCenter.VCodeCheck(Request.Form["VCode_hid"], VCode.Text))
            {
                function.WriteErrMsg("验证码不正确", Request.RawUrl);
            }
            M_GuestBook info = new M_GuestBook();
            M_GuestBookCate cateMod = cateBll.SelReturnModel(CateID);
            //不允许匿名登录,必须登录才能发表留言
            if (cateMod.NeedLog == 1)
            {
                if (buser.CheckLogin())
                {
                    info.UserID = DataConverter.CLng(buser.GetLogin().UserID);
                }
                else
                {
                    //divshap.Style.Remove("display");
                    function.WriteErrMsg("登录后才能发表留言", "/User/Login.aspx?ReturnUrl=" + Request.RawUrl);
                    Response.End();
                }
            }
            else if (buser.CheckLogin())
            {
                info.UserID = buser.GetLogin().UserID;
            }
            info.CateID = CateID;
            //是否开启审核
            info.Status = cateMod.Status != 1 ? 0 : 1;
            info.ParentID = 0;
            info.Title = Server.HtmlEncode(Title_T.Text.Trim());
            info.TContent = TxtTContent.Value;
            info.IP = EnviorHelper.GetUserIP();
            guestBll.AddTips(info);
            Response.Redirect(Request.RawUrl);
        }
    }
}