using System.Web.Mvc;

namespace Playground.Areas.EdinK
{
    public class EdinKAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "EdinK";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "EdinK_default",
                "EdinK/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}