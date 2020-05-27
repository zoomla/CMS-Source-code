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

namespace ZoomLaCMS.Search
{
    public partial class SearchShop : System.Web.UI.Page
    {
        private B_Node bll = new B_Node();
        protected void Page_Load(object sender, EventArgs e)
        {
            this.TxtKeyword.Attributes.Add("onclick", "setEmpty(this)");
            this.TxtKeyword.Attributes.Add("onblur", "settxt(this)");
            GetShopNode(0);
            this.DDLNode.Items.Clear();
            ListItem item = new ListItem("所有栏目", "0");
            this.DDLNode.Items.Insert(0, item);
        }
        int t = 0;
        /// <summary>
        /// 递归查询节点 for 商城
        /// </summary>
        /// <param name="Pid"></param>
        private void GetShopNode(int Pid)
        {
            DataTable templist = this.bll.GetAllShopNode(Pid);

            for (int i = 0; i < templist.Rows.Count; i++)
            {
                string Spanstr = new string('　', t);
                t = t + 1;
                this.DDLNode.Items.Add(new ListItem(Spanstr + templist.Rows[i]["NodeName"].ToString(), templist.Rows[i]["NodeID"].ToString()));
                GetShopNode(DataConverter.CLng(templist.Rows[i]["NodeID"].ToString())); //递归查询
                t = t - 1;
            }
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            if (this.TxtKeyword.Text.Trim() != "" && this.TxtKeyword.Text.Trim() != "请输入关键字")
            {
                string searchurl = "ShopList.aspx";
                string type = this.DDLtype.SelectedValue;
                string nodeid = this.DDLNode.SelectedValue;
                string keyword = this.TxtKeyword.Text.Trim();
                searchurl = searchurl + "?node=" + nodeid + "&keyword=" + function.HtmlEncode(keyword);
                Response.Write(" <script>window.parent.window.location.href = '" + searchurl + "' </script>");
                //Response.Redirect(searchurl);
            }
        }
    }
}