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
using System.Text;
using Newtonsoft.Json;

namespace ZoomLaCMS.Manage.Plus
{
    public partial class SurveyItem : CustomerPageAction
    {
        protected B_Survey surBll = new B_Survey();
        public int Sid { get { return DataConverter.CLng(Request.QueryString["Sid"]); } }
        public int Qid { get { return DataConverter.CLng(Request.QueryString["Qid"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Sid <= 0) { function.WriteErrMsg("缺少问卷投票的ID参数！", "SurveyManage.aspx"); }
                M_Survey surMod = surBll.GetSurveyBySid(Sid);
                if (surMod.IsNull) { function.WriteErrMsg("该问卷投票不存在！可能已被删除", "SurveyManage.aspx"); }
                this.Label1.Text = surMod.SurveyName;
                if (Qid > 0)
                {
                    this.ltlAction.Text = "修改";
                    this.EBtnSubmit.Text = "修改";
                    MyBind();
                }
                Call.SetBreadCrumb(Master, "<li><a href='" + customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='SurveyManage.aspx'>问卷投票管理</a></li><li><a href='Survey.aspx?SID=" + surMod.SurveyID + "'>" + surMod.SurveyName + "</a></li><li><a href='SurveyItemList.aspx?SID=" + surMod.SurveyID + "'>问卷投票问题列表</a></li><li class='active'><a href='SurveyItem.aspx'>添加问卷投票问题</a></li>");
            }
        }
        protected void MyBind()
        {

            M_Question qinfo = B_Survey.GetQuestion(Qid);
            if (qinfo != null)
            {
                NotNull.SelectedValue = qinfo.IsNull ? "1" : "0";
                TxtQTitle.Text = qinfo.QuestionTitle;
                Content_T.Text = qinfo.QuestionContent;
                Option_Hid.Value = qinfo.Qoption;
                function.Script(this, "SetRadVal('type_rad'," + qinfo.TypeID + ");");
            }
        }
        //保存
        protected void Button_Click(object sender, EventArgs e)
        {
            M_Question qinfo = new M_Question();
            M_QuestOption option = new M_QuestOption();
            option.FillByForm(Request);
            if (Qid > 0)
            {
                qinfo = B_Survey.GetQuestion(Qid);
            }
            qinfo.QuestionTitle = this.TxtQTitle.Text.Trim();
            qinfo.TypeID = Convert.ToInt32(Request.Form["type_rad"]);
            qinfo.IsNull = Convert.ToBoolean(Convert.ToInt32(NotNull.SelectedValue));
            qinfo.QuestionContent = Content_T.Text;
            qinfo.Qoption = JsonConvert.SerializeObject(option);
            if (Qid > 0)
            {
                B_Survey.UpdateQuestion(qinfo);
            }
            else
            {
                qinfo.SurveyID = Sid;
                qinfo.OrderID = B_Survey.GetMaxOrderID(Sid) + 1;
                int qid = B_Survey.AddQuestion(qinfo);
            }
            function.WriteSuccessMsg("操作成功", "SurveyItemList.aspx?SID=" + Sid);
        }
    }
}