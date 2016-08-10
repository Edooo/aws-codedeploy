using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;

using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Playground.Models.db
{
    public class DBAccess
    {
        protected string sqlPieceTable;
        protected string sqlPieceWhere;
        protected string sqlPieceFields;
        protected string sqlPieceOrderBy;
        protected int sqlPieceLimit;
        protected int sqlPieceOffset;

        protected List<string> columnNames = null;
        protected List<bool> columnQuoted = null;
        protected List<bool> columnNumber = null;
        protected List<bool> columnDate = null;
        protected List<SqlDbType> columnType = null;
        protected List<string> parameterizedNames = new List<string>();         // "FirstName"  "OrderDate" "Salary"
        protected List<Object> parameterizedValues = new List<Object>();        // "Dan"        2016-10-31  10000
        protected List<SqlDbType> parameterizedTypes = new List<SqlDbType>();   // 

        public DBAccess()
        {

        }

        // sql = "SELECT TOP 1000 * FROM [NewExtranetDB].[dbo].[LRailcars]"
        public void Query(string sql, Action<SqlCommand> fncAddParameters, Func<SqlDataReader, bool> fncProcessOneRow, Action<string, Exception> fncException = null)
        {
            // Get Connection String from web.config
            var key = ConfigurationManager.AppSettings["connStringVarName"];
            var connInfo = ConfigurationManager.ConnectionStrings[key];
            var connString = connInfo.ConnectionString;

            // establish connection to database; run query; process results
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(connString))
                {
                    dbConnection.Open();
                    SqlCommand dbCmd = new SqlCommand(sql, dbConnection);
                    for(int i=0; i<parameterizedNames.Count; i++)
                    {
                        string fieldName = parameterizedNames[i];
                        object fieldValue = parameterizedValues[i];
                        string paramKey = "p" + (i + 1);
                        SqlDbType typ = parameterizedTypes[i];
                        AddParameter(dbCmd, fieldValue, paramKey, typ);
                    }
                    if (fncAddParameters != null)
                    {
                        fncAddParameters(dbCmd);
                    }
                    var rowReader = dbCmd.ExecuteReader();
                    while (rowReader.Read())
                    {
                        if (fncProcessOneRow != null)
                        {
                            bool stopNow = fncProcessOneRow(rowReader);
                            if (stopNow) break;
                        }
                        else break;
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Database Exception: " + e.Message + "\r\n" + e.ToString());
                if (fncException != null)
                {
                    fncException(e.Message, e);
                }
                else
                {
                    throw e;
                }
            }
        }

        public int GetRequestQueryInt(HttpRequestBase Request, string key, int defaultValue)
        {
            string value = Request.QueryString[key];
            int n;
            if (int.TryParse(value, out n))
            {
                return n;
            }
            return defaultValue;
        }

        public DateTime? GetRequestQueryDateTime(HttpRequestBase Request, string key, DateTime? defaultValue)
        {
            string value = Request.QueryString[key];
            DateTime returnValue;
            if (!DateTime.TryParse(value, out returnValue))
            {
                return defaultValue;
            }
            return returnValue;
        }

        public void PrepareForSqlStringBuilding(HttpRequestBase Request, string tableName, string orderByFieldName, string whereClause = null, string defaultFieldList = null)
        {
            sqlPieceTable = tableName;
            sqlPieceOrderBy = orderByFieldName;
            sqlPieceFields = defaultFieldList;
            sqlPieceLimit = 1000;
            sqlPieceOffset = 0;
            string urlWhereClause = "";
            string argWhereClause = (whereClause != null ? whereClause : "");
            foreach (string key in Request.QueryString.Keys)
            {
                if (key.ToLower() == "limit")
                {
                    string limitString = Request.QueryString[key];
                    if (limitString != null && limitString.Length > 0)
                    {
                        int.TryParse(limitString, out sqlPieceLimit);
                    }
                }
                else if (key.ToLower() == "offset")
                {
                    string offsetString = Request.QueryString[key];
                    if (offsetString != null && offsetString.Length > 0)
                    {
                        int.TryParse(offsetString, out sqlPieceOffset);
                    }
                }
                else if (key.ToLower() == "orderby")
                {
                    string orderByString = Request.QueryString[key];
                    sqlPieceOrderBy = new string((from c in orderByString
                                          where char.IsLetterOrDigit(c)
                                          select c
                           ).ToArray());
                }
                else if (key.ToLower() == "fields")
                {
                    sqlPieceFields = null;
                    string fieldNamesString = Request.QueryString[key];
                    string[] fieldNames = fieldNamesString.Split(',');
                    foreach (string oneFieldName in fieldNames)
                    {
                        string cleanFieldName = new string((from c in oneFieldName
                                                            where char.IsLetterOrDigit(c) || c == '.'
                                                            select c
                               ).ToArray());
                        if (sqlPieceFields == null)
                        {
                            sqlPieceFields = cleanFieldName;
                        }
                        else
                        {
                            sqlPieceFields += "," + cleanFieldName;
                        }
                    }
                    if (sqlPieceFields == null || sqlPieceFields.Length <= 0)
                    {
                        sqlPieceFields = defaultFieldList;
                    }
                }
                else
                {
                    // could do: /cars?color=Blue,Green&doors=2,4&fields=a,b,c&orderby=color
                    if (columnNames == null)
                    {
                        columnNames = ColumnNamesForTable(tableName, out columnQuoted, out columnNumber, out columnDate, out columnType);
                        for(int i=0; i<columnNames.Count; i++)
                        {
                            Debug.WriteLine("" + columnNames[i] + " " + columnQuoted[i] + "," + columnNumber[i] + "," + columnDate[i]);
                        }
                    }
                    // @TODO: Check for special postfix: _gt, _gte, _lt, _lte, _eq, _neq (comparison operators)
                    string fieldName = key;
                    string operatorString = null;
                    if (fieldName.EndsWith("_gt"))
                    {
                        fieldName = fieldName.Substring(0, fieldName.Length - 3);
                        operatorString = ">";
                    }
                    if (fieldName.EndsWith("_gte"))
                    {
                        fieldName = fieldName.Substring(0, fieldName.Length - 4);
                        operatorString = ">=";
                    }
                    if (fieldName.EndsWith("_lt"))
                    {
                        fieldName = fieldName.Substring(0, fieldName.Length - 3);
                        operatorString = "<";
                    }
                    if (fieldName.EndsWith("_lte"))
                    {
                        fieldName = fieldName.Substring(0, fieldName.Length - 4);
                        operatorString = "<=";
                    }
                    int idx = columnNames.IndexOf(fieldName);
                    if (idx >= 0)
                    {
                        if (urlWhereClause.Length > 0)
                        {
                            urlWhereClause += " AND ";
                        }
                        string originalParam = Request.Params[key];
                        string[] originalValues = originalParam.Split(',');

                        if (operatorString != null || originalValues.Length == 1)
                        {   // parameterize
                            if (operatorString == null)
                            {
                                operatorString = "=";
                            }
                            parameterizedNames.Add(fieldName);
                            parameterizedValues.Add(originalParam);
                            SqlDbType fieldType = columnType[idx];
                            parameterizedTypes.Add(fieldType);
                            int paramIndex = parameterizedValues.Count;         // @p1 : FirstName = 'Dan'
                            urlWhereClause += "" + fieldName + operatorString + "@p" + (paramIndex);
                        }
                        else
                        {   // special "IN" case (cleans the values, and build the values to compare against)
                            List<string> cleansedValues = new List<string>();
                            foreach (string value in originalValues)
                            {
                                string v = value.Replace("\"", "");
                                v = v.Replace("\'", string.Empty);
                                if (columnQuoted[idx])
                                {
                                    v = "\'" + v + "\'";
                                }
                                cleansedValues.Add(v);
                            }
                            string cleansed = string.Join(",", cleansedValues.ToArray());
                            urlWhereClause += "" + fieldName + " IN (" + cleansed + ")"; // @TODO: Learn how to parameterize a "list"
                        }
                    }
                    else
                    {
                        // Note: other special arguments are passed in on the URL ... just ignore them here
                        //Debug.WriteLine("??? Why did the user pass this URL parameter ???");
                    }

                    Debug.WriteLine("Key:" + key + " = " + Request.Params[key]);
                }
            }
            if (sqlPieceFields == null) sqlPieceFields = "*";

            sqlPieceWhere = "";
            if (argWhereClause.Length > 0 || urlWhereClause.Length > 0)
            {
                sqlPieceWhere = " WHERE ";
                if (argWhereClause.Length > 0) sqlPieceWhere += argWhereClause;
                if (argWhereClause.Length > 0 && urlWhereClause.Length > 0) sqlPieceWhere += " AND ";
                if (urlWhereClause.Length > 0) sqlPieceWhere += urlWhereClause;
            }
        }

        // tableName = "[NewExtranetDB].[dbo].[LRailcars]"
        // ordeByFieldName = "rName"
        // whereClause = "rID>=@Param1 AND rID <=@Param2"
        public string BuildSqlString(HttpRequestBase Request, string tableName, string orderByFieldName, string whereClause=null, string defaultFieldList=null, string joinClause=null)
        {
            PrepareForSqlStringBuilding(Request, tableName, orderByFieldName, whereClause, defaultFieldList);

            string sql = "SELECT " + sqlPieceFields + " FROM " + sqlPieceTable +
                " " + joinClause +  " " + // " JOIN table ON column=column "
                sqlPieceWhere +
                " ORDER BY " + sqlPieceOrderBy + // Note: must have an OrderBy to use offset and limit
                " OFFSET " + sqlPieceOffset + " ROWS" +
                " FETCH NEXT " + sqlPieceLimit + " ROWS ONLY";
            Debug.WriteLine("SQL:" + sql);
            return sql;
        }

        public void AddParameter(SqlCommand cmd, object value, string paramName, SqlDbType paramType)
        {
            SqlParameter param = new SqlParameter(paramName, paramType);
            param.Value = value;
            cmd.Parameters.Add(param);
        }

        public Dictionary<string, object> BuildJsonRowFromSqlDataReader(SqlDataReader row)
        {
            Dictionary<string, object> json = new Dictionary<string, object>();
            for (int i = 0; i<row.FieldCount; i++)
            {
                json.Add(row.GetName(i), row[i]);
            }
            return json;
        }

        public Dictionary<string, object> RunQueryReturnJson(string sql, Action<SqlCommand> fncAddParameters)
        {
            Dictionary<string, object> json = new Dictionary<string, object>();
            List<object> rows = new List<object>();
            bool exceptionHappened = false;
            Query(sql, fncAddParameters, (SqlDataReader row) =>
            {
                rows.Add(BuildJsonRowFromSqlDataReader(row));
                return false;
            }, (string msg, Exception e) =>
            {
                exceptionHappened = true;
                json.Add("success", 0);
                json.Add("Message", msg);
            });
            if (!exceptionHappened)
            {
                json.Add("rows", rows);
            }
            return json;
        }

        public List<string> ColumnNamesForTable(string fullTableName, out List<bool> columnQuoted, out List<bool> columnNumber, out List<bool> columnDate, out List<SqlDbType> columnType)
        {
            string catalogName;
            string schemaName;
            string tableName;
            SeparateTableName(fullTableName, out catalogName, out schemaName, out tableName);

            List<string> columnNames = new List<string>();
            List<bool> columnNeedsQuotes = new List<bool>();
            List<bool> columnIsNumber = new List<bool>();
            List<bool> columnIsDate = new List<bool>();
            List<SqlDbType> columnTypes = new List<SqlDbType>();
            var sql = "SELECT COLUMN_NAME,CHARACTER_MAXIMUM_LENGTH,NUMERIC_PRECISION,DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE" +
                " TABLE_CATALOG = '" + catalogName + "' AND" +
                " TABLE_SCHEMA = '" + schemaName + "' AND" +
                " TABLE_NAME = '" + tableName + "'" +
                " ORDER BY ORDINAL_POSITION ASC";
            Query(sql, null, (SqlDataReader row) =>
            {
                string name = (string)row[0];
                columnNames.Add(name);
                bool quoted = row[1] == DBNull.Value ? false : true;    // CARACTER_MAXIMUM_LENGTH means "string"
                bool number = row[2] == DBNull.Value ? false : true;    // NUMERIC_PRECISION means "number"
                columnNeedsQuotes.Add(quoted);
                columnIsNumber.Add(number);
                bool isDate = row[3].Equals("datetime");
                columnIsDate.Add(isDate);
                string columnTypeName = (string)row[3];
                SqlDbType typ = (SqlDbType)Enum.Parse(typeof(SqlDbType), columnTypeName, true);
                columnTypes.Add(typ);

                return false;
            });
            columnQuoted = columnNeedsQuotes;
            columnNumber = columnIsNumber;
            columnDate = columnIsDate;
            columnType = columnTypes;
            return columnNames;
        }

        public void SeparateTableName(string fullTableName, out string databaseName, out string schemaName, out string tableName)
        {
            databaseName = "";
            schemaName = "dbo";
            tableName = "";
            var result = fullTableName.Split(new[] { '[', '.', ']' }, StringSplitOptions.RemoveEmptyEntries);
            if (result.Length == 1)
            {
                tableName = result[0];
            } else if (result.Length == 2)
            {
                databaseName = result[0];
                tableName = result[1];
            } else if (result.Length == 3)
            {
                databaseName = result[0];
                schemaName = result[1];
                tableName = result[2];
            }
        }

        private void ExampleMethod(HttpRequestBase Request, string sql)
        {
            string json = "{\"rows\":[";
            int jsonRows = 0;
            Query(sql, (SqlCommand cmd) =>
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
                json += BuildJsonRowFromSqlDataReader(row);
                return false;
            });
            json += "]}";
            Debug.WriteLine("JSON:" + json);
        }
    }

}