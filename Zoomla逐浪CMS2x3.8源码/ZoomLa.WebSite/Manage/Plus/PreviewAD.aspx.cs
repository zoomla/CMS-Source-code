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

public partial class manage_Plus_PreviewAD : CustomerPageAction
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
                string adpath = SiteConfig.SiteOption.AdvertisementDir +"/"+ adZoneById.ZoneJSName.Replace(" ","")+ "?temp=" + function.GetRandomString(6);
                if (!adZoneById.IsNull)
                {
                    this.ShowJS.InnerHtml = "<script type=\"text/javascript\" src='" + adpath + "'></script>";
                }
            }
        }
        else
        {
            this.ShowAd();
        }
        Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='ADManage.aspx'>广告管理</a></li><li class='active'>预览版位JS效果</li>");
    }

    private void ShowAd()
    {
        M_Advertisement advertisementById = B_Advertisement.Advertisement_GetAdvertisementByid(DataConverter.CLng(base.Request.QueryString["AdId"]));
        this.ShowJS.InnerHtml = B_Advertisement.GetAdContent(advertisementById);
    }
}
