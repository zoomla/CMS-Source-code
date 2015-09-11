namespace ZoomLa.WebSite.Manage.AddOn
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
    using ZoomLa.Components;
    using ZoomLa.Common;
    using ZoomLa.BLL;

    public partial class ADCount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strB = base.Request.QueryString["Action"];
            M_Advertisement advertisementById = B_Advertisement.Advertisement_GetAdvertisementByid(DataConverter.CLng(base.Request.QueryString["AdId"]));
            if (!advertisementById.IsNull)
            {
                if (advertisementById.CountView)
                {
                    advertisementById.Views++;
                }
                if (advertisementById.CountClick)
                {
                    advertisementById.Clicks++;
                }
                B_Advertisement.Advertisement_Update(advertisementById);
                if ((string.Compare("Click", strB, true) == 0) && !string.IsNullOrEmpty(advertisementById.LinkUrl))
                {
                    Response.Write("<script language='javascript' type='text/javascript'>window.location='" + advertisementById.LinkUrl + "';</script>");
                }
            }
        }        
    }
}