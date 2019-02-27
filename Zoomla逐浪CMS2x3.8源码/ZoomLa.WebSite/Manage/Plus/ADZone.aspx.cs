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
using System.IO;
using System.Text;
using ZoomLa.Components;
using System.Text.RegularExpressions;
namespace ZoomLa.WebSite.Manage.Plus
{
    public partial class ADZone : CustomerPageAction
    {
        M_Adzone adzone = new M_Adzone();
        protected void Page_Load(object sender, EventArgs e)
        {
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='ADZoneManage.aspx'>扩展功能</a></li><li><a href='ADZoneManage.aspx'>版位管理</a></li><li class='active'><a href='ADZone.aspx'>版位编辑</a></li>" + Call.GetHelp(28));
            ZoomLa.Common.function.AccessRulo();
            if (!this.Page.IsPostBack)
            {
                B_Admin badmin = new B_Admin();
                if (!B_ARoleAuth.Check(ZoomLa.Model.ZLEnum.Auth.other, "ADManage"))
                {
                    function.WriteErrMsg("没有权限进行此项操作");
                }
                this.DropFixedPosition.Attributes.Add("onchange", "ChangePositonShow(this)");
                this.DropFloatPosition.Attributes.Add("onchange", "ChangePositonShow(this)");
                this.DropMovePosition.Attributes.Add("onchange", "ChangePositonShow(this)");
                this.DropPopPosition.Attributes.Add("onchange", "ChangePositonShow(this)");
                foreach (ListItem lit in this.radlZonetype.Items)
                {
                    lit.Attributes.Add("onclick", "ShowZoenTypePanel()");
                }
                foreach (ListItem li2 in this.RBLDefaultSetting.Items)
                {
                    li2.Attributes.Add("onclick", "ShowZoenTypePanel()");
                }
                this.DropAdZoneSize.Attributes.Add("onchange", "Zone_SelectSize(this)");
                string zoneid = base.Request.QueryString["ZoneId"];
                if (string.IsNullOrEmpty(zoneid))
                {
                    this.TxtZoneJSName.Text = this.GetJSFileName().Trim() ;
                    zoneid = "0";
                    this.HdnZoneId.Value = zoneid;
                    this.Label1.Text = "添加广告版位";
                }
                else
                {
                    this.HdnZoneId.Value = zoneid;
                    this.Label1.Text = "修改广告版位";
                    adzone = B_ADZone.getAdzoneByZoneId(DataConverter.CLng(zoneid));
                    this.TxtZoneName.Text = adzone.ZoneName;
                    this.TxtZoneJSName.Text = adzone.ZoneJSName.Trim();
                    this.TxtZoneIntro.Text = adzone.ZoneIntro;
                    this.TxtZoneHeight.Text = adzone.ZoneHeight.ToString();
                    this.TxtZoneWidth.Text = adzone.ZoneWidth.ToString();
                    this.radlZonetype.SelectedValue = adzone.ZoneType.ToString();
                    if (adzone.Sales == 1)
                    {
                        this.CheckApply.Checked = true;
                    }
                    else { this.CheckApply.Checked = false; }
                    if (!adzone.DefaultSetting)
                    {
                        this.RBLDefaultSetting.SelectedValue = "0";
                    }
                    else
                    {
                        this.RBLDefaultSetting.SelectedValue = "1";
                    }
                    InitShowPanel(adzone.ZoneType);
                    InitSetting(adzone.ZoneSetting, adzone.ZoneType);
                    this.RadlShowType.SelectedValue = adzone.ShowType.ToString();
                    if (adzone.Active)
                        this.ChkActive.Checked = true;
                    else
                        this.ChkActive.Checked = false;
                }
            }
        }

        private string GetJSFileName()
        {
            return (DateTime.Today.Year.ToString() + DateTime.Today.Month.ToString().PadLeft(2, '0') + "/" + B_ADZone.ADZone_MaxID().ToString() + ".js");
        }
        private void InitShowPanel(int selected)
        {
            switch (selected)
            {
                case 0:
                    this.ZoneTypeSetting1.Attributes.Add("style", "display: ");
                    return;

                case 1:
                    this.ZoneTypeSetting2.Attributes.Add("style", "display: ");
                    return;

                case 2:
                    this.ZoneTypeSetting3.Attributes.Add("style", "display: ");
                    return;

                case 3:
                    this.ZoneTypeSetting4.Attributes.Add("style", "display: ");
                    return;

                case 4:
                    this.ZoneTypeSetting5.Attributes.Add("style", "display: ");
                    return;

                case 5:
                    this.ZoneTypeSetting6.Attributes.Add("style", "display: ");
                    return;

                case 6:
                    this.ZoneTypeSetting7.Attributes.Add("style", "display: ");
                    return;
            }
        }
        private string[] SplitZetting(string setting)
        {
            Regex regex = new Regex(",");
            return regex.Split(setting);
        }
        private void InitSetting(string setting, int showType)
        {
            string[] strArray = this.SplitZetting(setting + ",,,,,");
            switch (showType)
            {
                case 0:
                case 5:
                    break;

                case 1:
                    this.DropPopType.SelectedValue = strArray[0];
                    this.TxtPopLeft.Text = strArray[1];
                    this.TxtPopTop.Text = strArray[2];
                    this.TxtPopCookieHour.Text = strArray[3];
                    this.DropPopPosition.SelectedValue = strArray[4];
                    return;

                case 2:
                    this.TxtMoveLeft.Text = strArray[0];
                    this.TxtMoveTop.Text = strArray[1];
                    this.TxtMoveDelay.Text = strArray[2];
                    this.RadlMoveShowCloseAD.SelectedValue = strArray[3];
                    this.TxtMoveCloseFontColor.Text = strArray[4];
                    this.DropMovePosition.SelectedValue = strArray[5];
                    return;

                case 3:
                    this.TxtFixedLeft.Text = strArray[0];
                    this.TxtFixedTop.Text = strArray[1];
                    this.RadlFixedShowCloseAD.SelectedValue = strArray[2];
                    this.TxtFixedCloseFontColor.Text = strArray[3];
                    this.DropFixedPosition.SelectedValue = strArray[4];
                    return;

                case 4:
                    this.DropFloatType.SelectedValue = strArray[0];
                    this.TxtFloatLeft.Text = strArray[1];
                    this.TxtFloatTop.Text = strArray[2];
                    this.RadlFloatShowCloseAD.SelectedValue = strArray[3];
                    this.TxtFloatCloseFontColor.Text = strArray[4];
                    this.DropFloatPosition.SelectedValue = strArray[5];
                    return;

                case 6:
                    this.TxtCoupletLeft.Text = strArray[0];
                    this.TxtCoupletTop.Text = strArray[1];
                    this.TxtCoupletDelay.Text = strArray[2];
                    this.RadlCoupletShowCloseAD.SelectedValue = strArray[3];
                    this.TxtCoupletCloseFontColor.Text = strArray[4];
                    break;

                default:
                    return;
            }
        }
        private string GetZoneSetting(bool isDefaultSetting)
        {
            if (isDefaultSetting)
            {
                switch (this.radlZonetype.SelectedValue)
                {
                    case "1":
                        return "1,100,100,0,1";

                    case "2":
                        return "15,200,0.15,false,#FFFFFF,1";

                    case "3":
                        return "100,100,false,#FFFFFF,1";

                    case "4":
                        return "1,100,100,false,#FFFFFF,1";

                    case "5":
                        return "15,200,0.15,false,#FFFFFF";
                }
                return "";
            }
            switch (this.radlZonetype.SelectedValue)
            {
                case "1":
                    return (this.DropPopType.SelectedValue + "," + this.TxtPopLeft.Text.Trim() + "," + this.TxtPopTop.Text.Trim() + "," + this.TxtPopCookieHour.Text.Trim() + "," + this.DropPopPosition.SelectedValue);

                case "2":
                    return (this.TxtMoveLeft.Text.Trim() + "," + this.TxtMoveTop.Text.Trim() + "," + this.TxtMoveDelay.Text.Trim() + "," + this.RadlMoveShowCloseAD.SelectedValue + "," + this.TxtMoveCloseFontColor.Text.Trim() + "," + this.DropMovePosition.SelectedValue);

                case "3":
                    return (this.TxtFixedLeft.Text.Trim() + "," + this.TxtFixedTop.Text.Trim() + "," + this.RadlFixedShowCloseAD.SelectedValue + "," + this.TxtFixedCloseFontColor.Text.Trim() + "," + this.DropFixedPosition.SelectedValue);

                case "4":
                    return (this.DropFloatType.SelectedValue + "," + this.TxtFloatLeft.Text.Trim() + "," + this.TxtFloatTop.Text.Trim() + "," + this.RadlFloatShowCloseAD.SelectedValue + "," + this.TxtFloatCloseFontColor.Text.Trim() + "," + this.DropFloatPosition.SelectedValue);

                case "5":
                    return (this.TxtCoupletLeft.Text.Trim() + "," + this.TxtCoupletTop.Text.Trim() + "," + this.TxtCoupletDelay.Text.Trim() + "," + this.RadlCoupletShowCloseAD.SelectedValue + "," + this.TxtCoupletCloseFontColor.Text.Trim());
                case "6":
                    return (this.TxtCoupletLeft.Text.Trim() + "," + this.TxtCoupletTop.Text.Trim() + "," + this.TxtCoupletDelay.Text.Trim() + "," + this.RadlCoupletShowCloseAD.SelectedValue + "," + this.TxtCoupletCloseFontColor.Text.Trim());
            }
            return "";
        }        
        protected void EBtnAdZone_Click(object sender, EventArgs e)
        {
            if(this.Page.IsValid)
            {
                adzone.ZoneName = this.TxtZoneName.Text;
                adzone.ZoneType = DataConverter.CLng(this.radlZonetype.SelectedValue);
                adzone.ShowType = DataConverter.CLng(this.RadlShowType.SelectedValue);
                adzone.DefaultSetting = DataConverter.CBool(this.RBLDefaultSetting.SelectedValue);
                adzone.ZoneSetting = this.GetZoneSetting(DataConverter.CBool(this.RBLDefaultSetting.SelectedValue));
                
                adzone.Active = this.ChkActive.Checked;
                adzone.UpdateTime = DateTime.Now;
                adzone.ZoneHeight = DataConverter.CLng(this.TxtZoneHeight.Text.Trim());
                adzone.ZoneWidth = DataConverter.CLng(this.TxtZoneWidth.Text.Trim());
                adzone.UpdateTime = DateTime.Now;
                adzone.ZoneJSName = this.TxtZoneJSName.Text;
                adzone.ZoneIntro = this.TxtZoneIntro.Text;
                adzone.ZoneID = DataConverter.CLng(this.HdnZoneId.Value);
                if (this.CheckApply.Checked == true)
                {
                    adzone.Sales = 1;
                }
                else
                {
                    adzone.Sales = 0;
                }
                if (adzone.ZoneID>0)
                {
                    if (B_ADZone.ADZone_Update(adzone))
                    {
                        function.WriteSuccessMsg("修改成功", "ADZoneManage.aspx");
                    }
                }
                else
                {
                    adzone.ZoneID = B_ADZone.ADZone_MaxID();                    
                    if (B_ADZone.ADZone_Add(adzone))
                    {
                        function.WriteSuccessMsg("添加成功", "ADZoneManage.aspx");
                    }
                }
            }
        }

    }
}