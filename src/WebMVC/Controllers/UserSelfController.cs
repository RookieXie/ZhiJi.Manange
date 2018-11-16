using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Service;
using Service.Interface.Service;
using Service.Model;
using WebCore.Filter;
using WebCore.Models;
using WebCore.Utility;
using WebMVC.Filter;

namespace WebCore.Controllers
{
    [NoFilter]
    public class UserSelfController : Controller
    {
        private readonly IRedisService redisService;
        private IAdminUserService _userService;
        private IUserSelfService _userSelfService;
        public UserSelfController( IAdminUserService userService, IUserSelfService userSelfService, IRedisService redisService)
        {
            _userService = userService;
            _userSelfService = userSelfService;
            this.redisService = redisService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SelfInfo(AdminUserInfo userInfo)
        {
            //RedisService redis = new RedisService();
            //var userInfo = redis.GetUserInfo(id.ToString());
            ViewBag.User = userInfo;
            return View();
        }

        public IActionResult SelfPassword(AdminUserInfo userInfo)
        {
            //RedisService redis = new RedisService();
            //var userInfo = redis.GetUserInfo(id.ToString());
            ViewBag.User = userInfo;
            return View();
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<int> SelfPasswordEdit_Save(PasswordEditModel model)
        {
            string userId;
            Request.Cookies.TryGetValue(CookieConfig.oaUserId, out userId);
            return await _userService.UpdateUserPassword(int.Parse(userId),model.OldPassword,model.Password);
        }

        /// <summary>
        /// 修改个人信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<int> SelfInfoEdit_Save(AdminUser model)
        {
            string userId;
            Request.Cookies.TryGetValue(CookieConfig.oaUserId, out userId);
            var dbModel = await _userService.GetByIdAsync(int.Parse(userId));
            dbModel.Gender = model.Gender;
            dbModel.Phone = model.Phone;

            dbModel.NickName = model.NickName;

            var res = await _userService.UpdateModelAsync(dbModel);

            
            var userInfo = redisService.GetUserInfo(userId);
            userInfo.Gender = model.Gender;
            userInfo.Phone = model.Phone;
            userInfo.NickName = model.NickName;
            userInfo.Type = model.Type??0;
            redisService.SetUserInfo(userInfo);
            return res;
        }

        [HttpPost]
        public async Task<CusJsonResult> UpdateMenuCache()
        {
            try
            {
                string userId;
                Request.Cookies.TryGetValue(CookieConfig.oaUserId, out userId);
                
                var userMenus = await _userSelfService.GetUerMenusAsync(int.Parse(userId));
                redisService.SetMenus(userId, userMenus);
                return new CusJsonResult(true,"","");
            }
            catch (Exception ex)
            {
                return new CusJsonResult(false,ex.HResult.ToString(),ex.Message);
            }
           
        }
    }
}