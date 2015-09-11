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
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Components;
namespace ZoomLa.WebSite.Manage.AddOn
{

    public partial class PreviewAD : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {            
            int zoneId = DataConverter.CLng(base.Request.QueryString["ZoneID"]);
            if (string.Compare(base.Request.QueryString["Type"], "Zone", true) == 0)
            {
                if (B_Advertisement.GetADList(zoneId).Count == 0)
                {
                    this.ShowJS.InnerHtml = "广告版位中暂无广告信息";
                }
                else
                {
                    M_Adzone adZoneById = B_ADZone.getAdzoneByZoneId(zoneId);

                    if (!adZoneById.IsNull)
                    {
                        this.ShowJS.InnerHtml = "<script  type=\"text/javascript\" src='" + base.ResolveUrl("~/" + VirtualPathUtility.AppendTrailingSlash(SiteConfig.SiteOption.AdvertisementDir) + adZoneById.ZoneJSName) + "?temp=" + DataSecurity.RandomNum() + "'></script>";
                    }
                }
            }
            else
            {
                this.ShowAd();
            }
        }

        private void ShowAd()
        {
            M_Advertisement advertisementById = B_Advertisement.Advertisement_GetAdvertisementByid(DataConverter.CLng(base.Request.QueryString["AdId"]));
            this.ShowJS.InnerHtml = B_Advertisement.GetAdContent(advertisementById);
        }
        
    }
}