using Aspose.Words;
using Aspose.Words.Saving;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Winista.Text.HtmlParser.Filters;
using Winista.Text.HtmlParser.Visitors;
using ZoomLa.BLL;
using ZoomLa.BLL.Exam;
using ZoomLa.BLL.Helper;
using ZoomLa.BLL.User;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.User;

public partial class User_Exam_PaperCenter : System.Web.UI.Page
{
    B_Temp tempBll = new B_Temp();
    B_Exam_Sys_Questions questBll = new B_Exam_Sys_Questions();
    B_Exam_Answer answerBll = new B_Exam_Answer();
    B_Exam_Sys_Papers paperBll = new B_Exam_Sys_Papers();
    B_User buser = new B_User();
    B_TempUser UserBll = new B_TempUser();
    DataTable QuestDT { get { return (DataTable)ViewState["QuestDT"]; } set { ViewState["QuestDT"] = value; } }
    private int PClass { get { return DataConverter.CLng(Request.QueryString["pclass"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        //B_User.CheckIsLogged(Request.RawUrl);
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        QuestDT = null;
    }
    private void MyBind()
    {
        M_UserInfo mu = UserBll.GetLogin();
        M_Temp tempMod = tempBll.SelModelByUid(mu.UserID, 10);

        if (tempMod == null || string.IsNullOrEmpty(tempMod.Str1)) { function.WriteErrMsg("试题篮为空,请先选择试题!"); }
        Title_T.Text = DateTime.Now.ToString("yyyy年MM月dd日" + mu.UserName + "的组卷");
        QuestDT = questBll.SelByIDSForExam(tempMod.Str1);//获取问题,自动组卷则筛选合适的IDS
        QuestDT.DefaultView.RowFilter = "";
        DataTable typeDT = answerBll.GetTypeDT(QuestDT);
        MainRPT.DataSource = typeDT;
        MainRPT.DataBind();
    }
    protected void MainRPT_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView drv = e.Item.DataItem as DataRowView;
            Repeater RPT = e.Item.FindControl("RPT") as Repeater;
            DataTable dt = null;
            string normFilter = "p_type=" + drv["QType"] + " AND IsToShare=0 AND (pid=0 OR pid IS NULL)";
            string bigfilter = "pid=" + drv["QType"] + " AND IsToShare=0";//big下,qtype为其id
            if (drv["IsBig"].ToString().Equals("0")) { QuestDT.DefaultView.RowFilter = normFilter; }
            else { QuestDT.DefaultView.RowFilter = bigfilter; }
            dt = QuestDT.DefaultView.ToTable();
            RPT.DataSource = dt;
            RPT.DataBind();
        }
    }
    public string GetSubmit()
    {
        string option =SafeSC.ReadFileStr(M_Exam_Sys_Questions.OptionDir+Eval("p_id")+".opt");
        int id = Convert.ToInt32(Eval("p_id"));
        JArray arr = JsonConvert.DeserializeObject<JArray>(option);
        StringBuilder builder = new StringBuilder();
        //单,多,填,解
        switch (DataConverter.CLng(Eval("p_Type")))
        {
            case (int)M_Exam_Sys_Questions.QType.Radio:
                {
                    if (arr == null || arr.Count < 1) { return "未定义选项"; }
                    string name = "srad_" + id;
                    string tlp = "<li class='opitem'><label><input type='radio' name='{0}' value='{1}'>{1}. {2}</label></li>";
                    foreach (JObject obj in arr)
                    {
                        builder.Append(string.Format(tlp, name, obj["op"], obj["val"]));
                    }
                }
                break;
            case (int)M_Exam_Sys_Questions.QType.Multi:
                {
                    if (arr == null || arr.Count < 1) { return "未定义选项"; }
                    string name = "mchk_" + id;
                    string tlp = "<li class='opitem'><label><input class='opitem' type='checkbox' name='{0}' value='{1}'>{1}. {2}</label></li>";
                    foreach (JObject obj in arr)
                    {
                        builder.Append(string.Format(tlp, name, obj["op"], obj["val"]));
                    }
                }
                break;
            case (int)M_Exam_Sys_Questions.QType.FillBlank:
                {
                    //string tlp = "<div contenteditable='true' class='answerdiv'>解：</div>";
                    //builder.Append(tlp);
                }
                break;
            case (int)M_Exam_Sys_Questions.QType.Answer://放置一个ueditor
                {
                    string name = "answer_" + id;
                    string tlp = "<div id='" + name + "' contenteditable='true' class='answerdiv'>解：</div>";
                    builder.Append(tlp);
                }
                break;
        }
        return builder.ToString();
    }
    protected void Sure_Btn_Click(object sender, EventArgs e)
    {
        //function.WriteErrMsg(Qids_Hid.Value);
        M_UserInfo mu=UserBll.GetLogin();
        M_Temp TempMod = tempBll.SelModelByUid(mu.UserID, 10);
        M_Exam_Sys_Papers paperMod = new M_Exam_Sys_Papers();
        if (string.IsNullOrEmpty(TempMod.Str1)) { function.WriteErrMsg("试题篮为空,无法生成试卷"); }
        paperMod.p_name = Title_T.Text;
        paperMod.p_class = PClass;
        paperMod.p_Remark = "";
        paperMod.p_UseTime = 0;
        paperMod.p_BeginTime = DateTime.Now;
        paperMod.p_endTime = DateTime.Now.AddYears(1);
        paperMod.p_Style = 2;
        paperMod.UserID = mu.UserID;
        paperMod.QIDS = TempMod.Str1;
        paperMod.QuestList = QInfo_Hid.Value;
        paperMod.Price =DataConverter.CLng(Price_T.Text);
        int newid = paperBll.Insert(paperMod);
        //-------------------------
        TempMod.Str1 = ""; tempBll.UpdateByID(TempMod);
        Title_L.Text = paperMod.p_name;
        NewID_Hid.Value = newid.ToString();
        paper_view.HRef = "/User/Questions/ExamDetail.aspx?ID=" + newid;
        paper_down.HRef = "/User/Exam/DownPaper.aspx?ID="+newid;
        paper_edit.HRef = "/User/Exam/Add_Papers_System.aspx?id=" + newid;
        paper_edit.Visible = mu.UserID > 0;
        addsucc_div.Visible = true;
        add_div.Visible = false;
    }
}