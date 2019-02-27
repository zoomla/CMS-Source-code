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

    public partial class Content : System.Web.UI.Page
    {
        protected B_CreateHtml bll = new B_CreateHtml();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(base.Request.QueryString["ItemID"]))
            {
                int ItemID = DataConverter.CLng(base.Request.QueryString["ItemID"]);
                B_Content bcontent = new B_Content();
                B_Model bmode = new B_Model();
                B_Node bnode = new B_Node();
                M_CommonData ItemInfo = bcontent.GetCommonData(ItemID);
                if(ItemInfo.IsNull)
                    Response.Write("[产生错误的可能原因：您访问的内容信息不存在！]");
                M_ModelInfo modelinfo = bmode.GetModelById(ItemInfo.ModelID);
                string TempNode = bnode.GetModelTemplate(ItemInfo.NodeID, ItemInfo.ModelID);
                string TempContent = ItemInfo.Template;
                string TemplateDir = modelinfo.ContentModule;
                if (!string.IsNullOrEmpty(TempContent))
                    TemplateDir = TempContent;
                else 
                {
                    if (!string.IsNullOrEmpty(TempNode))
                        TemplateDir = TempNode;
                }
                if (string.IsNullOrEmpty(TemplateDir))
                {
                    Response.Write("[产生错误的可能原因：该内容所属模型未指定模板！]");
                }
                else
                {
                    TemplateDir = base.Request.PhysicalApplicationPath + SiteConfig.SiteOption.TemplateDir + TemplateDir;
                    TemplateDir = TemplateDir.Replace("/", @"\");
                    string ContentHtml = this.bll.CreateHtml(FileSystemObject.ReadFile(TemplateDir), 0, ItemID);
                    Response.Write(ContentHtml);
                }
            }
            else
            {
                Response.Write("[产生错误的可能原因：您访问的内容信息不存在！]");
            }
        }
    }
}