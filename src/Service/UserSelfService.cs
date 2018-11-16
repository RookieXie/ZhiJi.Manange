using Common;
using Model;
using Service.Interface.Service;
using Service.Interface.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class UserSelfService : IUserSelfService
    {

        private IAdminUserStore _userStore;

        private IAdminMenuStore _menuStore;
        public UserSelfService(IAdminUserStore userStore, IAdminMenuStore menuStore)
        {
            _userStore = userStore;
            _menuStore = menuStore;
        }

        public async Task<List<AdminMenu>> GetUerMenusAsync(int userId)
        {
            var roleIds = await _userStore.GetUserRolesAsync(userId);
            List<AdminMenu> menus = new List<AdminMenu>();
            if (roleIds != null && roleIds.Count > 0)
            {
                foreach (var roleId in roleIds)
                {
                    var roleMenus = await _menuStore.GetMenusOfRole(roleId);
                    menus.AddRange(roleMenus);
                }
                var disMenus = menus.Distinct(new MenuCompare()).ToList();
                return disMenus;
            }
            else
            {
                return null;
            }

        }
    }
}
