using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;

public partial class Manage_ContentRank : System.Web.UI.Page
{
    B_Node b_Node = new B_Node();
    B_Content_Count countBll = new B_Content_Count();
    B_Admin admin = new B_Admin();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack) 
        {
            MyBind();
        }
        Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='Subject.aspx?Type=subject'>工作统计</a></li><li class='active'>排行榜</li>" + Call.GetHelp(52));
    }
    public void MyBind()
    {
        DataTable dt = new DataTable();
        dt = b_Node.GetNodeListContainXML(0);
        NodeList.DataSource = dt;
        NodeList.DataTextField = "NodeName";
        NodeList.DataValueField = "NodeID";
        NodeList.DataBind();
        NodeList.Items.Insert(0, new ListItem("所有栏目", ""));
        NodeList.SelectedValue = "NodeID";
        //所有栏目
        dt = countBll.SelRankData(1);
        Hits_RPT.DataSource = dt;
        Hits_RPT.DataBind();
        dt = countBll.SelRankData(2);
        Com_RPT.DataSource = dt;
        Com_RPT.DataBind();
        dt = countBll.SelRankData(3);
        Di_RPT.DataSource = dt;
        Di_RPT.DataBind();
        //子栏目
        dt = b_Node.SelectNodeHtmlXML();
        dt.DefaultView.RowFilter = "ParentID=0";
        dt = dt.DefaultView.ToTable();
        ItemList_RPT.DataSource = dt;
        ItemList_RPT.DataBind();
        
    }
    protected void ItemList_RPT_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView drv = e.Item.DataItem as DataRowView;
        if (drv!=null)
        {
            Repeater rpt= e.Item.FindControl("Hits_RPT") as Repeater;
            DataTable dt= countBll.SelRankData(1, Convert.ToInt32(drv["NodeID"]));
            rpt.DataSource = dt;
            rpt.DataBind();
            rpt = e.Item.FindControl("Com_RPT") as Repeater;
            dt = countBll.SelRankData(2, Convert.ToInt32(drv["NodeID"]));
            rpt.DataSource = dt;
            rpt.DataBind();
            rpt = e.Item.FindControl("Gi_RPT") as Repeater;
            dt = countBll.SelRankData(3, Convert.ToInt32(drv["NodeID"]));
            rpt.DataSource = dt;
            rpt.DataBind();
        }
    }
}