using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;


namespace ZoomLaCMS.Manage.Content
{
    public partial class SpecialList : CustomerPageAction
    {
        B_Node nodeBll = new B_Node();
        string hasChild = "<input class='nodechk' type='checkbox' value='{0}' data-name='{1}' onclick='ChkChild(this);'><a href='javascript:;'><span class='fa fa-folder'></span></a><a href='javascript:;' id='a{0}' class='list1' onclick='ShowChild();'><span class='list_span'>{1}</span></a>";
        string noChild = "<input class='nodechk' type='checkbox' value='{0}' data-name='{1}'><a href='javascript:;'><span class='fa fa-file'></span></a><a href='javascript:;'>{1}</a>";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                B_Admin badmin = new B_Admin();
                B_ARoleAuth.Check(ZLEnum.Auth.content, "ContentEdit");
                B_Spec bll = new B_Spec();
                DataTable dt = bll.GetSpecAll();
                BindNode(dt);
                Call.HideBread(Master);
            }
        }
        public string GetLI(DataTable dt, int pid = 0)
        {
            string result = "";
            DataRow[] dr = dt.Select("Pid='" + pid + "'");
            for (int i = 0; i < dr.Length; i++)
            {
                result += "<li>";
                if (dt.Select("Pid='" + Convert.ToInt32(dr[i]["SpecID"]) + "'").Length > 0)
                {
                    result += string.Format(hasChild, dr[i]["SpecID"], dr[i]["SpecName"]);
                    result += "<ul class='tvNav tvNav_ul' style='display:none;'>" + GetLI(dt, Convert.ToInt32(dr[i]["SpecID"])) + "</ul>";
                }
                else
                {
                    result += string.Format(noChild, dr[i]["SpecID"], dr[i]["SpecName"]);
                }
                result += "</li>";
            }
            return result;
        }
        protected void BindNode(DataTable dt)
        {
            if (dt == null) { function.WriteErrMsg("取值错误,节点数据为空!!"); }

            NodeHtml_Lit.Text = "<ul class='tvNav'><li><input id='AllCheck' type='checkbox' onclick='checkAll()'><a href='javascript:;'><span class='fa fa-folder'></span></a><a class='list1' id='a0' href='javascript:;' ><span class='list_span'>全部内容</span></a>" + GetLI(dt) + "</li></ul>";
        }
    }
}