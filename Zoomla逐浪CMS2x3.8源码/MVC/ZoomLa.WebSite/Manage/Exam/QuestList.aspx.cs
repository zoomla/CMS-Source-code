namespace ZoomLaCMS.Manage.Exam
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.UI;
    using System.Data;
    using System.Web.UI.WebControls;
    using ZoomLa.Model;
    using ZoomLa.BLL;
    using ZoomLa.Common;
    using System.Xml;
    using ZoomLa.Components;
    using System.Text;
    using System.IO;
    using System.Web.UI.HtmlControls;
    using System.Text.RegularExpressions;

    public partial class QuestList : System.Web.UI.Page
    {
        public B_Exam_Sys_Questions questBll = new B_Exam_Sys_Questions();
        public B_Questions_Knowledge knowBll = new B_Questions_Knowledge();
        B_Exam_Class nodeBll = new B_Exam_Class();
        B_ExamPoint bep = new B_ExamPoint();
        B_User buser = new B_User();
        public int QType { get { return string.IsNullOrEmpty(Request.QueryString["qtype"]) ? 99 : DataConverter.CLng(Request.QueryString["qtype"]); } }//题目类型
        public string KeyWord { get { return Server.UrlDecode(Request.QueryString["keyWord"]); } }
        //试题类别
        public int NodeID { get { return DataConverter.CLng(Request.QueryString["NodeID"]); } }
        //按年级筛选
        public int Grade { get { return DataConverter.CLng(Request.QueryString["Grade"]); } }
        public int Diff { get { return DataConverter.CLng(Request.QueryString["Diff"]); } }
        public int Version { get { return DataConverter.CLng(Request.QueryString["Version"]); } }
        public DataTable KnowsNames = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Skey_T.Text = Request.QueryString["Skey"];
                if (NodeID > 0) { NodeName_L.Text = nodeBll.GetSelect(NodeID).C_ClassName; }
                else if (Grade > 0) { NodeName_L.Text = B_GradeOption.GetGradeOption(Grade).GradeName; }
                else { NodeName_L.Text = "全部试题"; }
                if (NodeID < 1) { add_sp.Visible = false; }
                MyBind();
                Call.HideBread(Master);
            }
        }
        private void MyBind()
        {
            M_UserInfo mu = buser.GetLogin();
            DataTable dt = questBll.SelByFilter(NodeID, QType, Grade, Diff, Version, KeyWord, 0);
            EGV.DataSource = dt;
            EGV.DataBind();
            KnowsBind();
        }
        //绑定知识点名称
        private void KnowsBind()
        {
            string knowsids = "";
            foreach (DataKey key in EGV.DataKeys)
            {
                knowsids += key.Value + ",";
            }
            if (!string.IsNullOrEmpty(knowsids.Trim(',')))
            {
                knowsids = string.Join(",", knowsids.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
                KnowsNames = knowBll.SelByIDS(knowsids.Trim(','));
                EGV.DataBind();
            }
        }
        //批量删除
        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            string ids = Request.Form["idchk"];
            if (!string.IsNullOrEmpty(ids))
            {
                questBll.DelByIDS(ids);
                MyBind();
            }
        }
        //取类别
        public string GetClass(string classid)
        {
            int id = DataConverter.CLng(classid);
            M_Exam_Class mec = nodeBll.GetSelect(id);
            if (mec != null && mec.C_id > 0)
            {
                return mec.C_ClassName;
            }
            else
            {
                return "";
            }

        }
        //取题型
        public string GetType(string id)
        {
            return M_Exam_Sys_Questions.GetTypeStr(DataConverter.CLng(id));
        }
        // 修改,删除 
        protected void gvCard_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "del2":
                    questBll.DelByIDS(e.CommandArgument.ToString());
                    break;
            }
            MyBind();
        }
        // 获取内容
        private string GetCon(string con)
        {
            if (con.Length > 50)
            {
                return con.Substring(0, 40) + "...";
            }
            else
            {
                return con;
            }
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("ExportExcel.aspx");
        }
        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            MyBind();
        }
        public string GetTagKeys()
        {
            if (KnowsNames.Rows.Count > 0 && !string.IsNullOrEmpty(Eval("Tagkey").ToString()))
            {
                string knownames = "";
                DataRow[] drs = KnowsNames.Select("k_id IN (" + Eval("Tagkey").ToString().Trim(',') + ")");
                foreach (DataRow item in drs)
                {
                    knownames += item["k_name"].ToString() + ",";
                }
                string names = knownames.Trim(',');
                names = names.Length > 10 ? names.Substring(0, 10) + "..." : names;
                return names.Length > 10 ? names.Substring(0, 10) + "..." : names;
            }
            return "";
        }
        protected void Search_B_Click(object sender, EventArgs e)
        {
            MyBind();
        }
        protected void BatDel_Btn_Click(object sender, EventArgs e)
        {
            string ids = Request.Form["idchk"];
            if (!string.IsNullOrEmpty(ids))
            {
                questBll.DelByIDS(ids);
                function.WriteSuccessMsg("删除成功");
            }
        }
        protected void EGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dr = e.Row.DataItem as DataRowView;
                e.Row.Attributes.Add("ondblclick", "location='AddEnglishQuestion.aspx?id=" + dr["p_id"] + "'");
            }
        }
    }
}