using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;

public partial class Manage_I_ASCX_NodeTreeJs : System.Web.UI.UserControl
{
        B_Node nodeBll = new B_Node();
        //----节点
        string hasChild, noChild;
        //点击需要打开的目标页面,默认为内容管理页
        private string Url
        {
            get
            {
                string _url = this.Attributes["Url"] == null ? "Content/ContentManage.aspx" : this.Attributes["Url"].ToString();
                return CustomerPageAction.customPath2 + _url;
            }
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
                hasChild = "<a href='javascript:;' onclick=\"ExNode(this,'{0}')\" target='main_right' id='a{0}' class='list1' style='padding-left:0.5em;'><span class='list_span'>{1}</span><i class='fa fa-chevron-down' title='浏览父节点'></i></a>";
                noChild = "<a href='javascript:;' onclick=\"ShowData(this,'{0}')\" target='main_right' onclick='NodeTree.activeSelf(this);' style='padding-left:0.5em;'>{1}</a>";
            
            BindNode();
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
        protected void BindNode()
        {
            B_Admin badmin = new B_Admin();
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
            string allnodelink = "";
                allnodelink = "<a  class='list1' id='a0' href='javascript:;' onclick='ShowData(0)' target='main_right' style='padding-left:0.5em;' ><span class='list_span'>全部内容</span><span class='fa fa-list'></span></a>";
            nodeHtml.Text = "<ul class='tvNav'><li>" + allnodelink + GetLI(dt) + "</li></ul>";
        }
    
}