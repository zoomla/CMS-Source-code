using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Exam;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.Exam;
using ZoomLa.SQLDAL;

public partial class User_Exam_AddVersion : System.Web.UI.Page
{
    B_Exam_Class nodeBll = new B_Exam_Class();
    B_Exam_Version verBll = new B_Exam_Version();
    B_Questions_Knowledge knowBll = new B_Questions_Knowledge();
    M_Exam_Version verMod = null;
    B_User buser = new B_User();
    private int Mid { get { return DataConvert.CLng(Request.QueryString["ID"]); } }
    private int Pid { get { return DataConvert.CLng(Request.QueryString["pid"]); } }
    //private int Pid { get { return DataConvert.CLng(ViewState["Pid"]); } set { ViewState["Pid"] = value; } }
    /// <summary>
    /// 标识页面任务状态,1:添加版本，2:添加章节,3:添加课程、知识点
    /// </summary>
    private int WorkStatus
    {
        get
        {
            if (Mid > 0)//修改
            {
                M_Exam_Version Mod = verBll.SelReturnModel(Mid);
                if (Mod.Pid > 0)
                {
                    M_Exam_Version pverMod = verBll.SelReturnModel(Mod.Pid);
                    if (pverMod.Pid > 0) { return -3; }//修改课程、知识点
                    else { return -2; }//修改章节
                }
                else { return -1; }//修改版本
            }
            else//添加
            {
                if (Pid > 0)
                {
                    M_Exam_Version pverMod = verBll.SelReturnModel(Pid);
                    if (pverMod.Pid > 0) { return 3; } //添加课程，知识点
                    else { return 2; }//添加章节
                }
                else { return 1; }//添加版本
            }
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!buser.IsTeach()) { function.WriteErrMsg("该页面只有教师可访问"); }
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    private void MyBind()
    {
        M_UserInfo mu = buser.GetLogin();
        Grade_Radio.DataSource = B_GradeOption.GetGradeList(6, 0);
        Grade_Radio.DataBind();
        if (Grade_Radio.Items.Count > 0) { Grade_Radio.SelectedIndex = 0; }
        NodeTree.DataSource = nodeBll.Select_All();
        NodeTree.MyBind();
        if (Mid > 0)
        {
            verMod = verBll.SelReturnModel(Mid);
            VName_T.Text = verMod.VersionName;
            Inputer_L.Text = verMod.Inputer;
            VTime_T.Text = verMod.VersionTime;
            Grade_Radio.SelectedValue = verMod.Grade.ToString();
            NodeID_Hid.Value = verMod.NodeID.ToString();
            Volume_T.Text = verMod.Volume;
            Chapter_T.Text = verMod.Chapter;
            SectionName_T.Text = verMod.SectionName;
            CourseName_T.Text = verMod.CourseName;
            Price_T.Text = verMod.Price.ToString("f2");
            if (!string.IsNullOrEmpty(verMod.Knows)) { TagKey_T.Value = knowBll.GetNamesByIDS(verMod.Knows); }
            if (mu.UserID != verMod.UserID) { function.WriteErrMsg("你无权修改该信息"); }
        }
        else { Inputer_L.Text = mu.UserName; }
        switch (WorkStatus)
        {
            case 1://添加版本教材
            case -1://修改版本教材
                {
                    //Chapter_Tr.Visible = false;
                    //Section_Body.Visible = false;
                }
                break;
            case 2://添加章节
                {
                    LoadParent(Pid);
                    //Section_Body.Visible = false;
                }
                break;
            case 3://添加知识点
                {
                    LoadParent(Pid);
                }
                break;
            case -2:
                {
                    verMod = verBll.SelReturnModel(Mid);
                    LoadParent(verMod.Pid);
                    //Section_Body.Visible = false;
                }
                break;
            case -3:
                {
                    verMod = verBll.SelReturnModel(Mid);
                    LoadParent(verMod.Pid);
                }
                break;
        }
    }
    protected void LoadParent(int pid)
    {
        //当作为子级添加时，版本名称、科目、版本时间、册序、年级、价格不可编辑，值与父级相同
        M_Exam_Version pverMod = verBll.SelReturnModel(pid);
        VName_T.Text = pverMod.VersionName;
        Inputer_L.Text = pverMod.Inputer;
        VTime_T.Text = pverMod.VersionTime;
        Grade_Radio.SelectedValue = pverMod.Grade.ToString();
        NodeID_Hid.Value = pverMod.NodeID.ToString();
        Volume_T.Text = pverMod.Volume;
        Price_T.Text = pverMod.Price.ToString("f2");

        VName_T.ReadOnly = true;
        VTime_T.ReadOnly = true;
        Grade_Radio.Enabled = false;
        Price_T.ReadOnly = true;
        Volume_T.ReadOnly = true;
        btnlist.Visible = false;
        function.Script(this, "readonly();");
        if (WorkStatus == 3 || WorkStatus == -3)
        {
            Chapter_T.Text = pverMod.Chapter;
            Chapter_T.ReadOnly = true;
        }
    }
    protected void Save_Btn_Click(object sender, EventArgs e)
    {
        M_UserInfo mu = buser.GetLogin();
        verMod = new M_Exam_Version();
        if (Mid > 0) { verMod = verBll.SelReturnModel(Mid); }
        verMod.VersionName = VName_T.Text;
        verMod.Inputer = mu.UserName;
        verMod.VersionTime = VTime_T.Text;
        verMod.NodeID = DataConvert.CLng(NodeID_Hid.Value);
        verMod.Grade = DataConvert.CLng(Grade_Radio.SelectedValue);
        verMod.Volume = Volume_T.Text;
        verMod.Chapter = Chapter_T.Text;
        verMod.SectionName = SectionName_T.Text;
        verMod.CourseName = CourseName_T.Text;
        string tagkey = Request.Form["Tabinput"];
        if (string.IsNullOrEmpty(tagkey))
        {
            verMod.Knows = "";
        }
        else
        {
            int firstid = nodeBll.SelFirstNodeID(DataConvert.CLng(NodeID_Hid.Value));
            verMod.Knows = knowBll.AddKnows(firstid, tagkey);
        }
        if (Mid > 0)
        {
            verBll.UpdateByID(verMod);
        }
        else
        {
            verMod.UserID = mu.UserID;
            verMod.Pid = Pid;
            verBll.Insert(verMod);
        }
        function.WriteSuccessMsg("操作成功", "VersionList.aspx");
    }
}