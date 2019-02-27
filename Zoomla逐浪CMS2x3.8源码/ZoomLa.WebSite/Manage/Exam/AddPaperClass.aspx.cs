using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL.Exam;
using ZoomLa.Model.Exam;
using ZoomLa.Common;
using ZoomLa.BLL;

public partial class Manage_Exam_AddPaperClass : System.Web.UI.Page
{
    B_Exam_PaperNode nodeBll = new B_Exam_PaperNode();
    public int PaperID { get { return DataConverter.CLng(Request.QueryString["id"]); } }
    public int ParentID { get { return DataConverter.CLng(Request.QueryString["pid"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li> <li><a href='Papers_System_Manage.aspx'>教育模块</a></li> <li><a href='Papers_System_Manage.aspx'>试卷管理</a></li><li><a href='Paper_Class_Manage.aspx'>试卷分类管理</a></li><li>添加试卷分类</li>");

        }
    }
    public void MyBind()
    {
        Type_Drop.DataSource = nodeBll.Sel();
        Type_Drop.DataBind();
        Type_Drop.Items.Insert(0, new ListItem("无所属节点", "0"));
        Type_Drop.SelectedValue = ParentID.ToString();
        if (PaperID > 0)
        {
            M_Exam_PaperNode model = nodeBll.SelReturnModel(PaperID);
            TypeName_T.Text = model.TypeName;
            Order_T.Text = model.OrderID.ToString();
            Type_Drop.SelectedValue = model.Pid.ToString();
        }
    }
    protected void Save_Btn_Click(object sender, EventArgs e)
    {
        M_Exam_PaperNode model = new M_Exam_PaperNode();
        model.TypeName = TypeName_T.Text;
        model.ID = PaperID;
        model.AdminID = B_Admin.GetLogin().AdminId;
        model.CDate = DateTime.Now;
        model.Remind = Remind_T.Text;
        model.OrderID =DataConverter.CLng(Order_T.Text);
        model.Pid =DataConverter.CLng(Type_Drop.SelectedValue);
        if (PaperID > 0) { nodeBll.UpdateByID(model); }
        else { nodeBll.Insert(model); }
        function.WriteSuccessMsg("保存成功!", "Paper_Class_Manage.aspx");
    }
}