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

    public partial class GradeCateManage : CustomerPageAction
    {
        B_Admin badmin = new B_Admin();
        B_GradeOption gradeBll = new B_GradeOption();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dBind();
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + CustomerPageAction.customPath2 + "plus/ADManage.aspx'>扩展功能</a></li><li class='active'><a href='"+Request.RawUrl+"'>多级数据字典管理</a></li>");
            }
        }
        private void dBind()
        {
            this.EGV.DataSource = B_GradeOption.GetCateList();
            this.EGV.DataKeyNames = new string[] { "CateID" };
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
        protected void Gdv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row!=null && e.Row.RowType == DataControlRowType.DataRow)
            {
                string js = string.Format("window.location.href='GradeOption.aspx?CateID=" + EGV.DataKeys[e.Row.RowIndex].Value + "';");
                e.Row.Attributes.Add("ondblclick", js); 
            }
        }
        protected void Lnk_Click(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Del")
            {
                string Id = e.CommandArgument.ToString();
                gradeBll.DelCate(DataConverter.CLng(Id));
                dBind();
            }
            if (e.CommandName == "Edit")
            {
                string Id = e.CommandArgument.ToString();
                M_GradeCate info = gradeBll.GetCate(DataConverter.CLng(Id));
                this.txtCateName.Text = info.CateName;
                this.txtRemark.Text = info.Remark;
                this.txtGradeField.Text = info.GradeAlias.Replace("|", "\r\n");
                this.HdnCateID.Value = Id;
                this.btnSave.Text = "修改";
            }
            if (e.CommandName == "DicList")
            {
                Response.Redirect("GradeOption.aspx?CateID=" + e.CommandArgument.ToString());
            }

        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string CateName = this.txtCateName.Text.Trim();
            string GradeAlias = this.txtGradeField.Text.Trim();
            if (string.IsNullOrEmpty(CateName))
            {
                function.WriteErrMsg("分类名称不能为空！");
            }
            else
            {
                if (string.IsNullOrEmpty(GradeAlias))
                {
                    function.WriteErrMsg("分级选项别名不能为空！");
                }
                else
                {
                    GradeAlias = GradeAlias.Replace("\r\n", "|");
                    string[] AliasArr = GradeAlias.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                    if (AliasArr.Length < 2)
                    {
                        function.WriteErrMsg("分级选项别名不能少于2个！");
                    }
                    else
                    {
                        int CateID = DataConverter.CLng(this.HdnCateID.Value);
                        if (CateID > 0)
                        {
                            M_GradeCate info = gradeBll.GetCate(CateID);
                            info.CateID = CateID;
                            info.CateName = CateName;
                            info.Remark = this.txtRemark.Text.Trim();
                            info.GradeAlias = GradeAlias;
                            B_GradeOption.UpdateCate(info);
                        }
                        else
                        {
                            M_GradeCate info = new M_GradeCate();
                            info.CateID = 0;
                            info.CateName = CateName;
                            info.Remark = this.txtRemark.Text.Trim();
                            info.GradeAlias = GradeAlias;
                            B_GradeOption.AddCate(info);
                        }
                        this.txtCateName.Text = "";
                        this.txtRemark.Text = "";
                        this.txtGradeField.Text = "";
                        this.HdnCateID.Value = "0";
                        this.btnSave.Text = "添加";
                        dBind();
                    }
                }
            }            
        }
        protected void BatDel_Btn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.Form["idchk"]))
            {
                gradeBll.DelByIDS(Request.Form["idchk"]);
                Response.Redirect(Request.RawUrl);
            }
        }
    }
}