using System;
using System.IO;
using System.Web.Caching;
using System.Web.Configuration;
using System.Web.UI;
using ZoomLa.BLL;
using ZoomLa.BLL.User;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.Model.User;

//显示用户的个人空间模板页

namespace ZoomLaCMS.Space
{
    public partial class Default : System.Web.UI.Page
    {
        B_CreateHtml bll = new B_CreateHtml();
        B_User_BlogStyle bsBll = new B_User_BlogStyle();
        B_User buser = new B_User();
        M_User_BlogStyle bsMod = null;
        private int Uid { get { return DataConverter.CLng(Request.QueryString["id"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            string errtitle = "<h3 class='panel-title'><span class='fa fa-exclamation-circle'></span> 不正确的访问</h3>";
            int id = DataConverter.CLng(Request.QueryString["id"]);
            M_UserInfo mu = buser.SelReturnModel(Uid);
            if (mu.IsNull) { function.WriteErrMsg(errtitle, "主页ID违规，请使用/Space/Default.aspx?id=[uid]方式访问", ""); }
            else if (mu.State != 2) { function.WriteErrMsg(errtitle, "未通过认证会员无法开启个人主页! !", ""); }
            else if (mu.PageID < 1) { function.WriteErrMsg(errtitle, "用户未指定模板", ""); }
            bsMod = bsBll.SelReturnModel(mu.PageID);
            if (bsMod == null) { function.WriteErrMsg(errtitle, "指定的风格不存在", ""); }
            string tlp = function.VToP(SiteConfig.SiteOption.TemplateDir + bsMod.UserIndexStyle);
            if (!File.Exists(tlp)) { function.WriteErrMsg(errtitle, "模板文件[" + bsMod.UserIndexStyle + "]不存在", ""); }
            string html = SafeSC.ReadFileStr(tlp);
            string ContentHtml = bll.CreateHtml(html);
            Response.Write(ContentHtml);
        }
    }
}