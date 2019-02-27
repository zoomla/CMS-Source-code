namespace ZoomLa.WebSite
{
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

    public partial class indexSearch : System.Web.UI.Page
    {
        private B_Node bll = new B_Node();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.DDLNode.DataSource = this.bll.GetNodeListContain(0);
                this.DDLNode.DataTextField = "NodeName";
                this.DDLNode.DataValueField = "NodeID";
                this.DDLNode.DataBind();
                ListItem item = new ListItem("所有栏目", "0");
                this.DDLNode.Items.Insert(0, item);
            }
        }

        //protected void Button1_Click(object sender, EventArgs e)
        //{
        //    if (this.TxtKeyword.Text.Trim() != "" && this.TxtKeyword.Text.Trim() != "关键字")
        //    {
        //        string searchurl = "SearchList.aspx";
        //        string type = this.DDLtype.SelectedValue;
        //        string nodeid = this.DDLNode.SelectedValue;
        //        string keyword = this.TxtKeyword.Text.Trim();
        //        searchurl = searchurl + "?type=" + type + "&node=" + nodeid + "&keyword=" + function.HtmlEncode(keyword);
        //        Response.Write(" <script>window.parent.window.location.href = '" + searchurl + "' </script>");
        //        //Response.Redirect(searchurl);
        //    }
        //}
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            if (this.TxtKeyword.Text.Trim() != "" && this.TxtKeyword.Text.Trim() != "关键字")
            {
                string searchurl = "SearchList.aspx";
                string type = this.DDLtype.SelectedValue;
                string nodeid = this.DDLNode.SelectedValue;
                string keyword = this.TxtKeyword.Text.Trim();
                searchurl = searchurl + "?type=" + type + "&node=" + nodeid + "&keyword=" + function.HtmlEncode(keyword);
                Response.Write(" <script>window.parent.window.location.href = '" + searchurl + "' </script>");
                //Response.Redirect(searchurl);
            }
        }
}
}