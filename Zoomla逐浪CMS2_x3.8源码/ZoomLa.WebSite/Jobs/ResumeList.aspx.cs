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
namespace ZoomLa.WebSite.Jobs
{
    public partial class ResumeList : System.Web.UI.Page
    {
        private B_User buser = new B_User();
        private B_Model bmod = new B_Model();
        public M_UserInfo uinfo = new M_UserInfo();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (!UserModuleConfig.JobsConfig.IsUsed)
                {
                    function.WriteErrMsg("人才招聘模块没有开放！");
                }
                else
                {
                    if (!buser.CheckLogin())
                        function.WriteErrMsg("请先登录");
                    else
                    {
                        uinfo = buser.GetLogin();
                        this.lblUserName.Text = uinfo.UserName;
                        this.LblSiteName.Text = SiteConfig.SiteInfo.SiteName;
                        RepNodeBind();
                    }
                }
            }
        }
        private void RepNodeBind()
        {
            M_UserInfo uinfo = buser.GetLogin();
            this.Egv.DataSource = this.buser.GetResumeList(uinfo.UserID);
            this.Egv.DataKeyNames = new string[] { "CID" };
            this.Egv.DataBind();
        }
        protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.Egv.PageIndex = e.NewPageIndex;
            this.RepNodeBind();
        }
        protected void Lnk_Click(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Show")
                Page.Response.Redirect("/User/Info/ShowModel.aspx?ModelID=" + UserModuleConfig.JobsConfig.Resume.ToString() + "&ID=" + e.CommandArgument.ToString());
            if (e.CommandName == "Del")
            {                
                buser.DelResumePost(DataConverter.CLng(e.CommandArgument.ToString()));
                RepNodeBind();
            }
        }
        public string GetUser(string UserID)
        {
            return buser.GetUserByUserID(DataConverter.CLng(UserID)).UserName;
        }
        public string GetJob(string JobID)
        {
            B_ModelField bf = new B_ModelField();
            int JobModel = UserModuleConfig.JobsConfig.CompanyJobs;

            int JobField = UserModuleConfig.JobsConfig.JobsField;
            string fname = bf.GetModelByID(JobModel.ToString(),JobField).FieldName;
            DataTable JobData = buser.GetUserModeInfo(bmod.GetModelById(JobModel).TableName, DataConverter.CLng(JobID), 1);
            DataRow dr = JobData.Rows[0];

            return dr["" + fname].ToString();
        }
    }
}