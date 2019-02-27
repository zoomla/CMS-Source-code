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

public partial class manage_AddOn_ProjectManage : System.Web.UI.Page
{
    private B_Project bll = new  B_Project();
    private M_Project mproject = new M_Project();
    private B_ProjectWork bllwork = new B_ProjectWork();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            B_Admin badmin = new B_Admin();
            badmin.CheckMulitLogin();          
            MyBind();
        }
    }
    public void MyBind()
    {
        B_Project bproject = new B_Project();
        DataView dv = bproject.GetProjectAll();
        this.Egv.DataSource = dv;
        this.Egv.DataKeyNames = new string[] { "ProjectID" };
        this.Egv.DataBind();
    }
    //批量删除
    protected void btnDel_Click(object sender, EventArgs e)
    {       
        for (int i = 0; i <= Egv.Rows.Count - 1; i++)
        {
            CheckBox cbox = (CheckBox)Egv.Rows[i].FindControl("chkSel");
            if (cbox.Checked == true)
            {
                btnDel.Attributes.Add("OnClientClick", "return confirm('你确定要删除吗？');");
                this.bll.DeleteByID(DataConverter.CLng(Egv.DataKeys[i].Value));
            }
        }
        MyBind();
    }
    protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Egv.PageIndex = e.NewPageIndex;
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
                string Id = e.CommandArgument.ToString();
                this.bll.DeleteByID(DataConverter.CLng(Id));
                this.bllwork.DelProWorByPID(DataConverter.CLng(Id));//删除项目工作
                MyBind();
                break;
            case"ModifyProject":
                string Pid = e.CommandArgument.ToString();
                Page.Response.Redirect("AddProject.aspx?Pid="+Pid);
                 break;

        } 
    }
    protected void cbAll_CheckedChanged(object sender, EventArgs e)
    {
        for (int i = 0; i <= Egv.Rows.Count - 1; i++)
        {
            CheckBox cbox = (CheckBox)Egv.Rows[i].FindControl("chkSel");
            if (cbAll.Checked == true)
            {
                cbox.Checked = true;
            }
            else
            {
                cbox.Checked = false;
            }
        }
    }
    public int CountWork(int projectid)
    {
        return this.bllwork.CountWork(projectid);
    }
    public string GetProjectEndDate(int projectid)
    {
        B_Project bproject = new B_Project();
        return bproject.GetProjectEndDate(projectid);
    }
    public string CountRate(int projectid)
    {
        int result = 0;
        int FinishCount = this.bllwork.CountFinishWork(projectid);
        if (FinishCount == 0)
            return "0%";
        else
        {
            result = FinishCount * 100 / CountWork(projectid);
            this.mproject = this.bll.GetProjectByid(projectid);
            if (result == 100)
            {
                this.mproject.Status = 1;
            }
            else
            {
                this.mproject.Status = 0;
            }
            this.bll.Update(this.mproject);    
            return result+"%";
        }
    }

}
