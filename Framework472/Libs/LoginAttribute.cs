using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Framework472.Libs
{
    public class CheckLoginAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            if (filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(SkipCheckLoginAttribute), false)) return;

            if (filterContext.ActionDescriptor.IsDefined(typeof(SkipCheckLoginAttribute), false)) return;

            if (filterContext.HttpContext.Request.IsAuthenticated == false)
            {
                //跳转方法1：
                filterContext.HttpContext.Response.Redirect("/Enpot/Login");
                //跳转方法2：
                ViewResult view = new ViewResult();
                //指定要返回的完整视图名称
                view.ViewName = "~/Enpot/Login/Login.cshtml";
            }
        }
    }

    public class SkipCheckLoginAttribute : Attribute
    {

    }
}