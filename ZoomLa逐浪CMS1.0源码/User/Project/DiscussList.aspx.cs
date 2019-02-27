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
using ZoomLa.Model;
using ZoomLa.Common;
using System.IO;
using System.Text;

public partial class User_Project_DiscussList : System.Web.UI.Page
{
    private int m_uid = 1;
    private int m_pid = 0;
    private B_User buser = new B_User();
    private M_UserInfo muser = new M_UserInfo();
    private B_Project bpro = new B_Project();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (HttpContext.Current.Request.Cookies["UserState"] != null)
            {
                this.m_uid = DataConverter.CLng(HttpContext.Current.Request.Cookies["UserState"]["UserID"]);
            }
            InitPage();
            BindList();
        }
    }
    public void InitPage()
    {
        int wid = 0;
        
        M_ProjectWork mprojectwork = new M_ProjectWork();
        B_ProjectWork bprojectwork = new B_ProjectWork();
        if (Request.QueryString["wid"] != null)
        {
            wid = DataConverter.CLng(Request.QueryString["wid"].Trim());            
            mprojectwork = bprojectwork.SelectWorkByWID(wid);
            this.m_pid = mprojectwork.ProjectID;
            LblProIntro.Text = mprojectwork.WorkName.ToString();
            LblProName.Text = bpro.GetProjectByid(Convert.ToInt32(mprojectwork.ProjectID)).ProjectName.ToString();
        }
        if (this.m_uid > 0)
        {
            muser = buser.SeachByID(this.m_uid);
            if (muser.UserName != null)
                TxtUserName.Text = muser.UserName.ToString();
            else
                TxtUserName.Text="佚名";
        }        
       
    }
    public void BindList()
    {
        if (Request.QueryString["wid"] != null)
        {
            B_ProjectDiscuss bprodis = new B_ProjectDiscuss();
            int wid = DataConverter.CLng(Request.QueryString["wid"].Trim());
            Egv.DataSource = bprodis.GetDiscussByWid(wid);
            Egv.DataBind();       
        }
        
       
    }
    protected void EBtnSubmit_Click(object sender, EventArgs e)
    {
        M_ProjectDiscuss mpro=new M_ProjectDiscuss();
        B_ProjectDiscuss bpro=new B_ProjectDiscuss();
        mpro.ProjectID=DataConverter.CLng(Request.QueryString["pid"].Trim());
        mpro.WorkID=DataConverter.CLng(Request.QueryString["wid"].Trim());   
        mpro.UserID=this.m_uid;
        mpro.Content = TxtDisContent.Text.ToString();
        mpro.DiscussDate = DateTime.Now;
        if(bpro.AddProjectDiscuss(mpro))
        {
            Response.Write("<script language=javascript> alert('讨论发布成功！');window.document.location.href='DiscussList.aspx?wid=" + Request.QueryString["wid"].Trim() + "&pid=" + Request.QueryString["pid"].Trim() + "';</script>");
        }
    }
    public string pid
    {
        get { return Request.QueryString["pid"]; }
    }
    protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Egv.PageIndex = e.NewPageIndex;
        BindList();
    }
}
