using System.Web;
using System.Web.Mvc;

namespace NguyenQuocDai_2118110097
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
