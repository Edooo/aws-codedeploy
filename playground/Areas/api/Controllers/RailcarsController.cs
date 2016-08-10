using Playground.Models.db;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Playground.Areas.api.Controllers
{
    public class RailcarsController : Controller
    {
        // GET: api/{version}/railcars
        public ActionResult EntireCollection(string version)
        {
            DBAccess db = new DBAccess();
            string sql = db.BuildSqlString(Request, "[NewExtranetDB].[dbo].[LRailcars]", "rName");
            object json = db.RunQueryReturnJson(sql, null);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs("GET", "POST", "DELETE", "PUT")]
        // GET: /api/{version}/railcars/{id}
        public ActionResult OneItem(string version, int id)
        {
            switch (Request.HttpMethod.ToUpperInvariant())
            {
                case "DELETE":
                      return HttpNotFound();
                case "PUT":
                case "POST":
                    return HttpNotFound();
                case "GET":
                    return OneItemRailcar(id);
            }
            return HttpNotFound();
        }

        private ActionResult OneItemRailcar(int id)
        {
            DBAccess db = new DBAccess();
            string whereClause = "rID=@id";
            string sql = db.BuildSqlString(Request, "[NewExtranetDB].[dbo].[LRailcars]", "rName", whereClause);
            object json = db.RunQueryReturnJson(sql, (SqlCommand cmd) =>
            {
                SqlParameter myParam1 = new SqlParameter("@id", SqlDbType.Int);
                myParam1.Value = id;
                cmd.Parameters.Add(myParam1);
            });
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        // GET: /api/{version}/railcars/{id}/users
        public ActionResult Users(string version, int id)
        {
            // @TODO: return all users associated with this one railcar
            string json = "{\"Info\":\"YES\", \"id\":\"" + id + "\"}";
            return Json(json, JsonRequestBehavior.AllowGet);
        }
    }
}
