using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface.Service
{
    public interface IUserSelfService
    {
        Task<List<AdminMenu>> GetUerMenusAsync(int userId);
    }
}
