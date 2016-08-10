using System.Web.Mvc;

namespace Playground.Areas.LukaM
{
    public class LukaMAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "LukaM";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "LukaM_default",
                "LukaM/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}