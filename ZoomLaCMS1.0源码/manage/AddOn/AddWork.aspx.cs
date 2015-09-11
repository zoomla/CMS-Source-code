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
public partial class manage_AddOn_AddWork: System.Web.UI.Page
{
    private M_ProjectWork mprojectwork = new M_ProjectWork();
    private M_Project mproject = new M_Project();
    private B_Project bproject = new B_Project();
    private B_ProjectWork bprojectwork = new B_ProjectWork();
    private B_WorkRole bworkrole = new B_WorkRole();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
            InitPage();
    }
    public void InitPage()
    {
        if (Request.QueryString["Pid"] != null)//这项目添加内容
        {
            LblTitleAdd.Visible = true;
            LblTitleModify.Visible = false;
            TxtProjectName.Text = bproject.GetProjectByid(Convert.ToInt32(Request.QueryString["Pid"].Trim())).ProjectName;
            Bind();
        }
        if (Request.QueryString["Wid"] != null)//修改内容
        {
            LblTitleAdd.Visible = false;
            LblTitleModify.Visible = true;
            HFWid.Value = Request.QueryString["Wid"].Trim();
            mprojectwork=bprojectwork.SelectWorkByWID(DataConverter.CLng(Request.QueryString["Wid"].Trim()));
            TxtProjectName.Text = bproject.GetProjectByid(Convert.ToInt32(mprojectwork.ProjectID.ToString())).ProjectName;              
            TxtWorkName.Text = mprojectwork.WorkName;
            TxtWorkIntro.Text=mprojectwork.WorkIntro;
            txtEndDay.Value = mprojectwork.EndDate.ToShortDateString();          
            Bind();
            ArrayList alistrole = bworkrole.GetWorkRole(DataConverter.CLng(HFWid.Value));
            for (int i=0; i < alistrole.Count;i++ )
            {
                int j=DataConverter.CLng(alistrole[i]);
                for (int p = 0; p < cblRoleList.Items.Count; p++)
                {
                    
                    if (j == Convert.ToInt32(cblRoleList.Items[p].Value))
                    {
                        cblRoleList.Items[p].Selected = true;                     
                    }
                }            
            }
            EBtnSubmit.Visible = false;
            EBtnModify.Visible = true;
        }
    }
    private void Bind()
    {
        DataTable dt = B_Role.GetRoleName();
        this.cblRoleList.DataSource = dt;
        this.cblRoleList.DataTextField = "RoleName";
        this.cblRoleList.DataValueField = "RoleId";
        this.cblRoleList.DataBind();
    }
    protected void EBtnSubmit_Click(object sender, EventArgs e)
    {
        int power=0;
        bool addwork = false;
        bool workrole = false;
        M_ProjectWork mprojectwork = new M_ProjectWork();
        B_ProjectWork bprojecwork=new B_ProjectWork();
        M_WorkRole mworkrole = new M_WorkRole();
        B_WorkRole bworkrole=new B_WorkRole();
        if (Request.QueryString["Pid"] != null)
        {
            mprojectwork.WorkName = TxtWorkName.Text.Trim();
            mprojectwork.WorkIntro = TxtWorkIntro.Text.Trim();
            mprojectwork.ProjectID = DataConverter.CLng(Request.QueryString["pid"].Trim());
            mprojectwork.Approving = 0;//默认值
            mprojectwork.Status = 0;
            mprojectwork.EndDate = DataConverter.CDate(txtEndDay.Value.Trim());
            addwork = bprojecwork.AddProjectWork(mprojectwork);
            for (int t = 0; t < this.cblRoleList.Items.Count; t++)
            {
                power = Convert.ToInt32(this.cblRoleList.Items[t].Value);
                if (this.cblRoleList.Items[t].Selected)
                {
                    mworkrole.RoleID = power;
                    mworkrole.WorkID = bprojecwork.GetMaxWorkID(DataConverter.CLng(Request.QueryString["Pid"].Trim()));// GetMaxWorkID(int projectid).ge;
                    workrole = bworkrole.AddWorkRole(mworkrole);
                }
            }
            if (addwork)//&&&& workrole
            {
                Response.Write("<script language=javascript> alert('内容添加成功！');window.document.location.href='WorkManage.aspx?Pid=" + mprojectwork.ProjectID + "';</script>");
            }
        }
        
       
    }
    protected void EBtnModify_Click(object sender, EventArgs e)
    {       
        B_ProjectWork bprojecwork = new B_ProjectWork();
        M_ProjectWork mprojectwork = bprojecwork.SelectWorkByWID(DataConverter.CLng(HFWid.Value));// new M_ProjectWork();
        bool updatework = false;
        bool updaterole = false;
        bool deleterole=false;
        B_WorkRole bworkrole=new B_WorkRole();
        M_WorkRole mworkrole = new M_WorkRole();
        int power = 0;
        mprojectwork.WorkName = TxtWorkName.Text.Trim();
        mprojectwork.WorkIntro = TxtWorkIntro.Text.Trim();      
        mprojectwork.EndDate = DataConverter.CDate(txtEndDay.Value.Trim());
        updatework=bprojecwork.UpdateProjectWork(mprojectwork);
         for (int t = 0; t < this.cblRoleList.Items.Count; t++)//先删除所有
            {              
                if (this.cblRoleList.Items[t].Selected)
                {
                    deleterole=bworkrole.DelWorkRole(Convert.ToInt32(mprojectwork.WorkID));
                    t=this.cblRoleList.Items.Count-1;
                }
            }

        for (int t = 0; t < this.cblRoleList.Items.Count; t++)//再添加
            {
                power = Convert.ToInt32(this.cblRoleList.Items[t].Value);
                if (this.cblRoleList.Items[t].Selected)
                {
                    mworkrole.RoleID = power;
                    mworkrole.WorkID = bprojecwork.GetMaxWorkID(DataConverter.CLng(mprojectwork.ProjectID));// GetMaxWorkID(int projectid).ge;
                    updaterole = bworkrole.AddWorkRole(mworkrole);;
                }
            }
        if (updatework)// && deleterole && updaterole
        {
            Response.Write("<script language=javascript> alert('内容修改成功！');window.document.location.href='WorkManage.aspx?Pid=" + mprojectwork.ProjectID+ "';</script>");
        }
    }

}
