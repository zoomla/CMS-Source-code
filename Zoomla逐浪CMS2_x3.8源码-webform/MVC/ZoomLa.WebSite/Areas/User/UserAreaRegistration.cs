using System.Web.Mvc;

namespace ZoomLaCMS.Areas.User
{
    public class UserAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "User";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute("User_Default", "User/Default", new { controller = "Index", action = "Index" });
            context.MapRoute("User_Register", "User/Register", new { controller = "Index", action = "Register" });
            context.MapRoute("User_Login", "User/Login", new { controller = "Index", action = "Login" });
            context.MapRoute("User_Logout", "User/Logout", new { controller = "User", action = "Logout" });
            context.MapRoute("User_Index", "User/{controller}/{action}/{id}", new { controller = "Index", action = "Index", id = UrlParameter.Optional });
        }
    }
}
