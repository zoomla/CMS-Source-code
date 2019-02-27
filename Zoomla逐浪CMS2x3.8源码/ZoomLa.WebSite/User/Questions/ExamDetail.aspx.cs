using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.Common;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using ZoomLa.Model.Exam;
using ZoomLa.BLL.Exam;
using System.Linq;
using System.Text.RegularExpressions;
using ZoomLa.BLL.User;
using System.Xml;
using System.IO;

/*
 * 学生考试页
 */ 
public partial class User_Questions_ExamDetail : System.Web.UI.Page
{
    protected B_Exam_Sys_Papers paperBll = new B_Exam_Sys_Papers();
    protected B_Exam_Sys_Questions questBll = new B_Exam_Sys_Questions();
    protected B_Exam_Answer answerBll = new B_Exam_Answer();
    private B_TempUser tuserBll = new B_TempUser();
    //试卷ID
    public int Mid { get { return DataConverter.CLng(Request.QueryString["ID"]); } }
    public string Action { get { return (Request.QueryString["action"] ?? "").ToLower(); } }
    public string FlowID { get { return Request.QueryString["FlowID"]; } }
    //问题IDS,用于临时生成,不允许超过十个
    private string Qids { get { return Request.QueryString["qids"] ?? ""; } }
    //考试时间
    public int ExTime { get { return DataConverter.CLng(ViewState["ReTime"] ?? ""); } set { ViewState["ReTime"] = value; } }
    //-------------
    public DataTable QuestDT { get { return (DataTable)ViewState["QuestDT"]; } set { ViewState["QuestDT"] = value; } }
    public DataTable AnswerDT { get { return (DataTable)ViewState["AnswerDT"]; } set { ViewState["AnswerDT"] = value; } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Qids))
            {
                M_Exam_Sys_Papers paperMod = new M_Exam_Sys_Papers();
                paperMod.p_name = "临时组卷";
                paperMod.QIDS = Qids;
                paperMod.p_UseTime = 0;
                MyBind(paperMod);
            }
            else 
            {
                //B_User.CheckIsLogged(Request.RawUrl);
                MyBind();
            }
        }
    }
    private void MyBind(M_Exam_Sys_Papers paperMod = null)
    {
        M_UserInfo mu = tuserBll.GetLogin();
        if (Mid > 0) { paperMod = paperBll.SelReturnModel(Mid); }
        if (DateTime.Now < paperMod.p_BeginTime) { function.WriteErrMsg("还未到考试时间!"); }
        if (paperMod.p_endTime < DateTime.Now) { function.WriteErrMsg("考试时间已过!"); }
        if (string.IsNullOrEmpty(paperMod.QIDS)) { function.WriteErrMsg("该试卷没有添加题目!"); }
        //----------考试信息
        UName_T.Text = mu.HoneyName;
        //----------
        PName_L.Text = DateTime.Now.ToString("yyyy年MM月dd日  ") + "[" + paperMod.p_name + "]";
        PName_L.Text += "  考试时长:(" + (paperMod.p_UseTime > 0 ? paperMod.p_UseTime + "分钟)" : "不限定)");
        ExTime = DataConverter.CLng(paperMod.p_UseTime);
        QuestDT = questBll.SelByIDSForExam(paperMod.QIDS,paperMod.id);//获取问题,自动组卷则筛选合适的IDS
        QuestDT.DefaultView.RowFilter = "";
        QuestDT_Hid.Value = JsonConvert.SerializeObject(QuestDT.DefaultView.ToTable(false, "p_id,p_title,p_type,p_defaultScores,istoshare,pid".Split(',')));
        if (Action.Equals("view"))//显示答案
        {
            AnswerDT = answerBll.SelByPid(mu.UserID, Mid, FlowID);
            if (AnswerDT.Rows.Count < 1) { function.WriteErrMsg("你尚未完成答卷"); }
            AnswerDT.DefaultView.RowFilter = "";
            Answer_Hid.Value = JsonConvert.SerializeObject(AnswerDT.DefaultView.ToTable(true, "ID,QID,QType,QTitle,Answer,IsRight,Remark".Split(',')));
            Coll_Btn.Visible = true;
            return_a.Visible = true;
            Submit_Btn.Visible = false;
            //获取主记录用于显示时间
            function.Script(this, "LoadAnswer();");
            M_Exam_Answer answerMod = answerBll.SelMainModel(FlowID);
            //-----显示得分
            MySchool_T.Enabled = false;
            MyClass_T.Enabled = false;
            MyClass_T.Text = answerMod.MyClass;
            MySchool_T.Text = answerMod.MySchool;
            time_sp.InnerText = "用时 " + (answerMod.CDate - answerMod.StartDate).TotalMinutes.ToString("f0") + " 分钟";
            UName_T.Enabled = false;
            totalscore_sp.InnerHtml = "得分:" + DataConverter.CLng(AnswerDT.Select("Remind=1")[0]["TotalScore"]);
        }
        //先绑定按Type显示,然后再按大题显示
        DataTable typeDT = answerBll.GetTypeDT(QuestDT);
        MainRPT.DataSource = typeDT;
        MainRPT.DataBind();
        QuestId_Hid.Value = Mid.ToString();
    }
    protected void MainRPT_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView drv = e.Item.DataItem as DataRowView;
            Repeater RPT = e.Item.FindControl("RPT") as Repeater;
            DataTable dt = null;
            string normFilter = "p_type=" + drv["QType"] + " AND (pid=0 OR pid IS NULL)";
            string bigfilter = "pid=" + drv["QType"];//big下,qtype为其id
            if (Action.Equals("view"))//查看答案
            {
                if (drv["IsBig"].ToString().Equals("0"))
                { AnswerDT.DefaultView.RowFilter = normFilter; }
                else
                { AnswerDT.DefaultView.RowFilter = bigfilter; }
                dt = AnswerDT.DefaultView.ToTable();
            }
            else
            {
                //是否为大题,如果是的话,加载入小题,这里完成排序?
                if (drv["IsBig"].ToString().Equals("0"))
                {
                    QuestDT.DefaultView.RowFilter = normFilter;
                }
                else
                {
                    QuestDT.DefaultView.RowFilter = bigfilter;
                }
                dt = QuestDT.DefaultView.ToTable();
            }
            if (dt.Columns.Contains("order"))
            {
                dt.DefaultView.Sort = "order asc";
            }
            RPT.DataSource = dt.DefaultView.ToTable();
            RPT.DataBind();
        }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        QuestDT = null;
        AnswerDT = null;
        //ExTime = 0;
    }
    //答卷提交
    protected void Submit_Btn_Click(object sender, EventArgs e)
    {
        JArray arr = JsonConvert.DeserializeObject<JArray>(QuestDT_Hid.Value);
        M_UserInfo mu = tuserBll.GetLogin();
        M_Exam_Sys_Papers paperMod = paperBll.SelReturnModel(Mid);
        M_Exam_Answer firstMod = new M_Exam_Answer();
        List<M_Exam_Answer> answerList = new List<M_Exam_Answer>();
        bool isfrist = true;//首条记录用于存储批注信息,预估得分,人工批阅后才是真正得分
        int totalscore = 0;//
        string flowid = DateTime.Now.ToString("yyyyMMddHHmmssfff");
        foreach (JObject obj in arr)
        {
            M_Exam_Answer answerMod = new M_Exam_Answer();
            answerMod.UserID = mu.UserID;
            answerMod.UserName = mu.UserName;
            answerMod.QID = DataConverter.CLng(obj["p_id"].ToString());
            answerMod.QTitle = obj["p_title"].ToString();
            answerMod.QType = obj["p_Type"].ToString();
            answerMod.Score = DataConverter.CLng(obj["p_defaultScores"].ToString());
            answerMod.Answer = obj["answer"] == null ? "" : obj["answer"].ToString();
            answerMod.PaperID = Mid;
            answerMod.PaperName = paperMod != null ? paperMod.p_name : "临时组卷";
            answerMod.FlowID = flowid;
            answerMod = CheckIsRight(answerMod);
            answerMod.Remind = "0";
            answerMod.pid=DataConverter.CLng(obj["pid"].ToString());
            if (answerMod.IsRight == RIGHT)
            {
                totalscore += answerMod.Score;
            }
            if (isfrist)
            {
                answerMod.Remind = "1";
                isfrist = false;
                firstMod = answerMod;
                firstMod.UserName = UName_T.Text;
                firstMod.MySchool = MySchool_T.Text;
                firstMod.MyClass = MyClass_T.Text;
            }
            answerList.Add(answerMod);
        }
        firstMod.TotalScore = totalscore;
        foreach (M_Exam_Answer model in answerList)
        {
            model.StartDate= DateTime.Now.AddSeconds(-DataConverter.CLng(QuestTime_Hid.Value));//开始答题时间
            answerBll.Insert(model);
        }
        string url = "ExamDetail.aspx?action=View&FlowID=" + flowid;
        url += Mid < 1 ? "" : "&ID=" + Mid;
        url += string.IsNullOrEmpty(Qids) ? "" : "&qids=" + Qids;
        function.WriteSuccessMsg("提交答案成功", url);
    }
    private int NOAUDIT = 0, RIGHT = 1, ERROR = 2;
    //自动校验答案
    private M_Exam_Answer CheckIsRight(M_Exam_Answer answerMod)
    {
        M_Exam_Sys_Questions questMod = questBll.GetSelect(answerMod.QID);
        if (string.IsNullOrEmpty(questMod.p_Answer)) { answerMod.IsRight = NOAUDIT; return answerMod; }
        switch (questMod.MyQType)
        {
            case M_Exam_Sys_Questions.QType.Radio:
                answerMod.IsRight = answerMod.Answer.Equals(questMod.p_Answer) ? RIGHT : ERROR;
                break;
            case M_Exam_Sys_Questions.QType.Multi:
                //检测是否包含指定选项,数量不能有差异,顺序可以不计
                {
                    answerMod.Answer = answerMod.Answer.TrimEnd(',');
                    string[] answerArr = answerMod.Answer.Split(',');
                    string[] rightArr = questMod.p_Answer.Split('|');
                    if (answerArr.Length != rightArr.Length) { answerMod.IsRight = ERROR; break; }
                    foreach (string answer in answerArr)
                    {
                        if (!rightArr.Contains(answer)) { answerMod.IsRight = ERROR; break; }
                    }
                    answerMod.IsRight = RIGHT;
                }
                break;
            case M_Exam_Sys_Questions.QType.FillBlank:
                {
                    //检测是否包含指定选项,数量不能有差异,需要按顺序
                    answerMod.Answer = answerMod.Answer.TrimEnd("|".ToCharArray());
                    string[] answerArr = Regex.Split(answerMod.Answer, Regex.Escape("|||"));
                    string[] rightArr = questMod.p_Answer.Split('|');
                    if (answerArr.Length != rightArr.Length) { answerMod.IsRight = ERROR; break; }
                    for (int i = 0; i < answerArr.Length; i++)
                    {
                        if (answerArr[i] != rightArr[i]) { answerMod.IsRight = ERROR; break; }
                    }
                    answerMod.IsRight = RIGHT;
                }
                break;
            case M_Exam_Sys_Questions.QType.Answer:
                {
                    //解析题如果答案相符则计对,否则人工审核
                    answerMod.Answer = answerMod.Answer.Trim().TrimStart("解：".ToCharArray());
                    answerMod.IsRight = answerMod.Answer.Equals(questMod.p_Answer) ? RIGHT : NOAUDIT;
                }
                break;
        }
        return answerMod;
    }
    //-----------------------------------
    public string GetContent()
    {
        return questBll.GetContent(DataConverter.CLng(Eval("p_id")), DataConverter.CLng(Eval("p_Type")), Eval("p_Content").ToString());
    }
    public string AngularJS = "";
    //显示回答区
    public string GetSubmit() 
    {
        return questBll.GetSubmit(DataConverter.CLng(Eval("p_id")), DataConverter.CLng(Eval("p_Type")), ref AngularJS);
    }
    public string GetIsRight() 
    {
        string tlp="";
        switch (Eval("IsRight").ToString())
        {
            case "0":
                tlp = "<span class='fa fa-cloud' style='font-size:1.2em;' title='尚未批阅'></span><span></span>";
                break;
            case "1":
                tlp = "<span class='fa fa-check' style='font-size:1.2em;color:green;' title='正确'></span><span>(" + Eval("Score") + "分)</span>";
                break;
            case "2":
                tlp = "<span class='fa fa-remove' style='font-size:1.2em;color:red;' title='错误'></span>";
                break;
            default:
                break;
        }
        return tlp;
    }
} 
