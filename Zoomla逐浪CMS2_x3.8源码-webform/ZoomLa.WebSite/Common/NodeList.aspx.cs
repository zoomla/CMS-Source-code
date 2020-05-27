using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
public partial class Common_NodeList : System.Web.UI.Page
{

    B_Node nodeBll = new B_Node();
    private string SiteUrl { get { return Request.QueryString["SiteUrl"]; } }
    private string ModelIDS { get { return Request.QueryString["ModelIDS"]; } }//筛选指定的模型节点
    public string Source { get { return Request.QueryString["Source"]; } }
    string hasChild = "{2}<label><input name='nodeChk' type='checkbox' data-name='{1}' value='{0}' onclick='ChkChild(this);'><i class='fa fa-folder' style='color:#337AB7'></i></label><a href='javascript:;' id='a{0}' class='list1' onclick='ShowChild(this);'><span class='list_span'>{1}</span></a>";
    string noChild = "{2}<label><input name='nodeChk' type='checkbox' data-name='{1}' value='{0}'><i class='fa fa-file-text' style='color:#337AB7'></i></label><a href='javascript:;'>{1}</a>";
    protected void Page_Load(object sender, EventArgs e)
    {
        B_Admin.CheckIsLogged();
        if (!IsPostBack)
        {
            DataTable dt = new DataTable();
            if (string.IsNullOrEmpty(SiteUrl))//取本地
            {
                if (!string.IsNullOrEmpty(ModelIDS))
                {
                    dt = nodeBll.SelNodeByModel(ModelIDS);
                }
                else { dt = nodeBll.SelToNodeList(); }
            }
            else
            {
                WSHelper wsHelper = new WSHelper();
                dt = wsHelper.GetNodeList(SiteUrl);
            }
            BindNode(dt);
        }
    }
    public string GetLI(DataTable dt, int depth, int pid)
    {
        string result = "", pre = "<img src='/Images/TreeLineImages/t.gif' border='0'>", span = "<img src='/Images/TreeLineImages/tree_line4.gif' border='0' width='19' height='20'>";
        DataRow[] dr = dt.Select("ParentID='" + pid + "'");
        depth++;
        for (int i = 1; i < depth; i++)
        {
            pre = span + pre;
        }
        for (int i = 0; i < dr.Length; i++)
        {
            result += "<li>";
            if (dt.Select("ParentID='" + Convert.ToInt32(dr[i]["NodeID"]) + "'").Length > 0)
            {
                result += string.Format(hasChild, dr[i]["NodeID"], dr[i]["NodeName"], pre);
                result += "<ul class='tvNav tvNav_ul list-unstyled' style='display:none;'>" + GetLI(dt, depth, Convert.ToInt32(dr[i]["NodeID"])) + "</ul>";
            }
            else
            {
                result += string.Format(noChild, dr[i]["NodeID"], dr[i]["NodeName"], pre);
            }
            result += "</li>";
        }
        return result;
    }
    public string GetOtherLi(DataTable dt)
    {
        string result = "", pre = "<img src='/Images/TreeLineImages/t.gif' border='0'>";
        string strwhere = "";
        DataRow[] dr = dt.Select("ParentID=0");
        foreach (DataRow item in dr)
        {
            strwhere+=" AND ParentID<>"+item["NodeID"];
        }
        dr = dt.Select("ParentID>0" + strwhere);
        foreach (DataRow item in dr)
        {
            result+="<li>"+string.Format(noChild, item["NodeID"], item["NodeName"], pre)+"</li>";
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
        NodeHtml_Lit.Text = "<ul class='tvNav list-unstyled'><li><input id='AllCheck' type='checkbox' onclick='checkAll()'><a class='list1' id='a0' href='javascript:;' ><span class='fa fa-list'></span><span class='list_span'>全部内容</span></a>" + GetLI(dt,0,0) + "</li></ul>";
    }
}