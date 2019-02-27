using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.BLL;
using ZoomLa.Model.Exam;
using ZoomLa.BLL.Exam;

public partial class manage_Question_Add_Papers_System : CustomerPageAction
{
    B_Exam_Sys_Papers paperBll=new B_Exam_Sys_Papers();
    B_Exam_Class nodeBll = new B_Exam_Class();
    public int Mid { get { return DataConverter.CLng(Request.QueryString["id"]); } }
    public int NodeID { get { return DataConverter.CLng(Request.QueryString["NodeID"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        B_ARoleAuth.CheckEx(ZLEnum.Auth.content, "ContentMange");
        if (!IsPostBack)
        {
            GetQuesType();
            if (Mid > 0)
            {
                M_Exam_Sys_Papers paperMod = paperBll.SelReturnModel(Mid);
                Label2.Text = "修改试卷";
                if (paperMod != null && paperMod.id > 0)
                {
                    txtPaperName.Text = paperMod.p_name;
                    NodeID_Hid.Value = paperMod.p_class.ToString();
                    ddType.SelectedValue = paperMod.p_type.ToString();
                    txtRemark.Text = paperMod.p_Remark;
                    //ddStyle.SelectedValue = mps.p_Style.ToString();
                    //ddRtyle.SelectedValue = paperMod.p_type.ToString();
                    txtTime.Text = paperMod.p_UseTime.ToString();
                    txtBegionTime.Text = paperMod.p_BeginTime.ToString();
                    txtEndTime.Text = paperMod.p_endTime.ToString();
                    TagKey_T.Text = paperMod.TagKey;
                }
            }
            else
            {
                Label2.Text = "添加试卷";
                if (NodeID > 0)
                {
                    NodeID_Hid.Value = NodeID.ToString();
                    Label2.Text += "[" + nodeBll.GetSelect(DataConverter.CLng(NodeID_Hid.Value)).C_ClassName + "]";
                }
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='Papers_System_Manage.aspx'>教育模块</a></li> <li><a href='Papers_System_Manage.aspx'>试卷管理</a></li><li>" + Label2.Text + "</li>");
        }
    }
    protected void EBtnSubmit_Click(object sender, EventArgs e)
    {
        M_Exam_Sys_Papers paperMod = new M_Exam_Sys_Papers();
        if (Mid > 0) { paperMod = paperBll.SelReturnModel(Mid); }
        paperMod.p_name = txtPaperName.Text.Trim();
        paperMod.p_class = DataConverter.CLng(NodeID_Hid.Value);
        paperMod.p_type = DataConverter.CLng(ddType.SelectedValue);
        paperMod.p_Remark = txtRemark.Text;
        paperMod.p_UseTime = DataConverter.CDouble(txtTime.Text);
        paperMod.p_BeginTime = DataConverter.CDate(txtBegionTime.Text);
        paperMod.p_endTime = DataConverter.CDate(txtEndTime.Text);
        paperMod.TagKey = Request.Form["tabinput"];
        if (Mid > 0)
        {
            paperBll.UpdateByID(paperMod);
        }
        else
        {
            paperBll.GetInsert(paperMod);
        }
        function.WriteSuccessMsg("操作成功", "Papers_System_Manage.aspx");
    }
    public void GetQuesType()
    {
        DataTable dt = nodeBll.Select_All();
        Quest_Tree.DataSource = dt;
        Quest_Tree.MyBind();
    }
}
