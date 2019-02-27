using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Components;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.BLL.Exam;
using System.Data;

public partial class manage_Question_AddExTeacher : CustomerPageAction
{
    private B_ExTeacher bet = new B_ExTeacher();
    private B_Exam_Class bqc = new B_Exam_Class();
    private B_Admin badmin = new B_Admin();
    B_Exam_PaperNode nodeBll = new B_Exam_PaperNode();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int tid = DataConverter.CLng(Request.QueryString["id"]);
            hftid.Value = tid.ToString();
            M_ExTeacher met = bet.GetSelect(tid);
            DataTable dt = nodeBll.Sel();
            PaperType_Drop.DataSource = dt;
            PaperType_Drop.DataBind();
            if (dt.Rows.Count <= 0) { PaperType_Drop.Items.Add(new ListItem("未定义分类", "-1")); }
           
            if (met != null && met.ID > 0)
            {
                txt_name.Text = met.TName;
                txt_Post.Text = met.Post;
                txt_Teach.Text = met.Teach;
                //txtClassname.Text = GetClassname(met.TClsss);
                PaperType_Drop.SelectedValue = met.TClsss.ToString();
                textarea1.Text = met.Remark;
               
                liCoures.Text = "修改教师";
                Label1.Text = "修改教师";
            }
            else
            {
                liCoures.Text = "添加教师";
                Label1.Text = "添加教师";
            }
            //txtClassname.Enabled = false; 
        }
    }

   
    protected void EBtnSubmit_Click(object sender, EventArgs e)
    {
        int tid = DataConverter.CLng(hftid.Value);
        M_ExTeacher met = bet.GetSelect(tid);
        met.TName = BaseClass.Htmlcode(txt_name.Text);
        met.Teach = BaseClass.Htmlcode(txt_Teach.Text);
        met.Post = BaseClass.Htmlcode(txt_Post.Text);
        met.Remark = textarea1.Text;
        met.AddUser = badmin.GetAdminLogin().AdminId;
        met.TClsss = DataConverter.CLng(PaperType_Drop.SelectedValue);
        if (tid > 0)
        {
            bool result = bet.GetUpdate(met);
            if (result)
            {
                function.WriteSuccessMsg("修改成功！", "ExTeacherManage.aspx");
            }
            else
            {
                function.WriteErrMsg("修改失败！");
            }
        }
        else
        {
            met.CreatTime = DateTime.Now;
            int ids = bet.GetInsert(met);
            if (ids > 0)
            {
                function.WriteSuccessMsg("添加成功！", "ExTeacherManage.aspx");
            }
            else
            {
                function.WriteErrMsg("添加失败！");
            }
        }
    }
}