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
    using ZoomLa.Common;
    using ZoomLa.BLL;
    using ZoomLa.Model;
    using ZoomLa.Components;

    public partial class NodePage : System.Web.UI.Page
    {
        protected B_CreateHtml bll = new B_CreateHtml();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(base.Request.QueryString["NodeID"]))
            {
                int ItemID = DataConverter.CLng(base.Request.QueryString["NodeID"]);
                B_Node bnode = new B_Node();
                M_Node nodeinfo = bnode.GetNode(ItemID);
                string TemplateDir = "";                
                TemplateDir = nodeinfo.ListTemplateFile;                

                if (string.IsNullOrEmpty(TemplateDir))
                {
                    Response.Write("该节点列表页未指定模板");
                }
                else
                {
                    TemplateDir = SiteConfig.SiteOption.TemplateDir + TemplateDir;
                    int Cpage = 1;
                    if (string.IsNullOrEmpty(base.Request.QueryString["page"]))
                    {
                        Cpage = 1;
                    }
                    else
                    {
                        Cpage = DataConverter.CLng(base.Request.QueryString["page"]);
                    }
                    TemplateDir = base.Request.PhysicalApplicationPath + TemplateDir;
                    TemplateDir = TemplateDir.Replace("/", @"\");
                    string ContentHtml = this.bll.CreateHtml(FileSystemObject.ReadFile(TemplateDir), Cpage, ItemID);
                    Response.Write(ContentHtml);
                }
            }
            else
            {
                Response.Write("没有指定栏目ID");
            }
        }
    }
}