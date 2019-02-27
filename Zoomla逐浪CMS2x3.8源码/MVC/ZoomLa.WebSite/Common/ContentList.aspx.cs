using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
namespace ZoomLaCMS.Common
{
    public partial class ContentList : System.Web.UI.Page
    {
        B_Node nodeBll = new B_Node();
        string hasChild = "<i class='fa fa-folder'></i><a href='javascript:;' id='a{0}' class='list1' onclick='ShowChild();'><span class='list_span'>{1}</span><span class='NodeP_Span' style='font-size:1em' title='浏览父节点'></span></a>";
        string noChild = "<i class='fa fa-file-text'></i><a href='{2}'>{1}</a>";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["nodeid"]))
            {
                MyBind();
                return;
            }
            B_Admin.CheckIsLogged();
            if (!IsPostBack)
            {
                DataTable dt = new DataTable();
                if (string.IsNullOrEmpty(Request.QueryString["SiteUrl"]))//取本地
                {
                    dt = nodeBll.SelectNodeHtmlXML();
                }
                else
                {
                    WSHelper wsHelper = new WSHelper();
                    dt = wsHelper.GetNodeList(Request.QueryString["SiteUrl"]);
                }
                BindNode(dt);
            }
        }
        void MyBind()
        {
            B_Content conbll = new B_Content();
            DataTable dt = conbll.GetCommModelsByNodeId(Convert.ToInt32(Request.QueryString["nodeid"]));
            Egv.DataSource = dt;
            Egv.DataBind();
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
                    //GetCommModelsByNodeId
                    string aurl = Request.Path + "?" + "SiteUrl=" + Request.QueryString["SiteUrl"] + "&nodeid=" + dr[i]["NodeID"];
                    string adata = "javascript:showContent(\"" + aurl + "\")";
                    result += string.Format(noChild, dr[i]["NodeID"], dr[i]["NodeName"], adata);
                }
                result += "</li>";
            }
            return result;
        }
        protected void BindNode(DataTable dt)
        {
            if (dt == null) { function.WriteErrMsg("取值错误,节点数据为空!!"); }
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["NodeName"].ToString().Length > 7)
                {
                    dr["NodeName"] = dr["NodeName"].ToString().Substring(0, 7) + "..";
                }
            }
            string url = Request.Path + "?" + "SiteUrl=" + Request.QueryString["SiteUrl"] + "&nodeid=0";
            NodeHtml_Lit.Text = "<ul class='tvNav'><li><a class='list1' id='a0' href=\"javascript:showContent('" + url + "');\" ><span class='list_span'>全部内容</span><span class='fa fa-list'></span></a>" + GetLI(dt) + "</li></ul>";
        }
    }
}