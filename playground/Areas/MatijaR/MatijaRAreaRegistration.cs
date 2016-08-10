using System.Web.Mvc;

namespace Playground.Areas.MatijaR
{
    public class MatijaRAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "MatijaR";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "MatijaR_default",
                "MatijaR/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}