using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Helper;
using ZoomLa.BLL.Plat;
using ZoomLa.Common;
using ZoomLa.Model;
namespace ZoomLaCMS.Plat.Blog
{
    public partial class SpecBody : System.Web.UI.Page
    {
        B_Plat_Topic topicBll = new B_Plat_Topic();
        M_User_Plat upMod = null;
        B_User buser = new B_User();
        private int CPage { get { return PageCommon.GetCPage(); } }
        private int PSize { get { return DataConverter.CLng(Request["psize"]); } }
        private string Filter { get { return Request["filter"] ?? ""; } }
        private string Skey { get { return HttpUtility.UrlDecode(Request["skey"] ?? ""); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            MyBind();
        }
        private void MyBind()
        {
            int pcount = 0;
            upMod = B_User_Plat.GetLogin();
            DataTable dt = topicBll.SelByPage(CPage, PSize, out pcount, upMod.CompID, upMod.UserID, Filter, Skey);
            RPT.DataSource = dt;
            RPT.DataBind();
            string hrefTlp = "<a href='javascript:;' onclick='topic.load(\"" + Filter + "\",\"@query\",@page);' title=''>@text</a>";
            Page_L.Text = PageCommon.CreatePageHtml(pcount, CPage, PSize, hrefTlp);
        }
        public string GetTName()
        {
            return StringHelper.SubStr(Eval("TName", ""), 10);
        }
        public string GetContent()
        {
            string msg = StringHelper.StripHtml(Eval("MsgContent", ""));
            return StringHelper.SubStr(StrHelper.RemoveBySE(msg));
        }
    }
}