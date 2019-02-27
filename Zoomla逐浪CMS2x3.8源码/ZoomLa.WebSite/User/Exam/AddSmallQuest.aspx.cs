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
using System.Text;


public partial class User_Exam_AddSmallQuest : System.Web.UI.Page
{
    protected B_Exam_Sys_Questions questBll = new B_Exam_Sys_Questions();
    protected B_Exam_Sys_Papers bps = new B_Exam_Sys_Papers();
    B_User buser = new B_User();
    B_Admin badmin = new B_Admin();
    public int QuestID { get { return DataConverter.CLng(Request.QueryString["id"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!buser.IsTeach() && !badmin.CheckLogin()) { function.WriteErrMsg("您必须教师才能访问!"); }
        if (!IsPostBack)
        {
            if (QuestID > 0) { MyBind(); }
        }
    }
    public void MyBind()
    {
        M_Exam_Sys_Questions questMod = questBll.GetSelect(QuestID);
        txtP_Content.Text = questMod.p_Content;
        ddlNumber1.SelectedValue = questMod.p_ChoseNum.ToString();
        //txtP_title.Text = questMod.p_title;
        Optioninfo_Hid.Value= SafeSC.ReadFileStr(questMod.GetOPDir());
        Answer_T.Text = questMod.p_Answer;
        AnswerHtml_T.Text = questMod.p_shuming;
        function.Script(this, "SetRadVal('qtype_rad'," + questMod.p_Type + ");");
    }

    protected void EBtnSubmit_Click(object sender, EventArgs e)
    {
        M_Exam_Sys_Questions questMod = FillMod();
        if (QuestID > 0) { questBll.GetUpdate(questMod); }
        else { questMod.p_id = questBll.GetInsert(questMod); }
        SafeSC.WriteFile(questMod.GetOPDir(), Optioninfo_Hid.Value);
        DataTable dt = questBll.SelByIDSForType(questMod.p_id.ToString());
        string json = JsonConvert.SerializeObject(dt);
        function.Script(this, "parent.SelQuestion(" + json + ","+QuestID+")");
    }
    public M_Exam_Sys_Questions FillMod()
    {
        M_UserInfo userinfo = buser.GetLogin();
        M_Exam_Sys_Questions questMod = new M_Exam_Sys_Questions();
        if (QuestID > 0)
        {
            questMod = questBll.GetSelect(QuestID);
        }
        if(buser.CheckLogin())
        {
            questMod.UserID = userinfo.UserID;
            questMod.p_Inputer = userinfo.UserName;
        }
        //questMod.p_title = txtP_title.Text;
        questMod.p_Difficulty = DataConverter.CDouble(rblDiff_Hid.Value);
        questMod.p_Class = DataConverter.CLng(NodeID_Hid.Value);
        questMod.p_Views = DataConverter.CLng(p_Views_Hid.Value);
        questMod.Tagkey = Tagkey_Hid.Value;
        questMod.p_Type = DataConverter.CLng(Request.Form["qtype_rad"]);
        questMod.p_shuming = AnswerHtml_T.Text;
        questMod.p_Content = txtP_Content.Text;
        questMod.p_ChoseNum = DataConverter.CLng(ddlNumber1.SelectedValue);
        questMod.IsBig = 0;
        questMod.IsShare = DataConverter.CLng(IsShare_Hid.Value);
        questMod.p_defaultScores = DataConverter.CFloat(p_defaultScores_Hid.Value);
        questMod.p_Answer = Answer_T.Text.Trim();
        questMod.Jiexi = Jiexi_Hid.Value;
        questMod.Version = DataConverter.CLng(Version_Hid.Value);
        questMod.IsSmall = 1;
        return questMod;
    }


}