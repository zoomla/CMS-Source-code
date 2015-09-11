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
    
    using ZoomLa.Model;
    using ZoomLa.Components;

    public partial class SearchList : System.Web.UI.Page
    {
        private B_Content bll = new B_Content();
        private B_Node bnode = new B_Node();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                DataSecurity.StartProcessRequest();
                string type = "";
                if(!string.IsNullOrEmpty(base.Request.QueryString["type"]))
                    type = base.Request.QueryString["type"];
                else
                    type="1";
                string node = "0";
                string keyword = "";
                int Cpage=0;
                if (!string.IsNullOrEmpty(base.Request.QueryString["node"]))
                    node = base.Request.QueryString["node"];
                if (string.IsNullOrEmpty(base.Request.QueryString["keyword"]))
                {
                    function.WriteErrMsg("没有输入查询关键字");
                }
                else
                {
                    keyword = base.Request.QueryString["keyword"];
                }

                this.DDLNode.DataSource = this.bnode.GetNodeListContain(0);
                this.DDLNode.DataTextField = "NodeName";
                this.DDLNode.DataValueField = "NodeID";
                this.DDLNode.DataBind();
                ListItem item = new ListItem("所有栏目", "0");
                this.DDLNode.Items.Insert(0, item);

                if (!string.IsNullOrEmpty(base.Request.QueryString["p"]))
                    Cpage = DataConverter.CLng(base.Request.QueryString["p"]);
                else
                    Cpage = 1;
                int PageSize = 20;
                string filter = "1=1";
                if (type == "1")
                {
                    filter = filter + " and Title like '%" + keyword + "%'";
                }
                if (type == "2")
                {
                    filter = filter + " and Inputer like '%" + keyword + "%'";
                }
                if (node != "0")
                {
                    filter = filter + " and NodeID=" + node;
                }
                int Total = this.bll.CountSearch(filter);
                this.Repeater1.DataSource = this.bll.ContentSearch(filter, PageSize, Cpage);
                this.Repeater1.DataBind();
                this.Pager1.InnerHtml = function.ShowPage(Total, PageSize, Cpage, true, "项");
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (this.TxtKeyword.Text.Trim() != "" && this.TxtKeyword.Text.Trim() != "关键字")
            {
                string searchurl = "SearchList.aspx";
                string type = this.DDLtype.SelectedValue;
                string nodeid = this.DDLNode.SelectedValue;
                string keyword = this.TxtKeyword.Text.Trim();
                searchurl = searchurl + "?type=" + type + "&node=" + nodeid + "&keyword=" + function.HtmlEncode(keyword);
                Response.Redirect(searchurl);
            }
        }
        public string GetUrl(string itemid)
        {
            M_CommonData cinfo = this.bll.GetCommonData(DataConverter.CLng(itemid));
            if (cinfo.IsCreate == 1)
                return SiteConfig.SiteInfo.SiteUrl + cinfo.HtmlLink;
            else
                return "/Content.aspx?ItemID=" + itemid;
        }
}
}