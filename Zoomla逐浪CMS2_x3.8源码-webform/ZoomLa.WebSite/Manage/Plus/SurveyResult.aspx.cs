namespace ZoomLa.WebSite.Manage.Plus
{
    using System;
    using System.Data;
    using System.Configuration;
    using System.Collections;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.WebControls.WebParts;
    using System.Web.UI.HtmlControls;
    using ZoomLa.BLL;
    using ZoomLa.Common;
    using ZoomLa.Model;
    using System.Collections.Generic;
    using System.Text;
    using System.Data.SqlClient;
    using ZoomLa.SQLDAL;

    public partial class SurveyResult : CustomerPageAction
    {
        protected B_Survey surBll = new B_Survey();
        public int Sid { get { return DataConvert.CLng(Request.QueryString["Sid"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Sid <= 0) { function.WriteErrMsg("缺少问卷投票的ID参数！", "../Plus/SurveyManage.aspx"); return; }
                M_Survey info = surBll.GetSurveyBySid(Sid);
                if (info.IsNull) { function.WriteErrMsg("该问卷投票不存在！可能已被删除", "../Plus/SurveyManage.aspx"); return; }
                //this.lblSurveyName.Text = info.SurveyName;
                //this.lblDesp.Text = info.Description;
                //if (!string.IsNullOrEmpty(info.Description))
                //{
                //    this.lblDesp.Text = "问卷描述:" + info.Description;
                //}
                MyBind();
                IList<M_Question> list = new List<M_Question>();
                list = B_Survey.GetQueList(Sid);
                rptReuslt.DataSource = list;
                rptReuslt.DataBind();
                Call.SetBreadCrumb(Master, "<li><a href='SurveyManage.aspx'>问卷投票</a></li><li><a href='Survey.aspx?SID=" + info.SurveyID + "'>" + info.SurveyName + "</a></li><li class='active'>问卷投票结果</li>");
            }
        }
        public void MyBind()
        {
            DataTable dt = surBll.GetCountScore(Sid);
            Egv.DataSource = dt;
            Egv.DataBind();
        }
        protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Egv.PageIndex = e.NewPageIndex;
            MyBind();
        }
        protected void rptReuslt_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem)
                return;
            Repeater rep = e.Item.FindControl("rptOption") as Repeater;
            Label lbl = e.Item.FindControl("lblTip") as Label;
            M_Question rowv = (M_Question)e.Item.DataItem;
            if (rowv.TypeID > 2)
            {
                rep.Visible = false;
                lbl.Visible = true;
                return;
            }
            lbl.Visible = false;
            string options = rowv.QuestionContent;
            if (options.Length <= 0)
            {
                options = " 暂时没有添加选项内容。。。";
            }
            List<string> lstopts = new List<string>();
            lstopts.AddRange(options.Split('|'));
            int qid = rowv.QuestionID;
            int sid = rowv.SurveyID;
            rep.ItemDataBound += delegate(object obj, RepeaterItemEventArgs ex)
            {

                if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem)
                    return;
                if (ex.Item.DataItem != null)
                {
                    string item = ex.Item.DataItem.ToString().Split(':')[0];
                    int count = surBll.SelectNum(sid, qid, item);
                    int papers = surBll.GetQuCount(qid);
                    string strPcent = "";
                    if (papers == 0)
                        strPcent = "未填写";
                    else
                        strPcent = ((float)count / papers).ToString("P");
                    Image img = ex.Item.FindControl("imgBar") as Image;
                    if (count == 0)
                    {
                        img.Style["Width"] = "0.2%";
                        (ex.Item.FindControl("lblPercent") as Label).Text = "0票/" + strPcent + "<span style='color:green;'>(0分)</span>";
                    }
                    else
                    {
                        img.Style["width"] = strPcent;
                        (ex.Item.FindControl("lblPercent") as Label).Text = count + "票/" + strPcent + "<span style='color:green;'>(分)</span>";//" + (count * score) + "
                    }
                }
            };
            rep.DataSource = lstopts;
            rep.DataBind();
        }
        //题目类型
        protected string GetQuesType(object qid)
        {
            int id = Convert.ToInt32(qid);
            string type = "";
            switch (id)
            {
                case 1:
                    type = "单选题";
                    break;
                case 2:
                    type = "多选题";
                    break;
                case 3:
                case 4:
                    type = "填空题";
                    break;
                default:
                    break;
            }
            return type;
        }
        public string GetScore(string qid, string qtypeid)
        {
            string result = "";
            if (Convert.ToInt32(qtypeid) != 1)
                result = "";
            else
            {
                M_Question QueMod = new M_Question();
                QueMod = B_Survey.GetQuestion(Convert.ToInt32(qid));
                B_Survey surBll = new B_Survey();
                if (!CheckScore(QueMod.QuestionContent))
                    result = "";
                else
                {
                    //result = "<td colspan='3'>";
                    //result += surBll.GetScore(Convert.ToInt32(qid), Convert.ToInt32(qtypeid)) + "<div><iframe width='400' height='350' id='TbLocation' src='/Plugins/Chart/pie-basic.aspx?Bases=400|400||";
                    //result += "%u4F4D&Datas=" + Server.UrlEncode(surBll.CreateIframe(Convert.ToInt32(qid))) + "' frameborder='0' scrolling='no'></iframe><div><td>";
                }
            }
            return result;
        }
        public bool CheckScore(string qcontent)
        {
            bool flag = true;
            string[] QCon = qcontent.Split('|');
            for (int i = 0; i < QCon.Length && flag; i++)
            {
                if (string.IsNullOrEmpty(QCon[i]) || QCon[i].Split(':')[1] == "0")
                    flag = false;
            }
            return flag;
        }
        public string GetCountScore()
        {
            float ds;
            float score = 0;
            DataTable dt = surBll.GetCountScore(Sid);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                score += Convert.ToInt32(dt.Rows[i]["AnswerScore"].ToString());
            }
            ds = (score / (surBll.GetCount(Sid)) * 9) * 100;
            return ds.ToString("0.00") + "%";
        }
        public string GetAll()
        {
            string result = "";
            DataTable dt = surBll.GetAnswerN(Sid);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                result += "<tr><td>(" + (char)(65 + i) + ")" + dt.Rows[i][0].ToString() + "</td><td>" + dt.Rows[i][1].ToString() + "</td><td>" + surBll.GetAnswerNum(Sid, dt.Rows[i][0].ToString()) + "</td></tr>";
            }
            return result;
        }
    }
}