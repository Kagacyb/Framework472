using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;

namespace Framework472.Libs
{
    public static class ResultHelper
    {
        public static HttpResponseMessage ToJson(this Object obj)
        {
            HttpResponseMessage result;
            String str;

            switch (obj)
            {
                case Exception ex:
                    str = JsonConvert.SerializeObject(new
                    {
                        Success = false,
                        Message = ex.Message,
                    });
                    break;
                default:
                    str = JsonConvert.SerializeObject(new
                    {
                        Success = true,
                        Content = obj
                    });
                    break;
            }


            result = new HttpResponseMessage { Content = new StringContent(str, Encoding.GetEncoding("UTF-8"), "application/json") };

            return result;
        }



    }
}