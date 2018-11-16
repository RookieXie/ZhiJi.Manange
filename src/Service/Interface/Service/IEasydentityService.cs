using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface.Service
{
    public interface IEasydentityService
    {
        Task<AdminUser> Login(string userName, string password);

        CusJsonResult Validate(string userId,string token,string url);

    }
}
