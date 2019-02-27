using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;
using System.Data;
using System.ComponentModel;

/// <summary>
/// 1,指定ID,ParentID,NodeName,如果同名则不需要指定
/// 2,后台指定好li与allli中的超链模板
/// 3,给定数据,绑定显示
/// 用于用户中心节点树
/// </summary>
public partial class Manage_I_ASCX_TreeView : System.Web.UI.UserControl
{
    public string hasChild_tlp = "<li data-id='@NodeID' class='showchild'><span class='fa fa-minus-circle treeicon'></span><span class='tree_name'>@url</span><ul>@childs</ul></li>";
    public string childs_tlp = "<li data-id='@NodeID' class='lastchild' style='@islast'><span class='tree_name'>@url</span></li>";
    [Browsable(false)]
    public DataTable DataSource { get; set; }
    //父节点id字段名
    [Browsable(true)]
    public string NodePid { get; set; }
    //id字段名
    [Browsable(true)]
    public string NodeID { get; set; }
    //节点字段名
    [Browsable(true)]
    public string NodeName { get; set; }
    //"<a class='filter_class' data-val='@NodeID' href='MyMarks.aspx?c_id=@NodeID'>@NodeName</a>";  li中超链接模板
    [Browsable(true)]
    public string LiContentTlp { get; set; }
    //是否收缩节点
    [Browsable(true)]
    public bool IsShrink { get; set; }
    //选中节点，值为NodeID
    [Browsable(true)]
    public int SelectedNode { get; set; }
    //<li><span class='fa fa-list treeicon'></span><a class='filter_class' data-val='0' href=''>全部</a></li>
    public string liAllTlp = "";
    public new void DataBind()
    {
        if (!string.IsNullOrEmpty(NodeID))
            DataSource.Columns[NodeID].ColumnName = "NodeID";
        if (!string.IsNullOrEmpty(NodePid))
            DataSource.Columns[NodePid].ColumnName = "ParentID";
        if (!string.IsNullOrEmpty(NodeName))
            DataSource.Columns[NodeName].ColumnName = "NodeName";
        QuestType_Lit.Text = GetAllQuest(DataSource, 0);
    }
    public string GetAllQuest(DataTable questTypes, int pid)
    {
        string html = "";
        questTypes.DefaultView.RowFilter = "ParentID=" + pid;//抽出父节点
        DataTable parents = questTypes.DefaultView.ToTable();
        for (int i = 0; i < parents.Rows.Count; i++)
        {
            DataRow item = parents.Rows[i];
            if (questTypes.Select("ParentID=" + item["NodeID"]).Length > 0)
            {
                html += TlpReplace(hasChild_tlp, liField, parents, item)
                    .Replace("@childs", GetAllQuest(questTypes, Convert.ToInt32(item["NodeID"]))).Replace("@url", TlpReplace(LiContentTlp, liconField, parents, item));
            }
            else
            {
                html += TlpReplace(childs_tlp, liField, parents, item).Replace("@islast", i == parents.Rows.Count - 1 ? "background-position:0 -1766px;" : "")
                        .Replace("@url", TlpReplace(LiContentTlp, liconField, parents, item));
            }
        }
        return html;
    }
    //----------------------
    private string[] liField = "NodeID,NodeName,ParentID".Split(',');
    private string[] liconField = "NodeID,Link,NodeName".Split(',');
    //填充与替换模板中的内容
    private string TlpReplace(string tlp, string[] fields, DataTable dt, DataRow dr)
    {
        foreach (string field in fields)
        {
            if (dt.Columns.Contains(field)) { tlp = tlp.Replace("@" + field, dr[field].ToString()); }
        }
        return tlp;
    }
}