using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using System.Data;

namespace ZoomLaCMS.Manage.Exam
{
    public partial class AddCoures : System.Web.UI.Page
    {
        private B_Course bcourse = new B_Course();
        private B_Exam_Class bqclass = new B_Exam_Class();

        protected void Page_Load(object sender, EventArgs e)
        {
            B_ARoleAuth.CheckEx(ZLEnum.Auth.content, "ContentMange");
            if (!IsPostBack)
            {
                int id = DataConverter.CLng(Request.QueryString["id"]);
                M_Course mcourse = bcourse.GetSelect(id);
                if (mcourse != null && mcourse.id > 0)
                {
                    liCoures.Text = "修改课程";
                    txt_Courename.Text = mcourse.CourseName;
                    PYtitle.Text = mcourse.CoureseThrun;
                    txt_Code.Text = mcourse.CoureseCode;
                    hfid.Value = mcourse.CoureseClass.ToString();
                    txtClassname.Text = GetClassname(mcourse.CoureseClass);
                    txt_Creidt.Text = mcourse.CoureseCredit.ToString();
                    rblHot.Checked = mcourse.Hot == 1 ? true : false;
                    txtRemark.Text = mcourse.CoureseRemark;
                    coureId.Value = mcourse.id.ToString();
                    Button2.Enabled = true;
                    Button2.OnClientClick = "location.href='AddCourseware.aspx?CourseID=" + id + "'; return false;";
                }
                else
                {
                    liCoures.Text = "添加课程";
                    Button2.Enabled = false;
                }
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li> <li><a href='Papers_System_Manage.aspx'>教育模块</a></li><li><a href='QuestionManage.aspx'>考试管理</a></li><li><a href='CoureseManage.aspx'>课程管理</a></li><li>" + liCoures.Text + "</li>");
                txtClassname.Enabled = false;
            }
        }

        private string GetClassname(int classid)
        {
            M_Exam_Class mqc = bqclass.GetSelect(classid);
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
            int id = DataConverter.CLng(coureId.Value);
            M_Course mcourse = bcourse.GetSelect(id);
            mcourse.AddUser = B_Admin.GetLogin().AdminId;
            mcourse.CoureseClass = DataConverter.CLng(hfid.Value);
            mcourse.CoureseCode = txt_Code.Text;
            mcourse.CoureseCredit = DataConverter.CDouble(txt_Creidt.Text);
            mcourse.CoureseRemark = txtRemark.Text;
            mcourse.CoureseThrun = PYtitle.Text;
            mcourse.CourseName = txt_Courename.Text;
            mcourse.Hot = rblHot.Checked ? 1 : 0;
            if (mcourse != null && mcourse.id > 0)
            {
                bool result = bcourse.GetUpdate(mcourse);
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
                mcourse.AddTime = DateTime.Now;
                int ids = bcourse.GetInsert(mcourse);
                if (ids > 0)
                {
                    Button2.Enabled = true;
                    Button2.OnClientClick = "location.href='AddCourseware.aspx?CourseID=" + ids + "'; return false;";
                    function.WriteSuccessMsg("添加成功！");
                }
                else
                {
                    function.WriteErrMsg("添加失败！");
                }
            }
        }
    }
}