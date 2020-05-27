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


namespace ZoomLaCMS.Design.h5
{
    public partial class mbview : System.Web.UI.Page
    {
        B_Design_Scence pageBll = new B_Design_Scence();
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
        public int TlpID { get { return DataConvert.CLng(Request.QueryString["TlpID"]); } }
        public string Mid { get { return Request.QueryString["ID"] ?? ""; } }
        //wxapi
        public string appid = "";
        public string timeStamp = "";
        public string noncestr = "5K8264ILTKCH16CQ2502SI8ZNMTM67VS";
        public string sign = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (TlpID > 0)
            {
                pageMod = pageBll.SelModelByTlp(TlpID);
            }
            else if (!string.IsNullOrEmpty(Mid))
            {
                pageMod = pageBll.SelModelByGuid(Mid);
            }
            //场景不存在或处理回收站状态
            if (pageMod == null || pageMod.ID < 1 || pageMod.Status == (int)ZLEnum.ConStatus.Recycle)
            {
                M_Design_Tlp tlpMod = tlpBll.SelModelByDef("404");
                if (tlpMod == null) { function.WriteErrMsg("404模板不存在"); }
                pageMod = pageBll.SelModelByTlp(tlpMod.ID);
                if (pageMod == null) { function.WriteErrMsg("404场景不存在"); }
            }
            if (DeviceHelper.GetBrower() == DeviceHelper.Brower.Micro)
            {
                WX_Share.Visible = true;
                //--WXAPI
                WxAPI api = WxAPI.Code_Get();
                appid = api.AppId.APPID;
                timeStamp = WxAPI.HP_GetTimeStamp();
                sign = api.JSAPI_GetSign(api.JSAPITicket, noncestr, timeStamp, Request.Url.AbsoluteUri);
            }

            Title_L.Text = pageMod.Title;
            Tit_L.Text = pageMod.Title;
            if (string.IsNullOrEmpty(pageMod.PreviewImg))
            {
                string[] defwx = "/UploadFiles/demo/h4.jpg|/UploadFiles/demo/h5.jpg".Split('|');
                pageMod.PreviewImg = defwx[new Random().Next(0, defwx.Length)];
            }
            Wx_Img.ImageUrl = pageMod.PreviewImg;
            ////------解析标签
            //if (!string.IsNullOrEmpty(pageMod.labelArr))
            //{
            //    DataTable labelDT = new DataTable();
            //    labelDT.Columns.Add(new DataColumn("guid", typeof(string)));
            //    labelDT.Columns.Add(new DataColumn("label", typeof(string)));
            //    labelDT.Columns.Add(new DataColumn("htmlTlp", typeof(string)));
            //    string[] labelArr = pageMod.labelArr.Trim('|').Split('|');
            //    foreach (string label in labelArr)
            //    {
            //        DataRow dr = labelDT.NewRow();
            //        dr["guid"] = label.Split(':')[0];
            //        dr["label"] = label.Split(':')[1];
            //        string html = createBll.CreateHtml(StringHelper.Base64StringDecode(dr["label"].ToString()));
            //        dr["htmlTlp"] = StringHelper.Base64StringEncode(html);
            //        labelDT.Rows.Add(dr);
            //    }
            //    extendData = JsonConvert.SerializeObject(labelDT);
            //}
        }
    }
}