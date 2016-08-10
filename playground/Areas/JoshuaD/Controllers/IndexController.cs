using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Playground.Areas.JoshuaD.Controllers
{
    public class IndexController : Controller
    {
        /// <summary>
        /// Get the Index.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
    }
}