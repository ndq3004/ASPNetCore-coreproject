using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace NDQUAN.Core.DL
{
    public class DLCore
    {

    }

    public static class DLConnection
    {
        private static string Host = "192.168.1.14";
        private static string User = "postgres";
        private static string DBname = "postgres";
        private static string Password = "30041998";
        private static string Port = "5432";
        public static string connectionString = String.Format(
                    "Server={0};Username={1};Database={2};Port={3};Password={4};Pooling=true;",
                    Host,
                    User,
                    DBname,
                    Port,
                    Password);
        public static NpgsqlConnection GetDemoConnection(bool openConnection = true)
        {
            var conn = new NpgsqlConnection(connectionString);
            if (openConnection)
            {
                conn.Open();
            }
            //SqlConnection connection = new SqlConnection();
            //connection.
            return conn;
        }
    }

    public static class DLCommand
    {
        public static List<T> QueryCommandText<T>(IDbConnection cnn, string sql, object param = null, int commandTimeout = -1)
        {
            List<T> result = new List<T>();
            OpenConnection(cnn);
            try
            {
                var queryResult = cnn.Query<T>(sql, param, commandType: CommandType.Text);
                result = queryResult.AsList<T>();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            cnn.Close();
            return result;
        }

        public static bool ExecuteQuery(IDbConnection cnn, string sql, object param = null, int commandTimeout = -1)
        {
            OpenConnection(cnn);
            var result = cnn.Execute(sql, param);
            return result > 0;
        }

        //public static int ExecuteInsert(IDbConnection cnn, string sql, object param = null)
        //{
        //    OpenConnection(cnn);
        //    try
        //    {
                
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        public static void OpenConnection(IDbConnection cnn)
        {
            if(cnn.State != System.Data.ConnectionState.Open)
            {
                cnn.Open();
            }
        }
    }

    public static class LogDB
    {
        public static void LogDuration(string message)
        {
            DLCommand.ExecuteQuery(DLConnection.GetDemoConnection(true), 
                $"insert into core.log4net values ('{message}')");
        }
    }
}
