using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using System.Data;
using ZoomLa.Common;
using ZoomLa.Model.Project;
using ZoomLa.BLL.Project;

public partial class manage_AddOn_ProjectsAddType : System.Web.UI.Page
{
    B_Pro_Type typeBll = new B_Pro_Type();
    public int TypeID { get { return Request.QueryString["ID"] == null ? 0 : DataConverter.CLng(Request.QueryString["ID"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        B_Admin badmin = new B_Admin();
        if (TypeID > 0)
        {
            MyBind();
        }
        Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='Projects.aspx'>项目管理</a></li><li><a href='ProjectsType.aspx'>分类管理</a></li><li class='active'>添加类型</li>");
    }
    public void MyBind()
    {
        M_Pro_Type typeMod = typeBll.SelReturnModel(TypeID);
        TxtProjectName.Text = typeMod.TName;
        TxtTypeInfo.Text = typeMod.Remind; TxtProjectName.ReadOnly = true;
        lblText.Text = "查看/修改项目类型";
    }

    protected void Return_B_Click(object sender, EventArgs e)
    {
        Response.Redirect("ProjectsType.aspx");
    }

    protected void Commit_B_Click(object sender, EventArgs e)
    {
        if (TypeID > 0)
        {
            M_Pro_Type typeMod = typeBll.SelReturnModel(TypeID);
            typeMod.Remind = TxtTypeInfo.Text.Trim();
            typeBll.UpdateByID(typeMod);
            function.WriteSuccessMsg("修改成功！", "ProjectsType.aspx");
        }
        else
        {
            M_Pro_Type typeMod = new M_Pro_Type();
            typeMod.TName = TxtProjectName.Text.Trim(' ');
            typeMod.Remind = TxtTypeInfo.Text;
            typeBll.Insert(typeMod);
            function.WriteSuccessMsg("添加成功！", "ProjectsType.aspx");
        }
    }
}
