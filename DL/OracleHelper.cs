using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DL
{
    public class OracleHelper
    {
        public static string GetConditionByDateFromTo(string columnName, string dateFrom, string dateTo, string format = "yyyy-MM-dd")
        {
            var start = new DateTime(1990, 1, 1).ToString(format);
            var end = new DateTime(2030, 12, 31).ToString(format);


            var fromDate = string.IsNullOrEmpty(dateFrom) ? start : new DateTime(Convert.ToInt32(dateFrom.Substring(0, 4)), Convert.ToInt32(dateFrom.Substring(5, 2)), Convert.ToInt32(dateFrom.Substring(8, 2))).ToString(format);

            var toDate = string.IsNullOrEmpty(dateTo) ? end : new DateTime(Convert.ToInt32(dateTo.Substring(0, 4)), Convert.ToInt32(dateTo.Substring(5, 2)), Convert.ToInt32(dateTo.Substring(8, 2))).AddDays(1).ToString(format);


            return string.Format(" AND TO_DATE('{1}','yyyy-MM-dd') <= {0} AND {0} < TO_DATE('{2}','yyyy-MM-dd') ", columnName, fromDate, toDate);

        }
    }
}
