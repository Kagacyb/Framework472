using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Framework472.Libs;
using System.Web.Security;
using System.Net.Http.Headers;

namespace Framework472.Controllers
{
    public class LoginController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Post([FromBody]JObject data)
        {
            //dynamic json = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(data.ToString());

            string user = Convert.ToString(data["user"]);
            string pwd = Convert.ToString(data["pwd"]);

            try
            {
                var webResult = new DL.Security.UserBll().WebLogin(user, pwd);

                if (webResult.Success)
                {
                    var userData = webResult.Data;
                    FormsAuthentication.SetAuthCookie(string.IsNullOrEmpty(userData.Name) ? userData.Account : userData.Name, true);
                    var loginID = Guid.NewGuid().ToString();
                    var resp = new
                    {
                        Url = FormsAuthentication.DefaultUrl,
                        LoginID = loginID
                    }.ToJson();

                    var jsonTxt = Newtonsoft.Json.JsonConvert.SerializeObject(userData);
                    var cookie = new CookieHeaderValue(loginID, jsonTxt);
                    cookie.Expires = DateTimeOffset.Now.AddDays(1);
                    cookie.Domain = Request.RequestUri.Host;
                    cookie.Path = "/";


                    resp.Headers.AddCookies(new CookieHeaderValue[] { cookie });

                    //HttpCookie cookie = new HttpCookie(loginID);
                    //DateTime dt = DateTime.Now;
                    //TimeSpan ts = new TimeSpan(1, 0, 0, 0, 0);//过期时间为1天
                    //cookie.Expires = dt.Add(ts);//设置过期时间


                    //var jsonTxt = Newtonsoft.Json.JsonConvert.SerializeObject(userData);
                    //cookie.Values.Add("ENPOT_USER_ACCOUNT", System.Web.HttpContext.Current.Server.UrlEncode(jsonTxt));

                    //   this.Headers.AddCookies
                    //   this.Request.
                    // Response.AppendCookie(cookie);

                    //      dynamic result = ;

                 


                    return resp;
                }
                else
                {
                    throw new Exception(webResult.Message);
                }
            }
            catch (Exception ex)
            {
                return ex.ToJson();
            }


        }
    }
}