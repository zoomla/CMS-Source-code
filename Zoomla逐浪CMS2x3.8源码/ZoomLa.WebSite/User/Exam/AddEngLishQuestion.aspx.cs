using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using ZoomLa.BLL;
using ZoomLa.BLL.Exam;
using ZoomLa.Common;
using ZoomLa.Model;



public partial class manage_Question_AddEngLishQuestion : CustomerPageAction
{
    B_Exam_Sys_Questions questBll = new  B_Exam_Sys_Questions ();
    B_Exam_Sys_Papers bps = new  B_Exam_Sys_Papers ();   
    B_Exam_Class nodeBll = new  B_Exam_Class ();
    B_Exam_Type bqt = new  B_Exam_Type ();
    B_ExamPoint bep = new B_ExamPoint();
    B_Exam_Version verBll = new B_Exam_Version();
    B_Questions_Knowledge knowBll = new B_Questions_Knowledge();
    B_User buser = new B_User();
    public int Mid { get { return DataConverter.CLng(Request.QueryString["id"]); } }
    //1:大题添加小题
    public int IsSmall { get { return DataConverter.CLng(Request.QueryString["issmall"]); } }
    public int NodeID { get { return DataConverter.CLng(Request.QueryString["NodeID"]); } }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (IsSmall > 0) { MasterPageFile = "~/User/Empty.master"; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!buser.IsTeach()) { function.WriteErrMsg("当前页面只有教师才可访问"); }
        if (!IsPostBack)
        {
            if (NodeID>0)
            {
                M_Exam_Class nodeMod = nodeBll.GetSelect(NodeID);
                CurNode_L.Text = "[" + nodeMod.C_ClassName + "]";
            }
            else
            {
                nodetr.Style.Remove("display");
            }
            InitQuestGrade();
            GetQuesType();
            if (Mid > 0)
            {
                SaveNew_Btn.Visible = true;
                M_Exam_Sys_Questions questMod = questBll.GetSelect(Mid);
                if (questMod != null && questMod.p_id > 0)
                {
                    M_UserInfo mu=buser.GetLogin();
                    if (questMod.UserID != mu.UserID) { function.WriteErrMsg("你没有修改该试题的权限"); }
                    txtP_title.Text = questMod.p_title;
                    //QType_Radio.SelectedValue = questMod.p_Type.ToString();
                    //rblDiff.SelectedValue = questMod.p_Difficulty.ToString();
                    Diffcult_T.Text = questMod.p_Difficulty.ToString("f2");
                    AnswerHtml_T.Text = questMod.p_shuming;
                    Grade_Radio.SelectedValue = questMod.p_Views.ToString();
                    if (!string.IsNullOrEmpty(questMod.Tagkey)) { TagKey_T.Value = knowBll.GetNamesByIDS(questMod.Tagkey); }
                    NodeID_Hid.Value = questMod.p_Class.ToString();
                    txtP_Content.Text = questMod.p_Content;
                    if (questMod.p_Type == 10) { txtP_Content.Text = questMod.LargeContent; }
                    txtDefaSocre.Text = questMod.p_defaultScores.ToString();
                    ddlNumber1.SelectedValue = questMod.p_ChoseNum.ToString();
                    Optioninfo_Hid.Value = SafeSC.ReadFileStr(questMod.GetOPDir());
                    Qinfo_Hid.Value = questMod.Qinfo;
                    Answer_T.Text = questMod.p_Answer;
                    txtJiexi.Value = questMod.Jiexi;
                    IsShare_Chk.Checked = questMod.IsShare == 1;
                    Version_Rad.SelectedValue = questMod.Version.ToString();
                    //LargeContent_T.Text = questMod.LargeContent;
                    function.Script(this, "SetRadVal('qtype_rad'," + questMod.p_Type + ");");
                }
            }
            else { if (NodeID > 0) { NodeID_Hid.Value = NodeID.ToString(); } }
        }
    }
    //从字典获取相关试题类型
    public void InitQuestGrade()
    {
        //难度
        //rblDiff.DataSource = B_GradeOption.GetGradeList(5, 0);
        //rblDiff.DataBind();
        //if (rblDiff.Items.Count > 0) { rblDiff.Items[0].Selected = true; }
        //年级
        Grade_Radio.DataSource = B_GradeOption.GetGradeList(6, 0);
        Grade_Radio.DataBind();
        if (Grade_Radio.Items.Count > 0) { Grade_Radio.Items[0].Selected = true; }
        Version_Rad.DataSource = verBll.Sel();
        Version_Rad.DataBind();
        if (Version_Rad.Items.Count > 0) { Version_Rad.Items[0].Selected = true; }
    }
    // 保存
    protected void EBtnSubmit_Click(object sender, EventArgs e)
    {
        M_Exam_Sys_Questions questMod = FillMod();
        if (Mid > 0)
        {
          questBll.GetUpdate(questMod);
        }
        else
        {
            questMod.p_id = questBll.GetInsert(questMod);
        }
        SafeSC.WriteFile(questMod.GetOPDir(), Optioninfo_Hid.Value);
        if (IsSmall > 0)//判断当前是否是添加小题状态
        {
            DataTable dt = questBll.SelByIDSForType(questMod.p_id.ToString());
            string json = JsonConvert.SerializeObject(dt);
            function.Script(this, "parent.SelQuestion(" + json + ")");
        }
        else { function.WriteSuccessMsg("操作成功!", "QuestList.aspx?NodeID="+NodeID); }
    }
    //添加为新文章
    protected void Save_New_Btn_Click(object sender, EventArgs e)
    {
        int result = questBll.GetInsert(FillMod());
        function.WriteSuccessMsg("添加成功!", "QuestList.aspx?NodeID=" + NodeID);
    }
    private M_Exam_Sys_Questions FillMod() 
    {
        M_UserInfo mu = buser.GetLogin();
        M_Exam_Sys_Questions questMod = null;
        if (Mid > 0)
        {
            questMod = questBll.GetSelect(Mid);
        }
        else 
        { 
            questMod = new M_Exam_Sys_Questions();
            questMod.UserID = mu.UserID;
            questMod.p_Inputer = mu.UserName;
        }
        questMod.p_title = txtP_title.Text;
        //questMod.p_Difficulty = DataConverter.CLng(rblDiff.SelectedValue);
        questMod.p_Difficulty = DataConverter.CDouble(Diffcult_T.Text);
        questMod.p_Class = DataConverter.CLng(NodeID_Hid.Value);
        //questMod.p_Shipin = QuestType_Hid.Value;
        questMod.p_Views = DataConverter.CLng(Grade_Radio.SelectedValue);
        questMod.p_Knowledge = DataConverter.CLng(Request.Form["knowname"]);
        string tagkey = Request.Form["Tabinput"];
        if (string.IsNullOrEmpty(tagkey))
        {
            questMod.Tagkey = "";
        }
        else
        {
            int firstid = nodeBll.SelFirstNodeID(questMod.p_Class);
            questMod.Tagkey = knowBll.AddKnows(firstid, tagkey,0,false);
        }
        //questMod.p_Order = DataConverter.CLng(Order.Text);
        if (string.IsNullOrEmpty(AnswerHtml_T.Text)) { AnswerHtml_T.Text = Answer_T.Text; }
        questMod.p_Type = DataConverter.CLng(Request.Form["qtype_rad"]);
        questMod.p_shuming = AnswerHtml_T.Text;
        if (questMod.p_Type == 10) { questMod.p_Content = Qids_Hid.Value;questMod.LargeContent = txtP_Content.Text; }
        else { questMod.p_Content = txtP_Content.Text; }
        questMod.Qinfo=Qinfo_Hid.Value;
        questMod.p_ChoseNum = DataConverter.CLng(ddlNumber1.SelectedValue);
        questMod.IsBig = 0;
        questMod.IsShare = IsShare_Chk.Checked ? 1 : 0;
        questMod.p_defaultScores = DataConverter.CFloat(txtDefaSocre.Text);
        questMod.p_Answer = Answer_T.Text.Trim();
        //questMod.p_Optioninfo = Optioninfo_Hid.Value;
        questMod.Jiexi = txtJiexi.Value;
        questMod.Version = DataConverter.CLng(Version_Rad.SelectedValue);
        return questMod;
    }
    #region 节点菜单(后期优化)
    string treetlp = "<img src='/Images/TreeLineImages/tree_line4.gif'  border='0' width='19' height='20' />";
    string parent_tlp = "<li data-pid='@pid'><a href='javascript:;' style='@isshow' data-pid='@pid' data-id=@id onclick='Tlp_toggleChild(this,@id)'>@tree4<span class='fa fa-folder'></span> @name</a></li>";
    string questtlp = "<li><a href='javascript:;' onclick='SelQuestType(this,@id)' data-id=@id style='display: none;' data-pid='@pid' onclick=''>@tree4<img src='/Images/TreeLineImages/t.gif'><span class='fa fa-file'></span> @name</a></li>";
    public string GetAllQuest(List<M_Exam_Class> questTypes, int pid, int index)
    {
        string html = ""; string tree4 = "";
        for (int i = 0; i < index; i++)
            tree4 += treetlp;
        List<M_Exam_Class> parents = questTypes.FindAll(m => m.C_Classid == pid);
        foreach (var item in parents)
        {
            if (questTypes.Exists(m => m.C_Classid == item.C_id))
            {
                html += parent_tlp.Replace("@id", item.C_id.ToString()).Replace("@name", item.C_ClassName).Replace("@tree4", tree4).Replace("@pid", item.C_Classid.ToString()).Replace("@isshow", pid > 0 ? "display:none" : "");
                html += GetAllQuest(questTypes, item.C_id, index + 1);
            }
            else
            {
                html += questtlp.Replace("@tree4", tree4).Replace("@pid", item.C_Classid.ToString()).Replace("@id", item.C_id.ToString()).Replace("@name", item.C_ClassName);
            }
        }
        return html;
    }
    public void GetQuesType()
    {
        Quest_Tree.DataSource = nodeBll.Select_All();
        Quest_Tree.MyBind();
    }
    #endregion
}
