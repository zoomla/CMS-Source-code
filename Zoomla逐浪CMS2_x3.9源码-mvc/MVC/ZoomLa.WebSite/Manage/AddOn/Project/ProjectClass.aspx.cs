using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.BLL;
using ZoomLa.Model;


namespace ZoomLaCMS.Manage.AddOn.Project
{
    public partial class ProjectClass : System.Web.UI.Page
    {
        private string SiteUrl { get { return Request.QueryString["SiteUrl"]; } }
        string hasChild = "{2}<a href='javascript:;' id='a{0}' class='list1' onclick='ShowChild(this);'><span class='folders fa fa-folder'></span> <span class='list_span'>{1}</span></a>";
        string noChild = "{2}<a href='javascript:;'><span class='fa fa-file-word-o'></span> {1}</a>";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                B_Admin.CheckIsLogged();
                if (!IsPostBack)
                {
                    DataTable dt = new DataTable();
                    MyBind(dt);
                }
            }
        }
        public string GetLI(DataTable dt, int depth, int pid)
        {
            if (dt == null || dt.Rows.Count < 1) return "";
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
                if (dt.Select("ParentID='" + Convert.ToInt32(dr[i]["ID"]) + "'").Length > 0)
                {
                    result += string.Format(hasChild, dr[i]["ID"], dr[i]["Name"], pre);
                    result += "<ul class='tvNav tvNav_ul list-unstyled' style='display:none;'>" + GetLI(dt, depth, Convert.ToInt32(dr[i]["ID"])) + "</ul>";
                }
                else
                {
                    result += string.Format(noChild, dr[i]["ID"], dr[i]["Name"], pre);
                }
                result += "</li>";
            }
            return result;
        }
        //绑定类型
        protected string GetProType(string typeid)
        {
            //if (typeBll.GetSelect(DataConverter.CLng(typeid)).ProjectTypeName == "")
            //    return " ";
            //else
            //    return typeBll.GetSelect(Convert.ToInt32(typeid)).ProjectTypeName;
            return "";
        }
        public string GetOtherLi(DataTable dt)
        {
            string result = "", pre = "<img src='/Images/TreeLineImages/t.gif' border='0'>";
            string strwhere = "";
            DataRow[] dr = dt.Select("ParentID=0");
            foreach (DataRow item in dr)
            {
                strwhere += " AND ParentID<>" + item["ID"];
            }
            dr = dt.Select("ParentID>0" + strwhere);
            foreach (DataRow item in dr)
            {
                result += "<li>" + string.Format(noChild, item["ID"], item["Name"], pre) + "</li>";
            }
            return result;
        }
        protected void MyBind(DataTable dt)
        {
            if (dt == null) { function.WriteErrMsg("取值错误,节点数据为空!!"); }
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["Name"].ToString().Length > 7)
                {
                    dr["Name"] = dr["Name"].ToString().Substring(0, 7) + "..";
                }
            }
            NodeHtml_Lit.Text = "<ul class='tvNav list-unstyled'><li><a class='list1' id='a0' href='javascript:;' ><span class='fa fa-folder-open'></span> <span class='list_span'>全部内容</span></a>" + GetLI(dt, 0, 0) + "</li></ul>";
        }
    }
}