using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Common.Redis;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Service;
using Service.Interface.Service;
using WebCore.Filter;
using WebCore.Utility;
using WebMVC.Filter;

namespace WebCore.Controllers
{
   [NoFilter]
    public class AccountController : Controller
    {
        private readonly IRedisService redis;
       
        private IEasydentityService _easydentity;
        public AccountController(IEasydentityService easydentity, IRedisService _redisService)
        {
            _easydentity = easydentity;
            redis = _redisService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string userName,string password,string code)
        {
            var passwordMD5 = MD5Helper.MD5Crypto16(password);
            var user =await _easydentity.Login(userName, passwordMD5);
            if (user == null)
            {
                return View("index","用户名或密码错误");
            }
            else
            {
                var token = redis.GetToken(user.Id.ToString());
                if (token == null )
                {
                    return RedirectToAction("Index", "Account");
                }

                var urlHost = Request.Host.Host;
                CookieOptions cookieOptions;
                if ("localhost" == urlHost)
                {
                    cookieOptions = new CookieOptions
                    {
                        Expires = DateTimeOffset.Now.AddDays(1),
                        HttpOnly = true,
                    };
                }

                else
                {
                    cookieOptions = new CookieOptions
                    {
                        Expires = DateTimeOffset.Now.AddDays(1),
                        HttpOnly = true,
                        Domain = CookieConfig.domainTopLevel
                        //Domain = cookieKey.DomainDic[CookieConfig.oaToken]
                    };
                }
                Response.Cookies.Append(CookieConfig.oaToken, token, cookieOptions);
                Response.Cookies.Append(CookieConfig.oaUserId, user.Id.ToString(), cookieOptions);
                return RedirectToAction("index","home");
            }
           
        }

        public IActionResult Logout()
        {
            string userId;
            Request.Cookies.TryGetValue(CookieConfig.oaUserId, out userId);
            
            redis.DeleteUserLoginInfo(userId);
            Response.Cookies.Delete(CookieConfig.oaUserId);
            Response.Cookies.Delete(CookieConfig.oaToken);
            return RedirectToAction("index", "Account");
        }

        public CusJsonResult Validate(string userId,string token,string url)
        {
            try
            {
                return _easydentity.Validate(userId, token, url);
            }
            catch (Exception ex)
            {
                return new CusJsonResult(false, "101", ex.Message);
            }
        }
    }
}