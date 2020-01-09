using Framework472.Libs;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Framework472.Controllers
{
    public class EnpotController : Controller
    {
        public ActionResult Login()
        {
            ViewBag.FileVersion = System.Configuration.ConfigurationManager.AppSettings["AppVersion"];
            return View();
        }

        public ActionResult Main()
        {
            return View();
        }


        public JsonResult Menu()
        {
            List<dynamic> menu = new List<dynamic>();

            List<dynamic> demo1 = new List<dynamic>();


            demo1.Add(new
            {
                id = Guid.NewGuid().ToString("N"),
                text = "标准表格",
                url = "../Demo1/SingleGrid"
            });

            demo1.Add(new
            {
                id = Guid.NewGuid().ToString("N"),
                text = "分页表格",
                url = "../Demo1/PagingGrid"
            });
            demo1.Add(new
            {
                id = Guid.NewGuid().ToString("N"),
                text = "双表格",
                url = "../Demo1/MuiltGrid"
            });
            menu.Add(new
            {
                id = Guid.NewGuid().ToString("N"),
                text = "功能项",
                children = demo1
            });

            List<dynamic> demo2 = new List<dynamic>();

            demo2.Add(new
            {
                id = Guid.NewGuid().ToString("N"),
                text = "基础控件",
                url = "../Demo2/BaseControl"
            });
            demo2.Add(new
            {
                id = Guid.NewGuid().ToString("N"),
                text = "查询",
                url = "../Demo2/Search"
            });
            menu.Add(new
            {
                id = Guid.NewGuid().ToString("N"),
                text = "样式",
                children = demo2
            });


            return Json(menu);
        }
    }
}