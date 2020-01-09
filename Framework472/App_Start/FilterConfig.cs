using Framework472.Libs;
using System.Web;
using System.Web.Mvc;

namespace Framework472
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new CheckLoginAttribute());
        }
    }
}
