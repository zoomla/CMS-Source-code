using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.SQLDAL;


namespace ZoomLaCMS.Manage.Config
{
    public partial class SiteOption : CustomerPageAction
    {
        B_Admin badmin = new B_Admin();
        B_Label labelBll = new B_Label();
        EnviorHelper webhelper = new EnviorHelper();
        private string[] orderparam = "ordered,payed".Split(',');
        private string ProDirName { get { return Request.QueryString["ProDirName"]; } }
        public string Action { get { return Request.QueryString["action"]; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ProDirName) || "setdir".Equals(Action)) { function.Script(this, "showconfig('1');"); }
            if (!B_ARoleAuth.Check(ZLEnum.Auth.other, "SiteConfig")) { function.WriteErrMsg("没有权限进行此项操作"); }
            if (!IsPostBack)
            {
                txtStylePath.Text = SiteConfig.SiteOption.StylePath;
                txtAdvertisementDir.Text = SiteConfig.SiteOption.AdvertisementDir;
                languages.SelectedValue = SiteConfig.SiteOption.Language;

                TraditionalChinese.SelectedValue = System.Configuration.ConfigurationManager.AppSettings["TraditionalChinese"];
                SiteManageMode_Chk.Checked = SiteConfig.SiteOption.SiteManageMode == 1;
                EnableSiteManageCod.Checked = SiteConfig.SiteOption.EnableSiteManageCode;

                EnableSoftKey.Checked = SiteConfig.SiteOption.EnableSoftKey;
                EnableUploadFiles.Checked = SiteConfig.SiteOption.EnableUploadFiles;
                rdoIapTrue.Checked = SiteConfig.SiteOption.IsAbsoluatePath;
                rdoBtnLSh.Checked = SiteConfig.SiteOption.RegPageStart;

                if (SiteConfig.SiteOption.IsOpenHelp == "0")
                {
                    DeleteLocal.Text = "同时删除本地帮助文件";
                    PromptHelp.Visible = false;
                }
                else
                {
                    DeleteLocal.Text = "从云端下载帮助文件放到Manage/help下";
                    PromptHelp.Visible = true;
                }
                MailPermission.Checked = SiteConfig.SiteOption.MailPermission == "1" ? true : false;
                FileN.Items[Convert.ToInt32(SiteConfig.SiteOption.FileN)].Selected = true;
                DomainMerge_Chk.Checked = SiteConfig.SiteOption.DomainMerge;

                //云盘权限
                for (int i = 0; i < cloud_ChkList.Items.Count && !string.IsNullOrEmpty(SiteConfig.SiteOption.Cloud_Auth); i++)
                {
                    cloud_ChkList.Items[i].Selected = SiteConfig.SiteOption.Cloud_Auth.Contains(cloud_ChkList.Items[i].Value);
                }
                //多用户网店
                IsMall.Checked = SiteConfig.SiteOption.IsMall;

                //是否开启短信
                OpenSendMessage.Checked = SiteConfig.SiteOption.OpenSendMessage;
                cloudLeadTips.Checked = SiteConfig.SiteOption.CloudLeadTips == "1" ? true : false;

                //快递跟踪,订单提醒
                //if (SiteConfig.SiteOption.KDAPI == 0)
                //{
                //    RB_switch.Checked = false;
                //}
                //else
                //{
                //    RB_switch.Checked = true;
                //    Api.Style.Add("display", "block");
                //}
                KDKey_T.Text = SiteConfig.SiteOption.KDKey;
                SetCheckVal(OrderMsg_Chk, SiteConfig.SiteOption.OrderMsg_Chk);
                SetCheckVal(OrderMasterMsg_Chk, SiteConfig.SiteOption.OrderMasterMsg_Chk);
                SetCheckVal(OrderMasterEmail_Chk, SiteConfig.SiteOption.OrderMasterEmail_Chk);
                ReturnDate_T.Text = SiteConfig.SiteOption.THDate.ToString();
                OrderMsg_ordered_T.Text = SetJsonVal(SiteConfig.SiteOption.OrderMsg_Tlp, "ordered");
                OrderMsg_payed_T.Text = SetJsonVal(SiteConfig.SiteOption.OrderMsg_Tlp, "payed");
                OrderMasterMsg_ordered_Tlp.Text = SetJsonVal(SiteConfig.SiteOption.OrderMasterMsg_Tlp, "ordered");
                OrderMasterMsg_payed_Tlp.Text = SetJsonVal(SiteConfig.SiteOption.OrderMasterMsg_Tlp, "payed");
                OrderMasterEmail_ordered_Tlp.Text = SetJsonVal(SiteConfig.SiteOption.OrderMasterEmail_Tlp, "ordered");
                OrderMasterEmail_payed_Tlp.Text = SetJsonVal(SiteConfig.SiteOption.OrderMasterEmail_Tlp, "payed");
                /*-------------------------------------------------------*/
                EditVer.SelectedValue = SiteConfig.SiteOption.EditVer;
                //freedomain.Text = SiteConfig.SiteOption.freedomain;
                DomainRoute_chk.Checked = SiteConfig.SiteOption.DomainRoute == "1" ? true : false;
                //Savadaylog.Text = SiteConfig.SiteOption.Savadaylog;
                //Savanumlog.Text = SiteConfig.SiteOption.Savanumlog;
                IndexEx.Text = SiteConfig.SiteOption.IndexEx;
                SiteCollKey_T.Text = SiteConfig.SiteOption.SiteCollKey;
                PayType_Hid.Value = SiteConfig.SiteOption.SiteID;
                //IsManageReg.Checked = SiteConfig.SiteOption.RegManager == 1;
                safeDomain_Chk.Checked = SiteConfig.SiteOption.SafeDomain.Equals("1");
                txtSiteManageCode.Text = SiteConfig.SiteOption.SiteManageCode;
                if (!string.IsNullOrEmpty(ProDirName))
                {
                    txtCssDir.Text = "/Template/" + ProDirName + "/style";
                }
                else
                {
                    DataTable Dfileinfo = FileSystemObject.GetDirectorySmall(function.VToP("/Template/"));
                    for (int i = 0; i < Dfileinfo.Rows.Count; i++)
                    {
                        ListItem li = new ListItem("/Template/" + Dfileinfo.Rows[i]["name"]);
                        if (li.Text.Equals(SiteConfig.SiteOption.TemplateDir))
                        {
                            li.Selected = true;
                        }
                        DropTemplateDir.Items.Add(li);
                    }
                    txtCssDir.Text = SiteConfig.SiteOption.CssDir;
                }
                if (!string.IsNullOrEmpty(SiteConfig.SiteOption.TemplateDir))
                {
                    IndexTemplate_DP_hid.Value = SiteConfig.SiteOption.IndexTemplate.Trim('/');
                }
                txtProjectServer.Text = SiteConfig.SiteOption.ProjectServer.Trim();
                txtCatalog.Text = SiteConfig.SiteOption.GeneratedDirectory;
                txtPdf.Text = SiteConfig.SiteOption.PdfDirectory;//生成PDF目录
                ShopTemplate_DP_hid.Value = SiteConfig.SiteOption.ShopTemplate.Trim('/');
                txtUploadDir.Text = SiteConfig.SiteOption.UploadDir;
                txtUploadFileExts.Text = SiteConfig.SiteOption.UploadFileExts;
                txtUploadFileMaxSize.Text = webhelper.GetMaxFile();
                TxtUpPicExt.Text = SiteConfig.SiteOption.UploadPicExts;
                TxtUpPicSize.Text = SiteConfig.SiteOption.UploadPicMaxSize.ToString();
                TxtUpMediaExt.Text = SiteConfig.SiteOption.UploadMdaExts;
                TxtUpMediaSize.Text = SiteConfig.SiteOption.UploadMdaMaxSize.ToString();
                TxtUpFlashSize.Text = SiteConfig.SiteOption.UploadFlhMaxSize.ToString();
                TxtSensitivity.Text = SiteConfig.SiteOption.Sensitivity;
                //短消息提示
                if (SiteConfig.SiteOption.UAgent)
                {
                    UAgent.Checked = true;
                    uaMag.Visible = true;
                }
                else
                {
                    UAgent.Checked = false;
                    uaMag.Visible = false;
                }

                OpenMessage.Checked = SiteConfig.SiteOption.OpenMessage == 1 ? true : false;
                //标题查重
                //IsRepet.Checked = SiteConfig.SiteOption.FileRj == 1 ? true : false;
                DupTitleNum_T.Text = SiteConfig.SiteOption.DupTitleNum.ToString();
                rdoIsSensitivity.Checked = SiteConfig.SiteOption.IsSensitivity == 1 ? true : false;
                OpenAudit.Checked = SiteConfig.SiteOption.OpenAudit == 1 ? true : false;
                //商城配置
                OpenMoneySel_Chk.Checked = SiteConfig.SiteOption.OpenMoneySel;
                IsCheckPay.Checked = SiteConfig.ShopConfig.IsCheckPay == 1;
                //txtSetPrice.Text = SiteConfig.ShopConfig.OrderNum.ToString();
                ItemRegular_T.Text = SiteConfig.ShopConfig.ItemRegular;
                OrderExpired_T.Text = SiteConfig.ShopConfig.OrderExpired.ToString();
                PointRatio_T.Text = SiteConfig.ShopConfig.PointRatiot.ToString();
                PointRate_T.Text = SiteConfig.ShopConfig.PointRate.ToString();
                //EnablePointBuy_Chk.Checked = SiteConfig.ShopConfig.EnablePointBuy;
                Videourl.Text = SiteConfig.SiteOption.Videourl;
                FlexKey.Text = SiteConfig.SiteOption.FlexKey;
                APPAuth_T.Text = SiteConfig.SiteOption.WxAppID;
                DeleteLocal.Checked = SiteConfig.SiteOption.DeleteLocal;
                IsOpenHelp.Checked = SiteConfig.SiteOption.IsOpenHelp == "1" ? true : false;
                LoggedUrl_T.Text = SiteConfig.SiteOption.LoggedUrl;
                CommentRule.Text = SiteConfig.UserConfig.CommentRule.ToString();
                MastMoney_T.Text = SiteConfig.SiteOption.MastMoney.ToString("F2");
                //---------------------
                XmlDocument xdoc = new XmlDocument();
                xdoc.Load(Server.MapPath("/Config/globalization.config"));
                XmlNode langNode = xdoc.SelectSingleNode("globalization");
                languages.SelectedValue = langNode.Attributes["uiCulture"].InnerText;
                Call.SetBreadCrumb(Master, "<li><a href='SiteInfo.aspx'>" + Resources.L.系统设置 + "</a></li><li><a href='SiteInfo.aspx'>" + Resources.L.网站配置 + "</a></li><li class=\"active\"><a href='SiteOption.aspx'>" + Resources.L.网站参数配置 + "</a></li>" + Call.GetHelp(3));
            }
        }
        // 保存
        protected void Button1_Click(object sender, EventArgs e)
        {
            SiteConfig.SiteOption.AdvertisementDir = txtAdvertisementDir.Text;
            SiteConfig.SiteOption.CssDir = txtCssDir.Text;
            SiteConfig.SiteOption.StylePath = txtStylePath.Text;
            SiteConfig.SiteOption.SiteManageMode = SiteManageMode_Chk.Checked ? 1 : 0;
            //检索是否存在该语言

            //if (languages.SelectedIndex > 1)
            //{
            //    string dirPath = HttpContext.Current.Server.MapPath("~/Language/" + languages.SelectedValue + ".xml");
            //    if (!File.Exists(dirPath))
            //    {
            //        function.WriteErrMsg("对不起，系统未配置此语言，请检索配置或联系官网获取此语言配置！", Request.RawUrl);
            //    }
            //}
            //SiteConfig.SiteOption.Language = languages.SelectedValue;
            lang.LangOP = languages.SelectedValue;
            SiteConfig.SiteOption.EnableSiteManageCode = EnableSiteManageCod.Checked;
            SiteConfig.SiteOption.EnableSoftKey = EnableSoftKey.Checked;
            SiteConfig.SiteOption.EnableUploadFiles = EnableUploadFiles.Checked;
            if (rdoIapTrue.Checked) { SiteConfig.SiteOption.IsAbsoluatePath = true; }
            else { SiteConfig.SiteOption.IsAbsoluatePath = false; }
            SiteConfig.SiteOption.OpenSendMessage = OpenSendMessage.Checked;
            SiteConfig.SiteOption.DomainMerge = DomainMerge_Chk.Checked;
            //云盘
            SiteConfig.SiteOption.Cloud_Auth = "";
            for (int i = 0; i < cloud_ChkList.Items.Count; i++)
            {
                if (cloud_ChkList.Items[i].Selected) SiteConfig.SiteOption.Cloud_Auth += cloud_ChkList.Items[i].Value + ",";
            }
            SiteConfig.SiteOption.IsMall = IsMall.Checked;
            SiteConfig.SiteOption.CloudLeadTips = cloudLeadTips.Checked ? "1" : "0";
            if (UAgent.Checked) { SiteConfig.SiteOption.UAgent = true; }
            else { SiteConfig.SiteOption.UAgent = false; }
            //SiteConfig.SiteOption.KDAPI = RB_switch.Checked ? 1 : 0;//快递100
            SiteConfig.SiteOption.KDKey = KDKey_T.Text.Trim();
            //商城模块配置
            SiteConfig.SiteOption.OrderMsg_Chk = GetCheckVal(OrderMsg_Chk);
            SiteConfig.SiteOption.THDate = Convert.ToInt32(ReturnDate_T.Text);
            SiteConfig.SiteOption.OrderMsg_Tlp = GetJson(orderparam, OrderMsg_ordered_T.Text, OrderMsg_payed_T.Text);
            SiteConfig.SiteOption.OrderMasterMsg_Chk = GetCheckVal(OrderMasterMsg_Chk);
            SiteConfig.SiteOption.OrderMasterMsg_Tlp = GetJson(orderparam, OrderMasterMsg_ordered_Tlp.Text, OrderMasterMsg_payed_Tlp.Text);
            SiteConfig.SiteOption.OrderMasterEmail_Chk = GetCheckVal(OrderMasterEmail_Chk);
            SiteConfig.SiteOption.OrderMasterEmail_Tlp = GetJson(orderparam, OrderMasterEmail_ordered_Tlp.Text, OrderMasterEmail_payed_Tlp.Text);
            //----
            //SiteConfig.SiteOption.SMSTips = SMSTips.Checked;
            SiteConfig.SiteOption.DomainRoute = DomainRoute_chk.Checked ? "1" : "0";
            //SiteConfig.SiteOption.Savadaylog = DataConverter.CLng(Savadaylog.Text).ToString();
            //SiteConfig.SiteOption.Savanumlog = DataConverter.CLng(Savanumlog.Text).ToString();
            SiteConfig.SiteOption.SiteCollKey = SiteCollKey_T.Text.Trim();
            SiteConfig.SiteOption.SafeDomain = safeDomain_Chk.Checked ? "1" : "0";
            SiteConfig.SiteOption.SiteManageCode = txtSiteManageCode.Text;
            SiteConfig.SiteOption.TemplateDir = DropTemplateDir.SelectedItem.Text;
            SiteConfig.SiteOption.ProjectServer = txtProjectServer.Text;
            SiteConfig.SiteOption.GeneratedDirectory = txtCatalog.Text;//生成页面目录
            SiteConfig.SiteOption.PdfDirectory = txtPdf.Text;//生成PDF目录
            SiteConfig.SiteOption.IndexEx = IndexEx.Text;
            SiteConfig.SiteOption.IndexTemplate = IndexTemplate_DP_hid.Value;
            SiteConfig.SiteOption.ShopTemplate = ShopTemplate_DP_hid.Value;
            SiteConfig.SiteOption.UploadDir = txtUploadDir.Text;
            SiteConfig.SiteOption.UploadFileExts = txtUploadFileExts.Text;
            SiteConfig.SiteOption.UploadPicExts = TxtUpPicExt.Text;
            SiteConfig.SiteOption.UploadPicMaxSize = int.Parse(TxtUpPicSize.Text);
            SiteConfig.SiteOption.EditVer = EditVer.SelectedValue;
            SiteConfig.SiteOption.IsSaveRemoteImage = EditVer.SelectedValue == "1" ? true : false;
            SiteConfig.SiteOption.UploadMdaExts = TxtUpMediaExt.Text;
            SiteConfig.SiteOption.UploadMdaMaxSize = int.Parse(TxtUpMediaSize.Text);

            SiteConfig.SiteOption.UploadFlhMaxSize = int.Parse(TxtUpFlashSize.Text);
            //SiteConfig.ShopConfig.OrderNum = decimal.Parse(txtSetPrice.Text);
            SiteConfig.ShopConfig.ItemRegular = ItemRegular_T.Text;
            SiteConfig.ShopConfig.IsCheckPay = IsCheckPay.Checked ? 1 : 0;
            SiteConfig.ShopConfig.OrderExpired = DataConvert.CLng(OrderExpired_T.Text);
            //SiteConfig.ShopConfig.EnablePointBuy = EnablePointBuy_Chk.Checked;
            SiteConfig.ShopConfig.PointRatiot = DataConvert.CDouble(PointRatio_T.Text);
            SiteConfig.ShopConfig.PointRate = DataConvert.CDouble(PointRate_T.Text);
            SiteConfig.SiteOption.OpenMoneySel = OpenMoneySel_Chk.Checked;
            SiteConfig.SiteOption.OpenMessage = OpenMessage.Checked ? 1 : 0;
            SiteConfig.SiteOption.DupTitleNum = DataConvert.CLng(DupTitleNum_T.Text);
            SiteConfig.SiteOption.FileRj = SiteConfig.SiteOption.DupTitleNum > 0 ? 1 : 0;
            SiteConfig.SiteOption.OpenAudit = OpenAudit.Checked ? 1 : 0;
            SiteConfig.SiteOption.IsSensitivity = rdoIsSensitivity.Checked ? 1 : 0;
            SiteConfig.SiteOption.Sensitivity = TxtSensitivity.Text.Trim();
            SiteConfig.SiteOption.Videourl = Videourl.Text.Trim();
            SiteConfig.SiteOption.FlexKey = FlexKey.Text.Trim();
            SiteConfig.SiteOption.WxAppID = APPAuth_T.Text.Trim();

            SiteConfig.SiteOption.RegPageStart = rdoBtnLSh.Checked;
            SiteConfig.SiteOption.MailPermission = MailPermission.Checked ? "1" : "0";
            SiteConfig.SiteOption.FileN = FileN.SelectedIndex;
            SiteConfig.SiteOption.DeleteLocal = DeleteLocal.Checked;
            SiteConfig.SiteOption.IsOpenHelp = IsOpenHelp.Checked ? "1" : "0";
            SiteConfig.UserConfig.CommentRule = DataConverter.CLng(CommentRule.Text);
            SiteConfig.SiteOption.SiteID = Request.Form["PayType"];
            SiteConfig.SiteOption.LoggedUrl = LoggedUrl_T.Text;
            SiteConfig.SiteOption.MastMoney = DataConvert.CDouble(MastMoney_T.Text);
            //SiteConfig.SiteOption.RegManager = IsManageReg.Checked ? 1 : 0;
            string path = Request.PhysicalApplicationPath + "/manage/help";
            if (IsOpenHelp.Checked)
            {
                if (!Directory.Exists(path)) { Directory.CreateDirectory(path); }
                if (DeleteLocal.Checked)
                {
                    System.Net.WebClient myWebClient = new System.Net.WebClient();
                    DataSet ds = new DataSet();
                    try
                    {
                        ds.ReadXml(SiteConfig.SiteOption.ProjectServer + "/api/gettemplate.aspx?menu=gethelp");
                    }
                    catch
                    {
                        function.WriteErrMsg("请检查云端设置是否正常", CustomerPageAction.customPath + "Config/SiteOption.aspx");
                    }
                    DataTable dt = ds.Tables[0];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        myWebClient.DownloadFile(SiteConfig.SiteOption.ProjectServer + "/help/" + dt.Rows[i]["TempDirName"].ToString(), Request.PhysicalApplicationPath + "/manage/help/" + dt.Rows[i]["TempDirName"].ToString());
                    }
                }
            }
            if ((!IsOpenHelp.Checked) && DeleteLocal.Checked)
            {
                if (Directory.Exists(path))
                {
                    FileSystemObject.Delete(path, FsoMethod.Folder);
                }
            }
            XmlDocument appDoc = new XmlDocument();
            appDoc.Load(Server.MapPath("/Config/AppSettings.config"));
            XmlNodeList amde = appDoc.SelectSingleNode("appSettings").ChildNodes;
            foreach (XmlNode xn in amde)
            {
                XmlElement xe = (XmlElement)xn;
                if (xe.GetAttribute("key").ToString() == "TraditionalChinese")
                    xe.SetAttribute("value", TraditionalChinese.SelectedValue);
            }
            try
            {
                appDoc.Save(Server.MapPath("/Config/AppSettings.config"));
            }
            catch (System.IO.IOException) { }

            try
            {
                SiteConfig.Update();
                if (Convert.ToInt64(txtUploadFileMaxSize.Text) > 4096) { function.WriteErrMsg("IIS可支持最大文件上传的容量为4G!"); }
                webhelper.UpdateMaxFile((Convert.ToInt64(txtUploadFileMaxSize.Text) * 1024 * 1024).ToString());
                function.WriteSuccessMsg("网站参数配置保存成功", CustomerPageAction.customPath + "Config/SiteOption.aspx");
            }
            catch (FileNotFoundException)
            {
                function.WriteErrMsg("文件未找到", "SiteOption.aspx");
            }
            catch (UnauthorizedAccessException)
            {
                function.WriteErrMsg("检查您的服务器是否给配置文件或文件夹配置了写入权限", CustomerPageAction.customPath + "Config/SiteOption.aspx");
            }
        }
        //清空
        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtCssDir.Text = "";
            txtStylePath.Text = "";
        }
        protected void languages_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (languages.SelectedIndex > 1)
            {
                Prompt_L.Text = "*系统未配置此语言，请检索配置或联系官网获取此语言配置";
            }
            else
                Prompt_L.Text = "系统已配置此语言，此语言可以使用";
            XmlDocument appDoc = new XmlDocument();
            appDoc.Load(Server.MapPath("/Config/globalization.config"));
            XmlNode langNode = appDoc.SelectSingleNode("globalization");
            langNode.Attributes["culture"].InnerText = languages.SelectedValue;
            langNode.Attributes["uiCulture"].InnerText = languages.SelectedValue;
            appDoc.Save(Server.MapPath("/Config/globalization.config"));
        }
        protected void IsOpenHelp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsOpenHelp.Checked)
            {
                DeleteLocal.Text = "同时删除本地帮助文件";
                PromptHelp.Visible = false;
            }
            else
            {
                DeleteLocal.Text = "从云端下载帮助文件放到Manage/help下";
                PromptHelp.Visible = true;
            }
        }
        public string GetCheckVal(CheckBoxList chkList)
        {
            string value = "";
            for (int i = 0; i < chkList.Items.Count; i++)
            {
                if (chkList.Items[i].Selected) value += chkList.Items[i].Value + ",";
            }
            return value.TrimEnd(',');
        }
        public void SetCheckVal(CheckBoxList chkList, string value)
        {
            if (chkList.Items.Count < 1 || string.IsNullOrEmpty(value)) return;
            for (int i = 0; i < chkList.Items.Count; i++)
            {
                chkList.Items[i].Selected = value.Contains(chkList.Items[i].Value);
            }
        }
        public string GetJson(string[] name, params string[] value)
        {
            JObject obj = new JObject();
            if (name.Length != value.Length) function.WriteErrMsg("键与值的数量不匹配");
            for (int i = 0; i < name.Length; i++)
            {
                obj.Add(name[i], value[i]);
            }
            return obj.ToString();
        }
        public string SetJsonVal(string json, string name)
        {
            if (string.IsNullOrEmpty(json)) return "";
            JObject obj = (JObject)JsonConvert.DeserializeObject(json);
            return obj[name].ToString();
        }
    }
}