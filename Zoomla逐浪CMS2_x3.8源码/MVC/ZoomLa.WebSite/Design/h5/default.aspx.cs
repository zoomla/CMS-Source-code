namespace ZoomLaCMS.Design.h5
{
    using System;
    using System.Data;
    using ZoomLa.BLL;
    using ZoomLa.BLL.API;
    using ZoomLa.BLL.Design;
    using ZoomLa.Common;
    using ZoomLa.Model;
    using ZoomLa.Model.Design;
    public partial class _default : System.Web.UI.Page
    {
        B_User buser = new B_User();
        B_Design_Scence pageBll = new B_Design_Scence();
        B_Design_Tlp tlpBll = new B_Design_Tlp();
        M_APIResult retMod = new M_APIResult();
        //-------前端使用
        public M_Design_Page pageMod = new M_Design_Page();
        public string extendData = "[]";
        public string comp_global = "[]";
        public string sitecfg = "{}";
        //Page的Guid
        public string Mid { get { return Request.QueryString["ID"] ?? ""; } }
        public string Source { get { return Request.QueryString["Source"] ?? ""; } }
        // 要使用哪个模板创建场景,传了该参则不需要Mid
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
                    M_Design_Page seMod = pageBll.SelModelByTlp(UseTlp);
                    seMod.UserID = mu.UserID;
                    seMod.guid = System.Guid.NewGuid().ToString();
                    seMod.TlpID = 0;
                    seMod.ZType = 0;
                    seMod.UserName = mu.UserName;
                    seMod.CDate = DateTime.Now;
                    seMod.UPDate = DateTime.Now;
                    pageBll.Insert(seMod);
                    Response.Redirect("default.aspx?ID=" + seMod.guid);
                }
                ////如果用户没有数据,则新建一个
                //DataTable dt = pageBll.A_Sel(mu.UserID);
                //if (dt.Rows.Count < 1) { pageBll.A_Add(mu); }
                //用户未传入Mid
                if (string.IsNullOrEmpty(Mid) || Mid.Equals("[gid]"))
                {
                    DataTable scenceDt = pageBll.U_Sel(mu.UserID);
                    slist_div.Visible = true;
                    RPT.DataSource = scenceDt;
                    RPT.DataBind();
                }
                else
                {
                    pageMod = pageBll.SelModelByGuid(Mid);
                    if (pageMod == null) { function.WriteErrMsg("指定的场景不存在"); }
                    Title_L.Text = pageMod.Title;
                    switch (Source)
                    {
                        case "tlp":
                            B_Admin.CheckIsLogged(Request.RawUrl);
                            break;
                        default:
                            if (pageMod.UserID != mu.UserID) { function.WriteErrMsg("你无权修改该场景"); }
                            break;
                    }
                    //用户可选择关闭
                    //if (DeviceHelper.GetBrower() != DeviceHelper.Brower.Chrome) { function.Script(this, "ShowIE();"); return; }
                    MyBind();
                }
            }
        }
        private void MyBind()
        {
            DataTable dt = tlpBll.SelWith(0, 1, 12);
            Tlp_RPT.DataSource = dt;
            Tlp_RPT.DataBind();
        }
    }
}