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
        protected void Page_Load(object sender, EventArgs e)
        {
            ZoomLa.Common.function.AccessRulo();
            if (!this.Page.IsPostBack)
            {
                int SID = string.IsNullOrEmpty(Request.QueryString["SID"]) ? 0 : DataConverter.CLng(Request.QueryString["SID"]);
                if (SID <= 0)
                {
                    this.HdnSurveyID.Value = "0";
                    this.txtStartTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    this.txtEndTime.Text = DateTime.Now.AddDays(30).ToString("yyyy-MM-dd");
                    this.ChkIsOpen.Checked = true;
                    this.ChkNeedLogin.Checked = false;
                    this.ChkIsStatus.Checked = false;
                    this.ChkIsCheck.Checked = false;
                    this.ChkIsShow.Checked = false;
                    this.LTitle.Text = "添加问卷投票";
                }
                else
                {
                    M_Survey info = surveyBll.GetSurveyBySid(SID);
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
                        this.HdnSurveyID.Value = info.SurveyID.ToString();
                        this.ChkIsOpen.Checked = info.IsOpen;
                        this.ChkNeedLogin.Checked = info.NeedLogin;
                        this.ChkIsStatus.Checked = info.IsStatus;
                        this.ChkIsCheck.Checked = info.IsCheck;
                        this.ChkIsShow.Checked = info.IsShow == 1;
                        this.RblSurType.SelectedValue = info.SurType.ToString();
                        this.txtIPRepeat.Text = info.IPRepeat.ToString();
                        this.LTitle.Text = "修改问卷投票";
                    }
                }
            }
            if (RblSurType.SelectedValue == "3")
            {
                txtIPRepeat.ReadOnly = true;
                txtIPRepeat.ToolTip = "报名系统，IP限制为默认，不可改变";
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='ADManage.aspx'>扩展功能</a></li><li><a href='SurveyManage.aspx'>问卷投票管理</a><li class='active'><a href='Survey.aspx'>编辑问卷投票项目</a></li>");
        }
        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                if (string.IsNullOrEmpty(this.txtEndTime.Text) || string.IsNullOrEmpty(txtStartTime.Text))
                {
                    function.WriteErrMsg("起止时间不能为空！");
                }
                DateTime startTime = DataConverter.CDate(txtStartTime.Text);
                DateTime endtime = DataConverter.CDate(this.txtEndTime.Text);
                if (endtime <= startTime)
                {
                    function.WriteErrMsg("终止时间不能早于开始日期！");
                }
                int SID = DataConverter.CLng(this.HdnSurveyID.Value);
                if (SID == 0)
                {
                    if (B_Survey.IsExistSur(this.TxtSurveyName.Text.Trim()))
                    {
                        function.WriteErrMsg("已存在同名的问卷调查或投票！");
                    }
                    M_Survey info = new M_Survey();
                    info.SurveyID = 0;
                    info.SurveyName = this.TxtSurveyName.Text.Trim();
                    info.SurType = DataConverter.CLng(this.RblSurType.SelectedValue);
                    info.Description = this.TxtDescription.Text;
                    info.StartTime = startTime;
                    info.EndTime = endtime;
                    if (this.ChkNeedLogin.Checked == true)
                        info.NeedLogin = true;
                    else info.NeedLogin = false;
                    if (this.ChkIsOpen.Checked == true)
                        info.IsOpen = true;
                    else info.IsOpen = false;
                    if (this.ChkIsStatus.Checked == true)
                        info.IsStatus = true;
                    else info.IsStatus = false;
                    if (this.ChkIsCheck.Checked == true)
                        info.IsCheck = true;
                    else info.IsCheck = false;
                    if (this.ChkIsShow.Checked == true)
                        info.IsShow = 1;
                    else
                        info.IsShow = 0;
                    info.IPRepeat = DataConverter.CLng(this.txtIPRepeat.Text.Trim());
                    info.CreateDate = DateTime.Now;
                    if (surveyBll.AddSurvey(info))
                    {
                        function.WriteSuccessMsg("添加成功！", "SurveyManage.aspx");
                    }
                }
                else
                {
                    M_Survey info = surveyBll.GetSurveyBySid(SID);
                    if (!info.IsNull)
                    {
                        info.SurveyName = this.TxtSurveyName.Text.Trim();
                        info.SurType = DataConverter.CLng(this.RblSurType.SelectedValue);
                        info.Description = this.TxtDescription.Text;
                        info.StartTime = startTime;
                        info.EndTime = endtime;
                        info.IPRepeat = DataConverter.CLng(this.txtIPRepeat.Text.Trim());
                        if (this.ChkNeedLogin.Checked == true)
                            info.NeedLogin = true;
                        else info.NeedLogin = false;
                        if (this.ChkIsOpen.Checked == true)
                            info.IsOpen = true;
                        else info.IsOpen = false;
                        if (this.ChkIsStatus.Checked == true)
                            info.IsStatus = true;
                        else info.IsStatus = false;
                        if (this.ChkIsCheck.Checked == true)
                            info.IsCheck = true;
                        else info.IsCheck = false;
                        if (this.ChkIsShow.Checked == true)
                            info.IsShow = 1;
                        else
                            info.IsShow = 0;
                        if (surveyBll.UpdateSurvey(info))
                        {
                            function.WriteSuccessMsg("修改成功!", "SurveyManage.aspx");
                        }
                    }
                    else
                    {
                        function.WriteErrMsg("要修改的问卷投票不存在，可能已被删除！", "../Plus/SurveyManage.aspx");
                    }
                }
            }
        }
    }
}