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
    public partial class ProjectExam : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            function.AccessRulo();
            B_Admin badmin = new B_Admin();
            Call.SetBreadCrumb(Master, "<li>后台管理</li><li>项目管理</li><li><a href='ProjectManage.aspx'> 项目列表</a></li><li>项目审核</li>");
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["PId"] != null)
                {
                    int pid = Convert.ToInt32(Request.QueryString["PId"].Trim());
                    if (Request.QueryString["Action"] != null)
                    {
                        string action = Request.QueryString["Action"].Trim();
                        if (action == "Passed")
                            PassedProject(pid);
                        else if (action == "CancelPassed")
                            ProjectPassedCancel(pid);
                        Page.Response.Redirect("ProjectExam.aspx");
                    }
                }
                DataBind();
            }
        }
        private void PassedProject(int pid)
        {
            //this.bll.PassedProjectByID(pid);
            //Page.Response.Redirect("ProjectExam.aspx");
        }
        private void ProjectPassedCancel(int pid)
        {
            //this.bll.PassedProjectCancelByID(pid);
            //Page.Response.Redirect("ProjectExam.aspx");
        }
        public void DataBind(string key = "")
        {
            //this.Egv.DataSource = this.bll.ProjectALlUnPass();//.GetProjectAll();
            //this.Egv.DataBind();
        }
        protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Egv.PageIndex = e.NewPageIndex;
            this.DataBind();
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "DelProject":
                    int Id = DataConverter.CLng(e.CommandArgument.ToString());
                    //this.bll.DeleteByID(Id);
                    //this.bllwork.DelProWorByPID(Id);//删除项目节点
                    //this.bprojectfield.DeleteByID(Id);//删除对应的项目字段
                    DataBind();
                    break;
            }
        }
        protected void btnPassed_Click(object sender, EventArgs e)
        {
            int Id = 0;
            for (int i = 0; i <= Egv.Rows.Count - 1; i++)
            {
                CheckBox cbox = (CheckBox)Egv.Rows[i].FindControl("chkSel");
                if (cbox.Checked == true)
                {
                    Id = DataConverter.CLng(Egv.DataKeys[i].Value);
                    if (btnPassed.Text.Trim() == "批量通过审核")
                    {
                        PassedProject(Id);
                    }
                    else
                    {
                        ProjectPassedCancel(Id);
                    }

                }
            }

            if (btnPassed.Text.Trim() == "批量通过审核")
            {
                btnPassed.Text = "批量取消审核";
            }
            else
            {
                btnPassed.Text = "批量通过审核";
            }
            DataBind();
        }
    }
}