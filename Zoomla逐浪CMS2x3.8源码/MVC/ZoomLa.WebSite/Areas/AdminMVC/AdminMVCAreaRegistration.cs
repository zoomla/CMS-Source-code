using System.Web.Mvc;

namespace ZoomLaCMS.Areas.AdminMVC
{
    public class AdminMVCAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "AdminMVC";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "AdminMVC_default",
                "AdminMVC/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
