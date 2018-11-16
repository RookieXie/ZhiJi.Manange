using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Common.Redis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Model;
using Service;
using Service.Interface.Service;
using WebCore.Filter;
using WebCore.Models;
using WebCore.Utility;
using WebMVC.Filter;
using YYLog.ClassLibrary;

namespace WebCore.Controllers
{
    //[NoFilter]
    public class HomeController : Controller
    {
        private readonly IRedisService redisService;
        public HomeController(IRedisService _redisService)
        {
            redisService = _redisService;
        }
        //[CheckTokenFilter(NeedCheck = true)]
        public IActionResult Index()
        {
            var result = new List<ViewMenu>();

            string userId;
            string token;
            Request.Cookies.TryGetValue(CookieConfig.oaUserId, out userId);
            Request.Cookies.TryGetValue(CookieConfig.oaToken, out token);
            var dbMenus = redisService.GetMenus(userId);


            if (dbMenus != null && dbMenus.Count>0)
            {
                var menus = dbMenus.Select(m => new ViewMenu
                {
                    Id = m.Id,
                    Name = m.Name,
                    //Url = m.Url +"?token="+ System.Web.HttpUtility.UrlEncode(userId + "&" + token),
                    Url = m.Url,
                    Type = m.Type,
                    ParentId = m.ParentId,
                    OrderNum = m.OrderNum
                }).ToList();
                foreach (var it in menus)
                {
                    if (it.Type == 1)
                    {
                        result.Add(it);
                        it.SubMenus = menus.Where(m => m.ParentId == it.Id)
                            .OrderBy(m => m.OrderNum)
                            .ToList();
                    }
                }
            }
          
            
            var userInfo = redisService.GetUserInfo(userId);
            if (userInfo==null)
            {
                return RedirectToAction("index", "Account");
            }

            ViewBag.User = userInfo;
            ViewBag.Menus = result.OrderBy(m=>m.OrderNum).ToList();
            
            return View();
        }


        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Welcome()
        {
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        //private IOptions<DBMonoUtility.DBInitOption> _dbInitOption;
        //public HomeController(IOptions<DBMonoUtility.DBInitOption> dbInitOption)
        //{
        //    _dbInitOption = dbInitOption;
        //}
        //public string GetConnectionString()
        //{

        //    var res = DBMonoUtility.DataBasePool.AddDataBaseConnectionString(_dbInitOption.Value.PoolName,_dbInitOption.Value.PublicKey,50,5);
        //    var sss = "server=182.254.128.241;port=11131;uid=root;pwd=iuEKm1vK+Oqd5TQ=;database=show_interest;Min Pool Size=0;Pooling=true;connect timeout=120;CharSet=utf8";

        //    if (res == sss)
        //    {
        //        var a = res.Length;
        //        var b = sss.Length;
        //    }
        //    else
        //    {
        //        var a = res.Length;
        //        var b = sss.Length;
        //    }
        //    Request.HttpContext.Response.ContentType = "application/json";
        //    return sss;
        //}
    }
}
