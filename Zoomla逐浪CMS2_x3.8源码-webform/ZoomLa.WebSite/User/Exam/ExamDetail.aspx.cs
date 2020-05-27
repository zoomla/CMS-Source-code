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
using System.Text.RegularExpressions;
using System.IO;

public partial class User_Questions_ExamDetail : System.Web.UI.Page
{
    protected B_Exam_Sys_Papers paperBll = new B_Exam_Sys_Papers();
    protected B_Exam_Sys_Questions questBll = new B_Exam_Sys_Questions();
    protected B_User buser = new B_User();
    protected B_Admin badmin = new B_Admin();
    protected B_Exam_Answer answerBll = new B_Exam_Answer();
    //试卷ID
    public int Mid { get { return DataConverter.CLng(Request.QueryString["ID"]); } }
    public string Action { get { return (Request.QueryString["action"] ?? "").ToLower(); } }
    public string FlowID { get { return Request.QueryString["FlowID"]; } }
    //考试时间
    public int ExTime { get { return DataConverter.CLng(ViewState["ReTime"] ?? ""); } set { ViewState["ReTime"] = value; } }
    //-------------
    public DataTable QuestDT { get { return (DataTable)ViewState["QuestDT"]; } set { ViewState["QuestDT"] = value; } }
    public DataTable AnswerDT { get { return (DataTable)ViewState["AnswerDT"]; } set { ViewState["AnswerDT"] = value; } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!buser.IsTeach()&&!badmin.CheckLogin()) { function.WriteErrMsg("当前页面只有教师才可访问"); }
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    private void MyBind(M_Exam_Sys_Papers paperMod=null)
    {
        if (Mid > 0){ paperMod = paperBll.SelReturnModel(Mid); }
        if (paperMod == null) { function.WriteErrMsg("试卷不存在,可能已被删除"); }
        PName_L.Text = DateTime.Now.ToString("yyyy年MM月dd日  ") + "[" + paperMod.p_name + "]";
        PName_L.Text += "  考试时长:(" + (paperMod.p_UseTime > 0 ? paperMod.p_UseTime + "分钟)" : "不限定)");
        ExTime = DataConverter.CLng(paperMod.p_UseTime);
        QuestDT = questBll.SelByIDSForExam(paperMod.QIDS);
        //------------------
        AnswerDT = answerBll.SelByFlow(FlowID);
        QuestDT.DefaultView.RowFilter = "";
        QuestDT_Hid.Value = JsonConvert.SerializeObject(QuestDT.DefaultView.ToTable(false, "p_id,p_title,p_type".Split(',')));
        AnswerDT.DefaultView.RowFilter = "";
        if (AnswerDT.Rows.Count > 0)
        {
            Answer_Hid.Value = JsonConvert.SerializeObject(AnswerDT.DefaultView.ToTable(true, "ID,QID,QType,QTitle,Answer,IsRight,Remark,IsToShare".Split(',')));
            function.Script(this, "LoadAnswer();");
        }
        DataTable typeDT = answerBll.GetTypeDT(QuestDT);
        MainRPT.DataSource = typeDT;
        MainRPT.DataBind();
        //-------------从主信息中读取详情
        M_Exam_Answer firstMod = answerBll.SelMainModel(FlowID);
        UName_T.Text = firstMod.UserName;
        MyClass_T.Text = firstMod.MyClass;
        MySchool_T.Text = firstMod.MySchool;
        time_sp.InnerText = "用时 " + (firstMod.CDate - firstMod.StartDate).TotalMinutes.ToString("f0") + " 分钟";
    }
    protected void MainRPT_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView drv = e.Item.DataItem as DataRowView;
            Repeater RPT = e.Item.FindControl("RPT") as Repeater;
            string normFilter = "p_type=" + drv["QType"] + " AND IsToShare=0 AND (pid=0 OR pid IS NULL)";//非大题筛选语句
            string bigfilter = "pid=" + drv["QType"] + " AND IsToShare=0";//大题下小题,qtype为其id
            if (drv["IsBig"].ToString().Equals("0")) { AnswerDT.DefaultView.RowFilter = normFilter; }
            else { AnswerDT.DefaultView.RowFilter = bigfilter; }
            if (AnswerDT.Columns.Contains("order"))
            {
                AnswerDT.DefaultView.Sort = "order asc";
            }
            RPT.DataSource = AnswerDT.DefaultView.ToTable();
            RPT.DataBind();
        }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        QuestDT = null;
        AnswerDT = null;
    }
    //答卷提交
    protected void Submit_Btn_Click(object sender, EventArgs e)
    {
        JArray arr = JsonConvert.DeserializeObject<JArray>(Answer_Hid.Value);
        M_UserInfo userinfo = buser.GetLogin();
        foreach (JObject obj in arr)
        {
            M_Exam_Answer answerMod = answerBll.SelReturnModel(Convert.ToInt32(obj["ID"].ToString()));
            answerMod.TechID = userinfo.UserID;
            answerMod.TechName = userinfo.UserName;
            answerMod.IsRight = Convert.ToInt32(obj["IsRight"].ToString());
            answerMod.Score = DataConverter.CLng(obj["Score"]);
            answerMod.Remark = obj["Remark"].ToString();
            answerMod.RDate = DateTime.Now;
            answerBll.UpdateByID(answerMod);
        }
        answerBll.SumScore(FlowID);
        Response.Redirect("ToScore.aspx");
    }
    //-----------------------------------
    //解答与填空题允许自定义分数
    public string GetScoreHtml()
    {
        int qtype = DataConverter.CLng(Eval("QType"));
        string tlp = "";
        switch (qtype)
        {
            case (int)M_Exam_Sys_Questions.QType.FillBlank:
            case (int)M_Exam_Sys_Questions.QType.Answer:
                tlp = "<label><input type='radio' value='3' " + (Eval("IsRight").ToString().Equals("3") ? "checked=checked" : "") + " name='isright_" + Eval("ID") + "' />自定义分数</label>"
                    + "<input type='text' class='form-control text_xs' id='score_" + Eval("ID") + "' value='"+Eval("Score")+"'/>";
                break;
        }
        return tlp;
    }
    public string GetContent()
    {
        int id = Convert.ToInt32(Eval("p_id"));
        string content = Eval("p_Content").ToString();
        string tlp = "(<span class='answersp'></span>)";
        string tlp2 = "(<span class='answersp'>{0}</span>)";
        string blank = "（）";
        switch (DataConverter.CLng(Eval("QType")))
        {
            case 2:
                content = content.Replace(blank, tlp);
                return content;
            case 4:
                {
                    string[] conArr = Regex.Split(content, Regex.Escape(blank));
                    content = "";
                    for (int i = 0; i < conArr.Length; i++)
                    {
                        content = conArr[i] + string.Format(tlp2, (i + 1));
                    }
                    return content;
                }
            default:
                return content;
        }
    }
    public string AngularJS = "";
    public string GetSubmit() 
    {
        string option = "";
        string vpath = M_Exam_Sys_Questions.OptionDir + Eval("p_id") + ".opt";
        if (File.Exists(Server.MapPath(vpath)))
        { option = SafeSC.ReadFileStr(vpath); }
        string emptyTlp = "<span style='color:red;'>未定义选项</span>";
        int id =Convert.ToInt32(Eval("p_id"));
       JArray arr = JsonConvert.DeserializeObject<JArray>(option);
       StringBuilder builder = new StringBuilder();
        //单,多,填,解
       switch (DataConverter.CLng(Eval("QType")))
        {
            case 0:
                {
                    string name = "srad_"+id;
                    string tlp = "<li class='opitem'><label><input type='radio' name='{0}' value='{1}'>{1}. {2}</label></li>";
                    if (arr == null || arr.Count < 1) break;
                    foreach (JObject obj in arr)
                    {
                        builder.Append(string.Format(tlp, name,obj["op"], obj["val"]));
                    }
                }
                break;
            case 1:
                {
                    string name = "mchk_" + id;
                    string tlp = "<li class='opitem'><label><input class='opitem' type='checkbox' name='{0}' value='{1}'>{1}. {2}</label></li>";
                    if (arr == null || arr.Count < 1) break;
                    foreach (JObject obj in arr)
                    {
                        builder.Append(string.Format(tlp, name, obj["op"], obj["val"]));
                    }
                }
                break;
            case 2:
                {
                    //string tlp = "<div contenteditable='true' class='answerdiv'>解：</div>";
                    //builder.Append(tlp);
                }
                break;
           case 3://放置一个ueditor
                {
                    string name = "answer_" + id;
                    string tlp = "<div id='" + name + "' class='answerdiv'>解：</div>";
                    builder.Append(tlp);
                }
                break;
           case 4://完颜填空
                {
                    if (arr == null || arr.Count < 1) { return emptyTlp; }
                    string name = "filltextblank_" + id;
                    string tlp = "<li style='float:none;' ng-repeat='item in list." + name + "|orderBy:\"id\"'>"
                                 + "<div><div class='title'>{{item.id}},{{item.title}}</div>"
                                 + "<ul class='submitul'>"
                                 + "<li class='opitem' ng-repeat='opt in item.opts'><label><input type='radio' class='opitem' ng-value='opt.op' ng-model='item.answer'/>{{opt.op}}. <span ng-bind-html='opt.val | to_trusted'></span></label></li>"
                                 + "</ul></div><div style='clear:both;'></div></li>";
                    AngularJS += "$scope.list[\"" + name + "\"]=" + option + ";idsArr.push(" + id + ");";
                    builder.Append(tlp);
                }
                break;
        }
       return builder.ToString();
    }
} 
