using Hangfire.Dashboard;
using Service.Interface.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCore.Utility;

namespace WebMVC.Filter
{
    public class HangfireFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            var httpContext = context.GetHttpContext();
            string userId = "";
            httpContext.Request.Cookies.TryGetValue(CookieConfig.oaUserId, out userId);

            if (userId == "1")
            {
                return true;
            }
            // Allow all authenticated users to see the Dashboard (potentially dangerous).
            return false;
        }
    }
}
