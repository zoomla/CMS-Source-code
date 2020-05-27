using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;

namespace ZoomLaCMS.Manage.AddOn.Project
{
    public partial class ProjectsProcesses : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            function.AccessRulo();
            B_Admin badmin = new B_Admin();
            badmin.CheckIsLogin();

            int id = DataConverter.CLng(Request.QueryString["id"]);
            // LBtnProject.Text = "<font color=green>" + bpj.GetSelect(id).Name + "</font>";
            if (!IsPostBack)
            {
                Bind();
            }
        }
        protected void Bind()
        {
            int id = DataConverter.CLng(Request.QueryString["id"]);
            PagedDataSource pdc = new PagedDataSource();
            //DataTable bpstable = bps.SelectByProID(id);
            //bpstable.DefaultView.Sort = "id asc";
            //pdc.DataSource = bpstable.DefaultView;
            Repeater1.DataSource = pdc;
            Repeater1.DataBind();
        }
        protected string GetComplete(string iscomplete)
        {
            if (iscomplete == "1")
            {
                return "<font color=green>√</font>";
            }
            else
            {
                return "<font color=red>×</font>";
            }
        }
        protected void btnDeleteAll_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem ritem in Repeater1.Items)
            {
                if ((ritem.FindControl("ChBx") as CheckBox).Checked)
                {
                    Label lbl = ritem.FindControl("Label1") as Label;
                    int id = Convert.ToInt32(lbl.Text.ToString());
                    //bps.DeleteByGroupID(id);
                }
            }
            function.WriteSuccessMsg("批量删除成功！", "ProjectsProcesses.aspx?id=" + Request.QueryString["id"]);
        }
        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            switch (e.CommandName)
            {
                case "del":
                    // bps.DeleteByGroupID(id);
                    function.WriteSuccessMsg("删除成功！", "ProjectsProcesses.aspx?id=" + Request.QueryString["id"]);
                    break;
                case "Complete":
                    // bps.ChangeComplete(id);
                    Response.Redirect("ProjectsProcesses.aspx?id=" + Request.QueryString["id"]);
                    break;
                case "edit":
                    Response.Redirect("AddProcesses.aspx?processesid=" + e.CommandArgument);
                    break;
                case "Comments":
                    Response.Redirect("ProcessesComments.aspx?processesid=" + e.CommandArgument);
                    break;
                default:
                    break;
            }
        }
        protected string GetCom(string iscomplete)
        {
            if (iscomplete == "1")
            {
                return "未";
            }
            else
            {
                return "";
            }
        }
        protected bool Getbool(string ID)
        {
            int id = DataConverter.CLng(ID);
            return false;
        }
    }
}