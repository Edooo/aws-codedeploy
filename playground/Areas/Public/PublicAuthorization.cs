using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using System.Net.Http;
using Playground.Models.db;
using System.Data.SqlClient;

namespace Playground.Areas.Public
{
    public class PublicAuthorization : AuthorizeAttribute
    {
        public string Role { get; set; }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var url = filterContext.HttpContext.Request.Url;
            var path = url.AbsolutePath;
            if (Role == null || Role.Length <= 0)
            {
                SetUnauthorizedResult(filterContext, "Programmer: Please add [PublicAuthorization(Role=\"any\")] to your endpoint: " + path);
            }
            else if (Role.ToLower().CompareTo(Role) != 0)
            {
                SetUnauthorizedResult(filterContext, "Programmer: Please make sure your Roles(" + Role + ") are always lowercase. Check your endpoint: " + path);
            }
            else
            {
                var queryString = filterContext.HttpContext.Request.QueryString;
                var apikey = queryString.Get("apikey");
                if (apikey == null || apikey.Length <= 0)
                {
                    SetUnauthorizedResult(filterContext, "Please include your API Key in your request: " + path + "?apikey=abc123");
                }
                else
                {   // Check if their APIKey is valid
                    var dba = new DBAccess();
                    var sql = "SELECT * FROM [NewExtranetDB].[dbo].[PublicApiUser] WHERE ApiKey=@apikey";
                    int ApiUserId = (int)Query(filterContext, dba, sql, "Id", 0, (SqlCommand cmd) =>
                    {
                        dba.AddParameter(cmd, apikey, "@apikey", System.Data.SqlDbType.NVarChar);
                    });

                    if (ApiUserId == 0)
                    {
                        SetUnauthorizedResult(filterContext, "Your apikey(" + apikey + ") is invalid.");
                    }
                    else
                    {   // check if their apiKey allows them access to the Role
                        if (Role.CompareTo("any") == 0)
                        {
                            // OK
                        }
                        else
                        {
                            sql = "SELECT * FROM [NewExtranetDB].[dbo].[PublicApiUserRole] JOIN [NewExtranetDB].[dbo].[PublicApiRole] ON PublicApiRoleId = PublicApiRole.Id WHERE PublicApiUserId = @userid AND Name = @name";
                            int ok = (int)Query(filterContext, dba, sql, "Id", 0, (SqlCommand cmd) =>
                            {
                                dba.AddParameter(cmd, ApiUserId, "@userid", System.Data.SqlDbType.Int);
                                dba.AddParameter(cmd, Role, "@name", System.Data.SqlDbType.NVarChar);
                            });
                            if (ok == 0)
                            {
                                SetUnauthorizedResult(filterContext, "Your apikey(" + apikey + ") is not authorized for the endpoint: " + path);
                            }
                        }
                    }
                }
            }
        }

        private object Query(AuthorizationContext filterContext, DBAccess dba, string sql, string columnName, object defaultValue, Action<SqlCommand> fncAddParameters)
        {
            object value = defaultValue;
            dba.Query(sql, fncAddParameters, (SqlDataReader row) =>
            {
                value = row[columnName];
                return true;
            }, (string msg, Exception ex) =>
            {
                Debug.WriteLine("Exception: " + msg);
                SetUnauthorizedResult(filterContext, "Server Exception: " + msg);
            });
            return value;
        }

        private void SetUnauthorizedResult(AuthorizationContext filterContext, string result)
        {
            if (filterContext.Result == null)
            {
                filterContext.Result = new HttpUnauthorizedResult(result);
            }
        }
    }
}