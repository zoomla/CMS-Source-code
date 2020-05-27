using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;
/*
*1,必须引入ZL_Common.js
*2,必须将页面上的隐藏控件ID赋给Selected属性
*3,绑定号NodeID,Pid,Name的字段名称
*/

namespace ZoomLaCMS.Manage.I.ASCX
{
    public partial class TreeTlpDP : System.Web.UI.UserControl
    {
        //id,pid,name

        public DataTable DataSource { get { return ViewState[ClientID + "_DataSource"] as DataTable; } set { ViewState[ClientID + "_DataSource"] = value; } }
        [Browsable(true)]
        public string EmpyDataText { get { return GetVal(ClientID + "_EmpyDataText", "请选择节点!"); } set { ViewState[ClientID + "_EmpyDataText"] = value; } }
        [Browsable(true)]
        public string NodeID
        {
            get { return GetVal("Tlp_NodeID", "ID"); }
            set { ViewState["Tlp_NodeID"] = value; }
        }
        [Browsable(true)]
        public string Pid { get { return GetVal(ClientID + "_Pid", "Pid"); } set { ViewState[ClientID + "_Pid"] = value; } }
        [Browsable(true)]
        public string Name { get { return GetVal(ClientID + "_Name", "Name"); } set { ViewState[ClientID + "_Name"] = value; } }
        [Browsable(true)]
        /// <summary>
        /// 
        /// </summary>
        public string Selected
        {
            get { return GetVal(ClientID + "_Selected", ""); }
            set { ViewState[ClientID + "_Selected"] = value; }
        }
        private string GetVal(string name, string def)
        {
            if (ViewState[name] == null || string.IsNullOrEmpty(ViewState[name].ToString()))
            {
                return def;
            }
            else { return ViewState[name].ToString(); }
        }
        string treetlp = "<img src='/Images/TreeLineImages/tree_line4.gif'  border='0' width='19' height='20' />";
        string parent_tlp = "<li data-pid='@pid'><a href='javascript:;' class='treenode_parent' style='@isshow' data-pid='@pid' data-id=@id>@tree4<span class='fa fa-folder'></span> @name</a></li>";
        string questtlp = "<li data-pid='@pid'><a class='treenode' href='javascript:;' data-id=@id style='@isshow' data-pid='@pid' onclick=''>@tree4@treeicon<span class='fa fa-file'></span> @name</a></li>";
        public string GetAllData(int pid, int index)
        {
            string html = ""; string tree4 = "";
            for (int i = 0; i < index; i++)
                tree4 += treetlp;
            DataRow[] drs = DataSource.Select(Pid + "=" + pid);
            foreach (DataRow item in drs)
            {
                if (DataSource.Select(Pid + "=" + item[NodeID]).Length > 0)
                {
                    html += parent_tlp.Replace("@id", item[NodeID].ToString()).Replace("@name", item[Name].ToString()).Replace("@tree4", tree4).Replace("@pid", item[Pid].ToString()).Replace("@isshow", pid > 0 ? "display:none" : "");
                    html += GetAllData(DataConverter.CLng(item[NodeID]), index + 1);
                }
                else
                {
                    string isshow = item[Pid].ToString().Equals("0") ? "" : "display:none;";
                    string treeicon = item[Pid].ToString().Equals("0") ? "" : "<img src='/Images/TreeLineImages/t.gif'>";
                    html += questtlp.Replace("@tree4", tree4).Replace("@treeicon", treeicon).Replace("@pid", item[Pid].ToString()).Replace("@id", item[NodeID].ToString()).Replace("@name", item[Name].ToString()).Replace("@isshow", isshow);
                }
            }
            return html;
        }
        public void MyBind()
        {
            if (DataSource == null)
            {
                function.WriteErrMsg("没有绑定节点");
            }
            TreeTlp_Li.Text = GetAllData(0, 0);
            function.Script(this.Page, "TreeTlp.Init('" + this.ClientID + "','" + Selected + "');");
        }
    }
}