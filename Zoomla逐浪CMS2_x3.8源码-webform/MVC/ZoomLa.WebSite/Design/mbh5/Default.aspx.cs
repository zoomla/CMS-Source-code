namespace ZoomLaCMS.Design.mbh5
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using ZoomLa.BLL;
    using ZoomLa.BLL.API;
    using ZoomLa.BLL.Design;
    using ZoomLa.BLL.Site;
    using ZoomLa.Common;
    using ZoomLa.Model;
    using ZoomLa.Model.Design;
    using ZoomLa.SQLDAL;
    public partial class Default : System.Web.UI.Page
    {
        B_User buser = new B_User();
        B_Design_Scence pageBll = new B_Design_Scence();
        B_Design_SiteInfo sfBll = new B_Design_SiteInfo();
        B_Design_Tlp tlpBll = new B_Design_Tlp();
        B_Design_RES resBll = new B_Design_RES();
        M_APIResult retMod = new M_APIResult();
        //-------前端使用
        public M_Design_Page pageMod = new M_Design_Page();
        public string extendData = "[]";
        public string comp_global = "[]";
        public string sitecfg = "{}";
        public string Version = "34";
        //Page的Guid
        public string Mid { get { return Request.QueryString["ID"] ?? ""; } }
        public string Source { get { return Request.QueryString["Source"] ?? ""; } }
        public int UseTlp { get { return DataConverter.CLng(Request.QueryString["UseTlp"]); } }
        public int Sid { get { return DataConvert.CLng(ViewState["Sid"]); } set { ViewState["Sid"] = value; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                M_UserInfo mu = buser.GetLogin();
                if (!mu.IsNull && UseTlp > 0)//使用模板
                {
                    M_Design_Page seMod = pageBll.SelModelByTlp(UseTlp);
                    seMod.UserID = mu.UserID;
                    seMod.guid = System.Guid.NewGuid().ToString();
                    seMod.TlpID = 0;
                    seMod.ZType = 0;
                    seMod.UserName = mu.UserName;
                    seMod.CDate = DateTime.Now;
                    seMod.UPDate = DateTime.Now;
                    pageBll.Insert(seMod);
                    Response.Redirect("/MBH5/" + seMod.ID);
                }
                Sid = DataConvert.CLng(B_Route.GetParam("SID", Page));
                pageMod = GetModel();
                if (pageMod == null)//未指定主键和Guid,则跳转至最后一个创建的的场景
                {
                    B_User.CheckIsLogged(Request.RawUrl);
                    pageMod = pageBll.SelModelByDef(mu.UserID);
                    //如果用户没有数据,则新建一个,且的话则取一个跳转
                    if (pageMod == null) { pageMod = pageBll.A_Add(mu); }
                    Response.Redirect("/MBH5/" + pageMod.ID);
                }
                else
                {
                    //通过微信分享链接,或直接输入地址,则进入预览页
                    if (Request.UrlReferrer == null && !(Request["action"] ?? "").Equals("edit")) { Response.Redirect("/H5/" + pageMod.ID); }
                    //-----进入正常编辑页
                    Title_L.Text = pageMod.Title + "-来自[" + mu.TrueName + "]的手机创作";
                    string[] defwx = "/UploadFiles/demo/h4.jpg|/UploadFiles/demo/h5.jpg".Split('|');
                    Wx_Img.ImageUrl = string.IsNullOrEmpty(pageMod.PreviewImg) ? defwx[new Random().Next(0, defwx.Length)] : pageMod.PreviewImg;
                    if (pageMod == null) { function.WriteErrMsg("指定的场景不存在"); }
                    switch (Source)
                    {
                        case "tlp":
                            B_Admin.CheckIsLogged(Request.RawUrl);
                            break;
                        default:
                            if (pageMod.UserID != mu.UserID) { function.WriteErrMsg("你无权修改该场景"); }
                            break;
                    }
                    MyBind();
                }
            }
        }
        public void MyBind()
        {
            DataTable tlpdt = tlpBll.SelWith(0, 1, 12);
            TlpRPT.DataSource = tlpdt;
            TlpRPT.DataBind();
            //------
            DataTable bkdt = resBll.Search("", "bk_h5", "img", "", "", "");
            BKRPT.DataSource = bkdt;
            BKRPT.DataBind();
        }
        //兼容以前分享出去的链接
        public M_Design_Page GetModel()
        {
            if (Sid > 0) { return pageBll.SelReturnModel(Sid); }
            if (!string.IsNullOrEmpty(Mid)) { pageMod = pageBll.SelModelByGuid(Mid); Sid = pageMod.ID; return pageMod; }
            return null;
        }
    }
}