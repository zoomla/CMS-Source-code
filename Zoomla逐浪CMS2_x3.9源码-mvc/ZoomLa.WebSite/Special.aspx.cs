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
    public partial class Special : System.Web.UI.Page
    {
        protected B_CreateHtml bll = new B_CreateHtml();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(base.Request.QueryString["SpecID"]))
            {
                int ItemID = DataConverter.CLng(base.Request.QueryString["SpecID"]);
                B_Spec bll = new B_Spec();
                M_Spec info = bll.GetSpec(ItemID);
                string TemplateDir = "";
                TemplateDir = "/" + info.ListTemplate;

                if (string.IsNullOrEmpty(TemplateDir))
                {
                    Response.Write("该专题列表页未指定模板");
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

                    //SetCatch("Special" + StringHelper.MD5(TemplateDir), TemplateDir);
                    //string Templatestr = GetCatch("Special" + StringHelper.MD5(TemplateDir)).ToString();

                    string Templatestr = FileSystemObject.ReadFile(TemplateDir);
                    string ContentHtml = this.bll.CreateHtml(Templatestr, Cpage, ItemID, "2");
                    Response.Write(ContentHtml);
                }
            }
            else
            {
                //Response.Write("没有指定专题ID,调用方法:Special.aspx?SpecID=专题ID");
                Response.Write("没有指定专题ID,调用方法:Special.aspx?SpecID=[节点ID]");
            }
        }
    }
}