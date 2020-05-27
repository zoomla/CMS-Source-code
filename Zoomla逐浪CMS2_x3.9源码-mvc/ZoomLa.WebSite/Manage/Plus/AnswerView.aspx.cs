using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;

namespace ZoomLaCMS.Manage.Plus
{
    public partial class AnswerView : CustomerPageAction
    {
        protected B_Survey surBll = new B_Survey();
        protected M_Survey surMod = new M_Survey();
        protected M_Question Quemod = new M_Question();
        public int Sid { get { return DataConverter.CLng(Request.QueryString["Sid"]); } }
        private int Uid { get { return DataConverter.CLng(Request.QueryString["Uid"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Sid < 1 || Uid < 1) { function.WriteErrMsg("参数错误！"); }
            surMod = surBll.SelReturnModel(Sid);
            if (surMod == null) { function.WriteErrMsg("问卷不存在!"); }
            if (!IsPostBack)
            {
                DataBind();
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='SurveyManage.aspx'>问卷投票管理</a></li><li><a href='Survey.aspx?SID=" + surMod.SurveyID + "'>问卷投票名:" + surMod.SurveyName + "</a></li><li class='active'>问卷详情</li>");
        }
        public void DataBind(string key = "")
        {
            DataTable dt = surBll.GetAnswerByUID(Uid, Sid);
            Egv.DataSource = dt;
            Egv.DataBind();
        }
        protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Egv.PageIndex = e.NewPageIndex;
            DataBind();
        }
        public string GetCountScore()
        {
            return surBll.GetScoreByUID(Sid, Uid);
        }
        public string GetTypeStr()
        {
            M_QuestOption option = JsonConvert.DeserializeObject<M_QuestOption>(Eval("QOption").ToString());
            switch (DataConverter.CLng(Eval("TypeID")))
            {
                case 0:
                    return option.Sel_Type_Rad_Str;
                case 1:
                    return option.Text_Str_DP_Str;
                default:
                    return "未知";
            }
        }
    }
}