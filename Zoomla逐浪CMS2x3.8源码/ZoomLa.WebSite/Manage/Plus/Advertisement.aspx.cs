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
namespace ZoomLa.WebSite.Manage.AddOn
{
    public partial class Advertisement : CustomerPageAction
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ZoomLa.Common.function.AccessRulo();
            string action = "添加广告内容";
            if (!IsPostBack)
            {
                B_Admin badmin = new B_Admin();
                if (!B_ARoleAuth.Check(ZoomLa.Model.ZLEnum.Auth.other, "ADManage"))
                {
                    function.WriteErrMsg("没有权限进行此项操作");
                }
                listbind();
                txtOverdueDate.Text = DateTime.Now.AddYears(1).ToString("yyyy-MM-dd");
                //this.InitJSScript();
                string id = base.Request.QueryString["ADId"];
                if (string.IsNullOrEmpty(id))
                {
                    id = "0";
                    this.HdnID.Value = id;
                    action = "添加广告内容";
                    this.Label1.Text = "添加广告内容";
                    string ZoneIDs = base.Request.QueryString["ZoneId"];
                    if (!string.IsNullOrEmpty(ZoneIDs))
                    {
                        this.SetLstZoneNameSelected(ZoneIDs);
                    }
                }
                else
                {
                    this.HdnID.Value = id;
                    this.Label1.Text = "修改广告";
                    M_Advertisement adv = B_Advertisement.Advertisement_GetAdvertisementByid(DataConverter.CLng(id));
                    this.TxtADName.Text = adv.ADName;
                    this.TxtPriority.Text = adv.Priority.ToString();
                    this.txtOverdueDate.Text = adv.OverdueDate.ToString("yyyy-MM-dd");
                    this.InitRadlAdType(adv.ADType);
                    this.InitShowPanel(adv);
                    this.SetLstZoneNameSelected(B_Advertisement.GetZoneIDByAd(adv.ADID));
                    this.ChkCountClick.Checked = adv.CountClick;
                    this.ChkCountView.Checked = adv.CountView;
                    this.TxtClicks.Text = adv.Clicks.ToString();
                    this.TxtViews.Text = adv.Views.ToString();
                    ChkPasses.Checked = adv.Passed;
                    if (adv.ADType > 0)
                    {
                        this.ChkCountClick.Enabled = false;
                        this.TxtClicks.Enabled = false;
                    }
                    this.Price.Text = Math.Round(adv.Price, 2).ToString();
                    ADBuy.Checked = adv.ADBuy == 1 ? true : false;
                    action = "<a href='Advertisement.aspx?ADId=" + id + "'>修改广告</a>[" + adv.ADName + "]";
                }
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='ADManage.aspx'>广告管理</a></li><li class='active'>"+action+"</li>" + Call.GetHelp(29));
        }
        private void listbind()
        {
            this.LstZoneName.DataSource = B_ADZone.ADZone_GetAll();
            this.LstZoneName.DataTextField = "ZoneName";
            this.LstZoneName.DataValueField = "ZoneID";
            this.LstZoneName.DataBind();
        }
        private void InitRadlAdType(int seleced)
        {
            foreach (ListItem lit in this.RadlADType.Items)
            {
                int i = DataConverter.CLng(lit.Value);
                if (i == seleced)
                    lit.Selected = true;
            }
        }
        private void InitShowPanel(M_Advertisement adv)
        {
            switch (adv.ADType)
            {
                case 1:
                    this.ADContent1.Attributes.Add("style", "display: ");
                    this.Table1.Attributes.Add("style", "display: ");
                    this.txtpic.Text = adv.ImgUrl;
                    this.TxtImgWidth.Text = adv.ImgWidth.ToString();
                    this.TxtImgHeight.Text = adv.ImgHeight.ToString();
                    this.TxtLinkUrl.Text = adv.LinkUrl;
                    this.TxtLinkAlt.Text = adv.LinkAlt;
                    this.RadlLinkTarget.SelectedValue = adv.LinkTarget.ToString();
                    this.TxtADIntro.Text = adv.ADIntro;
                    this.txtpic1.Text = adv.ImgUrl1;
                    this.TxtImgHeight1.Text = adv.Views.ToString();
                    this.TxtImgWidth1.Text = adv.Views.ToString();
                    this.TxtLinkUrl1.Text = adv.LinkUrl1.ToString();
                    this.RadlLinkTarget1.SelectedValue = adv.Views.ToString();
                    this.TxtLinkAlt1.Text = adv.LinkAlt1.ToString();
                    this.TxtADIntro1.Text = adv.ADIntro1.ToString();
                    return;
                case 2:
                    this.ADContent1.Attributes.Add("style", "display: none ");
                    this.ADContent2.Attributes.Add("style", "display: ");
                    this.Table1.Attributes.Add("style", "display: none ");
                    this.Table2.Attributes.Add("style", "display: ");
                    this.txtFlashPath.Text = adv.ImgUrl;
                    this.TxtFlashWidth.Text = adv.ImgWidth.ToString();
                    this.TxtFlashHeight.Text = adv.ImgHeight.ToString();
                    this.RadlFlashMode.SelectedValue = adv.FlashWmode.ToString();
                    this.txtFlashPath1.Text = adv.ImgUrl1;
                    this.TxtFlashWidth1.Text = adv.ImgWidth1.ToString();
                    this.TxtFlashHeight1.Text = adv.ImgHeight1.ToString();
                    this.RadlFlashMode1.SelectedValue = adv.Views.ToString();
                    return;
                case 3:
                    this.ADContent1.Attributes.Add("style", "display: none ");
                    this.ADContent3.Attributes.Add("style", "display: ");
                    this.Table1.Attributes.Add("style", "display: none ");
                    this.Table3.Attributes.Add("style", "display: ");
                    this.TxtADText.Text = adv.ADIntro;
                    this.TxtADText1.Text = adv.ADIntro1;
                    return;
                case 4:
                    this.ADContent1.Attributes.Add("style", "display: none ");
                    this.ADContent4.Attributes.Add("style", "display: ");
                    this.Table1.Attributes.Add("style", "display: none ");
                    this.Table4.Attributes.Add("style", "display: ");
                    this.TxtADCode.Text = adv.ADIntro;
                    this.TxtADCode1.Text = adv.ADIntro1;
                    return;
                case 5:
                    this.ADContent1.Attributes.Add("style", "display: none ");
                    this.ADContent5.Attributes.Add("style", "display: ");
                    this.Table1.Attributes.Add("style", "display: none ");
                    this.Table5.Attributes.Add("style", "display: ");
                    this.TxtWebFileUrl.Text = adv.ADIntro;
                    this.TxtWebFileUrl1.Text = adv.ADIntro1;
                    return;
            }
            this.ADContent1.Attributes.Add("style", "display: ");
            this.Table1.Attributes.Add("style", "display: ");
        }

        private void InitJSScript()
        {
            // this.txtOverdueDate.Attributes.Add("onclick", "setday(this)");
            foreach (ListItem lit in this.RadlADType.Items)
            {
                //lit.Attributes.Add("onclick", "ADTypeChecked(" + lit.Value + ")");
            }
        }
        private void SetLstZoneNameSelected(string Ids)
        {
            string[] strArray = Ids.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < strArray.Length; i++)
            {
                for (int j = 0; j < this.LstZoneName.Items.Count; j++)
                {
                    if (strArray[i] == this.LstZoneName.Items[j].Value)
                    {
                        this.LstZoneName.Items[j].Selected = true;
                    }
                }
            }
        }
        private string GetAdZoneIdList()
        {
            //StringBuilder builder = new StringBuilder();
            //int count = this.LstZoneName.Items.Count;
            //for (int i = 0; i < count; i++)
            //{
            //    if (this.LstZoneName.Items[i].Selected)
            //    {
            //        builder.Append(this.LstZoneName.Items[i].Value);
            //        builder.Append(",");
            //    }
            //}
            //return builder.ToString();
            return LstZoneName.SelectedValue;
        }
        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            string adZoneIdList = LstZoneName.SelectedValue;
            if (this.Page.IsValid)
            {
                int adid = DataConverter.CLng(this.HdnID.Value.Trim());
                M_Advertisement adv = new M_Advertisement();
                adv.UserID = 0;
                adv.ADName = Server.HtmlEncode(this.TxtADName.Text.Trim());
                adv.ADType = DataConverter.CLng(this.RadlADType.SelectedValue);
                adv.Priority = DataConverter.CLng(this.TxtPriority.Text.Trim());
                adv.Passed = ChkPasses.Checked;
                adv.CountView = this.ChkCountView.Checked;
                adv.Views = DataConverter.CLng(this.TxtViews.Text.Trim());
                adv.CountClick = this.ChkCountClick.Checked;
                adv.Clicks = DataConverter.CLng(this.TxtClicks.Text.Trim());
                //adv.ZoneID = adZoneIdList;
                adv.ADID = adid;
                adv.OverdueDate = DataConverter.CDate(this.txtOverdueDate.Text);
                adv.Setting = "";
                adv.Price = DataConverter.CDecimal(this.Price.Text);
                adv.ADBuy = this.ADBuy.Checked ? 1 : 0;
                switch (adv.ADType)
                {
                    case 1:
                        {
                            adv.ImgUrl = this.txtpic.Text.Trim();
                            if (string.IsNullOrEmpty(adv.ImgUrl))
                            {
                                function.WriteErrMsg("图片广告的图片地址不能为空！");
                            }
                            adv.ImgHeight = DataConverter.CLng(this.TxtImgHeight.Text.Trim());
                            adv.ImgWidth = DataConverter.CLng(this.TxtImgWidth.Text.Trim());
                            string str2 = (this.TxtLinkUrl.Text.Trim() == "http://") ? "" : this.TxtLinkUrl.Text.Trim();
                            adv.LinkUrl = str2;
                            adv.LinkTarget = DataConverter.CLng(this.RadlLinkTarget.SelectedValue);
                            adv.LinkAlt = this.TxtLinkAlt.Text.Trim();
                            adv.ADIntro = this.TxtADIntro.Text.Trim();
                            adv.ImgUrl1 = this.txtpic1.Text.Trim();
                            adv.ImgHeight1 = DataConverter.CLng(this.TxtImgHeight1.Text.Trim());
                            adv.ImgWidth1 = DataConverter.CLng(this.TxtImgWidth1.Text.Trim());
                            string str3 = (this.TxtLinkUrl1.Text.Trim() == "http://") ? "" : this.TxtLinkUrl1.Text.Trim();
                            adv.LinkUrl1 = str3;
                            adv.LinkTarget1 = DataConverter.CLng(this.RadlLinkTarget1.SelectedValue);
                            adv.LinkAlt1 = this.TxtLinkAlt1.Text.Trim();
                            adv.ADIntro1 = this.TxtADIntro1.Text.Trim();
                            break;
                        }
                    case 2:
                        adv.ImgUrl = this.txtFlashPath.Text.Trim();
                        if (string.IsNullOrEmpty(adv.ImgUrl))
                        {
                            function.WriteErrMsg("动画广告的Flash地址不能为空");
                        }
                        adv.ImgHeight = DataConverter.CLng(this.TxtFlashHeight.Text.Trim());
                        adv.ImgWidth = DataConverter.CLng(this.TxtFlashWidth.Text.Trim());
                        adv.FlashWmode = DataConverter.CLng(this.RadlFlashMode.SelectedValue);
                        adv.ImgUrl1 = this.txtFlashPath1.Text.Trim();
                        adv.ImgHeight1 = DataConverter.CLng(this.TxtFlashHeight1.Text.Trim());
                        adv.ImgWidth1 = DataConverter.CLng(this.TxtFlashWidth1.Text.Trim());
                        adv.FlashWmode1 = DataConverter.CLng(this.RadlFlashMode1.SelectedValue);
                        break;
                    case 3:
                        adv.ADIntro = this.TxtADText.Text.Trim();
                        adv.ADIntro1 = this.TxtADText1.Text.Trim();
                        break;
                    case 4:
                        adv.ADIntro = this.TxtADCode.Text.Trim();
                        adv.ADIntro1 = this.TxtADCode1.Text.Trim();
                        break;
                    case 5:
                        adv.ADIntro = this.TxtWebFileUrl.Text.Trim();
                        adv.ADIntro1 = this.TxtWebFileUrl1.Text.Trim();
                        break;
                }
                bool flag = false;
                if (adv.ADID > 0)
                {
                    flag = B_Advertisement.Advertisement_Update(adv);
                }
                else
                {
                    adv.ADID = B_Advertisement.MaxID();
                    flag = B_Advertisement.Advertisement_Add(adv);
                    adv.ADID = B_Advertisement.MaxID() - 1;
                }
                if (flag)
                {
                    int zid = 0;
                    B_ADZone.Delete_ADZone_Ad(adv.ADID.ToString());
                    if (!string.IsNullOrEmpty(adZoneIdList))
                    {
                        if (adZoneIdList.IndexOf(',') > -1)
                        {
                            string[] arr = adZoneIdList.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                            for (int s = 0; s < arr.Length; s++)
                            {
                                zid = DataConverter.CLng(arr[s]);
                                if (!B_Advertisement.IsExistZoneAdv(zid, adv.ADID))
                                    B_Advertisement.Add_Zone_Advertisement(zid, adv.ADID);
                            }
                            B_ADZone.CreateJS(adZoneIdList);
                        }
                        else
                        {
                            zid = DataConverter.CLng(adZoneIdList);
                            bool isadd = false;
                            if (!B_Advertisement.IsExistZoneAdv(zid, adv.ADID))
                                isadd = B_Advertisement.Add_Zone_Advertisement(zid, adv.ADID);
                            B_ADZone.CreateJS(adZoneIdList);
                        }
                    }
                    function.WriteSuccessMsg("广告信息保存成功!", "ADManage.aspx");
                }
                else
                {
                    function.WriteErrMsg("保存失败!");
                }
            }
        }
    }

}