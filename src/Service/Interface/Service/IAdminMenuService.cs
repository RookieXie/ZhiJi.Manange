using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface.Service
{
    public interface IAdminMenuService:IBaseService<AdminMenu>
    {
        Task<List<AdminMenu>> GetMenusOfRole(int roleId);

        Task<List<AdminMenu>> GetMenusOfRole(int roleId, bool allShow);

        Task<List<AdminMenu>> GetListByPageDescAsync(int pageIndex, int pageSize);

        Task<int> DeleteMenuAsync(int id);
    }
}
