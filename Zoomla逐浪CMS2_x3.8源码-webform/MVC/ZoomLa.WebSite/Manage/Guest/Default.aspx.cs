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
    using ZoomLa.BLL;
    using ZoomLa.Common;
    using ZoomLa.Model;
    using ZoomLa.Components;
    public partial class Default : System.Web.UI.Page
    {
        public string cateName = "";
        B_GuestBook guestBll = new B_GuestBook();
        //为空则等于100,显示全部除回收站
        public int Status { get { return string.IsNullOrEmpty(Request.QueryString["status"]) ? 100 : DataConverter.CLng(Request.QueryString["status"]); } }
        public string RelStr { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            int Cateid = DataConverter.CLng(Request.QueryString["CateID"]);
            if (Status != -1)
                RelStr = " [<a href='Default.aspx?CateID=" + Cateid + "&status=-1'>回收站</a>]";
            else
            {
                Del_B.Visible = true;
                Rel_B.Visible = true;
                btnAdudit.Visible = false;
                btnSvaeAudit.Visible = false;
                btndelete.Visible = false;
                RelStr = " [<a href='Default.aspx?CateID=" + Cateid + "'>留言列表</a>]";
            }
            if (!IsPostBack)
            {
                int CateID = string.IsNullOrEmpty(Request.QueryString["CateID"]) ? 0 : DataConverter.CLng(Request.QueryString["CateID"]);
                if (CateID <= 0)
                    function.WriteErrMsg("缺少留言分类ID", "../Guest/GuestCateMana.aspx");
                else
                    this.HdnCateID.Value = CateID.ToString();
                cateName = ">>" + guestBll.GetQuest(CateID).Title;
                MyBind();
            }
        }
        public void MyBind()
        {
            DataTable dt = B_GuestBook.GetAllTips(DataConverter.CLng(this.HdnCateID.Value), Status, Key_T.Text.Trim(), 0);
            Egv.DataSource = dt;
            Egv.DataBind();
        }
        protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Egv.PageIndex = e.NewPageIndex;
            MyBind();
        }
        protected void Lnk_Click(object sender, GridViewCommandEventArgs e)
        {
            if (this.Page.IsValid)
            {
                switch (e.CommandName)
                {
                    case "QList":
                        Response.Redirect("GuestBookShow.aspx?GID=" + e.CommandArgument.ToString());
                        break;
                    case "Reply":
                        Response.Redirect("ReplyGuest.aspx?GID=" + e.CommandArgument.ToString());
                        break;
                    case "RList":
                        Response.Redirect("ReplyList.aspx?GID=" + e.CommandArgument.ToString());
                        break;
                    case "Del":
                        B_GuestBook.UpdateAudit(e.CommandArgument.ToString(), -1);
                        break;
                    case "Del2":
                        int gid = DataConverter.CLng(e.CommandArgument);
                        B_GuestBook.DelByIDS(e.CommandArgument.ToString());
                        break;
                    case "Audit":
                        B_GuestBook.UpdateAudit(e.CommandArgument.ToString(), 1);
                        break;
                    case "CancelAudit":
                        B_GuestBook.UpdateAudit(e.CommandArgument.ToString(), 0);
                        break;
                }
                MyBind();
            }
        }

        protected void btndelete_Click(object sender, EventArgs e)
        {
            btnAll(-1);
        }

        public string GetCate(string CateID)
        {
            string re = guestBll.GetQuest(DataConverter.CLng(CateID)).Title;
            return re;
        }
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            MyBind();
        }
        private string[] GetChecked()
        {
            if (!string.IsNullOrEmpty(Request.Form["idchk"]))
            {
                string[] chkArr = Request.Form["idchk"].Split(',');
                return chkArr;
            }
            else
                return null;
        }
        public string GetStatus()
        {
            int status = Eval("status") == DBNull.Value ? 0 : Convert.ToInt32(Eval("status"));
            string result = "";
            switch (status)
            {
                case 0:
                    result = "<span style='color:gray;'>未审核</span>";
                    break;
                case 1:
                    result = "<span style='color:green;'>已审核</span>";
                    break;
                case -1:
                    result = "<span style='color:red;'>回收站</span>";
                    break;
            }
            return result;
        }
        protected void SearchBtn_Click(object sender, EventArgs e)
        {
            MyBind();
        }
        protected void btnSelAudit_Click(object sender, EventArgs e)
        {
            btnAll(0);
        }
        protected void btnAdudit_Click(object sender, EventArgs e)
        {
            btnAll(1);
        }
        public void btnAll(int status)
        {
            if (!string.IsNullOrEmpty(Request.Form["idchk"]))
                B_GuestBook.UpdateAudit(Request.Form["idchk"], status);
            this.MyBind();
        }
        protected void Rel_B_Click(object sender, EventArgs e)
        {
            btnAll(0);
        }
        protected void Del_B_Click(object sender, EventArgs e)
        {
            B_GuestBook.DelByIDS(Request.Form["idchk"]);
            this.MyBind();
        }

        protected void Egv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dr = e.Row.DataItem as DataRowView;
                e.Row.Attributes.Add("ondblclick", "window.location.href='ReplyList.aspx?GID=" + dr["GID"] + "&CateID=" + dr["CateID"] + "'");
            }
        }
    }
}