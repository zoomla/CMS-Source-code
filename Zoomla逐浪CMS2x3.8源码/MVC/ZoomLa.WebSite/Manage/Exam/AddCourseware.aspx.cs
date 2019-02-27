namespace ZoomLaCMS.Manage.Exam
{
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
    public partial class AddCourseware : System.Web.UI.Page
    {
        private B_Courseware bcourw = new B_Courseware();
        private B_Course bcou = new B_Course();
        public int Course { get { return DataConverter.CLng(Request.QueryString["CourseID"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            liCoures.Text = "添加课件";
            cid.Value = Course.ToString();
            if (!string.IsNullOrEmpty(Request.QueryString["menu"]) && Request.QueryString["menu"] == "del")
            {
                int cids = DataConverter.CLng(Request.QueryString["id"]);
                bcourw.DeleteByGroupID(cids);
            }
            if (!IsPostBack)
            {
                int id = DataConverter.CLng(Request.QueryString["ID"]);
                coureId.Value = id.ToString();
                M_Courseware mcs = bcourw.GetSelect(id);
                if (mcs != null && mcs.ID > 0)
                {
                    liCoures.Text = "添加课件";
                    this.txt_Courename.Text = mcs.Courseware;
                    this.TextBox1.Text = mcs.Speaker;
                    this.TextBox2.Text = mcs.SJName;
                    this.txt_Order.Text = (mcs.CoursNum).ToString();
                    this.RadioButtonList1.SelectedValue = mcs.CoursType.ToString();
                    this.RadioButtonList2.SelectedValue = mcs.Status.ToString();
                    this.rblHot.Checked = mcs.Listen == 1 ? true : false;
                    this.txtP_Content.Text = mcs.Description;
                    this.txtUrl.Text = mcs.FileUrl;
                }
                else {
                    this.txt_Courename.Text = "";
                    this.TextBox1.Text = "";
                    this.TextBox2.Text = "";
                    this.txt_Order.Text = "";
                    this.RadioButtonList1.SelectedValue = "";
                    this.RadioButtonList2.SelectedValue = "";
                    this.rblHot.Checked = false;
                    this.txtP_Content.Text = "";
                    this.txtUrl.Text = "";
                }
                MyBind();
                Call.SetBreadCrumb(Master, "<li>教育模块</li><li><a href='QuestionManage.aspx'>在线考试系统</a></li><li>课程管理</li><li><a href='AddCourseware.aspx?CourseID=" + Request.QueryString["CourseID"] + "'>" + liCoures.Text + "</a></li>");
            }

        }
        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            int id = DataConverter.CLng(coureId.Value);
            M_Courseware mc = bcourw.GetSelect(id);
            mc.Courseware = this.txt_Courename.Text;
            mc.Speaker = this.TextBox1.Text;
            mc.SJName = this.TextBox2.Text;
            mc.CoursNum = DataConverter.CLng(this.txt_Order.Text);
            mc.CoursType = DataConverter.CLng(this.RadioButtonList1.SelectedValue);
            mc.Status = DataConverter.CLng(this.RadioButtonList2.SelectedValue);
            mc.Listen = rblHot.Checked ? 1 : 0;
            mc.Description = this.txtP_Content.Text;
            mc.FileUrl = this.txtUrl.Text;
            mc.UpdateTime = DateTime.Now;
            mc.CourseID = DataConverter.CLng(cid.Value);
            mc.CreationTime = DateTime.Now;
            if (mc != null && mc.ID > 0)
            {
                bool result = bcourw.GetUpdate(mc);
                if (result)
                {
                    function.WriteSuccessMsg("修改成功！");
                }
                else
                {
                    function.WriteErrMsg("修改失败！");
                }
            }
            else
            {
                int ids = bcourw.GetInsert(mc);
                if (ids > 0)
                {
                    function.WriteSuccessMsg("添加成功！");
                }
                else
                {
                    function.WriteErrMsg("添加失败!");
                }
            }
            int Course = DataConverter.CLng(Request.QueryString["CourseID"]);
            MyBind();
        }
        public void MyBind()
        {
            Repeater1.DataSource = bcourw.Select_CourseID(Course);
            Repeater1.DataBind();
        }
        public string GetCoursType(string Type)
        {
            if (Type == "0")
            {
                return "外部课件";
            }
            else {
                return "SCORM标准课件 ";
            }
        }
        public string GetStatus(string status)
        {
            if (status == "0")
            {
                return "可用";
            }
            else {
                return "不可用";
            }
        }
        public string GetListon(string lis)
        {
            if (lis == "0")
            {
                return "否";
            }
            else
            {
                return "是";
            }
        }
    }
}