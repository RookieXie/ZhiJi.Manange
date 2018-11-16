using Model;
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
    }
}
