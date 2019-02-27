namespace ZoomLa.WebSite.Manage.Page
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
    using ZoomLa.Model;
    using ZoomLa.Common;

    public partial class UserModel : CustomerPageAction
    {
        private B_Model bll = new B_Model();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                B_Admin badmin = new B_Admin();
                if (!B_ARoleAuth.Check(ZLEnum.Auth.page, "PageModel"))
                {
                    function.WriteErrMsg("没有权限进行此项操作");
                }
                string id = base.Request.QueryString["ModelID"];
                if (!string.IsNullOrEmpty(id))
                {
                    this.HdnModelId.Value = id;
                }
                else
                {
                    this.HdnModelId.Value = "0";
                    id = "0";
                }
                if (id != "0")
                {
                    this.LTitle.Text = "修改申请样式";
                    M_ModelInfo info = this.bll.GetModelById(DataConverter.CLng(id));
                    this.TxtModelName.Text = info.ModelName;
                    this.LblTablePrefix.Text = "";
                    this.LblTablePrefix.Visible = false;
                    this.TxtTableName.Text = info.TableName;
                    this.TxtDescription.Text = info.Description;
                    this.TxtTableName.Enabled = false;
                }
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='PageManage.aspx'>企业黄页</a></li><li class='active'>添加申请模型</li>");
        }
        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                M_ModelInfo info = new M_ModelInfo();
                info.ModelID = DataConverter.CLng(this.HdnModelId.Value);
                info.ModelName = this.TxtModelName.Text.Trim();
                if (info.ModelID > 0)
                    info.TableName = this.TxtTableName.Text.Trim();
                else
                    info.TableName = this.LblTablePrefix.Text + this.TxtTableName.Text.Trim();
                info.Description = this.TxtDescription.Text.Trim();
                info.ModelType = 10;
                info.ItemName = "";
                info.ItemIcon = "";
                info.ItemUnit = "";
                info.ContentModule = "";
                if (info.ModelID > 0)
                    this.bll.UpdateModel(info);
                else
                    this.bll.AddPageModel(info);
                Response.Redirect("UserModelManage.aspx");
            }
        }
    }
}