using System.Web.Mvc;

namespace Playground.Areas.Public
{
    public class PublicAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "public";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                name: "public_indexaction",
                url: "public/{version}/{controller}/{action}",
                defaults: new { action = "Index", version="v1" },
                constraints: new {  }
            );
        }
    }
}