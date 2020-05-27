using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ZoomLa.Model;
using ZoomLa.BLL;
using ZoomLa.Common;

namespace ZoomLaCMS.Manage.AddOn.Project
{
    public partial class ProjectCategoryManage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            function.AccessRulo();
            B_Admin badmin = new B_Admin();
            badmin.CheckIsLogin();
            Call.SetBreadCrumb(Master, "<li>后台管理</li><li>项目管理</li><li>项目分类管理</li>");
            if (!Page.IsPostBack)
            {
                MyBind();
            }
        }
        private void MyBind()
        {

            //DataView dv =this.bprocate.GetProjectCategoryAll().DefaultView;
            //this.GridView1.DataSource = dv;
            //this.GridView1.DataKeyNames = new string[] { "ProjectCategoryID" };
            //this.GridView1.DataBind();
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            MyBind();
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "DelProCate":
                    int Id = Convert.ToInt32(e.CommandArgument.ToString());
                    //this.bprocate.DeleteByID(Id);
                    MyBind();
                    break;
            }
        }
        protected void btndelete_Click(object sender, EventArgs e)
        {
            int flag = 0;
            for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
            {
                CheckBox cbox = (CheckBox)GridView1.Rows[i].FindControl("SelectCheckBox");
                if (cbox.Checked == true)
                {
                    //if (this.bprocate.DeleteByID(DataConverter.CLng(GridView1.DataKeys[i].Value)))
                    //    flag++;
                }
            }
            if (flag > 0)
            {
                Response.Write("<script language=javascript> alert('批量删除成功！');window.document.location.href='ProjectCategoryManage.aspx';</script>");

            }
            else
                Response.Write("<script language=javascript> alert('批量删除失败！\\n无选中删除项');window.document.location.href=window.document.location.href</script>");
        }
    }
}