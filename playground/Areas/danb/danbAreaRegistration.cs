using System.Web.Mvc;

namespace Playground.Areas.Danb
{
    public class DanbAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "danb";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "danb_default",
                "danb/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}