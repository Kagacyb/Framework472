using DL.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DL.Report
{
    class ReportDal
    {
        internal string PostJson(string sql)
        {
            //地址
            string _url = " http://192.168.1.118:22682/WarehouseService/api/WebPage/GetDeliveryMainReport";
            //json参数
            string jsonParam = sql;
            var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(_url);
            request.Method = "POST";
            request.ContentType = "application/json;charset=UTF-8";
            byte[] byteData = Encoding.UTF8.GetBytes(jsonParam);
            int length = byteData.Length;
            request.ContentLength = length;
            System.IO.Stream writer = request.GetRequestStream();
            writer.Write(byteData, 0, length);
            writer.Close();
            var response = (System.Net.HttpWebResponse)request.GetResponse();
            var responseString = new System.IO.StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8")).ReadToEnd();
            return responseString.ToString();
        }


        internal DynamicListResult ProductDeliveryScanNote_Search(string sqlContent)
        {
            var result = PostJson(JsonConvert.SerializeObject(new { sql = sqlContent }));
            var a = new DynamicListResult();
            //   return new DynamicListResult();
            a.Pass(JsonConvert.DeserializeObject<List<dynamic>>(result));
            return a;
        }
    }
}
