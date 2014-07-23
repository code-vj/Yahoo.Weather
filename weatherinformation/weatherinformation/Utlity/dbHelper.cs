using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;


namespace weatherinformation.Utlity
{
    public class DbHelper
    {
        const string ConnectionStringKey = "ConnString";

        public DbHelper()
        {
        }

      

        public IDbConnection OpenConnection()
        {
            IDbConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnString"]);
            connection.Open();

            return connection;
        }

        public List<T> WrapStoredProcedure<T>(object param, string query)
        {
            using (var db = OpenConnection())
            {
                try
                {
                    return db.Query<T>(query, param,CommandType.StoredProcedure).ToList();
                }
                catch (Exception ex)
                {
                    throw new DbException(ex.Message, ex);
                }
                finally
                {
                    db.Close();
                }
            }
        }



        public List<T> SelectQuery<T>(object param, string query)
        {
            using (var db = OpenConnection())
            {
                try
                {
                    return db.Query<T>(query, param).ToList();
                }
                catch (Exception ex)
                {
                    throw new DbException(ex.Message, ex);
                }
                finally
                {
                    db.Close();
                }

            }
        }

        public int ExecuteQuery(string query)
        {
            using (var db = OpenConnection())
            {
                try
                {
                    return db.Execute(query,"");
                }
                catch (Exception ex)
                {
                    throw new DbException(ex.Message, ex);
                }
                finally
                {
                    db.Close();
                }
            }
        }

        public int ExecuteQuery(object param, string query)
        {
            using (var db = OpenConnection())
            {
                try
                {
                    return db.Execute(query, param);
                }
                catch (Exception ex)
                {
                    throw new DbException(ex.Message, ex);
                }
                finally
                {
                    db.Close();
                }

            }
        }



        public List<T> SelectQuery<T>(string query)
        {
            return SelectQuery<T>(null, query);
        }
    }

    public class DbException : Exception
    {
        public DbException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        public string Query { get; set; }
    }
}
