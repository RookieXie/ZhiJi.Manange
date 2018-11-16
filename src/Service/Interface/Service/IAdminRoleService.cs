using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface.Service
{
    public interface IAdminRoleService:IBaseService<AdminRole>
    {
        Task<int> AddRoleAsync(AdminRole role, List<int> menuIds);
        Task<int> EditRoleAsync(AdminRole role, List<int> menuIds);
    }
}
