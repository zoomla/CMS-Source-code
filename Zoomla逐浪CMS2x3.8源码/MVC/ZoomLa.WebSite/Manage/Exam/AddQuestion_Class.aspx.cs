namespace ZoomLaCMS.Manage.Exam
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.UI;
    using System.Data;
    using System.Text.RegularExpressions;
    using System.Web.UI.WebControls;
    using ZoomLa.Common;
    using ZoomLa.BLL;
    using ZoomLa.Model;
    public partial class AddQuestion_Class : System.Web.UI.Page
    {
        B_Exam_Class examBll = new B_Exam_Class();
        public int Cid { get { return DataConverter.CLng(Request.QueryString["ID"]); } }
        public string CAction { get { return Request.QueryString["Action"]; } }

        public int Pid { get { return DataConverter.CLng(Request.QueryString["pid"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            B_ARoleAuth.CheckEx(ZLEnum.Auth.content, "ContentMange");
            if (!IsPostBack)
            {
                BindDrop();
                if (string.IsNullOrEmpty(CAction) || CAction.Equals("Add"))
                {
                    ddlC_ClassId.Visible = false;
                    txtClassId.Visible = true;
                    txtClassId.Enabled = false;
                    txtClassId.Text = "无所属分类";
                    if (Cid > 0) { txtClassId.Text = examBll.GetSelect(Cid).C_ClassName; }
                }
                else if (CAction.Equals("Modify"))
                {
                    this.ddlC_ClassId.Visible = true;
                    this.txtClassId.Visible = false;
                    M_Exam_Class examMod = examBll.GetSelect(Cid);
                    if (examMod != null && examMod.C_id > 0)
                    {
                        txtClassName.Text = examMod.C_ClassName;
                        ddlC_ClassId.Text = examMod.C_Classid.ToString();
                        txtC_OrderBy.Text = examMod.C_OrderBy.ToString();
                        C_ClassType.SelectedValue = examMod.C_ClassType.ToString();
                    }
                }

                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li> <li><a href='Papers_System_Manage.aspx'>教育模块</a></li> <li><a href='QuestionManage.aspx'>考试管理</a></li><li><a href='Question_Class_Manage.aspx'>分类管理</a></li><li>编辑分类</li>");
            }
        }


        //绑定所属ID的列表 
        private void BindDrop()
        {
            examBll = new B_Exam_Class();
            DataTable dt = examBll.Select_All();
            ListItem li1 = new ListItem();
            li1.Text = "请选择";
            li1.Value = "0";
            this.ddlC_ClassId.Items.Add(li1);
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ListItem li = new ListItem();
                    li.Value = dt.Rows[i]["C_id"].ToString();
                    li.Text = dt.Rows[i]["C_ClassName"].ToString();
                    this.ddlC_ClassId.Items.Add(li);
                }
            }
            if (dt != null)
            {
                dt.Dispose();
            }
        }

        //保存
        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(CAction) || CAction.Equals("Add"))
            {
                M_Exam_Class mqc = examBll.GetSelectByCName(this.txtClassName.Text.Trim());
                if (mqc != null && mqc.C_id > 0) { function.WriteErrMsg("已存在该分类,请重新添加!"); }
                mqc.C_ClassName = this.txtClassName.Text.Trim();
                mqc.C_Classid = Cid;
                mqc.C_OrderBy = txtC_OrderBy.Text == "" ? 0 : DataConverter.CLng(txtC_OrderBy.Text.Trim());
                mqc.C_ClassType = DataConverter.CLng(this.C_ClassType.SelectedValue);
                int resu = examBll.GetInsert(mqc);
                if (resu > 0) { function.WriteSuccessMsg("添加成功!", "/Admin/Exam/Question_Class_Manage.aspx"); }
                else { function.WriteErrMsg("添加失败!"); }
            }
            else if (CAction.Equals("Modify"))
            {
                M_Exam_Class mqc = examBll.GetSelectByCName(this.txtClassName.Text.Trim());
                M_Exam_Class model = examBll.GetSelect(Cid);
                if ((mqc != null && mqc.C_id > 0) && !model.C_ClassName.Equals(mqc.C_ClassName)) { function.WriteErrMsg("已存在该分类,请重新修改!"); }
                model.C_ClassName = txtClassName.Text.Trim();
                model.C_Classid = DataConverter.CLng(ddlC_ClassId.SelectedValue);
                model.C_OrderBy = txtC_OrderBy.Text == "" ? 0 : DataConverter.CLng(txtC_OrderBy.Text.Trim());
                bool resu = examBll.GetUpdate(model);  //更新
                if (resu) { function.WriteSuccessMsg("更新成功!", "/Admin/Exam/Question_Class_Manage.aspx"); }
                else { function.WriteErrMsg("更新失败!"); }
            }
        }

        //文本改变
        protected void txtClassName_TextChanged(object sender, EventArgs e)
        {
            examBll = new B_Exam_Class();
            M_Exam_Class mqc = new M_Exam_Class();
            int c_id = DataConverter.CLng(ViewState["Classid"]);
            examBll = new B_Exam_Class();
            M_Exam_Class mqc1 = new M_Exam_Class();
            mqc1 = examBll.GetSelect(c_id);
            mqc = examBll.GetSelectByCName(this.txtClassName.Text.Trim());
            if (mqc != null && mqc.C_id > 0)  //判断存在该分类
            {
                function.WriteErrMsg("已存在该分类,请重新添加!");
                this.EBtnSubmit.Enabled = false;
            }
            else
            {
                this.EBtnSubmit.Enabled = true;
            }
        }
        protected void BtnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Question_Class_Manage.aspx");
        }
    }
}