using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using ZoomLa.Model;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Components;
using System.Net;
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.MIS.OA.Mail
{
    public partial class MessageOutbox : System.Web.UI.Page
    {
        private B_User buser = new B_User();
        private M_UserInfo muser = new M_UserInfo();
        private B_Message msgBll = new B_Message();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                MyBind();
            }
        }
        private void MyBind()
        {
            DataTable dt = new DataTable();
            int UserID = buser.GetLogin().UserID;
            dt = msgBll.SelMyMail(buser.GetLogin().UserID, 1);

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
            MyBind();
        }
        protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteMsg")
            {
                msgBll.DelByIDS(e.CommandArgument.ToString(), buser.GetLogin().UserID);
            }
            if (e.CommandName == "ReadMsg")
            {
                Response.Redirect("MessageRead.aspx?id=" + e.CommandArgument.ToString());
            }
            MyBind();
        }
        protected void searchBtn_Click(object sender, EventArgs e)
        {
            SearchKey = searchText.Text.Trim();
            MyBind();
        }
        //batDel
        protected void Button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request.Form["idChk"]))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('请先选择需要操作的邮件!!!');", true);
            }
            else
            {
                msgBll.DelByIDS(Request.Form["idChk"], buser.GetLogin().UserID);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('操作成功!!');", true);
                MyBind();
            }
        }

        public string getStatus(string str)
        {
            string restr = DataConverter.CBool(str) ? "已读" : "未读";
            return restr;
        }

        public string GetUserName(string userid)
        {
            try {  return StringHelper.SubStr(buser.GetUserNameByIDS(userid),20);}
            catch {return userid; }
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