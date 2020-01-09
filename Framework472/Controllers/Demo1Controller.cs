using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Framework472.Libs;

namespace Framework472.Controllers
{
    public class Demo1Controller : Controller
    {
        public ActionResult SingleGrid()
        {
            return View();
        }

        public ActionResult PagingGrid() { return View(); }

        public ActionResult MuiltGrid() { return View(); }

        public JsonResult Load()
        {
            List<dynamic> src = new List<dynamic>();

            var r = new Random();
            for (var i = 0; i < 5000; i++)
            {
                src.Add(new
                {
                    ID = Guid.NewGuid().ToString("N"),
                    SAPRef = r.Next(1000, 10000).ToString("000000"),
                    ReceiveDate = DateTime.Now.ToString("yyyy-MM-dd")
                });
            }

            return Json(src);
        }
    }
}