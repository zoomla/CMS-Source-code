using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Components;
using System.Data;

public partial class manage_Question_AddPaperQuestion : CustomerPageAction
{
    private B_Exam_Type bqt = new B_Exam_Type ();
    private B_Paper_Questions bpq = new B_Paper_Questions();
    private B_Admin badmin = new B_Admin();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Bind();
            int id = DataConverter.CLng(Request.QueryString["id"]);
            int pid = DataConverter.CLng(Request.QueryString["pid"]);
            hfId.Value = id.ToString();
            hfpaperId.Value = pid.ToString();
            if (id > 0)
            {
                liPaper.Text = "修改大题";
                Label1.Text = "修改大题";
                M_Paper_Questions mpq = bpq.GetSelect(id);
                if (mpq != null && mpq.ID > 0)
                {
                    txt_name.Text = mpq.QuestionTitle;
                    ddType.SelectedValue = mpq.QuestionType.ToString();
                    txtSubTitle.Text = mpq.Subtitle;
                    txt_OrderBy.Text = mpq.OrderBy.ToString();
                    txtCourse.Text = mpq.Course.ToString();
                    txtNum.Text = mpq.QuesNum.ToString();
                    txtRemark.Text = mpq.Remark;
                }
            }
            else
            {
                liPaper.Text = "添加大题";
                Label1.Text = "添加大题";
                DataTable dt = bpq.Select_PaperId(pid);
                if (dt != null)
                {
                    txt_OrderBy.Text = (dt.Rows.Count+1).ToString();
                }
                else
                {
                    txt_OrderBy.Text = "1";
                }
            }
            Call.SetBreadCrumb(Master, "<li>教育模块</li><li><a href='QuestionManage.aspx'>在线考试系统</a></li><li>试卷管理</li><li>大题管理</li><li>" + liPaper.Text+ "</li>");
        }
    }

    private void Bind()
    {
        List<M_Exam_Type> dt = bqt.SelectAll();
        if (dt != null && dt.Count > 0)
        {
            ddType.Items.Clear();
            foreach (M_Exam_Type item in dt)
            {
                ListItem li = new ListItem();
                li.Text = item.t_name;
                li.Value = item.t_id.ToString();
                ddType.Items.Add(li);
            }
        }
    }
    protected void EBtnSubmit_Click(object sender, EventArgs e)
    {
        int id = DataConverter.CLng(hfId.Value);
        M_Paper_Questions mpq = bpq.GetSelect(id);
        mpq.QuestionTitle = txt_name.Text;
        mpq.QuestionType = DataConverter.CLng(ddType.SelectedValue);
        mpq.Remark = txtRemark.Text;
        mpq.Subtitle = txtSubTitle.Text;
        mpq.QuesNum = DataConverter.CLng(txtNum.Text);
        mpq.OrderBy = DataConverter.CLng(txt_OrderBy.Text);
        mpq.Course = DataConverter.CDouble(txtCourse.Text);
        mpq.AddUser = badmin.GetAdminLogin().AdminId;
        if (id > 0)
        {
            bool result = bpq.GetUpdate(mpq);
            if (result)
            {
                function.WriteSuccessMsg("修改成功", "Paper_QuestionManage.aspx?pid=" + hfpaperId.Value );
            }
            else
            {
                function.WriteErrMsg("修改失败");
            }
        }
        else
        {
            mpq.PaperID = DataConverter.CLng(hfpaperId.Value);
            mpq.CreateTime = DateTime.Now;
            int ids = bpq.GetInsert(mpq);
            if (ids > 0)
            {
                function.WriteSuccessMsg("添加成功", "Paper_QuestionManage.aspx?pid=" + hfpaperId.Value);
            }
            else
            {
                function.WriteErrMsg("添加失败");
            }
        }
    }
}