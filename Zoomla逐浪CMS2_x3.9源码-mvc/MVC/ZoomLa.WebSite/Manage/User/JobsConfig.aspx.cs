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
using ZoomLa.Components;
using System.Xml;
namespace ZoomLaCMS.Manage.User
{
    public partial class JobsConfig : CustomerPageAction
    {
        B_Model mdll = new B_Model();
        B_Group gll = new B_Group();
        protected void Page_Load(object sender, EventArgs e)
        {
            ZoomLa.Common.function.AccessRulo();
            B_Admin badmin = new B_Admin();
            badmin.CheckIsLogin();
            if (!this.Page.IsPostBack)
            {
                this.Isuse.SelectedValue = UserModuleConfig.JobsConfig.IsUsed ? "1" : "0";


                DataTable glist = gll.GetGroupList();
                userlist.DataSource = glist;
                userlist.DataValueField = "GroupID";
                userlist.DataTextField = "GroupName";
                userlist.DataBind();

                comlist.DataSource = glist;
                comlist.DataValueField = "GroupID";
                comlist.DataTextField = "GroupName";
                comlist.DataBind();

                userlist.Items.Insert(0, new ListItem("请选择", "0"));
                comlist.Items.Insert(0, new ListItem("请选择", "0"));

                userlist.SelectedValue = UserModuleConfig.JobsConfig.PersonGroup.ToString();
                comlist.SelectedValue = UserModuleConfig.JobsConfig.CompanyGroup.ToString();

                Resume.DataSource = mdll.GetListUser();
                Resume.DataTextField = "ModelName";
                Resume.DataValueField = "ModelID";
                Resume.DataBind();
                Resume.Items.Insert(0, new ListItem("请选择用户简历模型", "0"));

                Company.DataSource = mdll.GetListUser();
                Company.DataTextField = "ModelName";
                Company.DataValueField = "ModelID";
                Company.DataBind();
                Company.Items.Insert(0, new ListItem("请选择企业信息模型", "0"));

                CompanyJobs.DataSource = mdll.GetListUser();
                CompanyJobs.DataTextField = "ModelName";
                CompanyJobs.DataValueField = "ModelID";
                CompanyJobs.DataBind();
                CompanyJobs.Items.Insert(0, new ListItem("请选择招聘信息模型", "0"));

                Resume.SelectedValue = UserModuleConfig.JobsConfig.Resume.ToString();
                Company.SelectedValue = UserModuleConfig.JobsConfig.Company.ToString();
                BindDrop(UserModuleConfig.JobsConfig.Company, 0);
                if (UserModuleConfig.JobsConfig.CompanyField > 0)
                    CompanyField.SelectedValue = UserModuleConfig.JobsConfig.CompanyField.ToString();

                CompanyJobs.SelectedValue = UserModuleConfig.JobsConfig.CompanyJobs.ToString();
                BindDrop(UserModuleConfig.JobsConfig.CompanyJobs, 1);
                if (UserModuleConfig.JobsConfig.JobsField > 0)
                    JobsField.SelectedValue = UserModuleConfig.JobsConfig.JobsField.ToString();

                TxtConsumePoint.Text = UserModuleConfig.JobsConfig.ConsumePoint.ToString();
                ConsumeType.SelectedValue = UserModuleConfig.JobsConfig.ConsumeType.ToString();
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='UserManage.aspx'>用户管理</a></li><li><a href='Jobsconfig.aspx'>人才招聘</a></li><li>人才模版配置</li>");
        }


        protected void Button1_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 绑定字段选择下拉框
        /// </summary>
        /// <param name="ModelID">字段所属模型ID</param>
        /// <param name="type">
        /// 方式:
        /// 0=企业信息显示字段
        /// 1=招聘信息职位字段
        /// </param>
        private void BindDrop(int ModelID, int type)
        {
            B_ModelField bfield = new B_ModelField();
            DataTable dt = bfield.GetModelFieldList(ModelID);

            if (type == 0)
            {
                this.CompanyField.Items.Clear();
                this.CompanyField.DataSource = dt;
                this.CompanyField.DataTextField = "FieldAlias";
                this.CompanyField.DataValueField = "FieldID";
                this.CompanyField.DataBind();
                CompanyField.Items.Insert(0, new ListItem("选择字段", "0"));
            }
            else
            {
                this.JobsField.Items.Clear();
                this.JobsField.DataSource = dt;
                this.JobsField.DataTextField = "FieldAlias";
                this.JobsField.DataValueField = "FieldID";
                this.JobsField.DataBind();
                JobsField.Items.Insert(0, new ListItem("选择字段", "0"));
                //JobsField.SelectedValue = "0";
            }
        }

        protected void Company_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ModelID = DataConverter.CLng(this.Company.SelectedValue);
            if (ModelID > 0)
            {
                BindDrop(ModelID, 0);
            }
        }
        protected void CompanyJobs_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ModelID = DataConverter.CLng(this.CompanyJobs.SelectedValue);
            if (ModelID > 0)
            {
                BindDrop(ModelID, 1);
            }
        }
    }
}