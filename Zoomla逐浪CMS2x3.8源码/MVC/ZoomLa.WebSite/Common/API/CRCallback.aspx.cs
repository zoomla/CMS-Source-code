namespace ZoomLaCMS.Common.API
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using ZoomLa.BLL.Third;
    using ZoomLa.Common;
    using ZoomLa.Model.Third;
    using ZoomLa.PdoApi.CopyRight;
    public partial class CRCallback : System.Web.UI.Page
    {
        private string Code { get { return Request.QueryString["Code"] ?? ""; } }
        private string Url { get { string _url = string.IsNullOrEmpty(Request["Url"]) ? CustomerPageAction.customPath2 + "Copyright/Config.aspx" : Request["Url"]; return _url; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            M_Third_Info crMod = new B_Third_Info().SelModelByName("CR");
            //未配置信息,则先引导其去配置
            if (crMod == null || string.IsNullOrEmpty(crMod.Key) || string.IsNullOrEmpty(crMod.Secret))
            {
                Response.Redirect(CustomerPageAction.customPath2 + "APP/Suppliers.aspx");
            }
            else if (string.IsNullOrEmpty(Code)) { function.WriteErrMsg("未传入Code值,请先登录"); }
            else
            {
                C_CopyRight crBll = new C_CopyRight();
                if (string.IsNullOrEmpty(C_CopyRight.AccessToken)) { function.WriteErrMsg("获取Token失败,Code[" + Code + "]"); }
                Response.Redirect(Url);
            }
        }
    }
}