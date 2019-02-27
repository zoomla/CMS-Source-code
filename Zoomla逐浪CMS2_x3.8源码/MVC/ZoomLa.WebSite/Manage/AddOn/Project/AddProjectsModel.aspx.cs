namespace ZoomLaCMS.Manage.AddOn.Project
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Data;
    using ZoomLa.BLL;
    using ZoomLa.Model;
    using ZoomLa.Common;
    using ZoomLa.Components;
    using System.IO;
    public partial class AddProjectsModel : System.Web.UI.Page
    {
        private B_Model bll = new B_Model();
        private B_ModelField bmf = new B_ModelField();
        protected void Page_Load(object sender, EventArgs e)
        {
            function.AccessRulo();
            B_Admin badmin = new B_Admin();
            B_Admin.CheckIsLogged();
            Call.SetBreadCrumb(Master, "<li>后台管理</li><li>CRM应用</li><li><a href='ProjectsModel.aspx'>项目模型</a></li>");
            if (!this.Page.IsPostBack)
            {
                if (!File.Exists(HttpContext.Current.Server.MapPath("~/Images/ModelIcon/"))) { FileSystemObject.Create(HttpContext.Current.Server.MapPath("~/Images/ModelIcon/"), FsoMethod.Folder); }
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
                    this.LTitle.Text = "修改项目模型";
                    M_ModelInfo info = this.bll.GetModelById(int.Parse(id));
                    this.TxtModelName.Text = info.ModelName;
                    this.LblTablePrefix.Visible = false;
                    this.TxtTableName.Text = info.TableName;
                    this.TxtTableName.Enabled = false;
                    this.TxtItemName.Text = info.ItemName;
                    this.TxtItemUnit.Text = info.ItemUnit;
                    string selectValue = string.IsNullOrEmpty(info.ItemIcon) ? "Default.gif" : info.ItemIcon;
                    //this.ImgItemIcon.ImageUrl = "~/Images/ModelIcon/" + selectValue;
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
                info.ModelType = 8;
                info.MultiFlag = true;
                if (info.ModelID == 0)
                {
                    this.bll.AddModel(info);
                }
                else
                {
                    this.bll.UpdateModel(info);
                }
                function.WriteSuccessMsg("操作成功", "ProjectsModel.aspx");
            }
        }
    }
}