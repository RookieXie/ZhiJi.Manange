using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface.Service
{
    public interface ILogInService
    {
        Task<bool> LogIn(string userId,string password);
    }
}
