using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Util;

namespace DL.Base
{
    internal class BaseDal : CommonDal
    {
        protected R DoSecurity<R, T>(Action<R, SqlConnection> action) where R : Result<T>, new()
        {
            return DoWork<R, T>("SEC", action);
        }

        protected R DoMain<R, T>(Action<R, SqlConnection> action) where R : Result<T>, new()
        {
            return DoWork<R, T>("EML", action);
        }

        private R DoWork<R, T>(string key, Action<R, SqlConnection> action) where R : Result<T>, new()
        {
            R result = new R();

            var connStr = System.Configuration.ConfigurationManager.AppSettings[key]; //EML.Util.SQLHelper.DictConnString(key);

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    if (conn.State != ConnectionState.Open)
                        conn.Open();
                   
                    action(result, conn);

                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
                catch (Exception ex)
                {

                    result.Fail(ex);

                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                    conn.Dispose();
                }
            }


            return result;
        }

        protected R DoTransaction<R, T>(Action<R, SqlConnection, SqlTransaction> action) where R : Result<T>, new()
        {
            return DoTransaction<R, T>("EML", action);
        }

        protected R DoTransaction<R, T>(string constrKey, Action<R, SqlConnection, SqlTransaction> action) where R : Result<T>, new()
        {
            SqlTransaction trans = null;
            R result = new R();

            var conStr = System.Configuration.ConfigurationManager.AppSettings[constrKey];
            using (SqlConnection conn = new SqlConnection(conStr))
            {
                try
                {
                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    trans = conn.BeginTransaction();
                    action(result, conn, trans);
                    trans.Commit();

                    if (conn.State == ConnectionState.Open)
                        conn.Close();

                }
                catch (Exception ex)
                {

                    if (trans != null)
                        trans.Rollback();


                    result.Fail(ex);

                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                    conn.Dispose();
                }
            }


            return result;
        }

        protected R DoEachTransaction<R, T>(Action<R, SqlConnection, SqlTransaction> action) where R : Result<T>, new()
        {
            SqlTransaction trans = null;
            R result = new R(); 
            var connStr = System.Configuration.ConfigurationManager.AppSettings["EML"];

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    action(result, conn, trans);

                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
                catch (Exception ex)
                {
                    result.Fail(ex);
                }

            }

            return result;
        }

    }
}
