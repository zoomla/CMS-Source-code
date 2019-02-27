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
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.Manage.User
{
    public partial class Message : CustomerPageAction
    {
        B_User buser = new B_User();
        B_Message msgBll = new B_Message();
        public string view
        {
            get
            {
                if (ViewState["view"] == null || string.IsNullOrEmpty(ViewState["view"].ToString()))
                {
                    ViewState["view"] = Request.QueryString["view"] ?? "all";
                }
                return ViewState["view"].ToString();
            }
            set
            {
                ViewState["view"] = value;
            }
        }
        public string Skey { get { return Request.QueryString["skey"] ?? ""; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!B_ARoleAuth.Check(ZLEnum.Auth.user, "MessManage"))
                {
                    function.WriteErrMsg("没有权限进行此项操作");
                }
                MyBind(Skey);
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li> ");
            }
        }
        private void MyBind(string skey = "")
        {
            DataTable dt = msgBll.Sel(view, skey);
            EGV.DataSource = dt;
            EGV.DataBind();
        }
        //删除多少日前的已读短消息
        protected void BtnDelDate_Click(object sender, EventArgs e)
        {
            B_Message.DeleteByDate(DataConverter.CLng(DropDelDate.SelectedValue));
            MyBind();
        }
        // 批量删除
        protected void Button2_Click(object sender, EventArgs e)
        {
            string ids = Request.Form["idChk"];
            if (string.IsNullOrEmpty(ids)) { return; }
            msgBll.DelByAdmin(ids);
            MyBind();
        }
        //英文状态下的逗号将用户名隔开实现多会员同时删除
        protected void BtnDelSender_Click(object sender, EventArgs e)
        {
            string incept = TxtSender.Text;
            if (!string.IsNullOrEmpty(incept))
            {
                string[] inarr = incept.Split(new char[] { ',' });
                for (int i = 0; i < inarr.Length; i++)
                {
                    B_Message.DeleteByUser(inarr[i]);
                }
            }
            MyBind();
            TxtSender.Text = "";
        }
        protected void Row_Command(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToLower())
            {
                case "deletemsg":

                    int MsgID = DataConverter.CLng(e.CommandArgument);
                    msgBll.DelByAdmin(MsgID.ToString());
                    MyBind();
                    break;
                case "readmsg":
                    Response.Redirect("MessageRead.aspx?id=" + e.CommandArgument.ToString());
                    break;
            }
        }

        public string GetStatus(int msgID, string type)
        {
            M_Message msg = B_Message.GetMessByID(msgID);
            if (msg.IsDelInbox == 1) { return "已删除"; }
            if (msg.IsDelInbox == -1) { return "已清"; }
            switch (type)
            {
                case "incept":
                    return msg.status == 1 ? "已读" : "未读";
                case "sender":
                    return msg.Savedata == 0 ? "已发" : "草稿";
                default: return "";
            }
        }
        protected string GetMsgType(int msgType)
        {
            switch (msgType)
            {
                case 1:
                    return "系统消息";
                case 2:
                    return "手机短信";
                case 3:
                    return "验证码";
                default:
                    return "未知类型";
            }
        }

        protected void EGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("style", "cursor:pointer");
                e.Row.Attributes.Add("title", "双击阅读信息");
                e.Row.Attributes.Add("ondblclick", "window.location.href = 'MessageRead.aspx?id=" + (e.Row.DataItem as DataRowView)["MsgID"] + "';");
            }
        }
        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            MyBind();
        }

        protected void Search_B_Click(object sender, EventArgs e)
        {
            string skey = Skey_T.Text.Trim(' ');
            if (!string.IsNullOrEmpty(skey)) { sel_box.Attributes.Add("style", "display:inline;"); template.Attributes.Add("style", "margin-top:44px;"); }
            else { sel_box.Attributes.Add("style", "display:none;"); template.Attributes.Add("style", "margin-top:0px;"); }
            MyBind(skey);
        }
    }
}