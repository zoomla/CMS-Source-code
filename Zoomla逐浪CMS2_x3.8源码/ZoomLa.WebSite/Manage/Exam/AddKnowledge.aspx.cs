using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;
using System.Data;

public partial class Manage_Exam_AddKnowledge : System.Web.UI.Page
{
    B_Questions_Knowledge knowBll = new B_Questions_Knowledge();
    B_Exam_Class classBll = new B_Exam_Class();
    B_Admin badmin = new B_Admin();
    B_User buser = new B_User();
    public int KnowID { get { return DataConverter.CLng(Request.QueryString["id"]); } }
    public int Pid { get { return DataConverter.CLng(Request.QueryString["pid"]); } }
    public int NodeID { get { return DataConverter.CLng(Request.QueryString["nid"]); } }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li> <li><a href='Papers_System_Manage.aspx'>教育模块</a></li><li><a href='KnowledgeManage.aspx'>知识点管理</a></li><li class='active'>编辑知识点</a></li>");
            GradeBind();
            MyBind();
            
        }
    }
    public void MyBind()
    {
        if (Pid > 0)
        {
            PKnow_L.Text = knowBll.SelReturnModel(Pid).K_name;
        }
        if (NodeID > 0)//添加知识点
        {
            QuessClass_L.Text= classBll.GetSelect(NodeID).C_ClassName;
        }
        if (KnowID > 0)
        {
            M_Questions_Knowledge knowMod = knowBll.SelReturnModel(KnowID);
            Name_T.Text = knowMod.K_name;
            PKnow_L.Text = "无";
            M_Questions_Knowledge parentMod = knowBll.SelReturnModel(knowMod.Pid);
            if (parentMod != null)
            {
                PKnow_L.Text = knowBll.SelReturnModel(knowMod.Pid).K_name ;
            }
            QuessClass_L.Text = classBll.GetSelect(knowMod.K_class_id).C_ClassName;
            GradeList_Drop.SelectedValue = knowMod.Grade.ToString();
            IsSyst_Check.Checked = knowMod.IsSys == 1;
            Status_Check.Checked = knowMod.Status == 1;
            OldName_Hid.Value = knowMod.K_name;
        }
    }

    protected void Save_Btn_Click(object sender, EventArgs e)
    {
        M_Questions_Knowledge knowMod = new M_Questions_Knowledge();
        if (KnowID > 0) { knowMod = knowBll.SelReturnModel(KnowID); }
        knowMod.CDate = DateTime.Now;
        knowMod.CUser = badmin.GetAdminLogin().AdminId;
        string name = Name_T.Text.Replace(" ", "");//替换空格
        knowMod.K_name = name;
        knowMod.Status = Status_Check.Checked ? 1 : 0;
        knowMod.IsSys = IsSyst_Check.Checked ? 1 : 0;
        knowMod.Pid = KnowID > 0 ? knowMod.Pid : Pid;
        knowMod.Grade = DataConverter.CLng(GradeList_Drop.SelectedValue);
        knowMod.K_class_id = NodeID;
        if (!OldName_Hid.Value.Equals(name))
        {
            DataTable tempdt = knowBll.SelByName(NodeID, Name_T.Text);
            if (tempdt.Rows.Count > 0) { function.WriteErrMsg("同级下知识点名称不能重复!"); }
        }
        if (KnowID > 0)
        {
            knowMod.K_id = KnowID;
            knowBll.GetUpdate(knowMod);
        }
        else
        {
            knowBll.insert(knowMod);
        }
        function.WriteSuccessMsg("操作成功!", "KnowledgeManage.aspx?nid="+NodeID);
    }
    public void GradeBind()
    {
        GradeList_Drop.DataSource = B_GradeOption.GetGradeList(6, 0);
        GradeList_Drop.DataBind();
    }

    
}