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


    public partial class Shop : FrontPage
    {
        B_CreateHtml bll = new B_CreateHtml();
        B_Product pll = new B_Product();
        B_Model bmode = new B_Model();
        B_Node bnode = new B_Node();
        public new int ItemID
        {
            get
            {
                return DataConverter.CLng(B_Route.GetParam("ID", Page));
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ItemID < 1) function.WriteErrMsg("产生错误的可能原因：内容信息不存在或未开放!");
            M_Product proMod = pll.GetproductByid(ItemID);
            OrderCommon.ProductCheck(proMod);
            M_ModelInfo modelinfo = bmode.GetModelById(proMod.ModelID);
            proMod.AllClickNum = proMod.AllClickNum + 1;
            pll.updateinfo(proMod);
            string TempNode = bnode.GetModelTemplate(proMod.Nodeid, proMod.ModelID);
            string TempContent = proMod.ModeTemplate;
            string TemplateDir = "/" + modelinfo.ContentModule;
            if (!string.IsNullOrEmpty(TempContent))
            {
                TemplateDir = TempContent;
            }
            else if (!string.IsNullOrEmpty(TempNode))
            {
                TemplateDir = TempNode;
            }
            if (string.IsNullOrEmpty(TemplateDir))
            {
                function.WriteErrMsg("产生错误的可能原因：该商品所属模型未绑定模板!");
            }
            TemplateDir = base.Request.PhysicalApplicationPath + SiteConfig.SiteOption.TemplateDir + "/" + TemplateDir;
            TemplateDir = TemplateDir.Replace("/", @"\");
            if (!FileSystemObject.IsExist(TemplateDir, FsoMethod.File)) { ErrToClient("[产生错误的可能原因：(" + function.PToV(TemplateDir) + ")不存在或未开放!]"); }
            string Templatestr = FileSystemObject.ReadFile(TemplateDir);
            string ContentHtml = this.bll.CreateHtml(Templatestr, 0, ItemID, 0);
            Response.Write(ContentHtml);
        }
    }
}


