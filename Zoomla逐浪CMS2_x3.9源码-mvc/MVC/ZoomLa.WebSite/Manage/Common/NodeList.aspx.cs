using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;
using ZoomLa.BLL;
using System.Data;
using ZoomLa.Components;
using ZoomLa.Model;

namespace ZoomLaCMS.Manage.Common
{
    public partial class NodeList : CustomerPageAction
    {
        private B_Node nodeBll = new B_Node();
        string hasChild = "<input type='checkbox' name='nodes' data-name='{1}' value='{0}' /> <a href='javascript:;'><span class='fa fa-folder'></span></a><a href='javascript:;' id='a{0}' class='list1' onclick='ShowChild(this);'><span class='list_span'>{1}</span></a>";
        string noChild = "<input type='checkbox' name='nodes' data-name='{1}' value='{0}' /> <a href='javascript:;'><span class='fa fa-file'></span></a><a href='javascript:;'>{1}</a>";
        protected void Page_Load(object sender, EventArgs e)
        {
            Call.HideBread(Master);
            B_Admin badmin = new B_Admin();
            B_ARoleAuth.Check(ZLEnum.Auth.content, "ContentEdit");
            B_Spec bll = new B_Spec();
            DataTable dt = nodeBll.SelForShowAll(0, true);
            BindNode(dt);
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
        protected void BindNode(DataTable dt)
        {
            if (dt == null) { function.WriteErrMsg("取值错误,节点数据为空!!"); }

            NodeHtml_Lit.Text = "<ul class='tvNav'><li><a href='javascript:;'><span class='fa fa-folder-open'></span></a><a class='list1' id='a0' href='javascript:;' ><span class='list_span'>全部内容</span></a><ul class='tvNav tvNav_ul'>" + GetLI(dt) + "</li></ul></ul>";
        }
    }
}