using Playground.Models.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Playground.Areas.api.Controllers
{
    public class CompaniesController : Controller
    {
        // GET: api/{version}/companies
        public ActionResult EntireCollection(string version)
        {
            DBAccess db = new DBAccess();
            string defaultFields = "cid,cname,isClient,isCarrier";
            string sql = db.BuildSqlString(Request, "[IntlBridgeDB].[dbo].[Company]", "cId", null, defaultFields);
            object json = db.RunQueryReturnJson(sql, null);
            return Json(json, JsonRequestBehavior.AllowGet);
        }
    }
}