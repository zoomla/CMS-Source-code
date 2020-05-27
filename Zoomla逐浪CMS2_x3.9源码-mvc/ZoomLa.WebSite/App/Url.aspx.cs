using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL.Helper;
using ZoomLa.BLL.User;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model.User;
using ZoomLa.Model;
//根据不同的设备,跳转至不同的链接处,判断完设备再对浏览器判断

namespace ZoomLaCMS.App
{
    public partial class Url : System.Web.UI.Page
    {
        private int AppID { get { return DataConverter.CLng(Request.QueryString["appid"]); } }
        private int Mid { get { return DataConverter.CLng(Request.QueryString["ID"]); } }
        B_Temp tempBll = new B_Temp();
        B_QrCode codeBll = new B_QrCode();
        protected void Page_Load(object sender, EventArgs e)
        {
            //MicroMessenger
            if (!IsPostBack)
            {
                string agent = Request.UserAgent;
                M_QrCode qrcodeMod = null;
                if (Mid > 0) { qrcodeMod = codeBll.SelReturnModel(Mid); }
                else if (AppID > 0) { qrcodeMod = codeBll.SelModelByAppID(AppID); }
                else { function.WriteErrMsg("参数不能为空"); }

                //M_Temp tempMod = tempBll.SelModelByStr1(Str1);
                if (qrcodeMod == null) { function.WriteErrMsg("参数错误,该链接不存在"); }
                string url = GetAppUrl(qrcodeMod, DeviceHelper.GetAgent(agent), DeviceHelper.GetBrower(agent));
                url = string.IsNullOrEmpty(url) ? GetAppUrl(qrcodeMod, DeviceHelper.Agent.Android, DeviceHelper.GetBrower(agent)) : url;
                if (string.IsNullOrEmpty(url)) { function.WriteErrMsg("未为该设备指定链接"); }
                Response.Redirect(url);
            }
        }
        public string GetAppUrl(M_QrCode model, DeviceHelper.Agent agent, DeviceHelper.Brower brower)
        {
            string url = "";
            if (string.IsNullOrEmpty(model.Urls)) return url;
            url = codeBll.GetUrlByAgent(agent, model);
            switch (agent)
            {
                case DeviceHelper.Agent.iPhone:
                case DeviceHelper.Agent.iPad:
                    switch (brower)
                    {
                        case DeviceHelper.Brower.Micro://如果是微信,并且是分发市场的Url,则提示其用外置浏览器打开
                            string html = SafeSC.ReadFileStr("/APP/AppStore.html");
                            //html = html.Replace("@Device", "");
                            Response.Clear();
                            Response.Write(html); Response.Flush(); Response.End();
                            break;
                    }
                    break;
                    //case Agent.PC:
                    //    break;
                    //case Agent.UnKnown:
                    //    break;
            }
            return url;
        }
    }
}