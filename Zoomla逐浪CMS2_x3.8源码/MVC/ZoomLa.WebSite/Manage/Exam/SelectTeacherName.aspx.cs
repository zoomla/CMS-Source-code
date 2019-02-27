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
    public partial class SelectTeacherName : System.Web.UI.Page
    {
        private B_ExTeacher bet = new B_ExTeacher();
        private B_Exam_Class bqc = new B_Exam_Class();

        protected void Page_Load(object sender, EventArgs e)
        {
            function.AccessRulo();
            B_Admin badmin = new B_Admin();
            if (!IsPostBack)
            {
                MyBind();
            }
            if (!string.IsNullOrEmpty(Request.QueryString["menu"]) && Request.QueryString["menu"] == "select")
            {
                int id = DataConverter.CLng(Request.QueryString["id"]);
                string name = Request.QueryString["name"];
                string scripttxt = "setvalue('TextBox1','" + name + "');";
                string scriptid = "setvalue('hfid','" + id + "');";
                function.Script(this, scriptid + scripttxt + ";parent.Dialog.close();");
            }
        }
        public void MyBind()
        {
            Teacher_RPT.DataSource = bet.Select_All();
            Teacher_RPT.DataBind();
        }
        public string GetTeachClass(string classid)
        {
            M_Exam_Class mqc = bqc.GetSelect(DataConverter.CLng(classid));
            if (mqc != null && mqc.C_id > 0)
            {
                return mqc.C_ClassName;
            }
            else
            {
                return "";
            }
        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            string item = Request.Form["idchk"];
            if (item != null && item != "")
            {
                if (item.IndexOf(',') > -1)
                {
                    string[] itemarr = item.Split(',');
                    for (int i = 0; i < itemarr.Length; i++)
                    {
                        bet.DeleteByGroupID(DataConverter.CLng(itemarr[i]));
                    }
                }
                else
                {
                    bet.DeleteByGroupID(DataConverter.CLng(item));
                }
            }
            function.WriteSuccessMsg("操作成功!", "ExTeacherManage.aspx");
        }

        public string GetRemark(string remark)
        {
            if (remark.Length > 25)
            {
                return remark.Substring(0, 25) + "...";
            }
            else
            {
                return remark;
            }
        }
    }
}