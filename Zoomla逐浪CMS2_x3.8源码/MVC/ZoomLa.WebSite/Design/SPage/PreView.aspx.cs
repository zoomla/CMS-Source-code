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
    public partial class PreView : System.Web.UI.Page
    {
        B_SPage_Page pageBll = new B_SPage_Page();
        B_CreateHtml bll=new B_CreateHtml();
        public M_SPage_Page pageMod = null;
        public int Mid { get { return DataConvert.CLng(Request.QueryString["ID"]); } }
        private string PName { get { return DataConvert.CStr(Request.QueryString["PName"]); } }
        public int ItemID { get { return DataConvert.CLng(Request.QueryString["ItemID"]); } }
        public int CPage {get{return DataConvert.CLng(Request.QueryString["CPage"]);} }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Mid > 0) { pageMod = pageBll.SelReturnModel(Mid); }
            else if (!string.IsNullOrEmpty(PName)) { pageMod = pageBll.SelModelByName(PName); }
            if (pageMod == null) { function.WriteErrMsg("页面不存在"); }
            pageMod.PageRes = bll.CreateHtml(pageMod.PageRes, CPage, ItemID);
            pageMod.PageDSLabel=StringHelper.Base64StringEncode(pageMod.PageDSLabel);
        }
    }
}