using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Design;
using ZoomLa.BLL.Helper;
using ZoomLa.BLL.Site;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.Design;
using ZoomLa.Model.Site;

namespace ZoomLaCMS.Design.mobile
{
    public partial class _default : System.Web.UI.Page
    {
        B_Design_MBSite mbBll = new B_Design_MBSite();
        B_User buser = new B_User();
        M_Design_MBSite mbMod = null;
        //后期改为中继跳转,根据传值加载不同的模板
        public int Mid { get { return DataConverter.CLng(Request.QueryString["ID"]); } }
        //内容或商品ID,进入详情页时需要
        public int Gid { get { return DataConverter.CLng(Request.QueryString["gid"]); } }
        //需要加载的页面名,不带后缀名
        public string PageName { get { return string.IsNullOrEmpty(Request["p"]) ? "index" : Request["p"]; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                M_UserInfo mu = buser.GetLogin();
                if (Mid < 1 && mu.IsNull)
                {
                    function.WriteErrMsg("你尚未指定站点信息");
                }
                else if (Mid < 1)//未指定但已登录,进入用户自己的站点
                {
                    mbMod = mbBll.SelModelByUid(mu.UserID);
                    if (mbMod == null) { Response.Redirect("newsite.aspx"); }
                    else { Response.Redirect("/design/mobile/default.aspx?id=" + mbMod.ID); }
                }
                else //正常浏览站点
                {
                    mbMod = mbBll.SelReturnModel(Mid);
                    if (mbMod == null) { function.WriteErrMsg("站点不存在"); }
                    //----用于微信分享
                    Title_L.Text = mbMod.SiteName + "-来自[" + mu.TrueName + "]的微站";
                    Share_Img.ImageUrl = string.IsNullOrEmpty(mbMod.SiteImg) ? "/design/mobile/tlp/" + mbMod.TlpID + "/view.jpg" : mbMod.SiteImg;
                    //----需要加载的通用页面资源,版本更新刷新缓存
                    string version = DateTime.Now.Millisecond.ToString();
                    string cssres = "<link href=\"/design/JS/mobile/tools.css?v=" + version + "\" rel=\"stylesheet\" />"
                                    + "<link href=\"/design/JS/sui/css/sm.min.css\" rel=\"stylesheet\" />";
                    string jsres = "<script src=\"/JS/Plugs/angular.min.js\"></script>"
                                    + "<script src=\"/JS/Controls/ZL_Webup.js?v=" + version + "\"></script>"
                                    + "<script src=\"/JS/Modal/APIResult.js\"></script>"
                                    + "<script src=\"/design/h5/js/zepto.min.js\"></script>"
                                    + "<script src=\"/design/JS/sui/js/sm.js\"></script>"
                                    + "<script src=\"/design/JS/mobile/tools.js?v=" + version + "\"></script>";
                    //----根据网站绑定的模板,加载指定页面
                    string html = ReadHtml(mbMod);
                    html = html.Replace("{$site}", Mid.ToString()).Replace("{$gid}", Gid.ToString());
                    html = cssres + jsres + html;//资源置顶
                    Html_Lit.Text = html;
                    //----配置信息,后期可扩展其他功能
                    SiteCfg cfg = new SiteCfg();
                    cfg.mid = Mid;
                    //直接进入,并且未强制指定为编辑模式
                    if (Request.UrlReferrer == null && !(Request["action"] ?? "").Equals("edit")) { }
                    else if (mbMod.UserID == mu.UserID) { cfg.edit = true; btns_wrap.Visible = true; pop_div.Visible = true; }
                    sitecfg_hid.Value = JsonConvert.SerializeObject(cfg);
                }
            }
        }
        private void RepToClient(string result)
        {
            Response.Clear(); Response.Write(result); Response.Flush(); Response.End();
        }
        //加载指定模板页,如不存在,则加载公用模板页(内容|商品详情)
        private string ReadHtml(M_Design_MBSite mbMod)
        {
            string vpath = "/design/mobile/tlp/" + mbMod.TlpID + "/" + PageName + ".html";
            string ppath = Server.MapPath(vpath);
            if (!File.Exists(ppath)) { vpath = "/design/mobile/tlp/common/" + PageName + ".html"; }
            return SafeSC.ReadFileStr(vpath);
        }
        public class SiteCfg
        {
            public bool edit = false;
            public int mid = 0;
        }
        /*
        <link href="/design/h5/css/swiper.min.css" rel="stylesheet" />
        <script src="/design/h5/js/swiper.min.js"></script>

        <link href="/design/JS/mobile/tools.css" rel="stylesheet" />  
        <link href="/design/JS/sui/css/sm.min.css" rel="stylesheet" />
        <script src="/JS/Plugs/angular.min.js"></script>
        <script src="/JS/Controls/ZL_Webup.js"></script>
        <script src="/JS/Modal/APIResult.js"></script>
        <script src="/design/h5/js/zepto.min.js"></script>
        <script src="/design/JS/sui/js/sm.js"></script>
        <script src="/design/JS/mobile/tools.js"></script>
         */
    }
}