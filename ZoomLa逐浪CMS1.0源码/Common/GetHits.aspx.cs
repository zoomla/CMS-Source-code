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
    using ZoomLa.Model;
    using ZoomLa.BLL;
    using ZoomLa.Common;

    public partial class GetHits : System.Web.UI.Page
    {
        B_Content bll = new B_Content();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                string id = base.Request.QueryString["id"];
                int generalId = DataConverter.CLng(id);
                this.ShowHits(generalId);
                
            }
        }
        private void ShowHits(int id)
        {            
            M_CommonData CData = this.bll.GetCommonData(id);
            int hits = CData.Hits + 1;
            this.bll.UpHits(id);
            base.Response.Write("document.write('" + hits + "');");
        }
    }
}