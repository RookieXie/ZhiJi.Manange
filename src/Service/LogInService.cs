using Service.Interface.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class AdminLogIn : ILogInService
    {
        public Task<bool> LogIn(string userId, string password)
        {
            throw new NotImplementedException();
        }
    }
}
