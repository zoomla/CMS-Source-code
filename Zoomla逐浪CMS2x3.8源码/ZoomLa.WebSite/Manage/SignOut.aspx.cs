namespace ZoomLa.WebSite
{
    using System;
    using System.Data;
    using System.Collections;
    using System.Web;
    using ZoomLa.BLL;
    using ZoomLa.Model;
    public partial class _SingOut : CustomerPageAction
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            M_AdminInfo model = B_Admin.GetLogin();
            string url = Request.QueryString["ReturnUrl"];
            ZLLog.ToDB(ZLEnum.Log.alogin, new M_Log()
            {
                UName = model.AdminName,
                Action = "退出登录",
                Message = "退出登录"
            });
            B_Admin.ClearLogin();
            if (!string.IsNullOrEmpty(url))
            {
                Response.Write("<script>location.href='" + url + "'</script>");
            }
            else
            {
                HttpContext.Current.Response.Redirect("login.aspx");
            }
        }
    }
}