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
using ZoomLa.BLL;//using ZLCollecte.Common;
using ZoomLa.Common;
using ZoomLa.Model;

public partial class manage_AddOn_AddProject : System.Web.UI.Page
{
    private int m_uid = 1;//测试用
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (HttpContext.Current.Request.Cookies["UserState"] != null)
            {
                this.m_uid = DataConverter.CLng(HttpContext.Current.Request.Cookies["UserState"]["UserID"]);
            }
            InitPage();
            if (Request.QueryString["Pid"] != null)
            {
                InItModify(Convert.ToInt32(Request.QueryString["Pid"].Trim()));
            }
        }
    }
    public void InItModify(int pid)
    {
        LblTitle.Text = "修改项目";
        B_Project bproject=new B_Project();
        M_Project mproject = bproject.GetProjectByid(pid);
        TxtProjectName.Text = mproject.ProjectName;
        TxtProjectIntro.Text = mproject.ProjectIntro;
        tbUBday.Value = mproject.StartDate.ToShortDateString();
        txtEndDay.Value = mproject.EndDate.ToShortDateString();
    }
    public void InitPage()
    {
        tbUBday.Value = DateTime.Now.ToShortDateString();       
    }
    protected void EBtnSubmit_Click(object sender, EventArgs e)
    {
        M_Project mproject=new M_Project();
        mproject.ProjectName=TxtProjectName.Text.Trim();
        mproject.ProjectIntro=TxtProjectIntro.Text.Trim();
        mproject.StartDate=DataConverter.CDate(tbUBday.Value.Trim());
        if (!string.IsNullOrEmpty(txtEndDay.Value.Trim()))
            mproject.EndDate = DataConverter.CDate(txtEndDay.Value.Trim());
        else
            mproject.EndDate=DateTime.MaxValue;//new DateTime();
        mproject.Uid = this.m_uid;
        mproject.Status = 0;
        if (Request.QueryString["rid"] != null)
            mproject.RequireID = DataConverter.CLng(Request.QueryString["rid"].Trim());
        else
            mproject.RequireID = 0;//无需求ID也可以由管理员自己添加项目
        B_Project bproject=new B_Project();
        if (bproject.Add(mproject))
        {
            Response.Write("<script language=javascript> alert('项目添加成功！');window.document.location.href='ProjectManage.aspx';</script>");
        }
    }
    protected void EBtnModify_Click(object sender, EventArgs e)
    {
        B_Project bproject=new B_Project();
        M_Project mproject = bproject.GetProjectByid(Convert.ToInt32(Request.QueryString["Pid"].Trim()));
        mproject.ProjectName = TxtProjectName.Text;
        mproject.ProjectIntro = TxtProjectIntro.Text;
        mproject.StartDate = DataConverter.CDate(tbUBday.Value.Trim());
        if (!string.IsNullOrEmpty(txtEndDay.Value.Trim()))
            mproject.EndDate = DataConverter.CDate(txtEndDay.Value.Trim());
        else
            mproject.EndDate = DateTime.MaxValue;//new DateTime();
        if (bproject.Update(mproject))
        {
            Response.Write("<script language=javascript> alert('项目修改成功！');window.document.location.href='ProjectManage.aspx';</script>");
        }
    }
}
