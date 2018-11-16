﻿using Common;
using Common.Redis;
using Model;
using Service.Interface.Service;
using Service.Model;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service
{
    public class RedisService: IRedisService
    {
        private static readonly string _key_userToken = "oa_userToken_";
        private static readonly string _key_userInfo = "oa_userInfo_";
        private static readonly string _key_userMenus = "oa_userMenus_";
        public TimeSpan DefaultTimeSpan { get; set; } = new TimeSpan(1, 0, 0, 0);
        private readonly IDatabase redis ;

        public RedisService(RedisCore redisCore)
        {
            redis= redisCore.RedisDB;
        }

        public void AddUserLoginInfo(string token, AdminUser user, List<AdminMenu> menus)
        {
            var userInfo = TransformUserInfo(user);
            redis.SetCache(_key_userInfo + user.Id, userInfo, DefaultTimeSpan);
            redis.SetCache(_key_userToken + user.Id, token, DefaultTimeSpan);
            redis.SetCache(_key_userMenus + user.Id, menus, DefaultTimeSpan);
        }
        public void DeleteUserLoginInfo(string userId)
        {
            redis.DeleteCache(_key_userToken + userId);
            redis.DeleteCache(_key_userInfo + userId);
            redis.DeleteCache(_key_userMenus + userId);
        }

        public void UpdateUserInfo(AdminUser user, List<AdminMenu> menus)
        {
            var userInfo = TransformUserInfo(user);
            redis.SetCache(_key_userInfo + user.Id, userInfo, DefaultTimeSpan);
            redis.SetCache(_key_userMenus + user.Id, menus, DefaultTimeSpan);
        }

        public string GetToken(string userId)
        {
            var token = redis.GetCache(_key_userToken + userId);
            //if (string.IsNullOrEmpty(token))
            //{
            //    throw new Exception(ErrorDefine.redisTokenIsNull);
            //}
            return token;
        }

        public bool SetMenus(string userId, List<AdminMenu> menus)
        {
            return redis.SetCache(_key_userMenus + userId, menus, DefaultTimeSpan);
        }

        public List<AdminMenu> GetMenus(string userId)
        {
            var menus = redis.GetCache<List<AdminMenu>>(_key_userMenus + userId);
            //if (menus == null)
            //{
            //    throw new Exception(ErrorDefine.redisMenuIsNull);
            //}
            return menus;
        }

        public AdminUserInfo TransformUserInfo(AdminUser user)
        {
            var userInfo = new AdminUserInfo();
            userInfo.Id = user.Id;
            userInfo.Name = user.Name;
            userInfo.NickName = user.NickName;
            userInfo.Password = user.Password;
            userInfo.Phone = user.Phone;
            userInfo.RealName = user.RealName;
            userInfo.PwdFlag = user.PwdFlag;
            userInfo.Type = user.Type??0;
            userInfo.Roles = user.UserRoles.Select(ur => new AdminRole
            {
                Id = ur.RoleId,
                Name = ur.Role.Name,
                Status = ur.Role.Status
            }).ToList();
            userInfo.Status = user.Status;
            userInfo.StatusTime = user.StatusTime;
            userInfo.Gender = user.Gender;
            userInfo.CreateTime = user.CreateTime;
            if (userInfo.Roles != null && userInfo.Roles.Count > 0)
            {
                userInfo.CurrentRole = userInfo.Roles[0];
            }
            return userInfo;
            //return redis.SetCache(_key_userInfo + user.Id, userInfo, DefaultTimeSpan);
        }

        public bool SetUserInfo(AdminUserInfo userInfo)
        {
            return redis.SetCache(_key_userInfo + userInfo.Id, userInfo, DefaultTimeSpan);
        }

        public AdminUserInfo GetUserInfo(string userId)
        {
            var userInfo = redis.GetCache<AdminUserInfo>(_key_userInfo + userId);
            //if (userInfo == null)
            //{
            //    throw new Exception(ErrorDefine.redisUserInfoIsNull);
            //}
            return userInfo;
        }
    }
}