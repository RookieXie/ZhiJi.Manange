using Common.Redis;
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
    public class CheckTokenFilterAttribute: ActionFilterAttribute
    {
        private readonly IRedisService redis;
        public CheckTokenFilterAttribute(IRedisService redis)
        {
            this.redis = redis;
        }
        public bool NeedCheck { get; set; } = true;
        /// <summary>
        /// 登陆状态检查
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.Filters.Any(a => a.GetType() == new NoFilterAttribute().GetType()))
            {
                return;
            }
            string userId = "";
            string token = "";
            context.HttpContext.Request.Cookies.TryGetValue(CookieConfig.oaUserId,out userId);
            context.HttpContext.Request.Cookies.TryGetValue(CookieConfig.oaToken, out token);
            
            var tokenOnserver = redis.GetToken(userId);

            if (!string.IsNullOrEmpty(tokenOnserver) )
            {

                if (tokenOnserver == token)
                {
                    return;

                }
            }
            context.Result = new RedirectToActionResult("index", "account", null);
            //context.HttpContext.Response.Redirect("/error/TokenOverdue");
            
        }
    }
}
