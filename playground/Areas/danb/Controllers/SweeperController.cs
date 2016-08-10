using Playground.Areas.Danb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Playground.Areas.Danb.Controllers
{
    public class SweeperController : Controller
    {
        public static ConwayModel instance = null;

        // GET: Danb/Sweeper
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Conway()
        {
            if (instance == null)
            {
                instance = new ConwayModel(30, 20);
                instance.DebugFillWithRandom();
            } else
            {
                instance.Tick();
            }
            return View(instance);
        }
    }
}