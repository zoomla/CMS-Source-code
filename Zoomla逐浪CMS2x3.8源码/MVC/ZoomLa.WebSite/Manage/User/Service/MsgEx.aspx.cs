using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.BLL;
using ZoomLa.Model;
using System.Data;

namespace ZoomLaCMS.Manage.User.Service
{
    public partial class MsgEx : CustomerPageAction
    {
        B_User buser = new B_User();
        B_ServiceSeat seatBll = new B_ServiceSeat();
        B_ChatMsg chatBll = new B_ChatMsg();
        private string Uids { get { return Request.QueryString["uids"]; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Call.SetBreadCrumb(Master, "<li><a href='../Main.aspx'>工作台</a></li><li><a href='ServiceSeat.aspx'>客服通</a></li><li><a href='" + Request.RawUrl + "'>聊天记录</a></li>");
                MyBind();
            }
        }
        private void MyBind()
        {

            string uids = "";
            if (string.IsNullOrEmpty(Uids))
            {
                DataTable dt = seatBll.Sel();
                foreach (DataRow dr in dt.Rows)
                {
                    uids += dr["S_AdminID"] + ",";
                }
                uids = uids.TrimEnd(',');
            }
            else { uids = Uids; }
            EGV.DataSource = chatBll.SelBySR(uids);
            EGV.DataBind();
        }

        public string GetSender()
        {
            int uid = DataConverter.CLng(Eval("UserID"));
            return buser.GetUserByUserID(uid).UserName;
        }

        public string GetReceUser()
        {
            string[] ids = Eval("ReceUser").ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            return "<a href='Userinfo.aspx?id=" + ids[0] + "' target='_blank'>" + buser.GetUserByUserID(DataConverter.CLng(ids[0])).UserName + "</a>";
        }

        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            MyBind();
        }
        protected void DelChats_Click(object sender, EventArgs e)
        {
            string ids = Request.Form["idchk"];
            if (!string.IsNullOrEmpty(ids))
            {
                chatBll.DelByIds(ids);
                MyBind();
            }
        }
        protected void DelByWeek_Click(object sender, EventArgs e)
        {
            chatBll.DelByWeek();
            MyBind();
        }
        protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("del"))
            {
                chatBll.Del(DataConverter.CLng(e.CommandArgument));
                MyBind();
            }
        }
    }
}