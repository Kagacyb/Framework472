using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DL
{
    public class SqlHelper
    {
        public static string GetConditionByDateFromTo(string columnName, string dateFrom, string dateTo)
        {
            var start = "1990-01-01";
            var end = "2030-12-31";

            return string.Format(" ( {0} >= '{1}' AND {0} < '{2}'  )", columnName, string.IsNullOrEmpty(dateFrom) ? start : dateFrom
                   , string.IsNullOrEmpty(dateTo) ? end : Convert.ToDateTime(dateTo).AddDays(1).ToString("yyyy-MM-dd"));

        }

        public static string GetConditionByDateFromTo(string columnName, string dateFrom, string dateTo, string format = "yyyy-MM-dd")
        {
            var start = new DateTime(1990, 1, 1).ToString(format);
            var end = new DateTime(2030, 12, 31).ToString(format);


            var fromData = string.IsNullOrEmpty(dateFrom) ? start : new DateTime(Convert.ToInt32(dateFrom.Substring(0, 4)), Convert.ToInt32(dateFrom.Substring(5, 2)), Convert.ToInt32(dateFrom.Substring(8, 2))).ToString(format);

            var toData = string.IsNullOrEmpty(dateTo) ? end : new DateTime(Convert.ToInt32(dateTo.Substring(0, 4)), Convert.ToInt32(dateTo.Substring(5, 2)), Convert.ToInt32(dateTo.Substring(8, 2))).AddDays(1).ToString(format);


            return string.Format(" ( {0} >= '{1}' AND {0} < '{2}'  )", columnName, fromData, toData);

        }

    }
}
