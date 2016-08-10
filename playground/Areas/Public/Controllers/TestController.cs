using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Playground.Areas.Public.Controllers
{
    [PublicAuthorization(Role = "any")]
    public class TestController : Controller
    {
        // GET: /public/v1/test
        public ActionResult Index(string version)
        {
            var dict = new Dictionary<string, string>();
            dict.Add("Message", "Success");
            dict.Add("Version", version);
            return Json(dict, JsonRequestBehavior.AllowGet);
        }
    }
}
