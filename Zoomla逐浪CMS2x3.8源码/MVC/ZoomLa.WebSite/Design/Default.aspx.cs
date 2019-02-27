namespace ZoomLaCMS.Design
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
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
    /*
     * 必须登录后再进
     * 1,空页进入,自动跳到你站点首页
     * 2,设计师进入,可传入模板ID与指定Source
     * 3,用户进入,需要传入指定的页面ID,
     * 4,用户选择好模板后,进入设计页面,但无保存权限,只有选定应用模板后,才可进行设计
     */
    public partial class Default : System.Web.UI.Page
    {
        B_User buser = new B_User();
        B_Design_Page pageBll = new B_Design_Page();
        B_Design_SiteInfo sfBll = new B_Design_SiteInfo();
        B_Design_Tlp tlpBll = new B_Design_Tlp();
        B_CreateHtml createBll = new B_CreateHtml();
        B_IDC_DomainList domBll = new B_IDC_DomainList();
        //-------前端使用
        public M_Design_Page pageMod = new M_Design_Page();
        public string extendData = "[]";
        public string comp_global = "[]";
        public string sitecfg = "{}";
        //Page的Guid
        public string Mid { get { return Request.QueryString["ID"] ?? ""; } }
        public string Source { get { return Request.QueryString["Source"] ?? ""; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            B_User.CheckIsLogged(Request.RawUrl);
            AutoCreateSite();
            //if (!new B_User().CheckLogin()) { function.Script(this, "AjaxLogin();"); return; }
            M_UserInfo mu = buser.GetLogin(false);
            //-----------获取需要修改的页面
            #region 根据路径与SiteID访问(disuse)
            //if (string.IsNullOrEmpty(Mid) && SiteID < 1 && string.IsNullOrEmpty(Path)) 
            //{
            //    Response.Redirect("/Design/User/");
            //}
            //if (!string.IsNullOrEmpty(Mid))
            //{
            //    pageMod = pageBll.SelModelByGuid(Mid);
            //}
            //else if (SiteID > 0)
            //{
            //    pageMod = pageBll.SelModelBySiteID(SiteID, Path);
            //}
            #endregion
            M_Design_SiteInfo sfMod = sfBll.SelModelByUid(mu.UserID);
            if (sfMod == null) { function.WriteErrMsg("尚未创建站点,<a href='/'>返回首页</a>"); }
            if (string.IsNullOrEmpty(Mid))
            {
                pageMod = pageBll.SelModelBySiteID(sfMod.ID, "");
                if (pageMod == null || pageMod.ID < 1) { function.WriteErrMsg("未指定首页 <a href='/Design/User/'>用户中心</a>"); }
                else { Response.Redirect("?ID=" + pageMod.guid); }
            }
            else
            {
                pageMod = pageBll.SelModelByGuid(Mid);
            }
            if (pageMod == null) { function.WriteErrMsg("指定的页面不存在"); }
            M_Design_Tlp tlpMod = tlpBll.SelReturnModel(pageMod.TlpID);
            if (tlpMod != null) { TlpName_L.Text = "[" + tlpMod.TlpName + "]"; }
            //demo应该注释该句
            if (pageMod.IsTemplate) { B_Permission.CheckAuthEx("design"); }
            //if (pageMod.UserID != mu.UserID) { function.WriteErrMsg("你无权修改该页面"); }
            pageMod = pageBll.MergeGlobal(pageMod, Source);
            if (pageMod.comp_global.Count > 0)
            {
                comp_global = JsonConvert.SerializeObject(pageMod.comp_global);
            }

            //---------站点//修改模板时无站点数据
            if (sfMod != null)
            {
                sitecfg = sfBll.ToSiteCfg(sfMod);
                mu.SiteID = sfMod.ID;
                //---------域名(没有则直接创建,只能有一个)
                M_IDC_DomainList domMod = domBll.SelModelByUid(mu.UserID);
                if (domMod != null)
                {
                    domain_a.HRef = "http://" + domMod.DomName;
                    domain_a.InnerText = domMod.DomName;
                }
            }
            //解析标签
            if (!string.IsNullOrEmpty(pageMod.labelArr))
            {
                DataTable labelDT = new DataTable();
                labelDT.Columns.Add(new DataColumn("guid", typeof(string)));
                labelDT.Columns.Add(new DataColumn("label", typeof(string)));
                labelDT.Columns.Add(new DataColumn("htmlTlp", typeof(string)));
                string[] labelArr = pageMod.labelArr.Trim('|').Split('|');
                foreach (string label in labelArr)
                {
                    DataRow dr = labelDT.NewRow();
                    dr["guid"] = label.Split(':')[0];
                    dr["label"] = label.Split(':')[1];
                    string html = createBll.CreateHtml(StringHelper.Base64StringDecode(dr["label"].ToString()));
                    dr["htmlTlp"] = StringHelper.Base64StringEncode(html);
                    labelDT.Rows.Add(dr);
                }
                extendData = JsonConvert.SerializeObject(labelDT);
            }
            //用户可选择关闭
            if (DeviceHelper.GetBrower() != DeviceHelper.Brower.Chrome) { function.Script(this, "ShowIE();"); return; }
        }
        //用户已登录,但无站点信息,这种情况下自动为其创建站点
        private void AutoCreateSite()
        {
            B_Design_Tlp tlpBll = new B_Design_Tlp();
            //M_Design_Page pageMod = pageBll.SelModelByGuid(Mid);
            M_UserInfo mu = buser.GetLogin();
            if (mu.IsNull) { function.WriteErrMsg("用户未登录"); }
            if (mu.SiteID < 1)
            {
                //直接创建站点
                Response.Redirect("/design/newsite.aspx?TlpID=-1");
            }
        }
    }
}