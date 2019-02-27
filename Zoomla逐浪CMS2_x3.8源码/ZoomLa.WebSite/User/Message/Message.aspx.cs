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
using ZoomLa.DZNT;
namespace ZoomLa.WebSite.User
{
    public partial class Message : System.Web.UI.Page
    {
        B_User buser = new B_User();
        B_Message msgBll = new B_Message();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
            }
        }
        private void MyBind()
        {
            DataTable dt = msgBll.SelMyMail(buser.GetLogin().UserID, 2, Skey_T.Text);
            EGV.DataSource = dt;
            EGV.DataKeyNames = new string[] { "MsgID" };
            EGV.DataBind();
            Session["UserDT"] = null;
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            MyBind();
        }
        public string getStatus(string str)
        {
            string restr = str.IndexOf("," + buser.GetLogin().UserID + ",") > -1 ? "" : "<span style='color:red;'> 未读</span>";
            return restr;
        }
        public string GetUserName(string ids)
        {
            string un = buser.GetUserNameByIDS(ids, UserDT);
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
        protected void BatDel_Btn_Click(object sender, EventArgs e)
        {
            string ids = Request.Form["idchk"];
            msgBll.DelByIDS(ids, buser.GetLogin().UserID);
            MyBind();
        }
        protected void Skey_Btn_Click(object sender, EventArgs e)
        {
            MyBind();
        }
        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            MyBind();
        }
        protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "del2":
                    int MsgID = Convert.ToInt32(e.CommandArgument.ToString());
                    msgBll.DelByID(MsgID, buser.GetLogin().UserID);
                    MyBind();
                    break;
            }
        }

        protected void EGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["ondblclick"] = "javascript:location.href='MessageRead.aspx?read=1&id=" + ((DataRowView)e.Row.DataItem)["MsgID"] + "';";
                e.Row.Attributes["title"] = "双击修改";
            }
        }
    }
}