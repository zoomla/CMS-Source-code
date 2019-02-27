using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
namespace ZoomLaCMS.Manage.I.ASCX
{
    public partial class ShopRecycle : System.Web.UI.UserControl
    {
        protected B_Node nodeBll = new B_Node();
        string hasChild = "<a href='javascript:;' id='a{0}' class='list1'><span class='list_span'>{1}</span><span class='fa fa-chevron-down'></span></a>";
        string noChild = "<a onclick='ShowMain(\"\",\"" + CustomerPageAction.customPath2 + "Shop/ShopRecycler.aspx?NodeID={0}\");'>{1}</a>";
        protected void Page_Load(object sender, EventArgs e)
        {
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
            DataTable dt = nodeBll.GetAllShopNode();
            nodeHtml.Text = "<ul class='tvNav'><li><a  class='list1' id='a0' onclick='ShowMain(\"\",\"" + CustomerPageAction.customPath2 + "Shop/ShopRecycler.aspx\");'><span class='list_span'>全部内容</span><span class='fa fa-list'></span></a>" + GetLI(dt) + "</li></ul>";
        }
    }
}