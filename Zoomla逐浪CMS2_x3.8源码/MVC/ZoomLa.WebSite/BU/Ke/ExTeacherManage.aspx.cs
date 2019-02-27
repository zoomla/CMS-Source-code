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


namespace ZoomLaCMS.MIS.Ke
{
    public partial class ExTeacherManage :CustomerPageAction
    {

        private B_ExTeacher bet = new B_ExTeacher();
        private B_Exam_Class bqc = new B_Exam_Class();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["menu"]) && Request.QueryString["menu"] == "delete")
            {
                int id = DataConverter.CLng(Request.QueryString["id"]);
                bet.DeleteByGroupID(id);
            }
            if (!IsPostBack)
            {
                MyBind();
            }
        }
        public void MyBind()
        {
            DataTable dt = bet.Sel_All();
            RPT.DataSource = dt;
            RPT.DataBind();
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
        protected void txtPage_TextChanged(object sender, EventArgs e)
        {
            MyBind();
        }
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            MyBind();
        }
        protected void Button3_Click(object sender, EventArgs e)
        {

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