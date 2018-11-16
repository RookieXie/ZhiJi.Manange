using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface.Store
{
    public interface IAdminMenuStore:IBaseStore<AdminMenu>
    {
        /// <summary>
        /// 获取角色权限列表 登陆时 不包含隐藏菜单
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<List<AdminMenu>> GetMenusOfRole(int roleId);

        /// <summary>
        /// 获取角色权限列表 编辑时 包含隐藏菜单
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<List<AdminMenu>> GetMenusOfRole(int roleId,bool allShow);

        Task<List<AdminMenu>> GetListByPageDescAsync(int pageIndex, int pageSize);

        /// <summary>
        /// 删除菜单包含下级菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<int> DeleteMenuAsync(int id);
    }
}
