using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Framework472.Controllers
{
    public class Demo2Controller : Controller
    {
        public ActionResult BaseControl()
        {
            return View();
        }

        public ActionResult Search() { return View(); }

        [HttpPost]
        public JsonResult Find(string data)
        {
            dynamic json = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(data);


            List<dynamic> result = new List<dynamic>();

            var r = new Random();

            for (int idx = r.Next(10, 20); idx > 0; idx--)
            {
                result.Add(new
                {
                    MATNR = r.Next(1000, 9999).ToString("0000"),
                    Url = "../Images/bg20191125001.jfif"
                });
            }

            // string payload = stream.ReadToEnd();
            return Json(result);
        }
    }
}