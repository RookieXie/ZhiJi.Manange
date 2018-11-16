using Common;
using Model;
using Service.Interface.Service;
using Service.Interface.Store;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class AdminUserService : BaseService<AdminUser>, IAdminUserService
    {
        private IAdminUserStore _userStore;
        public AdminUserService(IAdminUserStore userStore):base(userStore)
        {
            _userStore = userStore;
        }

        /// <summary>
        /// 获取用户信息包含角色id列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AdminUser> GetAdminUserAsync(int id)
        {
            return await _userStore.GetAdminUserAsync(id);
        }
        /// <summary>
        /// 获取用户信息包含角色id链表跟踪状态用于修改
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AdminUser> GetAdminUserTrackingAsync(int id)
        {
            return await _userStore.GetAdminUserTrackingAsync(id);
        }

        //public async Task<int> AddUserAsync(AdminUser user,List<int> roleIds)
        //{
        //    user.UserRoles = new List<AdminUserRole>();
        //    foreach (var it in roleIds)
        //    {
        //        user.UserRoles.Add(new AdminUserRole { RoleId = it});
        //    }
        //    return await _userStore.AddModelAsync(user);
        //}


        //public async Task<int> EditUserAsync(AdminUser user, List<int> roleIds)
        //{
        //    user.UserRoles = new List<AdminUserRole>();
        //    user.UserRoles.Add();
        //    return await _userStore.EditUserAsync(user, roleIds);
        //}

        /// <summary>
        /// 获取用户列表包含角色详情
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<List<AdminUser>> GetAdminsUsersAsync(int pageIndex, int pageSize)
        {
            return await _userStore.GetAdminsUsersAsync(pageIndex, pageSize);
        }

        public async Task<int> UpdateUserPassword(int userId,string oldPassword, string password)
        {
            var user = await _userStore.GetByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("获取用户信息失败，请重新登陆");

            }
            else if (user.Password!= MD5Helper.MD5Crypto16(oldPassword))
            {
                throw new Exception("原密码输入错误");
            }
           
            else
            {
                user.Password = MD5Helper.MD5Crypto16(password);
                return await _userStore.UpdateModelAsync(user);
            }
        }

        /// <summary>
        /// 通过账户名获取用户信息，查看是否同名用户已存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<AdminUser> GetAdminUserByNameAsync(string name)
        {
            return _userStore.GetAdminUserByNameAsync(name);
        }
    }
}
