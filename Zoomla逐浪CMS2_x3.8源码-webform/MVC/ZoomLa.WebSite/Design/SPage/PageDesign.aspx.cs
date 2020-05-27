using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Design;
using ZoomLa.Common;
using ZoomLa.Model.Design;
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.Design.SPage
{
    public partial class PageDesign : System.Web.UI.Page
    {
        B_SPage_Page pageBll = new B_SPage_Page();
        B_CreateHtml bll = new B_CreateHtml();
        public M_SPage_Page pageMod = null;
        public int Mid { get { return DataConvert.CLng(Request.QueryString["ID"]); } }
        private string PName { get { return DataConvert.CStr(Request.QueryString["PName"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            B_Admin.CheckIsLogged(Request.RawUrl);
            if (Mid > 0) { pageMod = pageBll.SelReturnModel(Mid); }
            else if (!string.IsNullOrEmpty(PName))
            {
                pageMod = pageBll.SelModelByName(PName);
                if (pageMod == null) { function.WriteErrMsg("页面不存在"); }
                Response.Redirect("PageDesign.aspx?ID=" + pageMod.ID);
            }
            if (pageMod == null) { function.WriteErrMsg("页面不存在"); }
            //取消掉{boot}的解析,避免与母版页中的bootstrap冲突
            pageMod.PageRes = pageMod.PageRes.Replace("{ZL:Boot()/}", "").Replace("<script src=\"/JS/jquery-1.11.1.min.js\"></script>", "");
            pageMod.PageRes = bll.CreateHtml(pageMod.PageRes);
            pageMod.PageBottom = bll.CreateHtml(pageMod.PageBottom);
            pageMod.PageDSLabel = StringHelper.Base64StringEncode(pageMod.PageDSLabel);
            if (string.IsNullOrEmpty(pageMod.ViewUrl)) { pageMod.ViewUrl = "Preview.aspx?ID=" + pageMod.ID; }
        }
    }
}