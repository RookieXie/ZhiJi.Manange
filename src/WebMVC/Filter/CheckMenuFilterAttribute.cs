using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Service;
using Service.Interface.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCore.Utility;
using WebMVC.Filter;

namespace WebCore.Filter
{
    public class CheckMenuFilterAttribute : ActionFilterAttribute
    {
        private readonly IRedisService redis;
        public CheckMenuFilterAttribute(IRedisService redis)
        {
            this.redis = redis;
        }
        public bool NeedCheck { get; set; } = true;

         public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.Filters.Any(a => a.GetType() == new NoFilterAttribute().GetType()))
            {
                return;
            }
            string userId;
            context.HttpContext.Request.Cookies.TryGetValue(CookieConfig.oaUserId, out userId);
            
            var menus = redis.GetMenus(userId);
            if (menus == null || menus.Count == 0)
            {
                context.Result = new RedirectToActionResult("Inadmissibility", "error",null) ;
                return;
            }

            var path = context.HttpContext.Request.Path.ToString().Trim('/');
            if (string.IsNullOrEmpty(path) || path =="/" )
            {
                return;
            }
            path = path.Split("_").FirstOrDefault();
            if (string.IsNullOrEmpty(path))
            {
                return;
            }

            var menu = menus.Where(m => m.Url.IndexOf(path,StringComparison.OrdinalIgnoreCase)>-1).FirstOrDefault();
            if (menu != null)
            {
                return;
            }
            else
            {
                context.Result = new RedirectToActionResult("Inadmissibility", "error", null);
                //context.HttpContext.Response.Redirect("/error/Inadmissibility");
                
            }
        }

      
    }
}
