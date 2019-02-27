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
    using System.Data.SqlClient;
    using ZoomLa.BLL;
    using ZoomLa.Common;
    using ZoomLa.Model;
    using ZoomLa.SQLDAL;

    public partial class ReplyList : System.Web.UI.Page
    {
        public string cateName = "";
        B_GuestBookCate cateBll = new B_GuestBookCate();
        protected M_GuestBookCate guestMod = new M_GuestBookCate();

        protected void Page_Load(object sender, EventArgs e)
        {
            string str = "分类名称";
            B_Admin badmin = new B_Admin();
            Egv.txtFunc = txtPageFunc;
            if (!this.Page.IsPostBack)
            {
                int CateID = string.IsNullOrEmpty(Request.QueryString["CateID"]) ? 0 : DataConverter.CLng(Request.QueryString["CateID"]);
                if (CateID <= 0)
                    function.WriteErrMsg("缺少留言分类ID", "../Plus/GuestCateMana.aspx");
                else
                    this.HdnCateID.Value = CateID.ToString();
                guestMod = cateBll.SelReturnModel(Convert.ToInt32(Request.QueryString["CateID"]));
                if (guestMod != null)
                    str = guestMod.CateName;
                cateName = ">>" + guestMod.CateName;
                DataBind();
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li></li><li><a href='GuestManage.aspx'>留言管理</a></li><li><a href='Default.aspx?CateID=" + Request.QueryString["CateID"] + "'>" + str + "</a></li><li class='active'>留言回复列表</li>");
        }

        public static DataTable GetAllReply(int GID)
        {
            string strSql = "select * from ZL_GuestBook where ParentID=@GID order by GDate ASC";
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("@GID", SqlDbType.Int) };
            sp[0].Value = GID;
            return SqlHelper.ExecuteTable(CommandType.Text, strSql, sp);
        }

        public void DataBind(string key = "")
        {
            int GID = Convert.ToInt32(Request.QueryString["GID"]);
            DataTable dt = GetAllReply(GID);
            Egv.DataSource = dt;
            Egv.DataBind();
        }
        protected void txtPageFunc(string size)
        {
            int pageSize;
            if (!int.TryParse(size, out pageSize))//如果转换失败,即不是一个数字时
            {
                pageSize = Egv.PageSize;
            }
            else if (pageSize < 1)//小于1时,均恢复默认PageSize,默认PageSize是你给序的
            {
                pageSize = Egv.PageSize;
            }
            Egv.PageSize = pageSize;
            Egv.PageIndex = 0;//改变后回到首页
            size = pageSize.ToString();
            DataBind();
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
                if (e.CommandName == "Del")
                {
                    string Id = e.CommandArgument.ToString();
                    B_GuestBook.DelTips(DataConverter.CLng(Id));
                    DataBind();
                }
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
            string[] chkArr = GetChecked();
            if (chkArr != null)
            {
                for (int i = 0; i < chkArr.Length; i++)
                {
                    int itemID = Convert.ToInt32(chkArr[i]);
                    B_GuestBook.DelTips(itemID);
                }
            }
            DataBind();
        }

        public string GetCate(string CateID)
        {
            string re = cateBll.SelReturnModel(DataConverter.CLng(CateID)).CateName;
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
    }
}