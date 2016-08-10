using System.Web.Mvc;
using System.Web.Routing;


/*
NOTE:
http://stackoverflow.com/questions/10906411/asp-net-web-api-put-delete-verbs-not-allowed-iis-8
To use the PUT and DELETE verbs with the Web API you need to edit %userprofile%\documents\iisexpress\config\applicationhost.config and add the verbs to the ExtensionlessUrl handler as follows:
<add name="ExtensionlessUrl-Integrated-4.0" path="*." verb="GET,HEAD,POST,DEBUG,PUT,DELETE" type="System.Web.Handlers.TransferRequestHandler" preCondition="integratedMode,runtimeVersionv4.0" />
*/

namespace Playground.Areas.api
{
    public class apiAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "api";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "api_entirecollection",
                "api/{version}/{controller}",
                new { action = "EntireCollection" }
            );
            context.MapRoute(
                name:  "api_oneitemfromcollection",
                url: "api/{version}/{controller}/{id}",
                defaults: new { action = "OneItem" },
                constraints: new { id = @"^\d+$" } // id must be all digits
            );
            context.MapRoute(
                name: "api_actiononcollection",
                url: "api/{version}/{controller}/{action}",
                defaults: new { },
                constraints: new { }
            );
            context.MapRoute(
                name: "api_actionononeitem",
                url: "api/{version}/{controller}/{id}/{action}",
                defaults: new { },
                constraints: new { id = @"^\d+$" } // id must be all digits
            );
            //context.MapRoute(
            //    "api_default",
            //    "api/{version}/{controller}/{*path}",
            //    new { action = "Index" }
            //);
            //context.MapRoute(
            //    "api_default",
            //    "api/{version}/{controller}/{id1}/{resource2}/{id2}",
            //    new { action = "Index", id1 = 0, resource2 = "", id2 = 0 }
            //);
        }
    }
}