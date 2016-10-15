using System.Web;
using System.Web.Mvc;
using WebLog.Resources;

namespace WebLog
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
