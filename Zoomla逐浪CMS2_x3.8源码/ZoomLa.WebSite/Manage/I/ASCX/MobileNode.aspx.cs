using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;

public partial class Manage_I_ASCX_MobileNode : System.Web.UI.Page
{
    B_Node nodeBll = new B_Node();
    string cpath = CustomerPageAction.customPath2 + "Content/";
    string spath = CustomerPageAction.customPath2 + "Shop/";//ShopPath
    string hasChild, noChild;
    B_Admin badmin = new B_Admin();
    public string NodeType 
    {
        get { return Request.QueryString["NodeType"]; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //" + path + "ContentManage.aspx?NodeID={0}
        switch (NodeType)
        {
            case "Shop"://商城
                hasChild = "<a id='a{0}' class='list1'><span class='list_span'>{1}</span><span class='fa fa-chevron-down NodeP_Span'></span></a>";
                noChild = "<a  href='" + spath + "ProductManage.aspx?NodeID={0}'>{1}</a>";
                BindShop();
                break;
            default://内容管理
                hasChild = "<a id='a{0}' class='list1'><span class='list_span'>{1}</span><span class='fa fa-chevron-down NodeP_Span' title='浏览父节点'></span></a>";
                noChild = "<a href='" + cpath + "ContentManage.aspx?NodeID={0}'>{1}</a>";
                BindContent();
                break;
        }
      
    }
    public string GetLI(DataTable dt, int pid = 0)
    {
        string result = "";
        DataRow[] dr = dt.Select("ParentID='" + pid + "'");
        for (int i = 0; i < dr.Length; i++)
        {
            result += "<li>";
            if (dt.Select("ParentID='" + Convert.ToInt32(dr[i]["NodeID"]) + "'").Length > 0)
            {
                result += string.Format(hasChild, dr[i]["NodeID"], dr[i]["NodeName"]);
                result += "<ul class='tvNav tvNav_ul' style='display:none;'>" + GetLI(dt, Convert.ToInt32(dr[i]["NodeID"])) + "</ul>";
            }
            else
            {
                result += string.Format(noChild, dr[i]["NodeID"], dr[i]["NodeName"]);
            }
            result += "</li>";
        }
        return result;
    }
    private void BindContent()
    {
        DataTable dt = nodeBll.SelectNodeHtmlXML();
        foreach (DataRow dr in dt.Rows)
        {
            if (dr["NodeName"].ToString().Length > 7)
            {
                dr["NodeName"] = dr["NodeName"].ToString().Substring(0, 7) + "..";
            }
        }
        if (!badmin.GetAdminLogin().IsSuperAdmin())
        {
            string nodeids = badmin.GetNodeAuthStr();
            if (string.IsNullOrEmpty(nodeids)) nodeids = "-1";
            dt.DefaultView.RowFilter = "NodeID in (" + nodeids + ")";
            dt = dt.DefaultView.ToTable();
        }
        nodeHtml.Text = "<ul class='tvNav'><li><a  class='list1' id='a0' href='" + cpath + "ContentManage.aspx'><span class='list_span'>全部内容</span><span class='fa fa-list' style='margin-left:20px;font-size:2em'></span></a>" + GetLI(dt) + "</li></ul>";
    }
    private void BindShop() 
    {
        DataTable dt = nodeBll.GetAllShopNode();
        nodeHtml.Text = "<ul class='tvNav'><li><a href='"+spath+"ProductManage.aspx' class='list1' id='a0'><span class='list_span'>全部内容</span><span class='fa fa-list'></span></a>" + GetLI(dt) + "</li></ul>";
    }
}