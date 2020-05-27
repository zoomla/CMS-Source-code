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

public partial class Manage_I_Exam_PublishDesc : System.Web.UI.Page
{
    M_Content_Publish pubMod = new M_Content_Publish();
    B_Content_Publish pubBll = new B_Content_Publish();
    public int Nid { get { return Convert.ToInt32(Request.QueryString["Nid"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
            Call.HideBread(Master);
        }
    }

    public void MyBind()
    {
        DataTable dt = pubBll.SelByNid(Nid);
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
        switch (e.CommandName.ToLower())
        {
            case "del2":
                {
                    int id = Convert.ToInt32(e.CommandArgument);
                    pubBll.Del(id);
                    MyBind();
                }
                break;
            case "edit2":
                {
                    int id = Convert.ToInt32(e.CommandArgument);
                    pubMod = pubBll.SelReturnModel(id);
                    function.Script(this, "ShowParent(" + id + ",'" + pubMod.NewsName + "');");
                }
                break;
        }
    }
}