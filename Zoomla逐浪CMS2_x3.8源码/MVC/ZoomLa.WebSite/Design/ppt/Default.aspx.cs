namespace ZoomLaCMS.Design.ppt
{
    using System;
    using System.Data;
    using ZoomLa.BLL;
    using ZoomLa.BLL.API;
    using ZoomLa.BLL.Design;
    using ZoomLa.Common;
    using ZoomLa.Model;
    using ZoomLa.Model.Design;

    public partial class Default : System.Web.UI.Page
    {
        B_User buser = new B_User();
        B_Design_PPT pptBll = new B_Design_PPT();
        B_Design_Tlp tlpBll = new B_Design_Tlp();
        M_APIResult retMod = new M_APIResult();
        B_Design_Scence seBll = new B_Design_Scence();
        //-------前端使用
        public M_Design_Page pageMod = new M_Design_Page();
        public string extendData = "[]";
        public string comp_global = "[]";
        public string sitecfg = "{}";
        //Page的Guid
        public string Mid { get { return Request.QueryString["ID"] ?? ""; } }
        public string Source { get { return Request.QueryString["Source"] ?? ""; } }
        // 要使用哪个模板创建,传了该参则不需要Mid
        public int UseTlp { get { return DataConverter.CLng(Request.QueryString["UseTlp"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                B_User.CheckIsLogged(Request.RawUrl);
                M_UserInfo mu = buser.GetLogin();
                //引用模板
                if (UseTlp > 0)
                {
                    M_Design_Page seMod = pptBll.SelModelByTlp(UseTlp);
                    seMod.UserID = mu.UserID;
                    seMod.guid = System.Guid.NewGuid().ToString();
                    seMod.TlpID = 0;
                    seMod.ZType = 0;
                    seMod.UserName = mu.UserName;
                    seMod.CDate = DateTime.Now;
                    seMod.UPDate = DateTime.Now;
                    pptBll.Insert(seMod);
                    Response.Redirect("default.aspx?ID=" + seMod.guid);
                }
                ////如果用户没有数据,则新建一个
                //DataTable dt = seBll.A_Sel(mu.UserID, "ppt");
                //if (dt.Rows.Count < 1) {pageMod= seBll.A_Add(mu, "ppt");Response.Redirect("/design/ppt/?id="+pageMod.guid); }
                //用户未传入Mid
                if (string.IsNullOrEmpty(Mid))
                {
                    DataTable scenceDt = pptBll.Sel(mu.UserID);
                    slist_div.Visible = true;
                    RPT.DataSource = scenceDt;
                    RPT.DataBind();
                }
                else
                {
                    pageMod = pptBll.SelModelByGuid(Mid);
                    if (pageMod == null) { function.WriteErrMsg("指定的PPT不存在"); }
                    switch (Source)
                    {
                        case "tlp":
                            B_Admin.CheckIsLogged(Request.RawUrl);
                            break;
                        default:
                            if (pageMod.UserID != mu.UserID) { function.WriteErrMsg("你无权修改该PPT"); }
                            break;
                    }
                }
            }
        }
    }
}