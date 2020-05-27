using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Components;


namespace ZoomLaCMS.Search
{
    public partial class Default : System.Web.UI.Page
    {
        private B_Node bll = new B_Node();
        private B_Content content = new B_Content();
        private B_Product product = new B_Product();

        string hasChild = "<option value='{0}'>{2}|-{1}</option>";
        string noChild = "<option value='{0}'>{2}|-{1}</option>";
        public string CreateDP(DataTable dt, int depth = 0, int pid = 0)
        {
            string result = "", pre = "", span = "&nbsp&nbsp";
            DataRow[] dr = dt.Select("ParentID='" + pid + "'");
            depth++;
            for (int i = 1; i < depth; i++)
            {
                pre = span + pre;
            }
            for (int i = 0; i < dr.Length; i++)
            {
                if (dt.Select("ParentID='" + Convert.ToInt32(dr[i]["NodeID"]) + "'").Length > 0)
                {
                    result += string.Format(hasChild, dr[i]["NodeID"], dr[i]["NodeName"], pre);
                    result += CreateDP(dt, depth, Convert.ToInt32(dr[i]["NodeID"]));
                }
                else
                {
                    result += string.Format(noChild, dr[i]["NodeID"], dr[i]["NodeName"], pre);
                }
            }
            return result;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                DataTable dt = this.bll.GetNodeListContainXML(0);
                NodeHtml_Li.Text = CreateDP(dt);
            }
        }
        // 根据关键字搜索
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (this.TxtKeyword.Text.Trim() != "" && this.TxtKeyword.Text.Trim() != "关键字")
            {
                string searchurl = "SearchList.aspx";

                string nodeid = Request.Form["ddlnode"];
                string keyword = this.TxtKeyword.Text.Trim();
                searchurl = searchurl + "?node=" + nodeid + "&keyword=" + Server.UrlEncode(keyword);
                Response.Write(" <script>window.parent.window.location.href = '" + searchurl + "' </script>");
            }
        }
        public string GetName()
        {
            return SiteConfig.SiteInfo.SiteName;
        }
    }
}