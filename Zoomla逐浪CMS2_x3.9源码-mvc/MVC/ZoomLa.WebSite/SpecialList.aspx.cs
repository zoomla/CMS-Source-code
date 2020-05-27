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
    public partial class SpecialList : System.Web.UI.Page
    {
        protected B_CreateHtml bll = new B_CreateHtml();
        protected B_Sensitivity sll = new B_Sensitivity();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(base.Request.QueryString["SCateID"]))
            {
                int ItemID = DataConverter.CLng(base.Request.QueryString["SCateID"]);
                string TemplateDir = "";

                if (string.IsNullOrEmpty(TemplateDir))
                {
                    Response.Write("该专题类别列表页未指定模板");
                }
                else
                {
                    TemplateDir = SiteConfig.SiteOption.TemplateDir + TemplateDir;
                    int Cpage = 1;
                    if (string.IsNullOrEmpty(base.Request.QueryString["p"]))
                    {
                        Cpage = 1;
                    }
                    else
                    {
                        Cpage = DataConverter.CLng(base.Request.QueryString["p"]);
                    }

                    TemplateDir = base.Request.PhysicalApplicationPath + "/" + TemplateDir;

                    TemplateDir = TemplateDir.Replace("/", @"\");
                    TemplateDir = TemplateDir.Replace(@"\\", @"\");

                    string Templatestr = FileSystemObject.ReadFile(TemplateDir);

                    //SetCatch("SpecialList" + StringHelper.MD5(TemplateDir), TemplateDir);
                    //string Templatestr = GetCatch("SpecialList" + StringHelper.MD5(TemplateDir)).ToString();

                    string ContentHtml = this.bll.CreateHtml(Templatestr, Cpage, ItemID, "3");
                    if (SiteConfig.SiteOption.IsSensitivity == 1)
                    {
                        Response.Write(sll.ProcessSen(ContentHtml));
                    }
                    else
                    {
                        Response.Write(ContentHtml);
                    }
                }
            }
            else
            {
                //Response.Write("没有指定专题类别ID,调用方法:SpecialList.aspx?SCateID=专题类别ID");
                Response.Write("没有指定专题类别ID，调用方法：SpecialList.aspx?SCateID=[节点ID]");
            }
        }
    }
}