using Model;
using Service.Interface.Service;
using Service.Interface.Store;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class AdminRoleService:BaseService<AdminRole>,IAdminRoleService
    {
        private IAdminRoleStore _adminRoleStore;
        public AdminRoleService(IAdminRoleStore adminRoleStore):base(adminRoleStore)
        {
            _adminRoleStore = adminRoleStore;
        }

        public async Task<int> AddRoleAsync(AdminRole role, List<int> menuIds)
        {
            role.RoleMenus = new List<AdminRoleMenu>();
            foreach (var it in menuIds)
            {
                role.RoleMenus.Add(new AdminRoleMenu() { MenuId = it });
            }
            return await _adminRoleStore.AddModelAsync(role);
        }

        public async Task<int> EditRoleAsync(AdminRole role, List<int> menuIds)
        {
            return await _adminRoleStore.EditRoleAsync(role, menuIds);
        }
    }
}
