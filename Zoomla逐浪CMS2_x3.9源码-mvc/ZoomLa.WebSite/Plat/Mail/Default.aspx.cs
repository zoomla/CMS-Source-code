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
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.API;
//using ZoomLa.DZNT;

namespace ZoomLaCMS.Plat.Mail
{
    public partial class Default : System.Web.UI.Page
    {
        private B_User buser = new B_User();
        private M_UserInfo muser = new M_UserInfo();
        B_Message msgBll = new B_Message();
        protected void Page_Load(object sender, EventArgs e)
        {
            B_User.CheckIsLogged();
            M_UserInfo info = buser.GetLogin();
            if (info.IsNull)
            {
                Response.Redirect("Login.aspx");
            }
            if (!IsPostBack)
            {
                DataBind();
            }
        }
        private new void DataBind()
        {
            DataTable dt = new DataTable();
            dt = msgBll.SelMyMail(buser.GetLogin().UserID);
            if (!string.IsNullOrEmpty(SearchKey))
            {
                dt.DefaultView.RowFilter = "Title Like '%" + SearchKey + "%'";
            }
            EGV.DataSource = dt;
            EGV.DataBind();
            Session["UserDT"] = null;
        }
        private string SearchKey
        {
            get
            {
                return ViewState["SearchKey"] as string;
            }
            set
            {
                ViewState["SearchKey"] = value;
            }

        }
        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            DataBind();
        }
        protected void searchBtn_Click(object sender, EventArgs e)
        {
            SearchKey = searchText.Text.Trim();
            DataBind();
        }
        public string getStatus(string str)
        {
            string restr = str.IndexOf("," + buser.GetLogin().UserID + ",") > -1 ? "已读" : "<font color='red'>未读</font>";
            return restr;
        }
        public string getMess(string str)
        {
            string restr = str.IndexOf("," + buser.GetLogin().UserID + ",") > -1 ? "阅读信息" : "<font color='red'>阅读信息</font>";
            return restr;
        }
        protected void Row_Command(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteMsg")
            {
                int msgid = Convert.ToInt32(e.CommandArgument);
                msgBll.DelByID(msgid, buser.GetLogin().UserID);
                DataBind();
            }
            if (e.CommandName == "ReadMsg")
            {
                Response.Redirect("MessageRead.aspx?read=1&id=" + e.CommandArgument.ToString());
            }
        }
        protected void batDelBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request.Form["idChk"]))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('请先选择需要操作的邮件!!!');", true);
            }
            else
            {
                msgBll.DelByIDS(Request.Form["idChk"], buser.GetLogin().UserID);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('操作成功!!');", true);
                DataBind();
            }
        }
        protected void batReadBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request.Form["idChk"]))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('请先选择需要操作的邮件!!!');", true);
            }
            else
            {
                msgBll.UnreadToRead(Request.Form["idChk"], buser.GetLogin().UserID);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('操作成功!!');", true);
                DataBind();
            }
        }
        public string GetUserName(string userid)
        {
            string un = buser.GetUserNameByIDS(userid, UserDT);
            return un.Length > 13 ? un.Substring(0, 12) + "..." : un;
        }
        public DataTable UserDT
        {
            get
            {
                if (Session["UserDT"] == null)
                {
                    Session["UserDT"] = buser.Sel();
                }
                return Session["UserDT"] as DataTable;
            }
            set
            {
                Session["UserDT"] = value;
            }
        }
    }
}