namespace ZoomLaCMS.Manage.Guest
{
    using System;
    using System.Data;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using ZoomLa.BLL;
    using ZoomLa.BLL.Message;
    using ZoomLa.Common;
    using ZoomLa.Model.Message;
    public partial class BarAuthSet : System.Web.UI.Page
    {
        B_User buser = new B_User();
        B_Guest_BarAuth authBll = new B_Guest_BarAuth();
        public string View { get { return Request.QueryString["view"] ?? "all"; } }
        public int BarID { get { return Convert.ToInt32(Request.QueryString["ID"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
            }
        }
        public void MyBind()
        {
            DataTable dt = buser.SelBarAuth(BarID);
            if (View.Equals("leastone")) { dt.DefaultView.RowFilter = "Look=1 OR Send=1 OR Reply=1"; }
            EGV.DataSource = dt.DefaultView;
            EGV.DataBind();
        }
        protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToLower())
            {
                case "del2":
                    break;
            }
        }
        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            MyBind();
        }
        //更新权限
        protected void Save_Btn_Click(object sender, EventArgs e)
        {
            M_Guest_BarAuth authMod = new M_Guest_BarAuth();
            foreach (GridViewRow row in EGV.Rows)
            {
                int uid = Convert.ToInt32((row.FindControl("Uid_Hid") as HiddenField).Value);
                authMod = authBll.SelModelByUid(BarID, uid);
                bool isnew = false;
                if (authMod == null) { isnew = true; authMod = new M_Guest_BarAuth(); authMod.Uid = uid; authMod.BarID = BarID; }
                authMod.Look = (row.FindControl("Look") as HtmlInputCheckBox).Checked ? 1 : 0;
                authMod.Send = (row.FindControl("Send") as HtmlInputCheckBox).Checked ? 1 : 0;
                authMod.Reply = (row.FindControl("Reply") as HtmlInputCheckBox).Checked ? 1 : 0;
                if (isnew) { authBll.Insert(authMod); }
                else { authBll.UpdateByID(authMod); }
            }
            function.WriteSuccessMsg("操作成功!");
            function.Script(this, "parent.CloseDiag();");
        }
        protected void EGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dr = e.Row.DataItem as DataRowView;
                (e.Row.FindControl("Look") as HtmlInputCheckBox).Checked = dr["Look"].ToString().Equals("1");
                (e.Row.FindControl("Send") as HtmlInputCheckBox).Checked = dr["Send"].ToString().Equals("1");
                (e.Row.FindControl("Reply") as HtmlInputCheckBox).Checked = dr["Reply"].ToString().Equals("1");
            }
        }
        protected void Search_B_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Search_T.Text)) { sel_box.Attributes.Add("style", "display:inline;"); template.Attributes.Add("style", "margin-top:44px;"); }
            else { sel_box.Attributes.Add("style", "display:none;"); }
            DataTable dt = buser.SelBarAuth(BarID);
            dt.DefaultView.RowFilter = "UserName like '%" + Search_T.Text.Trim() + "%'";
            EGV.DataSource = dt.DefaultView.ToTable();
            EGV.DataBind();
        }
    }
}