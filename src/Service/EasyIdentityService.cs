using Common;
using Common.Redis;
using Model;
using Service.Interface.Service;
using Service.Interface.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class EasyIdentityService : IEasydentityService
    {
        private IAdminUserStore _userStore;
        private IUserSelfService _userSelfService;
        private readonly IRedisService redis;
        public EasyIdentityService(IAdminUserStore userStore, IUserSelfService userSelfService, IRedisService redisService)
        {
            _userStore = userStore;
            _userSelfService = userSelfService;
            redis = redisService;
        }
        public async Task<AdminUser> Login(string userName, string password)
        {
            var user = await _userStore.GetAdminUserAsync(userName, password);
            if (user == null)
            {
                return null;
            }
            else
            {
                if (user.Status == 0)
                {
                    return null;
                }
                var token = GetToken(user.Id);
                var userMenus = await _userSelfService.GetUerMenusAsync(user.Id);
                
                redis.AddUserLoginInfo(token, user, userMenus);
                return user;
            }
        }

       

        public CusJsonResult Validate(string userId, string token, string url)
        {
            
            var tokenOnserver = redis.GetToken(userId);
            if (string.IsNullOrEmpty(tokenOnserver))
            {
                return new CusJsonResult(false, "101", "未登录或登录信息过期，请重新登陆");
            }
            if (tokenOnserver != token)
            {
                return new CusJsonResult(false, "101", "未登录或登录信息过期，请重新登陆");

            }

            var menus = redis.GetMenus(userId);
            if (menus == null || menus.Count == 0)
            {
                return new CusJsonResult(false, "102", "权限不足");
            }


            if (string.IsNullOrEmpty(url) || url == "/" || url== "#")
            {
                return new CusJsonResult(true, "", "");
            }

            var path = url.Trim('/').Split("_").FirstOrDefault();

            var menu = menus.Where(m => m.Url.IndexOf(path, StringComparison.OrdinalIgnoreCase) > -1).FirstOrDefault();
            if (menu == null)
            {
                return new CusJsonResult(false, "102", "权限不足");
            }
            else
            {
                return new CusJsonResult(true,"","");
            }
            
        }

        

        public string GetToken(int userId)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(userId.ToString());
            Random random = new Random();
            string randomStr =  random.Next(1000,9999).ToString();
            stringBuilder.Append(randomStr);
            stringBuilder.Append(DateTime.UtcNow.ToString("yyyy/MM/dd/HH/mm/ss"));
          
            var token = MD5Helper.MD5Crypto16(stringBuilder.ToString());
            return token;
        }

        
    }
}
