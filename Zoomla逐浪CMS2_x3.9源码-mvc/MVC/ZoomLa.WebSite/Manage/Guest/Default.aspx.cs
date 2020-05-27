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
        protected void Page_Load(object sender, EventArgs e)
        {
            string RelStr = "";
            int Cateid = DataConverter.CLng(Request.QueryString["CateID"]);
            if (Status != -1)
                RelStr = " [<a href='Default.aspx?CateID=" + Cateid + "&status=-1'>回收站</a>]";
            else
            {
                Del_B.Visible = true;
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
                DataBind();
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='WdCheck.aspx'>百科问答</a></li><li><a href='GuestCateMana.aspx'>留言分类管理</a></li><li class='active'>留言列表 " + RelStr + "</li>");
            }
        }
        public void DataBind(string key = "")
        {
            DataTable dt = B_GuestBook.GetAllTips(DataConverter.CLng(this.HdnCateID.Value), Status);
            if (!string.IsNullOrEmpty(key))
            {
                dt.DefaultView.RowFilter = "Title Like '%" + key + "%'";
                dt = dt.DefaultView.ToTable();
            }
            Egv.DataSource = dt;
            Egv.DataBind();
        }
        protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Egv.PageIndex = e.NewPageIndex;
            DataBind();
        }
        protected void Lnk_Click(object sender, GridViewCommandEventArgs e)
        {
            if (this.Page.IsValid)
            {
                if (e.CommandName == "QList")
                {
                    Response.Redirect("GuestBookShow.aspx?GID=" + e.CommandArgument.ToString());
                }
                if (e.CommandName == "Reply")
                {
                    Response.Redirect("ReplyGuest.aspx?GID=" + e.CommandArgument.ToString());
                }
                if (e.CommandName == "RList")
                {
                    Response.Redirect("ReplyList.aspx?GID=" + e.CommandArgument.ToString());
                }
            }
        }

        protected void btndelete_Click(object sender, EventArgs e)
        {
            btnAll(-1);
            DataBind();
        }

        public string GetCate(string CateID)
        {
            string re = guestBll.GetQuest(DataConverter.CLng(CateID)).Title;
            return re;
        }
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataBind();
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
            DataBind(Key_T.Text.Trim());
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

            this.DataBind();
        }
        protected void Rel_B_Click(object sender, EventArgs e)
        {
            btnAll(0);
        }
        protected void Del_B_Click(object sender, EventArgs e)
        {
            B_GuestBook.DelByIDS(Request.Form["idchk"]);
            this.DataBind();
        }
    }
}