namespace ZoomLaCMS.Design.ppt
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Data;
    using ZoomLa.BLL;
    using ZoomLa.BLL.Design;
    using ZoomLa.BLL.Helper;
    using ZoomLa.Common;
    using ZoomLa.Components;
    using ZoomLa.Model;
    using ZoomLa.Model.Design;
    using ZoomLa.SQLDAL;

    public partial class pcview : System.Web.UI.Page
    {
        B_Design_PPT pptBll = new B_Design_PPT();
        B_Design_SiteInfo sfBll = new B_Design_SiteInfo();
        B_Design_Tlp tlpBll = new B_Design_Tlp();
        B_CreateHtml createBll = new B_CreateHtml();
        B_User buser = new B_User();
        //-------前端使用
        public M_Design_Page pageMod = new M_Design_Page();
        public string extendData = "[]";
        public string comp_global = "[]";
        public string sitecfg = "{}";
        public string SiteUrl { get { return SiteConfig.SiteInfo.SiteUrl; } }
        //public int TlpID { get { return DataConvert.CLng(Request.QueryString["TlpID"]); } }
        //public string Mid { get { return Request.QueryString["ID"] ?? ""; } }
        public int Sid { get { return DataConvert.CLng(B_Route.GetParam("SID", Page)); } }
        //wxapi
        public string appid = "";
        public string timeStamp = "";
        public string noncestr = WxAPI.nonce;
        public string sign = "";
        public string shareLink = "";//sharelink
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                M_UserInfo mu = buser.GetLogin();
                pageMod = GetScenceMod();
                if (!B_Design_Helper.Se_CheckAccessPwd(pageMod, mu))
                {
                    Server.Transfer("/design/h5/checkpwd.aspx?sid=" + Sid + "&ztype=ppt");
                    return;
                }
                //shareLink = Request.Url.Host + "/h5/" + pageMod.ID;
                if (DeviceHelper.GetBrower() == DeviceHelper.Brower.Micro)
                {
                    try
                    {
                        //Avoid sometimes WeChat API parsing error
                        WX_Share.Visible = true;
                        //--WXAPI
                        WxAPI api = WxAPI.Code_Get();
                        appid = api.AppId.APPID;
                        timeStamp = WxAPI.HP_GetTimeStamp();
                        sign = api.JSAPI_GetSign(api.JSAPITicket, noncestr, timeStamp, Request.Url.AbsoluteUri);
                    }
                    catch (Exception ex) { ZLLog.L("wxerror:" + ex.Message); }
                }
                Title_L.Text = pageMod.Title;
                if (string.IsNullOrEmpty(pageMod.PreviewImg))
                {
                    string[] defwx = "/UploadFiles/demo/h4.jpg|/UploadFiles/demo/h5.jpg".Split('|');
                    pageMod.PreviewImg = defwx[new Random().Next(0, defwx.Length)];
                }
                Wx_Img.ImageUrl = pageMod.PreviewImg;
            }
        }
        private M_Design_Page GetScenceMod()
        {
            M_Design_Page seMod = null;
            int TlpID = DataConvert.CLng(Request.QueryString["TlpID"]);
            string Mid = Request.QueryString["ID"] ?? "";
            if (Sid > 0) { seMod = pptBll.SelReturnModel(Sid); }
            else if (TlpID > 0)
            {
                seMod = pptBll.SelModelByTlp(TlpID);
            }
            else if (!string.IsNullOrEmpty(Mid))
            {
                seMod = pptBll.SelModelByGuid(Mid);
            }
            //场景不存在或处理回收站状态
            if (seMod == null || seMod.ID < 1 || seMod.Status == (int)ZLEnum.ConStatus.Recycle)
            {
                M_Design_Tlp tlpMod = tlpBll.SelModelByDef("404");
                if (tlpMod == null) { function.WriteErrMsg("404模板不存在"); }
                seMod = new B_Design_Scence().SelModelByTlp(tlpMod.ID);
                if (seMod == null) { function.WriteErrMsg("404场景不存在"); }
            }
            return seMod;
        }
    }
}