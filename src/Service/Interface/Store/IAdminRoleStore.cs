using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface.Store
{
    public interface IAdminRoleStore:IBaseStore<AdminRole>
    {
       
        Task<int> EditRoleAsync(AdminRole role, List<int> menuIds);
    }
}
