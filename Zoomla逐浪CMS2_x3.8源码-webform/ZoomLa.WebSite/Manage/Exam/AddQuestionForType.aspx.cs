using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using ZoomLa.Common;
using ZoomLa.BLL;
using ZoomLa.Model;

public partial class manage_Question_AddQuestionForType : CustomerPageAction
{
    protected B_Exam_Class bq = new B_Exam_Class();
    protected B_Exam_Sys_Papers bps = new B_Exam_Sys_Papers();
    protected B_ExamPoint bqk = new B_ExamPoint();
    protected int deep = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable classtable = bq.GetSelectByC_ClassId(0);
            for (int i = 0; i < classtable.Rows.Count; i++)
            {
                ListItem item = new ListItem();
                item.Value = classtable.Rows[i]["C_id"].ToString();
                item.Text = "├-" + classtable.Rows[i]["C_ClassName"].ToString();
                classtype.Items.Add(item);
                loaditem(DataConverter.CLng(classtable.Rows[i]["C_id"].ToString()));
                deep = deep - 1;
            }
            classtable.Dispose();
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li> <li><a href='Papers_System_Manage.aspx'>教育模块</a></li><li><a href='QuestionManage.aspx'>考试管理</a></li><li><a href='QuestionManage.aspx'>试题管理</a></li><li>" + Label2.Text + "</li>");
        }
    }

    protected void loaditem(int cid)
    {
        deep = deep + 1;
        DataTable classtable = bq.GetSelectByC_ClassId(cid);
        if (classtable != null && classtable.Rows.Count > 0)
        {
            for (int i = 0; i < classtable.Rows.Count; i++)
            {
                ListItem item = new ListItem();
                item.Value = classtable.Rows[i]["C_id"].ToString();
                string spantxt = new string('-', deep*2);
                item.Text = "├-"+spantxt + classtable.Rows[i]["C_ClassName"].ToString();
                classtype.Items.Add(item);
                loaditem(DataConverter.CLng(classtable.Rows[i]["C_id"]));
                deep = deep - 1;
            }
        }
        classtable.Dispose();
    }



    /// <summary>
    /// 类型
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

    protected void Button1_Click(object sender, EventArgs e)
    {
        int qtype = DataConverter.CLng(this.classtype.SelectedValue);
        M_Exam_Class clasinfo = bq.GetSelect(qtype);
        Response.Redirect("AddEngLishQuestion.aspx?classs=" + qtype);
    }
}
