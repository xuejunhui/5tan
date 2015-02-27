using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WTAN.CommonUtility;
using WTAN.Model.DModel;
using System.Data;
using System.Data.SqlClient;
using System.Net;

namespace WTAN.SQLServerDAL
{
    

    internal static class CurrentDataServer
    {
        private static class SqlHelper
        {
            #region sql execute
            public static List<T> ExecuteRecords<T>(string sqlText, params object[] nameValues) where T : RecordItem, new()
            {
                return ExecuteRecords<T>(sqlText, CommandType.Text, nameValues);
            }

            public static List<T> ExecuteRecords<T>(string sqlText, CommandType CT, params object[] nameValues) where T : RecordItem, new()
            {
                List<T> result = new List<T>();
                string connectionString = AppSettings.DefaultConnectionString;
                using (SqlConnection dbconn = new SqlConnection(connectionString))
                {
                    dbconn.Open();
                    SqlCommand cmd = new SqlCommand(sqlText, dbconn);
                    cmd.CommandType = CT;
                    for (int i = 0; i < nameValues.Length - 1; i += 2)
                    {

                        SqlParameter parameter = null;
                        parameter = new SqlParameter(nameValues[i].ToString(), nameValues[i + 1]);
                        cmd.Parameters.Add(parameter);
                    }

                    using (var reader = cmd.ExecuteReader())
                    {
                        string[] fields = new string[reader.FieldCount];
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            fields[i] = reader.GetName(i);
                        }
                        while (reader.Read())
                        {
                            T record = new T();
                            record.ResetFields(fields);
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                object value = reader.GetValue(i);
                                record[fields[i]] = Convert.IsDBNull(value) ? null : value;
                            }
                            result.Add(record);
                        }
                    }
                }
                return result;
            }

            public static List<RecordItem> ExecuteRecords(string sqlText, params object[] nameValues)
            {
                return ExecuteRecords(sqlText, CommandType.Text, nameValues);
            }

            public static List<RecordItem> ExecuteRecords(string sqlText, CommandType CT, params object[] nameValues)
            {
                List<RecordItem> result = new List<RecordItem>();
                string connectionString = AppSettings.DefaultConnectionString;
                using (SqlConnection dbconn = new SqlConnection(connectionString))
                {
                    dbconn.Open();
                    SqlCommand cmd = new SqlCommand(sqlText, dbconn);
                    cmd.CommandType = CT;
                    for (int i = 0; i < nameValues.Length - 1; i += 2)
                    {

                        SqlParameter parameter = null;
                        parameter = new SqlParameter(nameValues[i].ToString(), nameValues[i + 1]);
                        cmd.Parameters.Add(parameter);
                    }

                    using (var reader = cmd.ExecuteReader())
                    {
                        string[] fields = new string[reader.FieldCount];
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            fields[i] = reader.GetName(i);
                        }
                        while (reader.Read())
                        {
                            RecordItem record = new RecordItem(fields);
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                object value = reader.GetValue(i);
                                record[fields[i]] = value is DBNull ? null : value;
                            }
                            result.Add(record);
                        }
                    }
                }
                return result;
            }

            public static int ExecuteScalarInt(string sqlText, params object[] nameValues)
            {
                return ExecuteScalarInt(sqlText, CommandType.Text, nameValues);
            }

            public static int ExecuteScalarInt(string sqlText, CommandType CT, params object[] nameValues)
            {
                int result = 0;

                string connectionString = AppSettings.DefaultConnectionString;
                using (SqlConnection dbconn = new SqlConnection(connectionString))
                {
                    dbconn.Open();
                    SqlCommand cmd = new SqlCommand(sqlText, dbconn);
                    cmd.CommandType = CT;
                    for (int i = 0; i < nameValues.Length - 1; i += 2)
                    {
                        SqlParameter parameter = null;
                        parameter = new SqlParameter(nameValues[i].ToString(), nameValues[i + 1]);
                        cmd.Parameters.Add(parameter);
                    }

                    var r = cmd.ExecuteScalar();
                    result = Convert.ToInt32(r);
                }
                return result;
            }

            public static object ExecuteScalar(string sqlText, params object[] nameValues)
            {
                return ExecuteScalar(sqlText, CommandType.Text, nameValues);
            }

            public static object ExecuteScalar(string sqlText, CommandType CT, params object[] nameValues)
            {
                object result = 0;

                string connectionString = AppSettings.DefaultConnectionString;
                using (SqlConnection dbconn = new SqlConnection(connectionString))
                {
                    dbconn.Open();
                    SqlCommand cmd = new SqlCommand(sqlText, dbconn);
                    cmd.CommandType = CT;
                    for (int i = 0; i < nameValues.Length - 1; i += 2)
                    {
                        SqlParameter parameter = null;
                        parameter = new SqlParameter(nameValues[i].ToString(), nameValues[i + 1]);
                        cmd.Parameters.Add(parameter);
                    }

                    result = cmd.ExecuteScalar();
                }
                return result;
            }

            public static int ExecuteNoneQuery(string sqlText, params object[] nameValues)
            {
                return ExecuteNoneQuery(sqlText, CommandType.Text, nameValues);
            }

            public static int ExecuteNoneQuery(string sqlText, CommandType CT, params object[] nameValues)
            {
                int result = 0;

                string connectionString = AppSettings.DefaultConnectionString;
                using (SqlConnection dbconn = new SqlConnection(connectionString))
                {
                    dbconn.Open();
                    SqlCommand cmd = new SqlCommand(sqlText, dbconn);
                    cmd.CommandType = CT;
                    for (int i = 0; i < nameValues.Length - 1; i += 2)
                    {
                        SqlParameter parameter = null;
                        parameter = new SqlParameter(nameValues[i].ToString(), nameValues[i + 1]);
                        cmd.Parameters.Add(parameter);
                    }

                    result = cmd.ExecuteNonQuery();
                }
                return result;
            }
            #endregion
        }

        #region custom sql
        internal static T ExecuteOneRecord<T>(this string sqlText, params object[] nameValues) where T : RecordItem, new()
        {
            return ExecuteOneRecord<T>(sqlText, CommandType.Text, nameValues);
        }

        internal static T ExecuteOneRecord<T>(this string sqlText, CommandType CT, params object[] nameValues) where T : RecordItem, new()
        {
            try
            {
                return SqlHelper.ExecuteRecords<T>(sqlText, CT, nameValues).FirstOrDefault();
            }
            catch (Exception ex)
            {
                ex.AddLog("DataServer", "ExecuteOneRecord");
                throw ex;
            }
        }

        internal static List<T> ExecuteRecords<T>(this string sqlText, params object[] nameValues) where T : RecordItem, new()
        {
            return ExecuteRecords<T>(sqlText, CommandType.Text, nameValues);
        }

        internal static List<T> ExecuteRecords<T>(this string sqlText, CommandType CT, params object[] nameValues) where T : RecordItem, new()
        {
            try
            {
                return SqlHelper.ExecuteRecords<T>(sqlText, CT, nameValues);
            }
            catch (Exception ex)
            {
                ex.AddLog("DataServer", "ExecuteRecords Return List<T>");
                throw ex;
            }
        }

        internal static List<RecordItem> ExecuteRecords(this string sqlText, params object[] nameValues)
        {
            return ExecuteRecords(sqlText, CommandType.Text, nameValues);
        }

        internal static List<RecordItem> ExecuteRecords(this string sqlText, CommandType CT, params object[] nameValues)
        {
            try
            {
                return SqlHelper.ExecuteRecords(sqlText, CT, nameValues);
            }
            catch (Exception ex)
            {
                ex.AddLog("DataServer", "ExecuteRecords Return List<RecordItem>");
                throw ex;
            }
        }

        internal static int ExecuteNoneQuery(this string sqlText, params object[] nameValues)
        {
            return ExecuteNoneQuery(sqlText, CommandType.Text, nameValues);
        }

        internal static int ExecuteNoneQuery(this string sqlText, CommandType CT, params object[] nameValues)
        {
            try
            {
                return SqlHelper.ExecuteNoneQuery(sqlText, CT, nameValues);
            }
            catch (Exception ex)
            {
                ex.AddLog("DataServer", "ExecuteNoneQuery");
                throw ex;
            }
        }

        internal static object ExecuteScalar(this string sqlText, params object[] nameValues)
        {
            return ExecuteScalar(sqlText, CommandType.Text, nameValues);
        }

        internal static object ExecuteScalar(this string sqlText, CommandType CT, params object[] nameValues)
        {
            try
            {
                return SqlHelper.ExecuteScalar(sqlText, CT, nameValues);
            }
            catch (Exception ex)
            {
                ex.AddLog("DataServer", "ExecuteScalar");
                throw ex;
            }
        }

        internal static object ExecuteScalarInt(this string sqlText, params object[] nameValues)
        {
            return ExecuteScalarInt(sqlText, CommandType.Text, nameValues);
        }

        internal static object ExecuteScalarInt(this string sqlText, CommandType CT, params object[] nameValues)
        {
            try
            {
                return SqlHelper.ExecuteScalarInt(sqlText, CT, nameValues);
            }
            catch (Exception ex)
            {
                ex.AddLog("DataServer", "ExecuteScalarInt");
                throw ex;
            }
        }
        #endregion

        

        /// <summary>
        /// 進行單表查詢
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="columns"></param>
        /// <param name="whereStr"></param>
        /// <param name="nameValues"></param>
        /// <returns></returns>
        public static T GetInfo<T>(String columns, String whereStr, params String[] nameValues) where T : RecordItem, new()
        {
            String tableName = typeof(T).Name.RemoveEndsWith("TB");
            return GetInfoContainsOtherTable<T>(columns, tableName, whereStr, nameValues);
        }

        /// <summary>
        /// 查詢信息包含另外的表信息 用於多表查詢
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="columns"></param>
        /// <param name="whereStr"></param>
        /// <param name="nameValues"></param>
        /// <returns></returns>
        public static T GetInfoContainsOtherTable<T>(String columns, String tablesName, String whereStr, params String[] nameValues) where T : RecordItem, new()
        {
            String sql = String.Format("select {0} from {1} where {2}", columns, tablesName, whereStr);
            return sql.ExecuteOneRecord<T>(nameValues);
        }

        /// <summary>
        /// 查詢列表信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="columns"></param>
        /// <param name="whereStr"></param>
        /// <param name="nameValues"></param>
        /// <returns></returns>
        public static List<T> GetListInfo<T>(String columns, String whereStr, String order, params String[] nameValues) where T : RecordItem, new()
        {
            String tableName = typeof(T).Name.RemoveEndsWith("TB");
            return GetListInfoContainsOtherTable<T>(columns, tableName, whereStr, order, nameValues);
        }

        /// <summary>
        /// 查詢列表信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="columns"></param>
        /// <param name="tablesName"></param>
        /// <param name="whereStr"></param>
        /// <param name="nameValues"></param>
        /// <returns></returns>
        public static List<T> GetListInfoContainsOtherTable<T>(String columns, String tablesName, String whereStr, String order, params String[] nameValues) where T : RecordItem, new()
        {
            String sql = String.Format("select {0} from {1} where {2} order by {3}", columns, tablesName, whereStr, order);
            return sql.ExecuteRecords<T>(nameValues);
        }

        /// <summary>
        /// 查詢列表信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="columns"></param>
        /// <param name="tablesName"></param>
        /// <param name="whereStr"></param>
        /// <param name="nameValues"></param>
        /// <returns></returns>
        public static object GetScalar(String columns, String tablesName, String whereStr, params String[] nameValues)
        {
            String sql = String.Format("select top 1 {0} from {1} where {2}", columns, tablesName, whereStr);
            return sql.ExecuteScalar(nameValues);
        }

        /// <summary>
        /// 查詢列表信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="columns"></param>
        /// <param name="tablesName"></param>
        /// <param name="whereStr"></param>
        /// <param name="nameValues"></param>
        /// <returns></returns>
        public static int GetScalarInt(String tablesName, String whereStr, params String[] nameValues)
        {
            String sql = String.Format("select top 1 AutoKey from {0} where {1}", tablesName, whereStr);
            return sql.ExecuteScalarInt(nameValues).ToInt32Value();
        }


        /// <summary>
        /// 分頁查詢
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tableName">表名</param>
        /// <param name="orderBy">排序</param>
        /// <param name="pageSize">沒有顯示數據條數</param>
        /// <param name="rowCount">總數據條數</param>
        /// <param name="pageCount">總頁數</param>
        /// <param name="pageIndex">當前顯示頁</param>
        /// <param name="whereStr">查詢條件</param>
        /// <param name="nameValues">查詢參數</param>
        /// <returns></returns>
        public static List<T> GetPagingData<T>(this String tableName, String columns, String orderBy, String whereStr, int pageSize, out int rowCount, int pageIndex, params String[] nameValues) where T : RecordItem, new()
        {
            whereStr = whereStr.ToEmptyTrimString().Length > 0 ? (whereStr.Trim().StartsWith("where ") ? whereStr : "where " + whereStr) : whereStr;
            String sql = "select top {0} * from (select ROW_NUMBER() over (order by {1}) as RowNumber,{5} from {2} {4}) A where RowNumber > {3}";
            List<T> list = null;
            if (nameValues == null)
            {
                rowCount = ExecuteScalar(String.Format("select count(0) from {0} {1}", tableName, whereStr)).ToInt32Value();
                list = ExecuteRecords<T>(String.Format(sql, pageSize, orderBy, tableName, (pageIndex - 1) * pageSize, whereStr, columns));
            }
            else
            {
                rowCount = ExecuteScalar(String.Format("select count(0) from {0} {1}", tableName, whereStr, columns), nameValues).ToInt32Value();
                list = ExecuteRecords<T>(String.Format(sql, pageSize, orderBy, tableName, (pageIndex - 1) * pageSize, whereStr, columns), nameValues);
            }
            return list;
        }

        public static Boolean Del<T>(String where, params String[] nameValues) where T : DModelBase, new()
        {
            String tableName = typeof(T).Name.RemoveEndsWith("TB");
            String sql = String.Format("delete {0} where {1}", tableName, where);
            return sql.ExecuteNoneQuery(nameValues) > 0;

        }
    }
}
