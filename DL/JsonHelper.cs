using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace DL
{
    public class JsonHelper
    {
        public static List<string> GetNames(SqlDataReader reader)
        {

            var cols = new List<string>();
            for (var i = 0; i < reader.FieldCount; i++)
                cols.Add(reader.GetName(i));

            return cols;
        }

        public static List<dynamic> Serialize(SqlDataReader reader)
        {
            var results = new List<dynamic>();
            var cols = GetNames(reader);

            while (reader.Read())
                results.Add(SerializeRow(cols, reader));

            return results;
        }

        public static dynamic SerializeRow(IEnumerable<string> cols, SqlDataReader reader)
        {
            var result = new Dictionary<string, object>();
            foreach (var col in cols)
            {
                if (reader[col] != DBNull.Value)
                    result.Add(col, reader[col]);
                else
                    result.Add(col, null);
            }

            var jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(result);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(jsonText);
        }

        public static dynamic SerializeObject(SqlDataReader reader)
        {
            dynamic dync = new ExpandoObject();
            var cols = GetNames(reader);

            while (reader.Read())
                dync = SerializeRow(cols, reader);

            var jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(dync);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<ExpandoObject>(jsonText);
        }

    }
}
