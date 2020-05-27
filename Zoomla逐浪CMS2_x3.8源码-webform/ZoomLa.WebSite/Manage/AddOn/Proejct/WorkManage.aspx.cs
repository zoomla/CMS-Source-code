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
using System.Data.SqlClient;
using System.Text;
using ZoomLa.Components;
using ZoomLa.BLL.Project;
using ZoomLa.Model.Project;

public partial class Manage_AddOn_Proejct_WorkManage : System.Web.UI.Page
{
    private string m_projectname = "";
    private string m_projectid = "0";
    B_Pro_Flow flowBll = new B_Pro_Flow();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            MyBind();
            InitPage();
        }
        Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='Projects.aspx'>项目管理</a></li><li class='active'>流程管理<a href='AddWork.aspx'>[添加流程]</a></li>" + Call.GetHelp(43));
    }
    public void InitPage()
    {
        //StringBuilder strbuild = new StringBuilder();//显示项目自定义
        //StringBuilder strbuild2 = new StringBuilder();//显示进度条
        //StringBuilder strbuild3 = new StringBuilder();//   定义项目模型        添加流程节点 
        //StringBuilder strbComment = new StringBuilder();
        //if (Request.QueryString["Pid"] != null)
        //{
        //    Pid = Convert.ToInt32(Request.QueryString["Pid"].Trim());
        //    this.mproject = this.bproject.GetProjectByid(Pid);
        //    Staus = this.mproject.Status;
        //    DataTable dt = this.bprojectfield.GetProjectFieldByid(Convert.ToInt32(Request.QueryString["Pid"].Trim()));
        //    strbuild.Append("<table style=\"width: 100%; margin: 0 auto;\" cellpadding=\"2\" cellspacing=\"1\" class=\"border\">");
        //    strbuild.Append("<tr class=\"tdbg\"><td class=\"tdbgleft\" align=\"right\" style=\"width: 105px\"><strong>项目名称：&nbsp;</strong></td>");
        //    strbuild.Append("<td class=\"tdbg\" align=\"left\">" + ProjectName + "</td></tr>");
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        if (Staus == 1)
        //        {
        //            string fieldName = dr["FieldName"].ToString().Trim();
        //            if ((fieldName != "项目评价") && (fieldName != "项目评分"))
        //            {
        //                strbuild.Append("<tr class=\"tdbg\"><td class=\"tdbgleft\" align=\"right\" style=\"width: 105px\"><strong>" + dr["FieldName"] + "：&nbsp;</strong> </td>");
        //                strbuild.Append("<td class=\"tdbg\" align=\"left\">" + CheckField(dr["FieldValue"].ToString()) + "</td></tr>");
        //            }
        //            else//有评论
        //            {
        //                Commented = true;
        //                strbComment.Append("<tr class=\"tdbg\"><td class=\"tdbgleft\" align=\"right\" style=\"width: 105px\"><strong>" + dr["FieldName"] + "：&nbsp;</strong> </td>");
        //                strbComment.Append("<td class=\"tdbg\" align=\"left\">" + CheckField(dr["FieldValue"].ToString()) + "</td></tr>");
        //            }
        //        }
        //        else
        //        {
        //            strbuild.Append("<tr class=\"tdbg\"><td class=\"tdbgleft\" align=\"right\" style=\"width: 105px\"><strong>" + dr["FieldName"] + "：&nbsp;</strong> </td>");
        //            strbuild.Append("<td align=\"left\">" + CheckField(dr["FieldValue"].ToString()) + "</td></tr>");
        //        }
        //        if (dt != null)
        //        {
        //            dt.Dispose();
        //        }
        //    }
        //    strbuild.Append("</table>");
        //    LblProject.Text = strbuild.ToString();
        //    strbuild2.Append("<table style=\"width: 100%; margin: 0 auto;\" cellpadding=\"2\" cellspacing=\"1\" class=\"border\">");
        //    strbuild2.Append("<tr class=\"tdbg\"><td class=\"tdbgleft\" align=\"right\" style=\"width:" + CountRate(Convert.ToInt32(ProjectId)) + "; background-color:Green;\">");
        //    strbuild2.Append("</td><td class=\"tdbg\" align=\"left\">" + CountRate(Convert.ToInt32(ProjectId)) + "</td></tr></table>");
        //    Lbl.Text = strbuild2.ToString();
        //    strbuild3.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href=ProjectColumnAdd.aspx?Pid=" + ProjectId + ">定义项目模型</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
        //    strbuild3.Append("<a href=AddWork.aspx?Pid=" + ProjectId + ">添加流程节点</a>");
        //    Lbloper.Text = strbuild3.ToString();
        //    if ((this.bllwork.CountFinishWork(Pid) == CountWork(Pid)) && (CountWork(Pid) > 0))
        //    {
        //        LB.Visible = true;
        //        Pl.Visible = false;

        //        if (Commented)
        //        {
        //            LB.Visible = false;
        //            LbComment.Visible = true;
        //            LbComment.Text = "<table style=\"width: 100%; margin: 0 auto;\" cellpadding=\"2\" cellspacing=\"1\" class=\"border\">" + strbComment.ToString() + "</table>";
        //        }
        //    }
        //}
    }
    private string CheckField(string FieldValue)
    {
        //bool address = false;//局域网 //win04/d/web和http:// 有地址两字 就设为true

        string wordpath = FieldValue;

        if ((wordpath.IndexOf("http://") > -1) || (wordpath.IndexOf("//") > -1) || (wordpath.IndexOf("////") > -1) || (wordpath.IndexOf("\\\\") > -1))
        {
            // wordpath = "<a href='" + wordpath + "' target='_blank'>点击查看</a>";
            wordpath = "<a href='" + wordpath + "' target='_blank' ><img src=\"../../Images/ModelIcon/FriendSite.gif\" alt='点击查看'/></a>";

        }
        else if (wordpath.IndexOf(SiteConfig.SiteOption.UploadDir) > -1)//字段值是url 相对或绝对
        {
            wordpath = "<a href='../../" + wordpath + "' target='_blank'><img src=\"../../Images/ModelIcon/FriendSite.gif\"  alt='点击查看'/></a>";

        }
        return wordpath;
    }
    public void MyBind()
    {
        EGV.DataSource = flowBll.Sel();
        EGV.DataBind();
        //if (Request.QueryString["Pid"] != null)
        //{
        //    int Pid = DataConverter.CLng(Request.QueryString["Pid"].Trim());
        //    this.m_projectid = Pid.ToString();
        //    mproject = bproject.GetProjectByid(Pid);
        //    DataView dv = bprojectwork.SelectWorkByPID(Pid).DefaultView;
        //    this.m_projectname = mproject.ProjectName;
        //    this.Egv.DataSource = dv;
        //    this.Egv.DataKeyNames = new string[] { "WorkID" };
        //    this.Egv.DataBind();
        //}
        //else
        //{
        //    DataTable dt = bprojectwork.GetProjectWorkAll();
        //    this.Egv.DataSource = dt;
        //    this.Egv.DataBind();
        //    if (dt != null)
        //    {
        //        dt.Dispose();
        //    }
        //}

    }
    public string ProjectName
    {
        get { return this.m_projectname; }
    }
    public string ProjectId
    {
        get { return this.m_projectid; }
    }
    protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        MyBind();
    }

    protected void btnDel_Click(object sender, EventArgs e)
    {
        string ids = Request.Form["idchk"];
        if (!string.IsNullOrEmpty(Request.Form["idchk"]))
        {
            btnDel.Attributes.Add("OnClientClick", "return confirm('你确定要删除吗？');");
            foreach (char id in ids)
            {
                //bllwork.DelProWorByID(DataConverter.CLng(id));
            }
        }
        //for (int i = 0; i <= Egv.Rows.Count - 1; i++)
        //{
        //    CheckBox cbox = (CheckBox)Egv.Rows[i].FindControl("idchk");
        //    if (cbox.Checked == true)
        //    {
        //        btnDel.Attributes.Add("OnClientClick", "return confirm('你确定要删除吗？');");
        //        this.bllwork.DelProWorByID(DataConverter.CLng(Egv.DataKeys[i].Value));
        //    }
        //}
        MyBind();
    }
    //public string CountRate(int projectid)
    //{
    //    int result = 0;
    //   // int FinishCount = this.bllwork.CountFinishWork(projectid);
    //    //if (FinishCount == 0)
    //    //    return "0%";
    //    //else
    //    //{
    //    //    result = FinishCount * 100 / CountWork(projectid);
    //    //    //this.mproject = this.bproject.GetProjectByid(projectid);
    //    //    if (result == 100)
    //    //    {
    //    //      //  this.mproject.Status = 1;
    //    //    }
    //    //    else
    //    //    {
    //    //       // this.mproject.Status = 0;

    //    //    }
    //    //   // this.bproject.Update(this.mproject);
    //    //    return result + "%";
    //    //}
    //}
    //public int CountWork(int projectid)
    //{
    //    //return this.bllwork.CountWork(projectid);
    //}
    protected void LB_Click(object sender, EventArgs e)
    {
        //Pl.Visible = true;
        //LB.Visible = false;
    }
    protected void EBtnSubmit_Click(object sender, EventArgs e)
    {
        int Pid = Convert.ToInt32(Request.QueryString["Pid"].Trim());
        //this.mprojectfield.ProjectID = Pid;
        //this.mprojectfield.Alias = this.mprojectfield.FieldName = "项目评价";
        //this.mprojectfield.Type = "nvarchar";
        //this.mprojectfield.FieldValue=TBCommon.Text.Trim();
        //this.bprojectfield.Add(this.mprojectfield);
        //this.mprojectfield.Alias = this.mprojectfield.FieldName = "项目评分";
        //this.mprojectfield.Type = "int";//评分必须为(1-100)数字
        //this.mprojectfield.FieldValue = TxtScore.Text.Trim();
        //if(this.bprojectfield.Add(this.mprojectfield))
        //    Response.Write("<script language=javascript> alert('项目评论添加成功！');window.document.location.href='WorkManage.aspx?Pid="+Pid+"';</script>");
    }


    protected void Egv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int workid = DataConverter.CLng((e.Row.FindControl("hfid") as HiddenField).Value);
            LinkButton LnkDelete = e.Row.FindControl("LnkDelete") as LinkButton;
            LinkButton LinkButton1 = e.Row.FindControl("LinkButton1") as LinkButton;
            if (workid == 1)
            {
                //LnkDelete.Enabled = false;
                //LnkDelete.Attributes.Add("title", "系统数据不可删除");
                //LinkButton1.Enabled = false;
                //LinkButton1.Attributes.Add("title", "系统数据不可停用");
            }
            if (e.Row != null)
            {
                e.Row.Attributes.Add("ondblclick", "javascript:location.href='ProjectsDetail.aspx?ProjectID=" + EGV.DataKeys[e.Row.RowIndex].Value + "'");//双击事件
            }
        }
    }

    protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int id = DataConverter.CLng(e.CommandArgument);
        switch (e.CommandName)
        {
            case "FinishWork":
                //this.mprojectwork=this.bllwork.SelectWorkByWID(Convert.ToInt32(e.CommandArgument));
                //int i = this.mprojectwork.WorkID;
                //if (this.mprojectwork.Status == 0)
                //    this.mprojectwork.Status = 1;
                //else
                //    this.mprojectwork.Status = 0;
                //this.mprojectwork.EndDate = DateTime.Now;              
                //this.bllwork.UpdateProjectWork(this.mprojectwork);
                //if (Request.QueryString["Pid"] != null)
                //    suffix = "?Pid=" + Request.QueryString["Pid"].Trim();
                //Page.Response.Redirect("WorkManage.aspx" + suffix);
                M_Pro_Flow flowMod = flowBll.SelReturnModel(id);
                flowMod.Status = flowMod.Status == 0 ? 1 : 0;
                flowBll.UpdateByID(flowMod);
                break;
            case "EditWork"://修改
                Response.Redirect("AddWork.aspx?id=" + id + "");
                break;
            case "DelWork":
                flowBll.Del(id);
                break;
        }
        MyBind();
    }
}