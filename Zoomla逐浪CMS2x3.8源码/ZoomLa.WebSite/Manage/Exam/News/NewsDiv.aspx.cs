using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL.Exam;
using ZoomLa.Common;
using ZoomLa.Model.Exam;

public partial class Manage_I_Exam_NewsDiv : System.Web.UI.Page
{
    B_Content_Publish pubBll = new B_Content_Publish();
    M_Content_Publish pubMod = new M_Content_Publish();
    public int Pid { get { return Convert.ToInt32(Request.QueryString["Pid"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            pubMod = pubBll.SelReturnModel(Pid);
            Call.SetBreadCrumb(Master, "<li><a href='News.aspx'>数字出版</a></li><li><a href='"+Request.RawUrl+"'>版面管理</a><span class='curspan'>当前：" + pubMod.NewsName + "</span>[<a href='Publish.aspx?Pid="+pubMod.ID+"'>添加版面</a>]</li>");
            MyBind();
        }
    }
    public void MyBind()
    {
        DataTable dt = pubBll.SelByPid(Pid);
        EGV.DataSource = dt;
        EGV.DataBind();
    }
    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        MyBind();
    }
    protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Del2":
                int id = Convert.ToInt32(e.CommandArgument);
                pubBll.Del(id);
                break;
        }
        MyBind();
    }
}