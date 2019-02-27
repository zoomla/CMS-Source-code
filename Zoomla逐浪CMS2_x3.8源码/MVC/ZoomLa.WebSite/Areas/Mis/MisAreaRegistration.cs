using System.Web.Mvc;

namespace ZoomLaCMS.Areas.Mis
{
    public class MisAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Mis";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute("Mis_Index", "Mis/Default", new { controller = "Home", action = "Default" });
            context.MapRoute(
                "Mis_default",
                "Mis/{controller}/{action}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        
        }
    }
}
