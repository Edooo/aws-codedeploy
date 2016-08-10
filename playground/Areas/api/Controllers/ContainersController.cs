using Playground.Models.db;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Playground.Areas.api.Controllers
{
    public class ContainersController : Controller
    {
        // GET: api/{version}/containers?originid=123&shipdatestart=2015-12-25&shipdateend=2015-12-27
        public ActionResult EntireCollection(string version)
        {
            DBAccess db = new DBAccess();
            string extraSql = "JOIN [IntlBridgeDB].[dbo].[LCanType] ON ContainerType = ctId AND ctClass IN(1,5,7)";
            int originid = db.GetRequestQueryInt(Request, "originid", 0);
            if (originid > 0)
            {
                extraSql += " JOIN [IntlBridgeDB].[dbo].[Facilities] on oid = fId AND oid = " + originid;
            }
            DateTime? shipDateStart = db.GetRequestQueryDateTime(Request, "shipdatestart", null);
            DateTime? shipDateEnd = db.GetRequestQueryDateTime(Request, "shipdateend", null);
            string whereClause = null;
            if (shipDateStart != null && shipDateEnd != null)
            {
                //whereClause = "ShipDate >= @shipDateStart AND ShipDate < @shipDateEnd";
                whereClause = "ShipDate >= '" + shipDateStart + "' AND ShipDate < '" + shipDateEnd + "'";
            }

            string sql = db.BuildSqlString(Request, "[IntlBridgeDB].[dbo].[BoxInfo]", "BoxInfoID", whereClause, null, extraSql);
            object json = db.RunQueryReturnJson(sql, (SqlCommand cmd) =>
            {
                if (shipDateStart != null && shipDateEnd != null)
                {
                    db.AddParameter(cmd, shipDateStart, "shipDateStart", System.Data.SqlDbType.DateTime);
                    db.AddParameter(cmd, shipDateEnd, "shipDateEnd", System.Data.SqlDbType.DateTime);
                }
            });
            return Json(json, JsonRequestBehavior.AllowGet);
        }
    }
}