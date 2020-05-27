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
    public partial class ProcessesComments : System.Web.UI.Page
    {
        M_UserInfo mu = new M_UserInfo();
        B_User bu = new B_User();
        B_Pro_Project proBll = new B_Pro_Project();
        public int ProjectID { get { return Request.QueryString["ProjectID"] == null ? 0 : DataConverter.CLng(Request.QueryString["ProjectID"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            function.AccessRulo();
            Call.SetBreadCrumb(Master, "<li>后台管理</li><li>CRM应用</li><li>项目管理</li><li>查看评论</li>");
            B_Admin badmin = new B_Admin();
            badmin.CheckIsLogin();

            DataTable dt = new DataTable();
            if (ProjectID > 0)
            {
                TxtProjectName.Text = proBll.SelReturnModel(ProjectID).ProName;
            }
            if (Request.QueryString["processesid"] != null)
            {
                int processid = DataConverter.CLng(Request.QueryString["processesid"]);
                //mps = bps.GetSelect(processid);
                //TxtProcessName.Text = mps.Name;
                //TxtProjectName.Text=bpj.GetSelect(mps.OpjectID).Name;
                //dt = bpc.GetByProcessID(processid);
            }
            if (!IsPostBack)
            {
                CommentsBind(dt);
            }
            if (dt != null)
            {
                dt.Dispose();
            }
        }
        protected void BtnCommit_Click(object sender, EventArgs e)
        {
            //mpc.Rating = DataConverter.CLng(Request.Form["TxtRating"]);
            //mpc.Content = Request.Form["TxtContent"];
            //mpc.CommentsDate = DateTime.Now;
            //mu = bu.GetLogin();
            //if (Request.QueryString["ProjectID"] != null)
            //{
            //    mpc.ProjectsID = DataConverter.CLng(Request.QueryString["ProjectID"]);
            //    mpc.ProcessesID = 0;
            //}
            //if (Request.QueryString["processesid"] != null)
            //{
            //    int processesid = DataConverter.CLng(Request.QueryString["processesid"]);
            //    mps = bps.GetSelect(processesid);
            //    mpc.ProjectsID = mps.OpjectID;
            //    mpc.ProcessesID = processesid;
            //}
            //mpc.UserID = mu.UserID;
            //bpc.GetInsert(mpc);
            if (Request.QueryString["ProjectID"] != null)
            {
                function.WriteSuccessMsg("添加成功!", "ProcessesComments.aspx?ProjectID=" + Request.QueryString["ProjectID"]);
            }
            if (Request.QueryString["processesid"] != null)
            {
                function.WriteSuccessMsg("添加成功!", "ProcessesComments.aspx?processesid=" + Request.QueryString["processesid"]);
            }
        }
        protected void CommentsBind(DataTable dt)
        {
            PagedDataSource pds = new PagedDataSource();
            pds.DataSource = dt.DefaultView;
            pds.PageSize = 10;
            pds.AllowPaging = true;
            RpComments.DataSource = pds;
            RpComments.DataBind();
            if (RpComments != null && RpComments.Items.Count > 0)
            {
                foreach (RepeaterItem item in RpComments.Items)
                {
                    Label lblRating = item.FindControl("lblRating") as Label;  //评分
                    Label lblUser = item.FindControl("lblUser") as Label;  //作者
                    Label comID = item.FindControl("Label1") as Label; //评论ID

                    int cID = DataConverter.CLng(comID.Text);
                    //M_ProjectsComments mpc = bpc.GetSelect(cID);
                    //if (mpc != null && mpc.CommentsID > 0)
                    //{
                    //    if (mpc.Rating <= 0)
                    //    {
                    //        lblRating.Text = "未评分";
                    //    }
                    //    else
                    //    {
                    //        lblRating.Text = mpc.Rating.ToString();
                    //    }
                    //    M_UserInfo mu = new M_UserInfo();
                    //    B_User bu = new B_User();
                    //    mu = bu.GetUserByUserID(mpc.UserID);
                    //    if (mu != null && mu.UserID > 0)
                    //    {
                    //        lblUser.Text = mu.UserName;
                    //    }
                    //}
                }
            }
        }
        protected void BtnBack_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["ProjectID"] != null)
            {
                Response.Redirect("Projects.aspx");
            }
            if (Request.QueryString["processesid"] != null)
            {
                int processesid = DataConverter.CLng(Request.QueryString["processesid"]);
                //int projectid=bps.GetSelect(processesid).OpjectID;
                //Response.Redirect("ProjectsProcesses.aspx?ID=" + projectid);
            }
        }
        protected void btnDeleteAll_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem ritem in RpComments.Items)
            {
                CheckBox cb = ritem.FindControl("ChkBox") as CheckBox;
                if (cb.Checked)
                {
                    Label lbl = ritem.FindControl("Label1") as Label;
                    int id = DataConverter.CLng(lbl.Text);
                    //bpc.DeleteByGroupID(id);
                }
            }
            if (Request.QueryString["ProjectID"] != null)
            {
                function.WriteSuccessMsg("删除成功!", "ProcessesComments.aspx?ProjectID=" + Request.QueryString["ProjectID"]);
            }
            if (Request.QueryString["processesid"] != null)
            {
                function.WriteSuccessMsg("删除成功!", "ProcessesComments.aspx?processesid=" + Request.QueryString["processesid"]);
            }
        }
        protected void RpComments_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int id = DataConverter.CLng(e.CommandArgument);
            //bpc.DeleteByGroupID(id);
            if (Request.QueryString["ProjectID"] != null)
            {
                function.WriteSuccessMsg("删除成功!", "ProcessesComments.aspx?ProjectID=" + Request.QueryString["ProjectID"]);
            }
            if (Request.QueryString["processesid"] != null)
            {
                function.WriteSuccessMsg("删除成功!", "ProcessesComments.aspx?processesid=" + Request.QueryString["processesid"]);
            }
        }
    }
}