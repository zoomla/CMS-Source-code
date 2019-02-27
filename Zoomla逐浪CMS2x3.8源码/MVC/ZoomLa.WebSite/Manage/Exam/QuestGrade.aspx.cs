namespace ZoomLaCMS.Manage.Exam
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using ZoomLa.Model;
    using ZoomLa.BLL;
    using ZoomLa.Common;
    public partial class QuestGrade : System.Web.UI.Page
    {
        B_GradeOption gradeBll = new B_GradeOption();

        public int CateID { get { return DataConverter.CLng(Request.QueryString["cate"]); } }

        public string GetParamByCate()
        {
            switch (CateID)
            {
                case 5:
                    return "diff";
                case 6:
                    return "Grade";
                case 7:
                    return "Version";
                default:
                    return "Grade";
            }
        }
        public string CateName { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            InitCateType();
            if (!IsPostBack)
            {
                MyBind();
                Title_Q.InnerText = CateName + "管理";
                Title_L.Text = CateName + "名称";
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li> <li><a href='Papers_System_Manage.aspx'>教育模块</a></li><li>" + CateName + "管理 [<a href='javascript:;' onclick=\"ShowGrade()\">添加" + CateName + "</a>]</li>");
            }
        }
        public void MyBind()
        {
            RPT.DataSource = B_GradeOption.GetGradeList(CateID, 0);
            RPT.DataBind();
        }
        //判断字典类型
        public void InitCateType()
        {
            switch (CateID)
            {
                case 4:
                    CateName = "题型";
                    break;
                case 5:
                    CateName = "难度";
                    break;
                case 6:
                    CateName = "年级";
                    break;
                case 7:
                    CateName = "教材版本";
                    break;
                default:
                    CateName = "题型";
                    break;
            }
        }


        protected void Save_Btn_Click(object sender, EventArgs e)
        {
            M_Grade GradeMod = new M_Grade();
            GradeMod.GradeID = DataConverter.CLng(GradeID_Hid.Value);
            GradeMod.ParentID = 0;
            GradeMod.GradeName = GradeName_T.Text;
            GradeMod.Cate = CateID;
            if (GradeMod.GradeID > 0)
            {
                B_GradeOption.UpdateDic(GradeMod);
                function.WriteSuccessMsg("修改成功!");
            }
            else
            {
                B_GradeOption.AddGradeOption(GradeMod);
                function.WriteSuccessMsg("添加成功!");
            }
            MyBind();
        }

        protected void RPT_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("Del"))
            {
                B_GradeOption.DelGradeOption(DataConverter.CLng(e.CommandArgument));
            }
            MyBind();
        }
    }
}