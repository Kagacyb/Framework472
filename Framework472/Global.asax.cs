using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Linq.Expressions;
using System.Text;

namespace Framework472
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //var sb = new StringBuilder();
            //var path = @"C:\Enpot\Projects\D-Demo\Enpot.Mvc.Easyui.Demo\Framework472\Framework472\Images\famfamfam_silk_icons_v013\icons";
            //System.IO.Directory.GetFiles(path).ToList().ForEach(i =>
            //{
            //    var fileName = i.Substring(i.LastIndexOf('\\') + 1);
            //    var fileName2 = fileName.Substring(0, fileName.LastIndexOf('.'));

            //    sb.AppendLine($".icon-{fileName2}" + "{");
            //    sb.AppendLine($"background:url('../Images/famfamfam_silk_icons_v013/icons/{fileName}') no-repeat center center;");
            //    sb.AppendLine("}");

            //});
            //var a = sb.ToString();
        }
    }
}
