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
    public partial class SpecialList : FrontPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ItemID < 1) { ErrToClient("没有指定专题类别ID，调用方法：/Special_节点ID/List"); }
            string TemplateDir = "";
            if (string.IsNullOrEmpty(TemplateDir))
            {
                Response.Write("该专题类别列表页未指定模板");
            }
            else
            {
                TemplateDir = SiteConfig.SiteOption.TemplateDir + TemplateDir;
                TemplateDir = base.Request.PhysicalApplicationPath + "/" + TemplateDir;
                TemplateDir = TemplateDir.Replace("/", @"\");
                TemplateDir = TemplateDir.Replace(@"\\", @"\");
                string Templatestr = FileSystemObject.ReadFile(TemplateDir);
                string ContentHtml = this.bll.CreateHtml(Templatestr, Cpage, ItemID, "3");
                if (SiteConfig.SiteOption.IsSensitivity == 1)
                {
                    Response.Write(B_Sensitivity.Process(ContentHtml));
                }
                else
                {
                    Response.Write(ContentHtml);
                }
            }
        }
    }
}