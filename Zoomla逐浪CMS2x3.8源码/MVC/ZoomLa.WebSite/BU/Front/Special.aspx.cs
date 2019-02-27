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

namespace ZoomLaCMS
{
    public partial class Special : FrontPage
    {
        B_Spec spBll = new B_Spec();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ItemID < 1) { ErrToClient("没有指定专题ID,调用方法:/Special_节点ID/Default.aspx"); }
            M_Spec info = spBll.GetSpec(ItemID);
            string TemplateDir = "";
            TemplateDir = "/" + info.ListTemplate;
            if (string.IsNullOrEmpty(TemplateDir)){ErrToClient("该专题列表页未指定模板");}
            TemplateDir = SiteConfig.SiteOption.TemplateDir + TemplateDir;
            TemplateDir = base.Request.PhysicalApplicationPath + "/" + TemplateDir;
            TemplateDir = TemplateDir.Replace("/", @"\");
            string Templatestr = FileSystemObject.ReadFile(TemplateDir);
            string ContentHtml = this.bll.CreateHtml(Templatestr, Cpage, ItemID, "2");
            Response.Write(ContentHtml);
        }
    }
}