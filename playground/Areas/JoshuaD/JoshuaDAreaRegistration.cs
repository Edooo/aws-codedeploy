using System.Web.Mvc;

namespace Playground.Areas.JoshuaD
{
    public class JoshuaDAreaRegistration 
        : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "JoshuaD";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "JoshuaD_default",
                "JoshuaD/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}