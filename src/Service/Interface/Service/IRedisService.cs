using Model;
using Model.DTO;
using Service.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Interface.Service
{
    public interface IRedisService
    {
        void AddUserLoginInfo(string token, AdminUser user, List<AdminMenu> menus);
        void DeleteUserLoginInfo(string userId);
        void UpdateUserInfo(AdminUser user, List<AdminMenu> menus);
        string GetToken(string userId);
        bool SetMenus(string userId, List<AdminMenu> menus);
        List<AdminMenu> GetMenus(string userId);
        AdminUserInfo TransformUserInfo(AdminUser user);
        bool SetUserInfo(AdminUserInfo userInfo);
        AdminUserInfo GetUserInfo(string userId);
        /// <summary>
        /// 获取键值对缓存
        /// </summary>
        /// <returns></returns>
        List<KeyValueInfo> GetKeyValues();

        /// <summary>
        /// 设置键值对缓存
        /// </summary>
        /// <returns></returns>
        bool SetKeyValues(List<KeyValueInfo> list);

        /// <summary>
        /// 删除键值对缓存
        /// </summary>
        /// <param name="versions"></param>
        /// <returns></returns>
        long DeleteKeyValues();
    }
}
