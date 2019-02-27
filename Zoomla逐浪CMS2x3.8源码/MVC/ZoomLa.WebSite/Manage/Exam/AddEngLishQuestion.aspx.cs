using System;
using System.IO;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using ZoomLa.Common;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Components;
using ZoomLa.BLL.Helper;
using Newtonsoft.Json;
using ZoomLa.BLL.Exam;
namespace ZoomLaCMS.Manage.Exam
{
    public partial class AddEngLishQuestion : System.Web.UI.Page
    {
        B_Exam_Sys_Questions questBll = new B_Exam_Sys_Questions();
        B_Exam_Sys_Papers bps = new B_Exam_Sys_Papers();
        B_Exam_Class nodeBll = new B_Exam_Class();
        B_Exam_Type bqt = new B_Exam_Type();
        B_Questions_Knowledge knowBll = new B_Questions_Knowledge();
        B_ExamPoint bep = new B_ExamPoint();
        B_Exam_Version verBll = new B_Exam_Version();
        B_User buser = new B_User();
        public int Mid { get { return DataConverter.CLng(Request.QueryString["id"]); } }
        //1:大题添加小题
        private int IsSmall { get { return DataConverter.CLng(Request.QueryString["issmall"]); } }
        public int NodeID { get { return DataConverter.CLng(Request.QueryString["NodeID"]); } }
        public int Grade { get { return DataConverter.CLng(Request.QueryString["Grade"]); } }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (IsSmall > 0) { MasterPageFile = "~/User/Empty.master"; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Call.HideBread(Master);
            if (!IsPostBack)
            {

                if (NodeID > 0)
                {
                    M_Exam_Class nodeMod = nodeBll.GetSelect(NodeID);
                    CurNode_L.Text = "[" + nodeMod.C_ClassName + "]";
                }
                else
                {
                    nodetr.Style.Remove("display");
                }
                if (Grade > 0)
                {
                    CurNode_L.Text += "[" + B_GradeOption.GetGradeOption(Grade).GradeName + "]";
                    Grade_Radio.SelectedValue = Grade.ToString();
                }
                //--------------
                GetQuesType();
                InitQuestGrade();
                if (Mid > 0)
                {
                    SaveNew_Btn.Visible = true;
                    M_Exam_Sys_Questions questMod = questBll.GetSelect(Mid);
                    if (questMod != null && questMod.p_id > 0)
                    {
                        txtP_title.Text = questMod.p_title;
                        //rblDiff.SelectedValue = questMod.p_Difficulty.ToString();
                        Diffcult_T.Text = questMod.p_Difficulty.ToString("f2");
                        AnswerHtml_T.Text = questMod.p_shuming;
                        Grade_Radio.SelectedValue = questMod.p_Views.ToString();
                        if (!string.IsNullOrEmpty(questMod.Tagkey)) { TagKey_T.Value = knowBll.GetNamesByIDS(questMod.Tagkey); }
                        //TagKey_T.Text = questMod.Tagkey;
                        NodeID_Hid.Value = questMod.p_Class.ToString();
                        txtP_Content.Text = questMod.p_Content;
                        if (questMod.p_Type == 10) { txtP_Content.Text = questMod.LargeContent; }
                        txtDefaSocre.Text = questMod.p_defaultScores.ToString();
                        Qinfo_Hid.Value = questMod.Qinfo;
                        ddlNumber1.SelectedValue = questMod.p_ChoseNum.ToString();
                        Optioninfo_Hid.Value = SafeSC.ReadFileStr(questMod.GetOPDir());
                        Answer_T.Text = questMod.p_Answer;
                        txtJiexi.Value = questMod.Jiexi;
                        IsShare_Chk.Checked = questMod.IsShare == 1;
                        Version_Rad.SelectedValue = questMod.Version.ToString();
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
            else { function.WriteSuccessMsg("操作完成!", "QuestList.aspx?NodeID=" + NodeID); }
        }
        protected void Save_New_Btn_Click(object sender, EventArgs e)
        {
            int result = questBll.GetInsert(FillMod());
            function.WriteSuccessMsg("添加成功!", "QuestList.aspx?NodeID=" + NodeID);
        }
        private M_Exam_Sys_Questions FillMod()
        {
            M_Exam_Sys_Questions questMod = null;
            M_AdminInfo adminMod = B_Admin.GetLogin();
            if (Mid > 0)
            {
                questMod = questBll.GetSelect(Mid);
            }
            else
            {
                questMod = new M_Exam_Sys_Questions();
                //questMod.UserID = mu.UserID;
                //questMod.p_Inputer = mu.UserName;
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
                questMod.Tagkey = knowBll.AddKnows(firstid, tagkey);
            }
            if (string.IsNullOrEmpty(AnswerHtml_T.Text)) { AnswerHtml_T.Text = Answer_T.Text; }
            questMod.p_Type = DataConverter.CLng(Request.Form["qtype_rad"]);
            questMod.p_shuming = AnswerHtml_T.Text;
            if (questMod.p_Type == 10) { questMod.p_Content = Qids_Hid.Value; questMod.LargeContent = txtP_Content.Text; }
            else { questMod.p_Content = txtP_Content.Text; }
            questMod.Qinfo = Qinfo_Hid.Value;
            questMod.p_ChoseNum = DataConverter.CLng(ddlNumber1.SelectedValue);
            questMod.IsBig = 0;
            questMod.p_Inputer = adminMod.AdminName;
            //后台试题强制分享
            questMod.IsShare = 1;// IsShare_Chk.Checked ? 1 : 0;
            questMod.p_defaultScores = DataConverter.CFloat(txtDefaSocre.Text);
            questMod.p_Answer = Answer_T.Text.Trim();
            //questMod.p_Optioninfo = Optioninfo_Hid.Value;
            questMod.Jiexi = txtJiexi.Value;
            questMod.Version = DataConverter.CLng(Version_Rad.SelectedValue);
            return questMod;
        }
        #region 节点菜单(后期优化)
        public void GetQuesType()
        {
            DataTable dt = nodeBll.Select_All();
            Quest_Tree.DataSource = dt;
            Quest_Tree.MyBind();
        }
        #endregion
    }
}