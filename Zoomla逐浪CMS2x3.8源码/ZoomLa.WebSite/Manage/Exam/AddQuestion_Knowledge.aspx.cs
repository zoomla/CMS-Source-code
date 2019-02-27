using System;
using System.Collections.Generic;
using System.Web;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;
using ZoomLa.BLL;
using ZoomLa.Model;

public partial class manage_Question_AddQuestion_Knowledge : CustomerPageAction
{
    B_Exam_Class bqc;
    B_ExamPoint bqk;
    B_Admin badmin = new B_Admin();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["menu"] != null && Request.QueryString["menu"] != "")
            {
                string menu = Request.QueryString["menu"].ToString();
                if (menu.Equals("Edit")) //修改知识点
                {
                    this.Label2.Text = "修改知识点";
                    this.Label1.Text = "修改知识点";

                    GetClassList();
                    int kid = DataConverter.CLng(Request.QueryString["k_id"]);
                    bqk = new B_ExamPoint();
                    M_ExamPoint mqk = bqk.GetSelect(kid);
                    if (mqk != null && mqk.ID > 0)
                    {
                        this.txtK_Name.Text = mqk.TestPoint;
                        this.txtK_OrderBy.Text = mqk.OrderBy.ToString();
                        this.txtClassId.Visible = false;
                        ddlC_ClassId.Visible = true;
                        ddlC_ClassId.SelectedValue = mqk.ID.ToString();
                    }
                }
                if (menu.Equals("Add"))  //添加知识点
                {
                    this.Label2.Text = "添加知识点";
                    this.Label1.Text = "添加知识点";
                    if (Request.QueryString["C_id"] != null && Request.QueryString["C_id"] != "")
                    {
                        int c_id = DataConverter.CLng(Request.QueryString["C_id"]);
                        bqc = new B_Exam_Class ();
                        M_Exam_Class mqc = bqc.GetSelect(c_id);
                        if (mqc != null && mqc.C_id > 0)
                        {
                            txtClassId.Text = mqc.C_ClassName;
                            this.txtClassId.Enabled = false;
                            ddlC_ClassId.Visible = false;
                        }
                    }
                }
                Call.SetBreadCrumb(Master, "<li>教育模块</li><li><a href='QuestionManage.aspx'>在线考试系统</a></li><li>知识点管理</li><li>" + Label2.Text+ "</li>");
            }
        }
    }

    //返回分类列表
    protected void BtnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Question_Class_Manage.aspx");
    }

    #region private method
    //填充知识点列表
    private void GetKnowList()
    {
        bqk = new B_ExamPoint ();
        int id =DataConverter.CLng(Request.QueryString["ID"]);
        List<M_ExamPoint> mqks = bqk.GetSelectByCid(id);
        if (mqks != null && mqks.Count > 0)
        {
            ddlC_ClassId.Items.Clear();  //清空所有列表
            foreach (M_ExamPoint item in mqks)
            {
                ListItem li = new ListItem();
                li.Text = item.TestPoint;
                li.Value = item.ID.ToString();
                ddlC_ClassId.Items.Add(li);
            }
        }
    }

    //填充分类列表
    private void GetClassList()
    {
        bqc = new B_Exam_Class();
        List<M_Exam_Class> mqc = bqc.SelectQuesClasses();
        if (mqc != null && mqc.Count > 0)
        {
            ddlC_ClassId.Items.Clear();
            foreach (M_Exam_Class item in mqc)
            {
                ListItem li = new ListItem();
                li.Text = item.C_ClassName;
                li.Value = item.C_id.ToString();
                ddlC_ClassId.Items.Add(li);
            }
        }
    }
    #endregion

    //保存知识点
    protected void EBtnSubmit_Click(object sender, EventArgs e)
    {
        bqk = new B_ExamPoint ();
        string cid="";
        if (txtK_Name.Text.Trim() == "")
        {
            cid = ddlC_ClassId.SelectedValue;
        }
        else
        {
            cid = txtK_Name.Text.Trim();
        }
        M_ExamPoint mqk = bqk.GetSelectByNameAndCid(txtK_Name.Text, DataConverter.CLng(cid));
        if (mqk != null && mqk.ID > 0)
        {
            function.WriteErrMsg("该分类中已经存在此知识点!");
            return;
        }
        if (Request.QueryString["menu"] != null && Request.QueryString["menu"] != "")
        {
            if (txtK_OrderBy.Text.Trim() != "" && !Regex.IsMatch(txtK_OrderBy.Text.Trim(), @"^(-?\d+)(\.\d+)?$"))
            {
                function.WriteErrMsg("排序必须为数字!");
                return;
            }
            string menu = Request.QueryString["menu"].ToString();
            if (menu.Equals("Edit")) //修改知识点
            {
                int kid = DataConverter.CLng(Request.QueryString["k_id"]);
                M_ExamPoint mqk1 = new M_ExamPoint ();
                mqk1.ID = kid;
                mqk1.TestPoint = this.txtK_Name.Text.Trim();
                mqk1.TID = DataConverter.CLng(ddlC_ClassId.SelectedValue);
                mqk1.OrderBy = txtK_OrderBy.Text.Trim() == "" ? 0 : DataConverter.CLng(txtK_OrderBy.Text.Trim());
                bool result = bqk.GetUpdate(mqk1);
                if (result)
                {
                    function.WriteMessage("修改成功!", "Questions_Knowledge_Show.aspx?c_id="+mqk1.TID, "修改知识点");
                }
                else
                {
                    function.WriteErrMsg("修改失败!");
                }
            } if (menu.Equals("Add"))  //添加知识点
            {
                M_ExamPoint mqk1 = new M_ExamPoint ();
                mqk1.TestPoint = this.txtK_Name.Text.Trim();
                int c_id = DataConverter.CLng(Request.QueryString["C_id"]);
                mqk1.TID = c_id;
                mqk1.OrderBy = txtK_OrderBy.Text.Trim() == "" ? 0 : DataConverter.CLng(txtK_OrderBy.Text.Trim());
                int result = bqk.GetInsert(mqk1);
                if (result>0)
                {
                    function.WriteMessage("添加成功!", "Questions_Knowledge_Show.aspx?c_id=" + mqk1.TID, "添加知识点");
                }
                else
                {
                    function.WriteErrMsg("添加失败!");
                }
            }
        }
    }
}
