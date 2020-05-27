using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.BLL;
using ZoomLa.BLL.Exam;

public partial class manage_Question_Add_Papers_System : CustomerPageAction
{
    B_Exam_Sys_Papers paperBll=new B_Exam_Sys_Papers();
    B_Exam_PaperNode nodeBll = new B_Exam_PaperNode();
    B_Exam_Class clsBll = new B_Exam_Class();
    B_User buser = new B_User();

    public int Mid { get { return DataConverter.CLng(Request.QueryString["id"]); } }
    public string Menu { get { return (Request.QueryString["menu"] ?? "").ToLower(); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //if (!buser.IsTeach()) { function.WriteErrMsg("当前页面只有教师才可访问"); }
            GetQuesType();
            if (Mid > 0)
            {
                M_Exam_Sys_Papers paperMod = paperBll.GetSelect(Mid);
                M_UserInfo mu = buser.GetLogin();
                if (mu.UserID != paperMod.UserID) { function.WriteErrMsg("你无权修改该试卷"); }
                AddToNew_Btn.Visible = true;
                if (paperMod != null && paperMod.id > 0)
                {
                    this.txtPaperName.Text = paperMod.p_name;
                    NodeID_Hid.Value = paperMod.p_class.ToString();
                    ddType.SelectedValue = paperMod.p_type.ToString();
                    txtRemark.Text = paperMod.p_Remark;
                    ddRtyle.SelectedValue = paperMod.p_Style.ToString();
                    txtTime.Text = paperMod.p_UseTime.ToString();
                    txtBegionTime.Text = paperMod.p_BeginTime.ToString();
                    txtEndTime.Text = paperMod.p_endTime.ToString();
                    TagKey_T.Text = paperMod.TagKey;
                }
            }
            else
            {
                txtBegionTime.Text = DateTime.Now.ToString("yyyy/MM/dd 00:00");
                txtEndTime.Text = DateTime.Now.AddMonths(1).ToString("yyyy/MM/dd 00:00");
            }
        }
    }
    protected void EBtnSubmit_Click(object sender, EventArgs e)
    {
        if (Mid > 0)
        {
            M_Exam_Sys_Papers paperMod = paperBll.SelReturnModel(Mid);
            paperMod = FillMod(paperMod);
            paperBll.UpdateByID(paperMod);
        }
        else
        {
            M_Exam_Sys_Papers paperMod = FillMod();
            paperBll.GetInsert(paperMod);
        }
        function.WriteSuccessMsg("操作成功!", "Papers_System_Manage.aspx");
    }
    //添加为新试卷
    protected void AddToNew_Btn_Click(object sender, EventArgs e)
    {
        M_Exam_Sys_Papers paperMod = paperBll.SelReturnModel(Mid);
        paperMod.id = 0;
        paperMod = FillMod(paperMod);
        paperMod.p_name = paperMod.p_name + "_" + function.GetRandomString(6,2);
        paperBll.GetInsert(paperMod);
        function.WriteSuccessMsg("添加成功!", "Papers_System_Manage.aspx");
    }
    private M_Exam_Sys_Papers FillMod(M_Exam_Sys_Papers model = null)
    {
        M_UserInfo mu = buser.GetLogin();
        if (model == null) { model = new M_Exam_Sys_Papers(); }
        if (model.UserID < 1) { model.UserID = mu.UserID; }
        model.p_name = this.txtPaperName.Text.Trim();
        model.p_class = DataConverter.CLng(NodeID_Hid.Value);
        model.p_type = DataConverter.CLng(ddType.SelectedValue);
        model.p_Remark = txtRemark.Text;
        model.p_UseTime = DataConverter.CDouble(txtTime.Text);
        model.p_BeginTime = DataConverter.CDate(txtBegionTime.Text);
        model.p_endTime = DataConverter.CDate(txtEndTime.Text);
        model.p_Style = DataConverter.CLng(ddRtyle.SelectedValue);
        model.TagKey = Request.Form["tabinput"];
        return model;
    }
    public void GetQuesType()
    {
        DataTable dt = clsBll.Select_All();
        Quest_Tree.DataSource = dt;
        Quest_Tree.MyBind();
    }
}
