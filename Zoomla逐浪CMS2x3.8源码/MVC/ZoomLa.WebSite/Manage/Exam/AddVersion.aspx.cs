namespace ZoomLaCMS.Manage.Exam
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using ZoomLa.BLL;
    using ZoomLa.BLL.Exam;
    using ZoomLa.Common;
    using ZoomLa.Model;
    using ZoomLa.Model.Exam;
    using ZoomLa.SQLDAL;

    public partial class AddVersion : CustomerPageAction
    {
        B_Exam_Class nodeBll = new B_Exam_Class();
        B_Exam_Version verBll = new B_Exam_Version();
        B_Questions_Knowledge knowBll = new B_Questions_Knowledge();
        M_Exam_Version verMod = null;
        private int Mid { get { return DataConvert.CLng(Request.QueryString["ID"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
                Call.SetBreadCrumb(Master, "<li><a href='" + customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='Papers_System_Manage.aspx'>教育模块</a></li><li><a href='VersionList.aspx'>教材版本</a></li><li class='active'>版本管理</li>");
            }
        }
        private void MyBind()
        {
            M_AdminInfo adminMod = B_Admin.GetLogin();
            Grade_Radio.DataSource = B_GradeOption.GetGradeList(6, 0);
            Grade_Radio.DataBind();
            if (Grade_Radio.Items.Count > 0) { Grade_Radio.SelectedIndex = 0; }
            NodeTree.DataSource = nodeBll.Select_All();
            NodeTree.MyBind();
            if (Mid > 0)
            {
                verMod = verBll.SelReturnModel(Mid);
                VName_T.Text = verMod.VersionName;
                Inputer_T.Text = verMod.Inputer;
                VTime_T.Text = verMod.VersionTime;
                Grade_Radio.SelectedValue = verMod.Grade.ToString();
                Node_Hid.Value = verMod.NodeID.ToString();
                Volume_T.Text = verMod.Volume;
                SectionName_T.Text = verMod.SectionName;
                CourseName_T.Text = verMod.CourseName;
                Price_T.Text = verMod.Price.ToString("f2");
                if (!string.IsNullOrEmpty(verMod.Knows)) { TagKey_T.Value = knowBll.GetNamesByIDS(verMod.Knows); }
            }
            else
            {
                Inputer_T.Text = adminMod.AdminName;
            }
        }
        protected void Save_Btn_Click(object sender, EventArgs e)
        {
            M_AdminInfo adminMod = B_Admin.GetLogin();
            verMod = new M_Exam_Version();
            if (Mid > 0) { verMod = verBll.SelReturnModel(Mid); }
            verMod.VersionName = VName_T.Text;
            verMod.Inputer = Inputer_T.Text;
            verMod.VersionTime = VTime_T.Text;
            verMod.NodeID = DataConvert.CLng(Node_Hid.Value);
            verMod.AdminID = adminMod.AdminId;
            verMod.Grade = DataConvert.CLng(Grade_Radio.SelectedValue);
            verMod.Volume = Volume_T.Text;
            verMod.SectionName = SectionName_T.Text;
            verMod.CourseName = CourseName_T.Text;
            string tagkey = Request.Form["Tabinput"];
            if (string.IsNullOrEmpty(tagkey))
            {
                verMod.Knows = "";
            }
            else
            {
                int firstid = nodeBll.SelFirstNodeID(DataConvert.CLng(Node_Hid.Value));
                verMod.Knows = knowBll.AddKnows(firstid, tagkey);
            }
            if (Mid > 0)
            {
                verBll.UpdateByID(verMod);
            }
            else { verBll.Insert(verMod); }
            function.WriteSuccessMsg("操作成功", "VersionList.aspx");
        }
    }
}