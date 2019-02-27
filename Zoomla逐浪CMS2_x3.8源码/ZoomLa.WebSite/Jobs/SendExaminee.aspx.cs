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
    public partial class SendExaminee : System.Web.UI.Page
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
                        int ResumeID = DataConverter.CLng(Request.QueryString["ID"]); //简历ID
                        if (ResumeID <= 0)
                        {
                            function.WriteErrMsg("缺少必要参数");
                        }
                        else
                        {
                            int ComModelID = UserModuleConfig.JobsConfig.Company;
                            if (ComModelID <= 0)
                                function.WriteErrMsg("企业信息模型没有设定，可能此功能未开放");
                            int ComID = buser.UserModeInfoID(bmod.GetModelById(ComModelID).TableName, uinfo.UserID); //企业信息ID;
                            if (ComID > 0)
                            {
                                DataTable ResumeData = buser.GetUserModeInfo(bmod.GetModelById(UserModuleConfig.JobsConfig.Resume).TableName, ResumeID, 1);
                                DataRow dr = ResumeData.Rows[0];
                                int ResumUser = DataConverter.CLng(dr["UserID"]); //个人用户ID


                                //判断是否已经向当前简历所有人发送过面试通知
                                if (!buser.IsExitExaminee(ResumUser, ComID))
                                {
                                    //发送面试通知
                                    buser.AddPostExaminee(ResumUser, ComID, DateTime.Now);
                                    DataTable ComData = buser.GetUserModeInfo(bmod.GetModelById(ComModelID).TableName, ComID, 1);
                                    B_ModelField bf = new B_ModelField();
                                    int CompanyField = UserModuleConfig.JobsConfig.CompanyField;
                                    string fname = bf.GetModelByID(ComModelID.ToString(),CompanyField).FieldName;
                                    dr = ComData.Rows[0];
                                    string ComName = dr["" + fname].ToString();

                                }
                                else
                                {
                                    function.WriteErrMsg("你已向该用户发送过面试通知！", "javascript:window.close();");
                                }
                            }
                            else
                            {
                                function.WriteErrMsg("你还没有填写贵单位的信息");
                            }
                        }
                    }
                }
            }
        }
    }
}