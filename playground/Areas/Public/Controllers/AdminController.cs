using Playground.Models.db;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using System.Data;
using System.Text.RegularExpressions;

namespace Playground.Areas.Public.Controllers
{
    public class AdminController : Controller
    {
        [PublicAuthorization(Role = "admin")]
        // GET: public/v1/admin
        public ActionResult Index(string version)
        {
            var dict = new Dictionary<string, string>();
            dict.Add("Message", "Success. Try admin/adduser or admin/addrole");
            dict.Add("Version", version);
            return Json(dict, JsonRequestBehavior.AllowGet);
        }

        [PublicAuthorization(Role = "admin")]
        // GET: public/v1/admin/adduser?email=dan.baker@myib.com&userid=3237
        public ActionResult AddUser(string version)
        {
            var dict = new Dictionary<string, object>();

            var queryString = Request.QueryString;
            var email = queryString["email"];
            var userIdString = queryString["userid"];
            int userId = 0;
            int.TryParse(userIdString, out userId);
            if (userId <= 0 || email == null || email.Length <= 0)
            {
                string msg = "Failed to add user.";
                if (email == null || email.Length <= 0) msg += " Missing email address.";
                if (userId <= 0) msg += " Missing userId.";
                dict.Add("Message", msg);
                dict.Add("success", 0);
            }
            else
            {
                DBAccess dba = new DBAccess();
                var rowCount = 0;
                string sql = "SELECT COUNT(*) FROM [NewExtranetDB].[dbo].[PublicApiUser]";
                dba.Query(sql, null, (SqlDataReader row) =>
                {
                    rowCount = (int)row[0];
                    return false;
                });

                bool ok = true;
                string newApiKey = ApiKey.GenerateKey((rowCount+1) * 3);
                Debug.WriteLine("New API Key:" + newApiKey);

                sql = "INSERT INTO [NewExtranetDB].[dbo].[PublicApiUser] (email,SiteUserId,apikey) VALUES (@email,@userid,@apikey)";
                dba.Query(sql, (SqlCommand cmd) =>
                {
                    dba.AddParameter(cmd, email, "@email", SqlDbType.NVarChar);
                    dba.AddParameter(cmd, userId, "@userid", SqlDbType.Int);
                    dba.AddParameter(cmd, newApiKey, @"apikey", SqlDbType.NVarChar);
                }, null, (string msg,Exception e) => {
                    ok = false;
                    dict.Add("Message", "Failed to add user: " + msg);
                    dict.Add("success", 0);
                    dict.Add("Version", version);
                });
                if (ok)
                {
                    dict.Add("Message", "Success added user");
                    dict.Add("success", 1);
                    dict.Add("apikey", newApiKey);
                }
            }

            return Json(dict, JsonRequestBehavior.AllowGet);
        }

        [PublicAuthorization(Role = "admin")]
        // GET: public/v1/admin/addrole?name=admin
        public ActionResult AddRole(string version)
        {
            var dict = new Dictionary<string, object>();

            var queryString = Request.QueryString;
            var name = queryString["name"];
            if (name == null || name.Length <= 0)
            {
                string msg = "Failed to add role.";
                if (name == null || name.Length <= 0) msg += " Missing role name (?name=printing)";
                dict.Add("Message", msg);
                dict.Add("success", 0);
            }
            else
            {
                DBAccess dba = new DBAccess();
                name = name.ToLower();
                Regex rgx = new Regex("[^a-zA-Z0-9]");
                name = rgx.Replace(name, string.Empty);
                bool ok = true;

                string sql = "INSERT INTO [NewExtranetDB].[dbo].[PublicApiRole] (Name) VALUES (@name)";
                dba.Query(sql, (SqlCommand cmd) =>
                {
                    dba.AddParameter(cmd, name, "@name", SqlDbType.NVarChar);
                }, null, (string msg, Exception e) => {
                    ok = false;
                    dict.Add("Message", "Failed to add role: " + msg);
                    dict.Add("success", 0);
                    dict.Add("Version", version);
                });
                if (ok)
                {
                    dict.Add("Message", "Success added role: " + name);
                    dict.Add("success", 1);
                }
            }

            return Json(dict, JsonRequestBehavior.AllowGet);
        }
    }
}