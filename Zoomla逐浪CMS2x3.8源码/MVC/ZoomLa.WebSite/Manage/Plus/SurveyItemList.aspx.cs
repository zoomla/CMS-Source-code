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
    public partial class SurveyItemList : CustomerPageAction
    {
        B_Survey surBll = new B_Survey();
        M_Survey surMod = new M_Survey();
        public int Sid {
            get { return DataConverter.CLng(Request.QueryString["Sid"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Sid <= 0) { function.WriteErrMsg("缺少问卷投票的ID参数！", "SurveyManage.aspx"); }
                surMod = surBll.GetSurveyBySid(Sid);
                if (surMod.IsNull)
                {
                    function.WriteErrMsg("该问卷投票不存在！可能已被删除", "SurveyManage.aspx");
                }
                MyBind();
                string bread = "<li><a href='" + customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='SurveyManage.aspx'>问卷投票管理</a></li><li><a href='Survey.aspx?SID=" + surMod.SurveyID + "'>" + surMod.SurveyName + "</a></li><li class='active'><a href='" + Request.RawUrl + "'>问卷投票列表</a> <a href='SurveyItem.aspx?SID=" + surMod.SurveyID + "'>[添加问题]</a></li>";
                bread += "<a href='/Plugins/Vote.aspx?Sid=" + Sid + "' target='_blank' class='pull-right'><i class='fa fa-eye'></i> 预览</a>";
                Call.SetBreadCrumb(Master, bread);
            }
        }
        private void MyBind()
        {
            DataTable dtable = B_Survey.GetQuestionList(Sid);
            this.EGV.DataSource = dtable;
            this.EGV.DataKeyNames = new string[] { "QID" };
            this.EGV.DataBind();
        }
        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            MyBind();
        }
        protected void Lnk_Click(object sender, GridViewCommandEventArgs e)
        {
            int Id = DataConverter.CLng(e.CommandArgument);
            if (e.CommandName == "Edit")
            {
                Response.Redirect("SurveyItem.aspx?SID=" +Sid+ "&QID=" + Id);
            }
            if (e.CommandName == "MovePre")
            {
                M_Question info = B_Survey.GetQuestion(Id);
                if (info.OrderID != B_Survey.GetMinOrderID(info.SurveyID))
                {
                    M_Question Pre = B_Survey.GetQuestion(B_Survey.PreQusID(info.SurveyID, info.OrderID));
                    int CurrOrder = info.OrderID;
                    info.OrderID = Pre.OrderID;
                    Pre.OrderID = CurrOrder;
                    B_Survey.UpdateQuestion(info);
                    B_Survey.UpdateQuestion(Pre);
                }
            }
            if (e.CommandName == "MoveNext")
            {
                M_Question info = B_Survey.GetQuestion(Id);
                if (info.OrderID != B_Survey.GetMaxOrderID(info.SurveyID))
                {
                    M_Question Pre = B_Survey.GetQuestion(B_Survey.NexQusID(info.SurveyID, info.OrderID));
                    int CurrOrder = info.OrderID;
                    info.OrderID = Pre.OrderID;
                    Pre.OrderID = CurrOrder;
                    B_Survey.UpdateQuestion(info);
                    B_Survey.UpdateQuestion(Pre);
                }
            }
            MyBind();
        }
        // 问题的类型
        public string GetQType(string tType, string qid)
        {
            int t = DataConverter.CLng(tType);
            string re = "";
            switch (t)
            {
                case 0:
                    re="单选|多选";
                    break;
                case 1:
                    re = "填空|文本";
                    break;
                default:
                    re = tType;
                    break;
            }
            return re;
        }
        // 获取验证文本的具体类型
        public string GetVType(int qid)
        {
            string[] types = { "邮箱", "手机号码", "固定电话", "身份证号", "准考证号" };
            M_Question info = B_Survey.GetQuestion(qid);
            string tpIndex = info.QuestionContent.Split('|')[1];
            return types[DataConverter.CLng(tpIndex)];
        }
        protected void Order_B_Click(object sender, EventArgs e)
        {
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(Order_Hid.Value);
            foreach (DataRow dr in dt.Rows)
            {
                M_Question surmod = B_Survey.GetQuestion(DataConverter.CLng(dr["id"]));
                surmod.OrderID = DataConverter.CLng(dr["oid"]);
                B_Survey.UpdateQuestion(surmod);
            }
            MyBind();
        }

        protected void EGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dr = e.Row.DataItem as DataRowView;
                e.Row.Attributes.Add("ondblclick", "location='SurveyItem.aspx?Qid=" + dr["Qid"] + "&Sid="+Sid+"'");
            }
        }
        protected void BatDel_Btn_Click(object sender, EventArgs e)
        {
            B_Survey.DelQuestion(Request.Form["idchk"]);
            MyBind();
        }
}
}