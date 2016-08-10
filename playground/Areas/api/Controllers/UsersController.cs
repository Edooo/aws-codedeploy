using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using System.Web.Script.Serialization;

using System.Data.SqlClient;
using Playground.Models.db;
using System.Data;
using System.Net;
using System.Text;
using System.IO;

namespace Playground.Areas.api.Controllers
{
    public class UsersController : Controller
    {
        // GET: /api/{version}/users
        public ActionResult EntireCollection(string version)
        {
            Debug.WriteLine("All Users Version: " + version );
            switch (version.ToLower())
            {
                case "v1":
                    return v1AllUsers();
                case "v2":
                    return v2AllUsers();
            }
            return HttpNotFound();
        }

        [AcceptVerbs("GET", "POST", "DELETE", "PUT")]
        // GET: /api/{version}/users/{id}
        public ActionResult OneItem(string version, int id)
        {
            Debug.WriteLine("OneUser Version: " + version + "  UserId: " + id + "  Verb: " + Request.HttpMethod.ToUpperInvariant());

            switch (Request.HttpMethod.ToUpperInvariant())
            {
                case "DELETE":
                    return v1OneUser(id);
//                    return HttpNotFound();
                case "PUT":
                case "POST":
                    return v1OneUser(id);
//                    return HttpNotFound();
                case "GET":
                    switch (version.ToLower())
                    {
                        case "v1":
                            return v1OneUser(id);
                    }
                    break;
            }
            return HttpNotFound();
        }

        [AcceptVerbs("GET", "POST")]
        // GET: /api/{version}/users/{action}
        public ActionResult Reindex(string version)
        {
            Debug.WriteLine("REINDEX Version: " + version);
            switch (version.ToLower())
            {
                case "v1":
                    return Json("ok", JsonRequestBehavior.AllowGet);
            }
            return HttpNotFound();
        }

        private ActionResult v1OneUser(int userid)
        {
            Dictionary<string, string> info = new Dictionary<string, string>();
            info.Add("FirstName", "Dan");
            info.Add("LastName", "Baker");
            info.Add("id", userid.ToString());
            return Json(info, JsonRequestBehavior.AllowGet);
        }

        private ActionResult v1AllUsers()
        {
            Dictionary<string, List<Dictionary<string, object>>> info;
            info = new Dictionary<string, List<Dictionary<string,object>>>();
            List<Dictionary<string, object>> users = new List<Dictionary<string, object>>();
            for(int i=12; i<14; i++)
            {
                Dictionary<string, object> oneUser = new Dictionary<string, object>();
                oneUser.Add("FirstName", "Dan");
                oneUser.Add("LastName", "Baker");
                oneUser.Add("id", i);
                users.Add(oneUser);
            }
            info.Add("users", users);
            return Json(info, JsonRequestBehavior.AllowGet);
        }




        private ActionResult v2AllUsers()
        {
            Dictionary<string, List<Dictionary<string, object>>> info;
            info = new Dictionary<string, List<Dictionary<string, object>>>();


            //tryConnectToServer();

            tryDatabaseVoodoo("rID");



            return Json(info, JsonRequestBehavior.AllowGet);
        }

        private void tryDatabaseVoodoo(string orderByFieldName)
        {
            string orderby = orderByFieldName;
            string fields = null;   
            int limitN = 1000;
            int offsetN = 0;
            foreach (string key in Request.QueryString.Keys)
            {
                if (key.ToLower() == "limit")
                {
                    string limitString = Request.QueryString[key];
                    if (limitString != null && limitString.Length > 0)
                    {
                        int.TryParse(limitString, out limitN);
                    }
                }
                else if (key.ToLower() == "offset")
                {
                    string offsetString = Request.QueryString[key];
                    if (offsetString != null && offsetString.Length > 0)
                    {
                        int.TryParse(offsetString, out offsetN);
                    }
                }
                else if (key.ToLower() == "orderby")
                {
                    string orderByString = Request.QueryString[key];
                    orderby = new string((from c in orderByString
                                          where char.IsLetterOrDigit(c)
                                                        select c
                           ).ToArray());
                }
                else if (key.ToLower() == "fields")
                {
                    fields = null;
                    string fieldNamesString = Request.QueryString[key];
                    string[] fieldNames = fieldNamesString.Split(',');
                    foreach(string oneFieldName in fieldNames)
                    {
                        string cleanFieldName = new string((from c in oneFieldName
                                                    where char.IsLetterOrDigit(c)
                                                    select c
                               ).ToArray());
                        if (fields == null)
                        {
                            fields = cleanFieldName;
                        } else
                        {
                            fields += "," + cleanFieldName;
                        }
                    }
                }
                else
                {
                    // could do: /cars?color=Blue,Green&doors=2,4&fields=a,b,c&orderby=color

                    Debug.WriteLine("Key:" + key + " = " + Request.Params[key]);
                }
            }
            if (fields == null) fields = "*";




            string sql = "SELECT " + fields + " FROM [NewExtranetDB].[dbo].[LRailcars]" +
                " WHERE rID>=@Param1 AND rID <=@Param2" +
                " ORDER BY " + orderby + // Note: must have an OrderBy to use offset and limit
                " OFFSET " + offsetN + " ROWS" +
                " FETCH NEXT " + limitN + " ROWS ONLY";
            Debug.WriteLine("SQL:" + sql);
            DBAccess dba = new DBAccess();
            string json = "{\"rows\":[";
            int jsonRows = 0;
            dba.Query(sql, (SqlCommand cmd) =>
            {
                SqlParameter myParam1 = new SqlParameter("@Param1", SqlDbType.Int);
                myParam1.Value = 2;
                cmd.Parameters.Add(myParam1);
                SqlParameter myParam2 = new SqlParameter("@Param2", SqlDbType.Int);
                myParam2.Value = 13;
                cmd.Parameters.Add(myParam2);
            }, (SqlDataReader row) =>
            {
                jsonRows++;
                if (jsonRows > 1) json += ",";
                json += "{";
                for (int i = 0; i < row.FieldCount; i++)
                {
                    if (i > 0) json += ",";
                    json += "\"" + row.GetName(i) + "\":";
                    Type fieldType = row.GetFieldType(i);
                    if (fieldType == typeof(System.Int32))
                    {
                        json += row[i];
                    }
                    else
                    {
                        json += "\"" + row[i] + "\"";
                    }
                    //Debug.WriteLine("..." + row.GetName(i) + " = " + row[i] + " .. type=" + row.GetFieldType(i));
                }
                json += "}";
                return false;
            });
            json += "]}";
            Debug.WriteLine("JSON:" + json);
        }

        private void tryConnectToServer()
        {
            Debug.WriteLine("trying to connect to server to get a session id cookie");
            try
            {
                var httpPath = "https://extranet.myib.com/Default.aspx";
                WebRequest request = WebRequest.Create(httpPath);
                // Set the Method property of the request to POST.
                request.Method = "POST";
                string postData = "tbUsername=dan.baker&tbPassword=junk";
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = byteArray.Length;
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Debug.WriteLine(((HttpWebResponse)response).StatusDescription);

                dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                //Debug.WriteLine(responseFromServer);

                foreach (Cookie cook in response.Cookies)
                {
                    Debug.WriteLine("COOKIE:" + cook.Name + ": " + cook.Value);
                }

                reader.Close();
                dataStream.Close();
                response.Close();

            }
            catch (Exception e)
            {
                Debug.WriteLine("HTTP Exception: " + e.Message + "\r\n" + e.ToString());
            }
        }

    }
}
