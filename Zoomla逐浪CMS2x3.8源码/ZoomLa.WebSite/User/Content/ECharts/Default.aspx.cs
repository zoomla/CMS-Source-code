using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL.Content;
using ZoomLa.Model;
using ZoomLa.Common;
using System.Data;
using ZoomLa.BLL;


public partial class Manage_Content_ECharts_Default :CustomerPageAction
{
    B_Content_Chart chartBll = new B_Content_Chart();
    M_Content_Chart chartMod = new M_Content_Chart();
    B_User buser = new B_User();
    
    public string SType { get { return Request.QueryString["stype"]; } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind(); 
        }
    }
    public void MyBind()
    {
        DataTable dt = chartBll.SelByUid(buser.GetLogin().UserID);
        EGV.DataSource = dt;
        if (!string.IsNullOrEmpty(SType))
            dt.DefaultView.RowFilter = "SType='"+SType+"'";
        EGV.DataBind();
    }
    protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        MyBind();
    }
    public string GetStype() 
    {
        return chartMod.GetTypeStr(Eval("SType","{0}"));
    }
    protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "del2":
                chartBll.Del(Convert.ToInt32(e.CommandArgument));
                break;
        }
        MyBind();
    }
    protected void Dels_B_Click(object sender, EventArgs e)
    {
        chartBll.DelByIds(Request.Form["idchk"]);
        MyBind();
    }
}