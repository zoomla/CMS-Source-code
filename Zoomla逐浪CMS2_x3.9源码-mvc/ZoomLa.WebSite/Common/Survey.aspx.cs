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
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ZoomLaCMS.Common
{
    public partial class Survey : System.Web.UI.Page
    {
        private B_User buser = new B_User();
        B_Survey surveyBll = new B_Survey();
        private int Sid { get { return DataConverter.CLng(Request.QueryString["Sid"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Sid <= 0) { function.WriteErrMsg("缺少问卷的ID参数！"); }
                M_Survey info = surveyBll.GetSurveyBySid(Sid);
                if (info == null) { function.WriteErrMsg("该问卷不存在！可能已被删除"); }
                if (info.EndTime < DateTime.Now) { function.WriteErrMsg("该问卷已过期!"); }
                if (!B_Survey.HasQuestion(Sid)) { function.WriteErrMsg("该问卷没有设定问卷问题!"); }
                MyBind();
            }
        }
        public void MyBind()
        {
            M_Survey info = surveyBll.GetSurveyBySid(Sid);
            STitle_T.InnerText = info.SurveyName;
            DataTable dt = B_Survey.GetQuestionList(Sid);
            RPT.DataSource = dt;
            RPT.DataBind();
            //list = B_Survey.GetQueList(Sid);
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            M_Survey info = surveyBll.GetSurveyBySid(Sid);
            M_UserInfo mu = buser.GetLogin();
            string SIP = EnviorHelper.GetUserIP();
            if (info.NeedLogin && mu == null || mu.UserID < 1) { function.WriteErrMsg("该问卷需登录才能参与问卷,请先登录!"); }
            if (B_Survey.HasAnswerBySID(Sid, mu.UserID)) { function.WriteErrMsg("您已提交过该问卷！"); }
            if (info.IPRepeat > 0 && B_Survey.HasAnswerCountIP(Sid, SIP) >= info.IPRepeat)
            {
                function.WriteErrMsg("于IP：" + SIP + " 提交的问卷次数已达到限定次数：" + info.IPRepeat.ToString() + "次!");
            }
            //--------------------------
            IList<M_Question> list = B_Survey.GetQueList(Sid);
            for (int i = 0; i < list.Count; i++)
            {
                M_Answer anMod = new M_Answer();
                anMod.AnswerContent = Request.Form["result_" + list[i].QuestionID];
                anMod.QuestionID = list[i].QuestionID;
                anMod.SurveyID = Sid;
                anMod.SubmitIP = SIP;
                anMod.UserID = mu.UserID;
                B_Survey.AddAnswer(anMod);
            }
            if (B_Survey.AddAnswerRecord(Sid, mu.UserID, SIP, DateTime.Now, 0))
            { function.WriteSuccessMsg("提交成功！感谢您的参与!"); }
            //for (int i = 0; i < list.Count; i++)
            //{
            //    M_Answer ans = new M_Answer();
            //    string re = Request.Form["txt_" + i];
            //    string[] OptionValue = list[i].QuestionContent.Split(new char[] { '|' });
            //    if (string.IsNullOrEmpty(re))
            //    {
            //        re = OptionValue[0];
            //    }

            //    if (list[i].TypeID == 1)
            //    {
            //        ans.AnswerID = 0;
            //        ans.AnswerContent = re;
            //        ans.QuestionID = list[i].QuestionID;
            //        ans.SurveyID = Sid;
            //        ans.SubmitIP = SIP;
            //        ans.UserID = mu.UserID;
            //        B_Survey.AddAnswer(ans);
            //    }
            //    else
            //    {
            //        string[] ReArr = re.Split(new char[] { ',' });
            //        for (int s = 0; s < ReArr.Length; s++)
            //        {
            //            ans.AnswerID = 0;
            //            ans.AnswerContent = ReArr[s];
            //            ans.QuestionID = list[i].QuestionID;
            //            ans.SurveyID = Sid;
            //            ans.SubmitIP = SIP;
            //            ans.UserID = mu.UserID;
            //            B_Survey.AddAnswer(ans);
            //        }
            //    }
            //}
        }
        //---------------------Front Page
        public string GetRules()
        {
            string result = "";
            result += (Eval("IsNull", "") == "1") ? "必填," : "";
            M_QuestOption option = JsonConvert.DeserializeObject<M_QuestOption>(Eval("Qoption", ""));
            switch (DataConverter.CLng(Eval("TypeID")))
            {
                case 0://选择
                    result += option.Sel_Type_Rad_Str + ",";
                    break;
                case 1://填空
                    //必填, 至少1字, 只允许填写数字)
                    if (option.text_str_dp != "none")
                    {
                        result += "只允许" + option.Text_Str_DP_Str + ",";
                    }
                    break;
            }
            return result.TrimEnd(',');
        }
        public string GetResult()
        {
            int qid = Convert.ToInt32(Eval("Qid"));
            string textTlp = "<input type='text' class='{0} form-control text_300 result_text' name='result_" + qid + "' />";
            string areaTlp = "<textarea class='{0} form-control result_area'  name='result_" + qid + "'/>";
            string radTlp = "<label><input type='radio'  name='result_" + qid + "' value='{0}' />{0}</label>";
            string checkboxTlp = "<label><input type='checkbox'  name='result_" + qid + "' value='{0}'/>{0}</label>";
            string optionTlp = "<option value='{0}'>{0}</option>";
            string result = "";
            M_QuestOption option = JsonConvert.DeserializeObject<M_QuestOption>(Eval("Qoption", ""));
            switch (DataConverter.CLng(Eval("TypeID")))
            {
                case 0:
                    switch (option.sel_type_rad)
                    {
                        case "radio":
                            result = AddSelTlp(option, radTlp);
                            break;
                        case "checkbox":
                            result = AddSelTlp(option, checkboxTlp);
                            break;
                        case "select":
                            result = "<select  name='result_" + qid + "'>" + AddSelTlp(option, optionTlp) + "</select>";
                            break;
                    }
                    result = "<div class='result_div num_" + option.sel_num_dp + "'>" + result + "</div>";
                    break;
                case 1:
                    switch (option.text_type_rad)
                    {
                        case "text":
                            result = textTlp;
                            break;
                        case "textarea":
                            result = areaTlp;
                            break;
                    }
                    result = string.Format(result, option.text_str_dp);
                    break;
            }
            return result;
        }
        //---------------------Tools
        //根据配置,添加多选或单选等
        private string AddSelTlp(M_QuestOption option, string tlp)
        {
            string result = "";
            foreach (string val in option.sel_op_body.Split(','))
            {
                //如何决定每行的数量,用width?
                result += string.Format(tlp, val);
            }
            return result;
        }
    }
}