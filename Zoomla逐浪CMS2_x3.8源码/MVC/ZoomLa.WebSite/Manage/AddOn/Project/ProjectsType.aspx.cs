using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZoomLa.Model;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.BLL.Project;


namespace ZoomLaCMS.Manage.AddOn.Project
{
    public partial class ProjectsType : System.Web.UI.Page
    {
        B_Pro_Type typeBll = new B_Pro_Type();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='Projects.aspx'>项目管理</a></li><li class='active'>分类管理<a href='ProjectsAddType.aspx'>[添加类型]</a></li>" + Call.GetHelp(45));
            }
        }
        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            MyBind();
        }
        private void MyBind()
        {
            EGV.DataSource = typeBll.Sel();
            EGV.DataBind();
        }
        protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = DataConverter.CLng(e.CommandArgument);
            if (e.CommandName == "Del")
            {
                typeBll.Del(id);
                function.WriteSuccessMsg("删除成功！", "ProjectsType.aspx");
            }
            if (e.CommandName == "Update")
            {
                Response.Redirect("ProjectsAddType.aspx?ID=" + id);
            }
            MyBind();
        }
        protected void BatDel_Btn_Click(object sender, EventArgs e)
        {
            string ids = Request.Form["idchk"];
            if (!string.IsNullOrEmpty(Request.Form["idchk"]))
            {
                typeBll.DelByIDS(ids);
                function.WriteSuccessMsg("删除成功！", "ProjectsType.aspx");
            }
            MyBind();
        }
        protected void EGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("ondblclick", "javascript:location.href='ProjectsAddType.aspx?ID=" + EGV.DataKeys[e.Row.RowIndex].Value + "'");//双击事件
            }
        }
    }
}