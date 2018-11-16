using Model;
using Service.Interface.Service;
using Service.Interface.Store;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class AdminMenuService:BaseService<AdminMenu>,IAdminMenuService
    {
        private IAdminMenuStore _adminMenuStore;
        public AdminMenuService(IAdminMenuStore adminMenuStore):base(adminMenuStore)
        {
            _adminMenuStore = adminMenuStore;
        }

        public async Task<List<AdminMenu>> GetMenusOfRole(int roleId)
        {
            var menus = await _adminMenuStore.GetMenusOfRole(roleId);
            return menus;
        }

        public async Task<List<AdminMenu>> GetMenusOfRole(int roleId,bool allShow)
        {
            var menus = await _adminMenuStore.GetMenusOfRole(roleId, allShow);
            return menus;
        }

        public async Task<List<AdminMenu>> GetListByPageDescAsync(int pageIndex, int pageSize)
        {
            return await _adminMenuStore.GetListByPageDescAsync(pageIndex,pageSize);
        }

        /// <summary>
        /// 删除上级菜单包含下级菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> DeleteMenuAsync(int id)
        {
            return await _adminMenuStore.DeleteMenuAsync(id);
        }
    }
}
