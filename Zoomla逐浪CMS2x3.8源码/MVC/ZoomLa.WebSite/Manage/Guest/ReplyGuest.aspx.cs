namespace ZoomLaCMS.Manage.Guest
{
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
    using System.Data.SqlClient;
    using ZoomLa.BLL;
    using ZoomLa.Common;
    using ZoomLa.Model;
    using ZoomLa.SQLDAL;
    using System.Net;
    using ZoomLa.BLL.Helper;

    public partial class ReplyGuest : System.Web.UI.Page
    {
        protected B_GuestBook guestBll = new B_GuestBook();

        protected M_GuestBook guestMod = new M_GuestBook();
        public int Mid { get { return DataConverter.CLng(Request.QueryString["ID"]); } }

        protected void Page_Load(object sender, EventArgs e)
        {
            string str = "分类名称";
            if (!this.Page.IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["CateID"]))
                {
                    guestMod = guestBll.GetQuest(Convert.ToInt32(Request.QueryString["CateID"]));
                    if (guestMod != null)
                        str = guestMod.Title;
                }
                int GID = string.IsNullOrEmpty(Request.QueryString["GID"]) ? 0 : DataConverter.CLng(Request.QueryString["GID"]);
                if (GID <= 0)
                    function.WriteErrMsg("留言ID不正确！", "../Plus/Default.aspx");
                else
                {
                    this.HdnGID.Value = GID.ToString();
                    dBind();
                    //EgvBind();
                }
                M_GuestBook model = guestBll.GetQuest(Mid);
                if (model.GID > 0)
                {
                    TextBox1.Text = model.Title;
                    tx_Content.Value = model.TContent;
                }
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='ContentManage.aspx'>内容管理</a></li><li><a href='WdCheck.aspx'>百科问答</a></li><li><a href='GuestManage.aspx'>留言管理</a></li><li><a href='Default.aspx?CateID=" + Request.QueryString["CateID"] + "'>" + str + "</a></li><li class='active'>回复留言</li>");
        }

        //获取单个留言
        public static DataTable GetReply(int GID)
        {
            string strSql = "select * from ZL_GuestBook where GID=@GID order by GDate Desc";
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("@GID", SqlDbType.Int) };
            sp[0].Value = GID;
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, sp);
        }
        private void dBind()
        {
            int GID = DataConverter.CLng(this.HdnGID.Value);
            DataTable dt = GetReply(GID);
            int Total = B_GuestBook.GetTipsTotal(GID);
            this.Repeater1.DataSource = dt;
            this.Repeater1.DataBind();
        }

        public string GetUserName(string UserID)
        {
            B_User buser = new B_User();
            return buser.SeachByID(DataConverter.CLng(UserID)).UserName;
        }

        protected void repFileReName_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Del")
            {
                string Id = e.CommandArgument.ToString();
                B_GuestBook.DelTips(DataConverter.CLng(Id));
                dBind();
            }
        }
        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            B_User buser = new B_User();
            M_GuestBook info = new M_GuestBook();
            if (Mid > 0) { info = guestBll.GetQuest(Mid); }
            info.ParentID = Convert.ToInt16(Request.QueryString["GID"]);
            info.CateID = Convert.ToInt16(Request.QueryString["CateId"]);
            info.UserID = buser.GetLogin().UserID;
            info.Title = this.TextBox1.Text.Trim() == "" ? "<font style='color:#1e860b;'>[管理员回复]</font>" : BaseClass.CheckInjection(this.TextBox1.Text.Trim());
            info.TContent = BaseClass.CheckInjection(this.tx_Content.Value);
            info.Status = 1;
            info.IP = IPScaner.GetUserIP();
            if (guestBll.AddTips(info))
            {
                Response.Redirect("GuestBookShow.aspx?GID=" + Request.QueryString["GID"] + "&CateID=" + Request.QueryString["CateID"]);
            }
        }
    }
}