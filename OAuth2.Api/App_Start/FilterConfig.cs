using OAuth2.Api.Models.Mvc;
using System.Web;
using System.Web.Mvc;

namespace OAuth2.Api
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ApiErrorHandlerAttribute());
        }
    }
}
