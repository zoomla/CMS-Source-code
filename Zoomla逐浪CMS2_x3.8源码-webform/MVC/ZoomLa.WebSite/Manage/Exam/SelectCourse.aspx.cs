using System;
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
    using System.Data;
    public partial class SelectCourse : System.Web.UI.Page
    {
        private B_Course bcourse = new B_Course();
        private B_Admin badmin = new B_Admin();
        private B_Exam_Class bqclass = new B_Exam_Class();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable dt = bcourse.Select_All();
                if (dt != null)
                {
                    Page_list(dt);
                }
                if (!string.IsNullOrEmpty(Request.QueryString["menu"]) && Request.QueryString["menu"] == "select")
                {
                    int id = DataConverter.CLng(Request.QueryString["id"]);
                    string name = Request.QueryString["name"];
                    string scripttxt = "setvalue('txtCourse','" + name + "');";
                    string scriptid = "setvalue('hfid','" + id + "');";
                    function.Script(this, scriptid + scripttxt + ";parent.Dialog.close();");
                }
                Call.SetBreadCrumb(Master, "<li>教育模块</li><li><a href='QuestionManage.aspx'>在线考试系统</a></li><li>选择课程</li>");
            }
        }

        #region 通用分页过程
        /// <summary>
        /// 通用分页过程　by h.
        /// </summary>
        /// <param name="Cll"></param>
        public void Page_list(DataTable Cll)
        {
            RPT.DataSource = Cll;
            RPT.DataBind();
        }
        #endregion

        /// <summary>
        /// 是否热门
        /// </summary>
        /// <param name="isHot">热门</param>
        /// <returns></returns>
        public string GetHot(string isHot)
        {
            if (isHot == "0")
            {
                return "否";
            }
            else
            {
                return "是";
            }
        }

        public string GetAdminName(string adminid)
        {
            M_AdminInfo admin = B_Admin.GetAdminByAdminId(DataConverter.CLng(adminid));
            if (admin != null && admin.AdminId > 0)
            {
                return admin.AdminName;
            }
            else
            {
                return "";
            }
        }
    }
}