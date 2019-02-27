using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Plat;
using ZoomLa.Common;
using ZoomLa.Model;

namespace ZoomLaCMS.Plat.EMail
{
    public partial class MailList : System.Web.UI.Page
    {
        B_Plat_Mail mailBll = new B_Plat_Mail();
        B_Plat_MailConfig configBll = new B_Plat_MailConfig();
        B_User buser = new B_User();

        public int MailType
        {
            get
            {
                return DataConverter.CLng(Request.QueryString["mailtype"]);
            }
        }
        public string SKey { get { return Request.QueryString["skey"] ?? ""; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
            }
        }
        private void MyBind()
        {
            M_UserInfo mu = buser.GetLogin();
            DataTable configDT = configBll.SelByUid(mu.UserID);
            MailList_DP.DataSource = configDT;
            MailList_DP.DataBind();
            MailList_DP.Items.Insert(0, new ListItem("全部", ""));
            DataTable dt = mailBll.Sel(50000, mu.UserID, MailType, 1, MailList_DP.SelectedValue, SKey);
            count_sp.InnerText = dt.Rows.Count.ToString();
            EGV.DataSource = dt;
            EGV.DataBind();
        }
        protected void Dels_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.Form["idchk"]))
            {
                mailBll.DelByUid(Request.Form["idchk"], buser.GetLogin().UserID);
                MyBind();
            }
        }
        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            MyBind();
        }
        protected void MailList_DP_SelectedIndexChanged(object sender, EventArgs e)
        {
            MyBind();
        }
    }
}