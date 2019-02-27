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
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
public partial class manage_AddOn_WorkManage : System.Web.UI.Page
{
    private B_ProjectWork bllwork = new B_ProjectWork();
    private M_ProjectWork mprojectwork = new M_ProjectWork();
    private B_ProjectDiscuss bprodis = new B_ProjectDiscuss();
    private M_Project mproject = new M_Project();
    private B_Project bproject = new B_Project();
    private string m_projectname = "";
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
        B_ProjectWork bprojectwork = new B_ProjectWork();
        if (Request.QueryString["Pid"] != null)
        {
            int Pid = DataConverter.CLng(Request.QueryString["Pid"].Trim());//2;
            mproject=bproject.GetProjectByid(Pid);
            DataView dv = bprojectwork.SelectWorkByPID(Pid);
            this.m_projectname = mproject.ProjectName;
            this.Egv.DataSource = dv;
            this.Egv.DataKeyNames = new string[] { "WorkID" };
            this.Egv.DataBind();
        }
        else
        {
            DataTable dt = bprojectwork.GetProjectWorkAll();
            this.Egv.DataSource = dt;
            this.Egv.DataBind();
        }
       
    }
    public string ProjectName
    {
        get { return this.m_projectname; }
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
            case "FinishWork":
                this.mprojectwork=this.bllwork.SelectWorkByWID(Convert.ToInt32(e.CommandArgument));
                int i = this.mprojectwork.WorkID;
                this.mprojectwork.Status=1;
                this.mprojectwork.EndDate = DateTime.Now;              
                this.bllwork.UpdateProjectWork(this.mprojectwork);               
                MyBind();
                break;
            case "EditWork":
                Response.Redirect("AddWork.aspx?Wid=" + Convert.ToInt32(e.CommandArgument) + "");
                break;
            case "DelWork":
                string Id = e.CommandArgument.ToString();               
                this.bllwork.DelProWorByID(DataConverter.CLng(Id));
                //this.bprodis.DelProjectDiscuss(//项目内容删除时关键的讨论也应删除
                MyBind();
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
    protected void btnDel_Click(object sender, EventArgs e)
    {
        for (int i = 0; i <= Egv.Rows.Count - 1; i++)
        {
            CheckBox cbox = (CheckBox)Egv.Rows[i].FindControl("chkSel");
            if (cbox.Checked == true)
            {
                btnDel.Attributes.Add("OnClientClick", "return confirm('你确定要删除吗？');");
                this.bllwork.DelProWorByID(DataConverter.CLng(Egv.DataKeys[i].Value));
            }
        }
        MyBind();
    }

}
