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

namespace ZoomLaCMS.Manage.Plus
{
    public partial class SurveyItemList : CustomerPageAction
    {
        protected B_Survey surBll = new B_Survey();
        protected M_Survey surMod = new M_Survey();
        int SID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                SID = string.IsNullOrEmpty(Request.QueryString["SID"]) ? 0 : DataConverter.CLng(Request.QueryString["SID"]);
                if (SID <= 0)
                    function.WriteErrMsg("缺少问卷投票的ID参数！", "SurveyManage.aspx");
                else
                {
                    surMod = surBll.GetSurveyBySid(SID);
                    if (surMod.IsNull)
                    {
                        function.WriteErrMsg("该问卷投票不存在！可能已被删除", "SurveyManage.aspx");
                    }
                    else
                    {
                        Call.SetBreadCrumb(Master, "<li><a href='" + customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='SurveyManage.aspx'>问卷投票管理</a></li><li><a href='Survey.aspx?SID=" + surMod.SurveyID + "'>" + surMod.SurveyName + "</a></li><li class='active'><a href='SurveyItemList.aspx'>问卷投票问题列表</a>  <a href='SurveyItem.aspx?SID=" + surMod.SurveyID + "'>[添加问题]</a></li>");
                        this.HdnSID.Value = SID.ToString();
                    }
                }
                MyBind();
            }
        }
        private void MyBind()
        {
            DataTable dtable = B_Survey.GetQuestionList(DataConverter.CLng(this.HdnSID.Value));
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
            if (this.Page.IsValid)
            {
                int Id = DataConverter.CLng(e.CommandArgument);
                if (e.CommandName == "Edit")
                {
                    Response.Redirect("SurveyItem.aspx?SID=" + Request.QueryString["SID"] + "&QID=" + Id);
                }
                if (e.CommandName == "Del")
                {
                    B_Survey.DelQuestion(Id);
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
        }
        // 问题的类型
        public string GetQType(string tType, string qid)
        {
            int t = DataConverter.CLng(tType);
            string re = "";
            switch (t)
            {
                case 1:
                    re = "单选项";
                    break;
                case 2:
                    re = "复选项";
                    break;
                case 3:
                    re = "单行文本";
                    break;
                case 4: // 验证文本
                    re = GetVType(DataConverter.CLng(qid));
                    break;
                case 5:
                    re = "图片";
                    break;
                default:
                    re = "";
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
            if (!string.IsNullOrEmpty(Order_Hid.Value))
            {
                string[] oids = Order_Hid.Value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var item in oids)
                {
                    var oiddata = item.Split('|');
                    M_Question surmod = B_Survey.GetQuestion(DataConverter.CLng(oiddata[1]));
                    surmod.OrderID = DataConverter.CLng(oiddata[0]);
                    B_Survey.UpdateQuestion(surmod);
                }
            }
            MyBind();
        }
    }
}