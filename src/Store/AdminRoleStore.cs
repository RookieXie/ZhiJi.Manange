using EFCore;
using Microsoft.EntityFrameworkCore;
using Model;
using Service.Interface.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store
{
    public class AdminRoleStore : BaseStore<AdminRole>, IAdminRoleStore
    {

        public AdminRoleStore(MySqlContext mySqlContext) : base(mySqlContext)
        {
        }

        public async Task<int> EditRoleAsync(AdminRole role, List<int> menuIds)
        {
            var oldRole =await _mySqlContext.AdminRoles.Where(r => r.Id == role.Id).Include(r=>r.RoleMenus).FirstOrDefaultAsync();

            oldRole.Name = role.Name;
            oldRole.RoleMenus.RemoveRange(0, oldRole.RoleMenus.Count);
            foreach (var it in menuIds)
            {
                oldRole.RoleMenus.Add(new AdminRoleMenu() { MenuId = it });
            }

            _mySqlContext.AdminRoles.Update(oldRole);
            var result = await _mySqlContext.SaveChangesAsync();
            return result;
        }
    }
}
