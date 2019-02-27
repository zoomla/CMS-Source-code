using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Components;
using ZoomLa.Common;
using ZoomLa.Model;

public partial class manage_Question_AddExTeacher : CustomerPageAction
{
    private B_ExTeacher bet = new B_ExTeacher();
    private B_Exam_Class bqc = new B_Exam_Class();
    private B_Admin badmin = new B_Admin();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int tid = DataConverter.CLng(Request.QueryString["id"]);
            hftid.Value = tid.ToString();
            M_ExTeacher met = bet.GetSelect(tid);
            if (met != null && met.ID > 0)
            {
                txt_name.Text = met.TName;
                txt_Post.Text = met.Post;
                txt_Teach.Text = met.Teach;
                //txtClassname.Text = GetClassname(met.TClsss);
                hfid.Value = met.TClsss.ToString();
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
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='Papers_System_Manage.aspx'>教育模块</a></li><li><a href='ExTeacherManage.aspx'>培训资源库</a></li><li><a href='ExTeacherManage.aspx'>教师管理</a></li><li>" + liCoures.Text + "</li>");
        }
    }

    private string GetClassname(int classid)
    {
        M_Exam_Class mqc = bqc.GetSelect(classid);
        if (mqc != null && mqc.C_id > 0)
        {
            return mqc.C_ClassName;
        }
        else
        {
            return "";
        }
    }
    protected void EBtnSubmit_Click(object sender, EventArgs e)
    {
        int tid = DataConverter.CLng(hftid.Value);
        M_ExTeacher met = bet.GetSelect(tid);
        met.TName = BaseClass.Htmlcode(txt_name.Text);
        met.Teach = BaseClass.Htmlcode(txt_Teach.Text);
        met.Post = BaseClass.Htmlcode(txt_Post.Text);
        met.Remark = BaseClass.Htmlcode(textarea1.Text);
        met.AddUser = badmin.GetAdminLogin().AdminId;
        met.TClsss = DataConverter.CLng(hfid.Value);
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