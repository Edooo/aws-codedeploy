using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EDISAX;

namespace Playground.Areas.Danb.Controllers
{
    public class EdiController : Controller
    {
        // GET: Danb/Edi/Index
        public ActionResult Index()
        {
            EdiSax sax = new EdiSax();
            sax.Testing();
            return View();
        }
    }
}