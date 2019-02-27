using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZoomLa.Model;
using ZoomLa.BLL;
using ZoomLa.Common;
using System.Web.UI.HtmlControls;
using System.Xml;

namespace ZoomLaCMS.Manage.AddOn.Project
{
    public partial class ProjectsDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            function.AccessRulo();
            B_Admin badmin = new B_Admin();
            if (!IsPostBack)
            {
                if (Request.QueryString["ProjectID"] != null)
                {
                    //EGV.DataSource = bp.GetBaseByID(DataConverter.CLng(Request.QueryString["ProjectID"]));
                    EGV.DataBind();
                    //DataTable dt = bp.GetBaseByID(DataConverter.CLng(Request.QueryString["ProjectID"]));
                    //lblHtml.Text = bpf.GetUpdateHtml1(DataConverter.CLng(Request.QueryString["ProjectID"]), dt);
                    //mp = bp.GetSelect(DataConverter.CLng(Request.QueryString["ProjectID"]));
                    //LitlName.Text = mp.Name.ToString();
                    //BindProject();
                    //LitlApplicationTime.Text = mp.ApplicationTime.ToString();

                    //if (GetManageGroup(mp.Leader) == 1)
                    //{

                    //    LitlPrice.Text = "￥" + mp.Price.ToString() + ".00";
                    //}
                    //else
                    //{
                    //    LitlPrice.Text = "******";
                    //}
                    //LitlCoding.Text = mp.WebCoding;
                    //LitlReq.Text = mp.Requirements;
                    //if (mp.Leader == null || mp.Leader == "")
                    //    LitlLeader.Text = "暂无";
                    //else
                    //    LitlLeader.Text = mp.Leader;
                    //if (bpt.GetSelect(mp.TypeID).ProjectTypeName=="")
                    //    LitlType.Text = "类型被删";
                    //else
                    //    LitlType.Text = bpt.GetSelect(mp.TypeID).ProjectTypeName;
                    //if (dt != null)
                    //{
                    //    dt.Dispose();
                    //}
                }
                RepCommentsBind();
                RepProcessesBind();
            }
            string projectid = Request.QueryString["ProjectID"];
            //HtmlGenericControl line = (HtmlGenericControl)form1.FindControl("line");
            line.Style["width"] = GetLong(projectid);
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='Projects.aspx'>项目管理</a></li><li class='active'>查看项目</a></li>");
        }
        protected int GetManageGroup(string Leader)
        {
            B_Admin badmin = new B_Admin();

            if (badmin.GetAdminLogin().IsSuperAdmin(badmin.GetAdminLogin().RoleList))
            {
                return 1;
            }
            else
            {
                if (Leader == badmin.GetAdminLogin().AdminName)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }
        //读取XML用户信息
        protected void BindProject()
        {
            //读取XML用户信息
            XmlDocument xdm = new XmlDocument();
            //xdm.LoadXml(mp.UserInfo);
            XmlNodeList nodelist = xdm.SelectSingleNode("Info").ChildNodes;
            LitCusName.Text = nodelist[0].InnerText;
        }
        //返回进度百分比
        protected string GetLong(string id)
        {
            int i = Convert.ToInt32(id);
            //DataTable t = bps.SelectByProID(i);
            //int line = 0;
            //foreach (DataRow r in t.Rows)
            //{
            //    if (r[5].ToString() == "1")
            //    {
            //        line += DataConverter.CLng(r[4].ToString());
            //    }
            //}
            //if (line > 100) { line = 100; }
            //string li = line.ToString();
            //li += "%";
            //return li;
            return "";
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
        protected void RepProcessesBind()
        {
            int projectid = DataConverter.CLng(Request.QueryString["ProjectID"]);
            DataTable dt = new DataTable();
            //dt = bs.SelectByProID(projectid);
            PagedDataSource pds = new PagedDataSource();
            pds.DataSource = dt.DefaultView;
            pds.AllowPaging = true;
            EGV.DataSource = pds;
            EGV.DataBind();
            if (dt != null)
            {
                dt.Dispose();
            }
        }
        protected void RepCommentsBind()
        {
            int projectid = DataConverter.CLng(Request.QueryString["ProjectID"]);
            DataTable dt = new DataTable();
            //dt = bpc.GetByProjectID(projectid);
            RpComments.DataSource = dt;
            RpComments.DataBind();
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
            //if (bs.GetSelect(id).IsComplete == 1)
            //    return true;
            //else
            //    return false;
            return false;
        }
        protected void RepProcesses_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Del":
                    //bs.DeleteByGroupID(id);
                    function.Script(this, "del();");
                    //function.WriteErrMsg("删除成功！", "ProjectsDetail.aspx?ProjectID=" + Request.QueryString["ProjectID"]);
                    break;
                case "Complete":
                    // bs.ChangeComplete(id);
                    Response.Redirect("ProjectsDetail.aspx?ProjectID=" + Request.QueryString["ProjectID"]);
                    break;
                case "Edit":
                    Response.Redirect("AddProcesses.aspx?processesid=" + e.CommandArgument);
                    break;
                default:
                    break;
            }
        }
        protected void BtnCommit_Click(object sender, EventArgs e)
        {
            //评论
            //mpc.Rating = DataConverter.CLng(Request.Form["TxtRating"]);
            //mpc.Content = Request.Form["TxtContent"];
            //mpc.CommentsDate = DateTime.Now;
            //M_AdminInfo mu = new M_AdminInfo();
            //B_Admin bu = new B_Admin();
            //mu = bu.GetAdminLogin();
            //mpc.ProjectsID = DataConverter.CLng(Request.QueryString["ProjectID"]);
            //mpc.ProcessesID = 0;
            //mpc.UserID = mu.AdminId;
            //bpc.GetInsert(mpc);
            function.WriteSuccessMsg("添加成功!", "ProjectsDetail.aspx?ProjectID=" + Request.QueryString["ProjectID"]);
        }
        protected void BtnBack_Click(object sender, EventArgs e)
        {

        }
        //批量删除流程
        protected void btnDeleteAll_Click(object sender, EventArgs e)
        {
            //int projectid = DataConverter.CLng(Request.QueryString["ProjectID"]);
            //foreach (RepeaterItem item in RepProcesses.Items)
            //{
            //    CheckBox cb = (CheckBox)item.FindControl("ChBx");
            //    if (cb.Checked == true)
            //    {
            //        Label lbl = (Label)item.FindControl("Label1");
            //        int id = DataConverter.CLng(lbl.Text);
            //        bs.DeleteByGroupID(id);
            //    }
            //}
            //Response.Redirect("ProjectsDetail.aspx?ProjectID=" + projectid);
            //RepProcessesBind();
        }
        protected void Label1Label1(object sender, EventArgs e)
        {
            string ids = Request.Form["idchk"];
            //bpc.DeleteByGroupID(id);
            RepCommentsBind();
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            int ProjectID = DataConverter.CLng(Request.QueryString["ProjectID"]);
            Response.Redirect("AddUpdateProject.aspx?ID=" + ProjectID + "");
        }
        protected void EGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;

        }
        protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
    }
}