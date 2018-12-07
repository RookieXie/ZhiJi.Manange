using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.HomeListCach;
using Service.Interface.Service;
using WebMVC.Models;

namespace WebMVC.Controllers
{
    public class ZhiJiUserController : Controller
    {
        private readonly MySqlContext _eFContext;
        private readonly IRedisService redisService;
        public ZhiJiUserController(MySqlContext eFContext, IRedisService redisService)
        {
            _eFContext = eFContext;
            this.redisService = redisService;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="status">用户类型 0 用户 1 答主  -1 全部</param>
        /// <returns></returns>
        public IActionResult Index(string userId, int status = -1)
        {
            int pageIndex = PageHelper.GetPageIndex(Request);
            int pageSize = PageHelper.GetPageSize(Request);
            int totalCount = 0;
            List<ZJ_User> users = new List<ZJ_User>();
            List<HomeUsers> homeUsers = new List<HomeUsers>();
            var skipCount = (pageIndex - 1) * pageSize;
            if (!string.IsNullOrEmpty(userId))
            {
                totalCount = _eFContext.ZJ_Users.AsNoTracking().Where(a => a.Numbers == userId).Count();
                users = _eFContext.ZJ_Users.AsNoTracking().Where(a => a.Numbers == userId).Skip(skipCount).Take(pageSize).ToList();
            }
            else
            {
                if (status == -1)
                {
                    totalCount = _eFContext.ZJ_Users.AsNoTracking().Count();
                    users = _eFContext.ZJ_Users.AsNoTracking().Skip(skipCount).Take(pageSize).ToList();
                }
                else
                {
                    totalCount = _eFContext.ZJ_Users
                        .Where(a => a.UserType == status).AsNoTracking().Count();
                    users = _eFContext.ZJ_Users.AsNoTracking()
                        .Where(a => a.UserType == status).Skip(skipCount).Take(pageSize).ToList();
                }
            }
            foreach (var item in users)
            {
                var homeuser = new HomeUsers(item);
                var user= redisService.GetUserRedis(item.FId);
                homeuser.Order = user != null ? user.Order : 0;
                homeUsers.Add(homeuser);
            }
            ViewBag.Users = homeUsers;
            ViewBag.PageInfo = new PageInfo
            {
                Count = totalCount,
                PageIndex = pageIndex,
                PageSize = pageSize,
                PageUrl = PageHelper.GetPageUrl(Request, out string absoluteUrl)
            };
            ViewBag.userId = userId;
            ViewBag.status = status;
            //_eFContext.ZJ_Users(async=>async.)
            return View();
        }

        public IActionResult Index_Update(string userId)
        {
            var user = _eFContext.ZJ_Users.Where(a => a.FId == userId).FirstOrDefault();
            ViewBag.User = user;
            return View();
        }
        public string Index_UpdateSave(ZJ_User user)
        {
            var _user = _eFContext.ZJ_Users.Where(a => a.FId == user.FId).FirstOrDefault();
            if (_user != null)
            {
                _user.UserType = user.UserType;
                _user.AnswerPrice = user.AnswerPrice;
                _user.AnswerTime = user.AnswerTime;
                _eFContext.ZJ_Users.Update(_user);
                redisService.SetUserRedis(_user);
            }
            _eFContext.SaveChanges();
            
            return "ok";
        }
        public IActionResult Index_Edit_UserOrder(string userId, int order)
        {
            ViewBag.UserId = userId;
            ViewBag.Order = order;
            return View();
        }

        public string Index_Edit_UserOrderSave(string userId, int order)
        {
            var user=redisService.GetUserRedis(userId);
            if (user !=null)
            {
                user.Order = order;
                redisService.SetUserRedis(user);                
            }
            return "1";
        }
    }
}