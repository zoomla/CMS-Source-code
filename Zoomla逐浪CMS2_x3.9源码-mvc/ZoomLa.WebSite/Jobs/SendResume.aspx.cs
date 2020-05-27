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

namespace ZoomLaCMS.Jobs
{
    public partial class SendResume : System.Web.UI.Page
    {
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
                    B_User buser = new B_User();
                    B_Model bmod = new B_Model();
                    if (!buser.CheckLogin())
                        function.WriteErrMsg("请先登录");
                    else
                    {
                        M_UserInfo uinfo = buser.GetLogin();
                        int ResumeID = UserModuleConfig.JobsConfig.Resume;//简历模型ID
                        if (ResumeID <= 0)
                            function.WriteErrMsg("简历模型没有设定，可能此功能未开放");
                        int JobsID = DataConverter.CLng(Request.QueryString["JobID"]); //招聘信息ID
                        if (JobsID <= 0)
                        {
                            function.WriteErrMsg("缺少必要参数!请使用 JobID=招聘信息ID ");
                        }
                        else
                        {
                            int UserReID = buser.UserModeInfoID(bmod.GetModelById(ResumeID).TableName, uinfo.UserID);//当前用户简历ID
                            if (UserReID > 0)
                            {
                                int JobID = UserModuleConfig.JobsConfig.CompanyJobs; //招聘信息模型ID

                                DataTable JobData = buser.GetUserModeInfo(bmod.GetModelById(JobID).TableName, JobsID, 1);
                                if (JobData != null && JobData.Rows.Count > 0)
                                {
                                    DataRow dr = JobData.Rows[0];
                                    int CompanyUser = DataConverter.CLng(dr["UserID"]); //企业用户ID
                                    //判断是否已经向当前招聘信息投递过简历，没有则投递简历
                                    if (!buser.IsExitResume(CompanyUser, UserReID))
                                    {
                                        //投递简历
                                        buser.AddPostResume(JobsID, CompanyUser, UserReID, uinfo.UserID, DateTime.Now);
                                        function.WriteSuccessMsg("你的简历已投递成功，请耐心等待招聘单位的通知！", "javascript:window.close();");
                                    }
                                    else
                                    {
                                        function.WriteErrMsg("你已向该招聘单位投递过简历，没必要重复投递简历！", "javascript:window.close();");
                                    }
                                }
                                else
                                {
                                    function.WriteErrMsg("没有找到此职位");
                                }
                            }
                            else
                            {
                                function.WriteErrMsg("你还没有填写简历");
                            }
                        }
                    }
                }
            }
        }
    }
}