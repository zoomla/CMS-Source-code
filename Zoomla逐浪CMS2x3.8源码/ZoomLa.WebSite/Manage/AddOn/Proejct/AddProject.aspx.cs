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
using System.Text;
public partial class manage_AddOn_AddProject : System.Web.UI.Page
{
    private int m_uid = 1;//测试用
    protected void Page_Load(object sender, EventArgs e)
    {
        function.AccessRulo();
        B_Admin badmin = new B_Admin();
        badmin.CheckIsLogin();
        Call.SetBreadCrumb(Master, "<li>后台管理</li><li>项目管理</li><li><a href='ProjectManage.aspx'> 项目列表</a></li>");
        if (!Page.IsPostBack)
        {
            BindProCate();
            m_uid = badmin.GetAdminLogin().AdminId;
            InitPage();
            if (Request.QueryString["Pid"] != null)
            {
                InItModify(Convert.ToInt32(Request.QueryString["Pid"].Trim()));
            }
            
        }
    }
    private void BindProCate()
    {
        //B_ProjectCategory bprocate=new B_ProjectCategory();
        //this.DLProCate.DataSource=bprocate.GetProjectCategoryAll();
        //this.DLProCate.DataTextField = "ProjectCategoryName";
        //this.DLProCate.DataValueField = "ProjectCategoryID";
        //this.DLProCate.DataBind();
        //this.DLProCate.Items.Add("请选择分类");
        //this.DLProCate.Items[this.DLProCate.Items.Count - 1].Value = "0";
        //this.DLProCate.SelectedIndex = this.DLProCate.Items.Count - 1; 
    }
    public void InItModify(int pid)
    {
        LblTitle.Text = "修改项目";
        //B_Project bproject=new B_Project();
        //M_Project mproject = bproject.GetProjectByid(pid);
        //TxtProjectName.Text = mproject.ProjectName;
        //TxtProjectIntro.Text = mproject.ProjectIntro;
        
        //for(int i=0;i<DLProCate.Items.Count;i++)
        //{
        //    string x = i + "|" + mproject.ProCateid.ToString();
        //    if(DLProCate.Items[i].Value.ToString()==mproject.ProCateid.ToString())
        //    {
        //        DLProCate.SelectedIndex=i;
        //        TBCate.Text=DLProCate.Items[i].Text;                      
        //    }    
        //} 
        //tbUBday.Value = mproject.StartDate.ToShortDateString();
        //txtEndDay.Value = mproject.EndDate.ToShortDateString();
        EBtnModify.Visible = true;
        EBtnSubmit.Visible = false;
        Cancel.Attributes.Add("onclick","javascript:window.location.href='ProjectManage.aspx'");
    }
    public void InitPage()
    {
        tbUBday.Value = DateTime.Now.ToShortDateString();       
    }
    protected void EBtnSubmit_Click(object sender, EventArgs e)
    {

        StringBuilder strmessage = new StringBuilder();
        //strmessage.Append("创建失败可能原因如下:\\n");
        //mproject.ProjectName=TxtProjectName.Text.Trim();
        //mproject.ProjectIntro=TxtProjectIntro.Text.Trim();
        //mproject.StartDate=DataConverter.CDate(tbUBday.Value.Trim());
        //if (!string.IsNullOrEmpty(txtEndDay.Value.Trim()))
        //    mproject.EndDate = DataConverter.CDate(txtEndDay.Value.Trim());
        //else
        //    mproject.EndDate=DateTime.MaxValue;//new DateTime();
        //TimeSpan Ts = mproject.EndDate.Subtract(mproject.StartDate);
        //if (Ts.Days < 0)
        //{
        //    Response.Write("<script language=javascript> alert('完成时间不应小于开始时间！');window.document.location.href=window.document.location.href;</script>");
        //    flag = false;
        //}
        //mproject.Uid = this.m_uid;
        //mproject.Status = 0; 
        //if (Request.QueryString["rid"] != null)
        //    mproject.RequireID = DataConverter.CLng(Request.QueryString["rid"].Trim());
        //else
        //    mproject.RequireID = 0;//无需求ID也可以由管理员自己添加项目

        //if ((Convert.ToInt32(this.DLProCate.SelectedValue) > 0)&&(flag))
        //{
        //    mproject.ProCateid = Convert.ToInt32(this.DLProCate.SelectedValue);
        //    if (bproject.Add(mproject))
        //    {               
        //        Response.Write("<script language=javascript> alert('项目添加成功！');window.document.location.href='ProjectManage.aspx';</script>");
        //    }
        //}
        //else
        //{
        //    Response.Write("<script language=javascript> alert('请选择项目分类！');window.document.location.href=window.document.location.href;</script>");
           
        
        //}       
       
    }
    protected void EBtnModify_Click(object sender, EventArgs e)
    {
        //mproject.ProjectName = TxtProjectName.Text;
        //mproject.ProjectIntro = TxtProjectIntro.Text;
        //mproject.StartDate = DataConverter.CDate(tbUBday.Value.Trim());
        //mproject.ProCateid = DataConverter.CLng(DLProCate.SelectedValue);
        //if (!string.IsNullOrEmpty(txtEndDay.Value.Trim()))
        //    mproject.EndDate = DataConverter.CDate(txtEndDay.Value.Trim());
        //else
        //    mproject.EndDate = DateTime.MaxValue;//new DateTime();
        //if (bproject.Update(mproject))
        //{
        //    Response.Write("<script language=javascript> alert('项目修改成功！');window.document.location.href='ProjectManage.aspx';</script>");
        //}
    }
    protected void DLProCate_SelectIndexChanging(object sender, EventArgs e)
    {
        if (DLProCate.SelectedValue != "0")
        {
            TBCate.Text = this.DLProCate.Items[this.DLProCate.SelectedIndex].Text;
        }
        else
        {
            TBCate.Text = "";
        }
    }
}
