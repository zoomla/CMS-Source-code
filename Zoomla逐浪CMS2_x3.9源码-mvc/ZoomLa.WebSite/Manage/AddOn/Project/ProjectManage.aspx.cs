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
    public partial class ProjectManage : System.Web.UI.Page
    {
        //private DataView DataContainer =new DataView() new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            function.AccessRulo();
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li>项目管理</li>");
            B_Admin badmin = new B_Admin();
            badmin.CheckIsLogin();
            if (!Page.IsPostBack)
            {
                MyBind();
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
                    }
                }
                SearchBind();
                BindProCate();
            }
        }
        private void BindProCate()
        {
            //this.DLProCate.DataSource = bprocate.GetProjectCategoryAll();
            //this.DLProCate.DataTextField = "ProjectCategoryName";
            //this.DLProCate.DataValueField = "ProjectCategoryID";
            //this.DLProCate.DataBind();
            //this.DLProCate.Items.Add("按分类查看");
            //this.DLProCate.Items[this.DLProCate.Items.Count - 1].Value = "0";
            //if (Request.QueryString["cateid"] == null)
            //{
            //    this.DLProCate.SelectedIndex = this.DLProCate.Items.Count - 1;
            //}
            //else
            //{           
            //    for (int i = 0; i < DLProCate.Items.Count;i++)
            //    {
            //        if (DLProCate.Items[i].Value == Request.QueryString["cateid"].Trim())
            //        {
            //            this.DLProCate.SelectedIndex = i;
            //        }
            //    }
            //}
        }
        public void SearchBind()
        {
            string searchvalue = string.Empty;
            int searchtype = 0;
            if ((Request.QueryString["action"] != null) && (Request.QueryString["searchtype"] != null) && (Request.QueryString["searchvalue"] != null))
            {
                searchtype = DataConverter.CLng(Request.QueryString["searchtype"].Trim());
                searchvalue = Request.QueryString["searchvalue"].Trim();
                //DataView dv = this.bll.ProjectSearch(searchtype, searchvalue).DefaultView;
            }
            else if (Request.QueryString["cateid"] != null)
            {
                //DataView dv = this.bll.ProjectCategory(Convert.ToInt32(Request.QueryString["cateid"].Trim())).DefaultView;
                //this.Egv.DataSource = dv;
                //this.Egv.DataBind();
            }

        }
        private void PassedProject(int pid)
        {
            //if(this.bll.PassedProjectByID(pid))
            //    Page.Response.Redirect("ProjectManage.aspx");
        }
        private void ProjectPassedCancel(int pid)
        {
            //if(this.bll.PassedProjectCancelByID(pid))
            //    Page.Response.Redirect("ProjectManage.aspx");
        }
        public void MyBind()
        {
            //DataView dv = bproject.GetProjectAll().DefaultView;
            //this.Egv.DataSource = dv;
            //this.Egv.DataKeyNames = new string[] { "ProjectID" };
            //this.Egv.DataBind();
        }
        //批量删除
        protected void btnDel_Click(object sender, EventArgs e)
        {
            int Id = 0;
            for (int i = 0; i <= Egv.Rows.Count - 1; i++)
            {
                CheckBox cbox = (CheckBox)Egv.Rows[i].FindControl("chkSel");
                if (cbox.Checked == true)
                {
                    Id = DataConverter.CLng(Egv.DataKeys[i].Value);
                    //btnDel.Attributes.Add("OnClientClick", "return confirm('你确定要删除吗？');");
                    //this.bll.DeleteByID(DataConverter.CLng(Egv.DataKeys[i].Value));
                    //this.bllwork.DelProWorByPID(DataConverter.CLng(Id));//删除项目节点
                    //this.bprojectfield.DeleteByID(Id);//删除对应的项目字段
                }
            }
            MyBind();
        }
        protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Egv.PageIndex = e.NewPageIndex;
            if (Request.QueryString["action"] != null)
                SearchBind();
            else
                MyBind();
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "AddWork":
                    Response.Redirect("AddWork.aspx?Pid=" + Convert.ToInt32(e.CommandArgument) + "");
                    break;
                case "ShowWork":
                    Response.Redirect("WorkManage.aspx?Pid=" + Convert.ToInt32(e.CommandArgument) + "");
                    break;
                case "DelProject":
                    int Id = DataConverter.CLng(e.CommandArgument.ToString());
                    //this.bll.DeleteByID(Id);
                    //this.bllwork.DelProWorByPID(Id);//删除项目节点
                    //this.bprojectfield.DeleteByID(Id);//删除对应的项目字段
                    MyBind();
                    break;
                case "ModifyProject":
                    string Pid = e.CommandArgument.ToString();
                    Page.Response.Redirect("AddProject.aspx?Pid=" + Pid);
                    break;
                case "AddColumn":
                    string Prid = e.CommandArgument.ToString();
                    Page.Response.Redirect("ProjectColumnAdd.aspx?Pid=" + Prid);
                    break;
                case "ModifyPermissions":
                    Prid = e.CommandArgument.ToString();
                    Page.Response.Redirect("../User/GroupFieldPermissions.aspx?Pid=" + Prid);
                    break;


            }
        }
        public int CountWork(int projectid)
        {
            return 0;
            //return this.bllwork.CountWork(projectid);
        }
        public string GetProjectEndDate(int projectid)
        {
            //B_Project bproject = new B_Project();
            //return bproject.GetProjectEndDate(projectid);
            return "";
        }
        public string GetProjectEndDate2(string dt)
        {
            dt = Convert.ToDateTime(dt).ToShortDateString();
            if (dt == DateTime.MaxValue.ToShortDateString())
            {
                return string.Empty;
            }
            else
            {
                return dt;
            }
        }
        public string CountRate(int projectid)
        {
            //int result = 0;
            //int FinishCount = this.bllwork.CountFinishWork(projectid);
            //if (FinishCount == 0)
            //    return "0%";
            //else
            //{
            //    result = FinishCount * 100 / CountWork(projectid);
            //    this.mproject = this.bll.GetProjectByid(projectid);
            //    if (result == 100)
            //    {
            //        this.mproject.Status = 1;
            //    }
            //    else
            //    {
            //        this.mproject.Status = 0;
            //    }
            //    this.bll.Update(this.mproject);    
            //    return result+"%";
            //}
            return "";
        }
        protected void Search_Click(object sender, EventArgs e)
        {
            string keyvalue = SearchValue.Text.Trim();
            int typeid = DataConverter.CLng(DLType.SelectedValue);
            Page.Response.Redirect("ProjectManage.aspx?action=Search&searchtype=" + typeid + "&searchvalue=" + keyvalue);
        }
        public string GetSurplusDays(int projectid)
        {
            string days = string.Empty;
            //this.mproject = this.bll.GetProjectByid(projectid);
            //if (mproject.EndDate.ToShortDateString() == DateTime.MaxValue.ToShortDateString())
            //{
            //    days = " ";
            //}
            //else 
            //{
            //    //finishflag = this.mproject.Status;
            //    //if ((finishflag == 0)&&(this.mproject.Passed))
            //    //{
            //    //    System.TimeSpan diff1 = (mproject.EndDate).Subtract(DateTime.Now);
            //    //    if (diff1.Days < 0)
            //    //        days = "超时" + System.Math.Abs(Convert.ToInt32(diff1.Days.ToString())) + "天";
            //    //    else
            //    //        days = diff1.Days.ToString()+"天";
            //    //}          
            //}
            return days;
        }
        protected void DLProCate_SelectIndexChanging(object sender, EventArgs e)
        {
            //string procateid = DLProCate.SelectedValue.ToString();
            //string suffix=string.Empty;
            //if (procateid != "0")
            //{
            //    suffix = "?cateid=" + procateid;
            //}
            //Page.Response.Redirect("ProjectManage.aspx" + suffix);
        }
        protected void Egv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int i;
            for (i = -1; i < this.Egv.Rows.Count; i++)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Attributes.Add("onmouseover", "c=this.className='tdbgmouseover'");
                    e.Row.Attributes.Add("onmouseout", "this.className='tdbg'");
                }
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "this.className='tdbgmouseover'");
                e.Row.Attributes.Add("onmouseout", "this.className='tdbg'");
            }
        }
    }
}