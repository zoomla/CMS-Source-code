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

    public partial class Advertisement : System.Web.UI.Page
    {  
        private string m_FileExtArr;
        private string m_ShowPath;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                B_Admin badmin = new B_Admin();
                badmin.CheckMulitLogin();
                if (!badmin.ChkPermissions("ADManage"))
                {
                    function.WriteErrMsg("没有权限进行此项操作");
                }
                listbind();
                this.InitJSScript();
                string id = base.Request.QueryString["ADId"];
                if (string.IsNullOrEmpty(id))
                {
                    id = "0";
                    this.HdnID.Value = id;
                    this.Label1.Text = "添加广告";
                    string ZoneIDs = base.Request.QueryString["ZoneId"];
                    if (!string.IsNullOrEmpty(ZoneIDs))
                    {
                        this.SetLstZoneNameSelected(ZoneIDs);
                    }
                    this.txtOverdueDate.Text = DateTime.Now.AddDays(30).ToString("yyyy-MM-dd");
                }
                else
                {
                    this.HdnID.Value = id;
                    this.Label1.Text = "修改广告";
                    M_Advertisement adv = B_Advertisement.Advertisement_GetAdvertisementByid(DataConverter.CLng(id));
                    this.TxtADName.Text = adv.AdName;
                    this.TxtPriority.Text = adv.Priority.ToString();
                    this.txtOverdueDate.Text = adv.OverdueDate.ToString("yyyy-MM-dd");
                    this.InitRadlAdType(adv.AdType);
                    this.InitShowPanel(adv);                    
                    this.SetLstZoneNameSelected(B_Advertisement.GetZoneIDByAd(adv.AdId));
                    this.ChkCountClick.Checked = adv.CountClick;
                    this.ChkCountView.Checked = adv.CountView;
                    this.TxtClicks.Text = adv.Clicks.ToString();
                    this.TxtViews.Text = adv.Views.ToString();
                    if (adv.AdType > 0)
                    {
                        this.ChkCountClick.Enabled = false;
                        this.TxtClicks.Enabled = false;
                    }
                }
            }
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
                if ( i== seleced)
                    lit.Selected = true;
            }            
        }
        private void InitShowPanel(M_Advertisement adv)
        {
            switch (adv.AdType)
            {
                case 1:
                    this.ADContent1.Attributes.Add("style", "display: ");
                    this.txtpic.Text = adv.ImgUrl;
                    this.TxtImgWidth.Text = adv.ImgWidth.ToString();
                    this.TxtImgHeight.Text = adv.ImgHeight.ToString();
                    this.TxtLinkUrl.Text = adv.LinkUrl;
                    this.TxtLinkAlt.Text = adv.LinkAlt;
                    this.RadlLinkTarget.SelectedValue= adv.LinkTarget.ToString();
                    this.TxtADIntro.Text = adv.ADIntro;
                    return;

                case 2:
                    this.ADContent2.Attributes.Add("style", "display: ");
                    this.txtFlashPath.Text = adv.ImgUrl;
                    this.TxtFlashWidth.Text = adv.ImgWidth.ToString();
                    this.TxtFlashHeight.Text = adv.ImgHeight.ToString();
                    this.RadlFlashMode.SelectedValue= adv.FlashWmode.ToString();
                    return;

                case 3:
                    this.ADContent3.Attributes.Add("style", "display: ");
                    this.TxtADText.Text = adv.ADIntro;
                    return;

                case 4:
                    this.ADContent4.Attributes.Add("style", "display: ");
                    this.TxtADCode.Text = adv.ADIntro;
                    return;

                case 5:
                    this.ADContent5.Attributes.Add("style", "display: ");
                    this.TxtWebFileUrl.Text = adv.ADIntro;
                    return;
            }
            this.ADContent1.Attributes.Add("style", "display: ");
        }
        private void InitJSScript()
        {
            this.txtOverdueDate.Attributes.Add("onclick", "setday(this)");
            foreach (ListItem lit in this.RadlADType.Items)
            {
                lit.Attributes.Add("onclick", "ADTypeChecked("+lit.Value+")");
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
            StringBuilder builder = new StringBuilder();
            int count = this.LstZoneName.Items.Count;
            for (int i = 0; i < count; i++)
            {
                if (this.LstZoneName.Items[i].Selected)
                {
                    builder.Append(this.LstZoneName.Items[i].Value);
                    builder.Append(",");
                }
            }
            return builder.ToString();
        }
        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            string adZoneIdList = this.GetAdZoneIdList();
            if (this.Page.IsValid)
            {
                M_Advertisement adv = new M_Advertisement();
                adv.UserId = 0;
                adv.AdName = DataSecurity.HtmlEncode(this.TxtADName.Text.Trim());
                adv.AdType = DataConverter.CLng(this.RadlADType.SelectedValue);
                adv.Priority = DataConverter.CLng(this.TxtPriority.Text.Trim());
                adv.Passed = this.ChkPassed.Checked;
                adv.CountView = this.ChkCountView.Checked;
                adv.Views = DataConverter.CLng(this.TxtViews.Text.Trim());
                adv.CountClick = this.ChkCountClick.Checked;
                adv.Clicks = DataConverter.CLng(this.TxtClicks.Text.Trim());
                //adv.ZoneID = adZoneIdList;
                adv.AdId = DataConverter.CLng(this.HdnID.Value.Trim());
                adv.OverdueDate = DataConverter.CDate(this.txtOverdueDate.Text);
                adv.Setting = "";
                switch (adv.AdType)
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
                        string str2 = this.TxtLinkUrl.Text.Trim();
                        adv.LinkUrl = str2;
                        adv.LinkTarget = DataConverter.CLng(this.RadlLinkTarget.SelectedValue);
                        adv.LinkAlt = this.TxtLinkAlt.Text.Trim();
                        adv.ADIntro = this.TxtADIntro.Text.Trim();
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
                        break;

                    case 3:
                        adv.ADIntro = this.TxtADText.Text.Trim();
                        break;

                    case 4:
                        adv.ADIntro = this.TxtADCode.Text.Trim();
                        break;

                    case 5:
                        adv.ADIntro = this.TxtWebFileUrl.Text.Trim();
                        break;
                }
                bool flag = false;
                if (adv.AdId>0)
                {
                    flag=B_Advertisement.Advertisement_Update(adv);
                }
                else
                {
                    adv.AdId = B_Advertisement.MaxID();
                    flag = B_Advertisement.Advertisement_Add(adv);
                }
                if (flag)
                {
                    int zid = 0;
                    if (!string.IsNullOrEmpty(adZoneIdList))
                    {
                        string[] arr = adZoneIdList.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        for (int s = 0; s < arr.Length; s++)
                        {
                            zid = DataConverter.CLng(arr[s]);
                            if (!B_Advertisement.IsExistZoneAdv(zid, adv.AdId))
                                B_Advertisement.Add_Zone_Advertisement(zid, adv.AdId);
                        }
                        B_ADZone.CreateJS(adZoneIdList);
                    }
                    Response.Write("<script>alert('广告信息保存成功！');window.document.location.href='ADManage.aspx'</script>");
                }
                else
                {
                    Response.Write("<script>alert('广告信息保存失败！');</script>");
                }
            }
        }
}
}