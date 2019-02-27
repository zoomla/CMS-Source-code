using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Components;
using ZoomLa.Common;
using ZoomLa.Model;

public partial class manage_Question_AddQuestionType : CustomerPageAction
{
    private B_Exam_Type bqt = new B_Exam_Type ();
    private B_Admin badmin = new B_Admin();
        protected void Page_Load(object sender, EventArgs e)
    {
        function.AccessRulo();
        badmin.CheckIsLogin();
        if (!IsPostBack)
        {
            int id = DataConverter.CLng(Request.QueryString["id"]);
            if (id > 0)
            {
                liQuestionType.Text = "修改题型";
                Label1.Text = "修改题型";
                hftid.Value = id.ToString();
                M_Exam_Type mqt = bqt.GetSelectById(id);
                txt_name.Text = mqt.t_name;
                txtRemark.Text = mqt.t_remark;
                ddType.SelectedValue = mqt.t_type.ToString();
            }
            else
            {
                liQuestionType.Text = "添加题型";
                Label1.Text = "添加题型";
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li> <li><a href='Papers_System_Manage.aspx'>教育模块</a></li> <li><a href='QuestionManage.aspx'>考试管理</a></li> <li><a href='QuestionTypeManage.aspx'>题型管理</a></li><li>修改题型</li>");
        }
    }


    protected void EBtnSubmit_Click(object sender, EventArgs e)
    {
        int id = DataConverter.CLng(hftid.Value);
        M_Exam_Type mqt = bqt.GetSelectById(id);
        mqt.t_type = DataConverter.CLng(ddType.SelectedValue);
        mqt.t_remark = txtRemark.Text;
        mqt.t_name = txt_name.Text;
        mqt.t_creatuser = badmin.GetAdminLogin().AdminId;
        if (id > 0)
        {
            bool result = bqt.GetUpdate(mqt);
            if (result)
            {
                function.WriteSuccessMsg("修改成功！", "QuestionTypeManage.aspx");
            }
            else
            {
                function.WriteErrMsg("修改失败！");
            }
        }
        else
        {
            mqt.t_createtime = DateTime.Now;
            int ids = bqt.GetAdd(mqt);
            if (ids > 0)
            {
                function.WriteSuccessMsg("添加成功！", "QuestionTypeManage.aspx");
            }
            else
            {
                function.WriteErrMsg("添加失败！");
            }
        }
    }
}