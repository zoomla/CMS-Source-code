using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;

public partial class Manage_Content_DesignNodeList : CustomerPageAction
{
    B_Node nodeBll = new B_Node();
    public int Pid { get { return DataConverter.CLng(Request.QueryString["pid"]); } }
    private enum NodeEnum { Root = 0, Node = 1, SPage = 2, OuterLink = 3 };
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Pid <= 0) { function.WriteErrMsg("没有指定根节点"); }
            MyBind();
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>" + Resources.L.工作台 + "</a></li><li><a href='" + customPath2 + "Config/SiteInfo.aspx'>" + Resources.L.系统设置 + "</a></li><li class='active dropdown'><a href='DesignNodeManage.aspx'>动力节点</a></li><li class='active'>节点详情</li>");
        }
    }
    public void MyBind()
    {
        DataTable dt = nodeBll.SelByPid(Pid, true);
        EGV.DataSource = dt;
        EGV.DataBind();
    }
    public string GetNodeType(string NodeType)
    {
        return B_Node.GetNodeType(DataConverter.CLng(NodeType));
    }
    public string GetOper()
    {
        return GetOper((GetDataItem() as DataRowView).Row);
    }

    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        MyBind();
    }
    public string GetOper(DataRow dr)
    {
        int NodeID = Convert.ToInt32(dr["NodeID"]);
        int NodeType = Convert.ToInt32(dr["NodeType"]);
        int ChildCount = Convert.ToInt32(dr["ChildCount"]);
        //string addLink = "<a href='EditNode.aspx?ParentID=" + NodeID + "' class='option_style'><i class='fa fa-plus' title='" + Resources.L.添加 + "'></i>" + Resources.L.节点 + "</a> ";
        string delLink = "<a href='DelNode.aspx?view=child&pid=" + Pid + "&NodeID=" + NodeID + "' onclick='return confirm(\"确定要删除吗\");' class='option_style'><i class='fa fa-trash-o' title='" + Resources.L.删除 + "'></i>" + Resources.L.删除 + "</a>";
        string outstr = "<a href='EditNode.aspx?view=child&pid=" + Pid + "&NodeID=" + NodeID + "' class='option_style' ><i class='fa fa-pencil' title='" + Resources.L.修改 + "'></i>修改</a> ";/*+addLink*/
        //if (ChildCount > 0)
        //{
        //    outstr = outstr + " <a href='javascript:void(0)' onclick='open_page(" + NodeID + ",2)' class='option_style'><i class='fa fa-list-ol' title='" + Resources.L.排序 + "'></i>" + Resources.L.排序 + "</a>";
        //}
        outstr += delLink;
        return outstr;
    }
    protected void EGV_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView dr = e.Row.DataItem as DataRowView;
            e.Row.Attributes["ondblclick"] = "location.href='EditNode.aspx?view=child&pid=" + Pid + "&NodeID=" + dr["NodeID"] + "';";
            e.Row.Attributes["style"] = "cursor:pointer;";
            e.Row.Attributes["title"] = Resources.L.双击修改;
        }
    }
}