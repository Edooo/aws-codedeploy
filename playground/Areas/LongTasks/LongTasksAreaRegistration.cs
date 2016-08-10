using System.Web.Mvc;

namespace Playground.Areas.LongTasks
{
    public class LongTasksAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "LongTasks";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "LongTasks_default",
                "LongTasks/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}