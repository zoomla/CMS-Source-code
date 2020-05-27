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
using ZoomLa.Sns;


public partial class Store_ShopClass : System.Web.UI.Page
{
    protected B_CreateShopHtml shll = new B_CreateShopHtml();
    protected B_CreateHtml bll = new B_CreateHtml();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(base.Request.QueryString["NodeID"]))
        {
            int ItemID = DataConverter.CLng(base.Request.QueryString["NodeID"]);
            B_Node bnode = new B_Node();
            M_Node nodeinfo = bnode.GetNodeXML(ItemID);
            if (nodeinfo.IsNull)
                function.WriteErrMsg("产生错误的可能原因：您访问的内容信息不存在");
            string TemplateDir = "";
            if (string.IsNullOrEmpty(nodeinfo.IndexTemplate))
                TemplateDir = nodeinfo.ListTemplateFile;
            else
                TemplateDir = nodeinfo.IndexTemplate;

            if (string.IsNullOrEmpty(TemplateDir))
            {
                function.WriteErrMsg("产生错误的可能原因：节点不存在或未绑定模型模板");
            }
            else
            {
                TemplateDir = SiteConfig.SiteOption.TemplateDir + "/" + TemplateDir;
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

                string ContentHtml = FileSystemObject.ReadFile(TemplateDir);
                //ContentHtml = this.shll.CreateShopHtml(ContentHtml, 0, 0);
                ContentHtml = this.bll.CreateHtml(ContentHtml, Cpage, ItemID, "1");

                
                if (ContentHtml.IndexOf("$Zone_") > -1)
                {
                    ContentHtml = ZoneFun.MessageReplace(ContentHtml, 0, Guid.Empty, Guid.Empty);
                }
                Response.Write(ContentHtml);
            }
        }
        else
        {
            Response.Write("[产生错误的可能原因：没有指定栏目ID]");
        }
    }
}
