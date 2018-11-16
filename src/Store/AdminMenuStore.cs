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
    public class AdminMenuStore : BaseStore<AdminMenu>, IAdminMenuStore
    {

        public AdminMenuStore(MySqlContext mySqlContext) : base(mySqlContext)
        {
        }

        /// <summary>
        /// 加载角色权限时使用，不包含隐藏
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<List<AdminMenu>> GetMenusOfRole(int roleId)
        {
            var menus = await _mySqlContext.AdminRoleMenus
                .Include(r => r.Menu)
                .Where(r => r.RoleId == roleId && r.Menu.IsDisplay==true)
                .Select(r=>r.Menu)
                .ToListAsync();
            return menus;
        }

        /// <summary>
        /// 管理员编辑权限时使用，包含隐藏
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<List<AdminMenu>> GetMenusOfRole(int roleId,bool allShow)
        {
            if (!allShow)
            {
                return await GetMenusOfRole(roleId);

            }
            var menus = await _mySqlContext.AdminRoleMenus
                .Include(r => r.Menu)
                .Where(r => r.RoleId == roleId)
                .Select(r => r.Menu)
                .ToListAsync();
            return menus;
        }

        public async Task<List<AdminMenu>> GetListByPageDescAsync(int pageIndex, int pageSize)
        {
            var skipCount = (pageIndex - 1) * pageSize;
            var result = await _mySqlContext.AdminMenus.AsNoTracking().Skip(skipCount).Take(pageSize).OrderByDescending(u=>u.Id).ToListAsync(); ;
            return result;
        }

        public async Task<int> DeleteMenuAsync(int id)
        {
            var menu = await _mySqlContext.AdminMenus.FirstOrDefaultAsync(m=>m.Id==id);

            _mySqlContext.AdminMenus.Remove(menu);

            if (menu.Type == 3)
            {

            }
            else if (menu.Type == 2)
            {
                var menus = _mySqlContext.AdminMenus.Where(m=>m.ParentId==id);
                _mySqlContext.AdminMenus.RemoveRange(menus);
            }
            else if (menu.Type == 1)
            {
                
                var vmenus = _mySqlContext.AdminMenus.Where(m => m.ParentId == id);
                _mySqlContext.AdminMenus.RemoveRange(vmenus);
                foreach (var it in vmenus)
                {
                    var subs = _mySqlContext.AdminMenus.Where(m => m.ParentId == it.Id);
                    _mySqlContext.AdminMenus.RemoveRange(subs);
                }
            }
            return await _mySqlContext.SaveChangesAsync();
        }
    }
}
