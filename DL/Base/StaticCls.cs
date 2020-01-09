using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DL.Base
{
    internal class StaticCls : BaseDal
    {
        private static StaticCls _cls = new StaticCls();

        public static DateTime SystemDatetime
        {
            get
            {
                return _cls.GetSysDatetime().Data;
            }

        }


        #region 系统时间

        /// <summary>
        /// 系统时间
        /// </summary>
        public Result<DateTime> GetSysDatetime()
        {
            return DoMain<Result<DateTime>, DateTime>((result, sqlConn) =>
            {
                DateTime data = DateTime.MinValue;
                var cmd = sqlConn.CreateCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "rSystemDatetime";

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr != null)
                    {
                        while (dr.Read())
                        {
                            {
                                data = Convert.ToDateTime(dr["SystemDatetime"]);
                            }
                        }
                    }

                    result.Pass(data);
                }
            });
        }

        #endregion
    }

}