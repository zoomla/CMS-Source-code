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

namespace ZoomLaCMS.Manage.Plus
{
    public partial class Survey : CustomerPageAction
    {
        B_Survey surveyBll = new B_Survey();
        private int Sid { get { return DataConverter.CLng(Request.QueryString["Sid"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Sid <= 0)
                {
                    this.txtStartTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    this.txtEndTime.Text = DateTime.Now.AddDays(30).ToString("yyyy-MM-dd");
                    this.ChkIsOpen.Checked = true;
                    this.ChkNeedLogin.Checked = false;
                    this.ChkIsStatus.Checked = false;
                    this.ChkIsCheck.Checked = false;
                    this.ChkIsShow.Checked = false;
                }
                else
                {
                    M_Survey info = surveyBll.GetSurveyBySid(Sid);
                    if (info.IsNull)
                    {
                        function.WriteErrMsg("要修改的问卷投票不存在，可能已被删除！", "../Plus/SurveyManage.aspx");
                    }
                    else
                    {
                        this.TxtSurveyName.Text = info.SurveyName;
                        this.TxtDescription.Text = info.Description;
                        this.txtStartTime.Text = info.StartTime.ToString("yyyy-MM-dd");
                        this.txtEndTime.Text = info.EndTime.ToString("yyyy-MM-dd");
                        this.ChkIsOpen.Checked = info.IsOpen;
                        this.ChkNeedLogin.Checked = info.NeedLogin;
                        this.ChkIsStatus.Checked = info.IsStatus;
                        this.ChkIsCheck.Checked = info.IsCheck;
                        this.ChkIsShow.Checked = info.IsShow == 1;
                        this.RblSurType.SelectedValue = info.SurType.ToString();
                        this.txtIPRepeat.Text = info.IPRepeat.ToString();
                    }
                }
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='ADManage.aspx'>扩展功能</a></li><li><a href='SurveyManage.aspx'>问卷投票列表</a><li class='active'><a href='Survey.aspx'>问卷投票管理</a></li>");
            }
        }
        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            M_Survey info = new M_Survey();
            if (Sid > 0) { info = surveyBll.SelReturnModel(Sid); }
            DateTime startTime = DataConverter.CDate(txtStartTime.Text);
            DateTime endtime = DataConverter.CDate(txtEndTime.Text);
            info.SurveyName = this.TxtSurveyName.Text.Trim();
            info.SurType = DataConverter.CLng(RblSurType.SelectedValue);
            info.Description = this.TxtDescription.Text;
            info.StartTime = startTime;
            info.EndTime = endtime;
            info.NeedLogin = ChkNeedLogin.Checked;
            info.IsOpen = ChkIsOpen.Checked;
            info.IsStatus = ChkIsStatus.Checked;
            info.IsCheck = ChkIsCheck.Checked;
            info.IsShow = ChkIsShow.Checked ? 1 : 0;
            info.IPRepeat = DataConverter.CLng(this.txtIPRepeat.Text.Trim());

            if (endtime <= startTime)
            {
                function.WriteErrMsg("终止时间不能早于开始日期！");
            }
            if (info.SurveyID > 0) { surveyBll.UpdateByID(info); }
            else { surveyBll.insert(info); }
            function.WriteSuccessMsg("操作成功","SurveyManage.aspx");
        }
    }
}