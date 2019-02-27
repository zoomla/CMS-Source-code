namespace ZoomLa.WebSite.Manage.AddOn
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

    public partial class DictionaryManage : CustomerPageAction
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            B_Admin badmin = new B_Admin();
            if (!this.Page.IsPostBack)
            {
                dBind();
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + CustomerPageAction.customPath2 + "plus/ADManage.aspx'>扩展功能</a></li><li><a href='DictionaryManage.aspx'>数据字典</a></li><li class='active'>单级数据字典管理</li>");
            }
        }
        private void dBind()
        {
            this.EGV.DataSource = B_DataDicCategory.GetDicCateList();
            this.EGV.DataKeyNames = new string[] { "DicCateID" };
            this.EGV.DataBind();
        }
        protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.EGV.PageIndex = e.NewPageIndex;
            dBind();
        }
        protected void Gdv_Editing(object sender, GridViewEditEventArgs e)
        {

        }
        protected void EGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string js = string.Format("window.location.href='Dictionary.aspx?CateID=" + EGV.DataKeys[e.Row.RowIndex].Value + "';");
                e.Row.Attributes.Add("ondblclick", js);
            }
        }
        protected void Lnk_Click(object sender, GridViewCommandEventArgs e)
        {
            
                if (e.CommandName == "Del")
                {
                    string Id = e.CommandArgument.ToString();
                    B_DataDicCategory.DelCate(DataConverter.CLng(Id));
                    dBind();
                }
                if (e.CommandName == "Edit")
                {
                    string Id = e.CommandArgument.ToString();
                    M_DicCategory info = B_DataDicCategory.GetDicCate(DataConverter.CLng(Id));
                    this.txtCategoryName.Text = info.CategoryName;
                    this.HdnDicCateID.Value = Id;
                    this.btnSave.Text = "修改";
                }
                if (e.CommandName == "DicList")
                {
                    Response.Redirect("Dictionary.aspx?CateID=" + e.CommandArgument.ToString());
                }
            
        }
        public string GetUsedFlag(string tType)
        {
            bool t = DataConverter.CBool(tType);
            string re = DataConverter.CBool(tType) ? "<font color=green>√</font>" : "<font color=red>×</font>";
            return re;
        }
        protected void btndelete_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.Form["idchk"]))
            {
                B_DataDicCategory.DelCate(Request.Form["idchk"]);
                dBind();
            }
        }
        protected void btnSetUsed_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.Form["idchk"]))
            {
                B_DataDicCategory.SetUsedByArr(Request.Form["idchk"], true);
                dBind();
            }
        }
        protected void btnSetAllUsed_Click(object sender, EventArgs e)
        {
            B_DataDicCategory.SetUsedAll();
            dBind();

        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtCategoryName.Text.Trim()))
            {
                int DicCateID = DataConverter.CLng(this.HdnDicCateID.Value);

                if (DicCateID > 0)
                {
                    M_DicCategory info = B_DataDicCategory.GetDicCate(DicCateID);
                    info.CategoryName = this.txtCategoryName.Text.Trim();
                    B_DataDicCategory.Update(info);
                }
                else
                {
                    M_DicCategory info = new M_DicCategory();
                    info.DicCateID = 0;
                    info.CategoryName = this.txtCategoryName.Text.Trim();
                    info.IsUsed = true;
                    B_DataDicCategory.AddCate(info);
                }
                this.txtCategoryName.Text = "";
                this.HdnDicCateID.Value = "0";
                this.btnSave.Text = "添加";
                dBind();
            }
        }
        protected void btnSetUnUsed_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.Form["idchk"]))
            {
                B_DataDicCategory.SetUsedByArr(Request.Form["idchk"], false);
                dBind();
            }
          
        }
    }
}