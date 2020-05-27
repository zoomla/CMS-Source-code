using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;

namespace ZoomLaCMS.Manage.User.Service
{
    public partial class MsgList : System.Web.UI.Page
    {
        B_ChatMsg chatBll = new B_ChatMsg();
        B_User buser = new B_User();
        public string suid { get { return Request.QueryString["suid"]; } }
        public string ruid { get { return (Request.QueryString["ruid"] ?? "").Trim(','); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
                Call.HideBread(Master);
            }
        }
        private void MyBind()
        {
            M_UserInfo smu = buser.GetSelect(Convert.ToInt32(suid));
            M_UserInfo rmu = buser.GetSelect(Convert.ToInt32(ruid));
            string tlp = "<span class='rd_red'>" + smu.UserName + "</span>与<span class='rd_red'>" + rmu.UserName + "</span>的聊天记录";
            SToR_L.Text = string.Format(tlp, smu.UserName, rmu.UserName);
            DataTable dt = chatBll.SelHistoryMsg(suid, ruid);
            EGV.DataSource = dt;
            EGV.DataBind();
        }
        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            MyBind();
        }
    }
}