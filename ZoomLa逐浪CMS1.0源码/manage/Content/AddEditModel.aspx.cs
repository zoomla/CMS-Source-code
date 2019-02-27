namespace ZoomLa.WebSite.Manage
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
    using ZoomLa.Web;
    using ZoomLa.Common;

    public partial class AddEditModel : System.Web.UI.Page
    {
        private B_Model bll = new B_Model();
        protected void Page_Load(object sender, EventArgs e)
        {

            
            if (!this.Page.IsPostBack)
            {
                B_Admin badmin = new B_Admin();
                badmin.CheckMulitLogin();
                if (!badmin.ChkPermissions("ModelEdit"))
                {
                    function.WriteErrMsg("没有权限进行此项操作");
                }
                foreach (DataRow row in FileSystemObject.GetDirectoryInfos(HttpContext.Current.Server.MapPath("~/Images/ModelIcon/"), FsoMethod.File).Rows)
                {
                    this.DrpItemIcon.Items.Add(new ListItem(row["name"].ToString(), row["name"].ToString()));
                }
                this.DrpItemIcon.Attributes.Add("onchange", "ChangeImgItemIcon(this.value);ChangeTxtItemIcon(this.value);");
                this.TxtItemIcon.Attributes.Add("onchange", "ChangeImgItemIcon(this.value);");
                string id = base.Request.QueryString["ModelID"];
                if (!string.IsNullOrEmpty(id))
                {
                    this.HdnModelId.Value = id;
                    this.LNav.Text = "修改内容模型";
                    this.LTitle.Text = "修改内容模型";
                    M_ModelInfo info = this.bll.GetModelById(int.Parse(id));
                    this.TxtModelName.Text = info.ModelName;
                    this.LblTablePrefix.Visible = false;
                    this.TxtTableName.Text = info.TableName;
                    this.TxtTableName.Enabled = false;
                    this.TxtItemName.Text = info.ItemName;
                    this.TxtItemUnit.Text = info.ItemUnit;
                    string selectValue = string.IsNullOrEmpty(info.ItemIcon) ? "Default.gif" : info.ItemIcon;
                    this.ImgItemIcon.ImageUrl = "~/Images/ModelIcon/" + selectValue;
                    this.DrpItemIcon.SelectedValue = selectValue;
                    this.TxtItemIcon.Text = info.ItemIcon;
                    this.TxtDescription.Text = info.Description;
                }
                else
                {
                    this.HdnModelId.Value = "0";
                }
            }
        }
        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                M_ModelInfo info = new M_ModelInfo();
                info.ModelID = DataConverter.CLng(this.HdnModelId.Value);
                info.ModelName = this.TxtModelName.Text;
                if (info.ModelID > 0)
                {
                    info.TableName = this.TxtTableName.Text;
                }
                else
                {
                    info.TableName = this.LblTablePrefix.Text + this.TxtTableName.Text;
                }
                info.ItemName = this.TxtItemName.Text;
                info.ItemUnit = this.TxtItemUnit.Text;
                info.ItemIcon = this.TxtItemIcon.Text;
                info.Description = this.TxtDescription.Text;
                info.ModelType = 1;
                if (info.ModelID == 0)
                {
                    if (this.bll.AddModel(info))
                        Response.Redirect("ModelManage.aspx");
                }
                else
                {
                    if (this.bll.UpdateModel(info))
                        Response.Redirect("ModelManage.aspx");
                }
            }
        }

    }
}