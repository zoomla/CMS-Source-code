using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL.Exam;
using ZoomLa.Model.Exam;
using ZoomLa.Common;
using System.Data;

public partial class Manage_Exam_SubjectManage : System.Web.UI.Page
{
    B_EDU_Subject subll = new B_EDU_Subject();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='Papers_System_Manage.aspx'>教育模块</a></li><li>学科管理<a href='javascript:;' onclick='ShowAddDiag(\"添加学科\",\"添加\")'>[添加学科]</a></li>");
            MyBind();
        }
    }
    private void MyBind()
    {
        Sub_RPT.DataSource = subll.Sel();
        Sub_RPT.DataBind();
    }
    protected void Save_B_Click(object sender, EventArgs e)
    {
        M_EDU_Subject model = !string.IsNullOrEmpty(SubID_Hid.Value) ? subll.SelReturnModel(DataConverter.CLng(SubID_Hid.Value)) : new M_EDU_Subject();
        model.Name = SubName_T.Text;
        model.Subject = GroName_T.Text;
        model.MaxCount = DataConverter.CLng(MaxCount_T.Text);
        model.Flag = Flag_T.Text;
        if (!string.IsNullOrEmpty(SubID_Hid.Value))
            subll.UpdateByID(model);
        else
            subll.Insert(model);
        SubID_Hid.Value = "";
        MyBind();
    }
    protected void Sub_RPT_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int ID = DataConverter.CLng(e.CommandArgument);
        switch (e.CommandName)
        {
            case "Edit":
                M_EDU_Subject model = subll.SelReturnModel(ID);
                SubName_T.Text = model.Name;
                GroName_T.Text = model.Subject;
                SubID_Hid.Value = model.ID.ToString();
                Flag_T.Text = model.Flag;
                break;
            case "Del":
                subll.Del(ID);
                MyBind();
                break;
            default:
                break;
        }
    }
}