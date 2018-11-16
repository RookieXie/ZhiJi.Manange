using EFCore;
using Microsoft.EntityFrameworkCore;
using Model;
using Service.Interface.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store
{
    public class AdminUserStore : BaseStore<AdminUser>, IAdminUserStore
    {

        public AdminUserStore(MySqlContext mySqlContext) : base(mySqlContext)
        {
        }

        /// <summary>
        /// 登陆时获取用户信息包含角色详情
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<AdminUser> GetAdminUserAsync(string userName,string password)
        {
            var user =await _mySqlContext.AdminUsers
                .AsNoTracking()
                .Where(u=>u.Name == userName && u.Password==password)
                .Include(u => u.UserRoles)
                .ThenInclude(urs => urs.Role)
                .FirstOrDefaultAsync();
            return user;
        }

        /// <summary>
        /// 修改密码时验证原密码
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<AdminUser> CheckAdminUserAsync(string userName, string password)
        {
            var user = await _mySqlContext.AdminUsers
                .Where(u => u.Name == userName && u.Password == password)
                .FirstOrDefaultAsync();
            return user;
        }

        /// <summary>
        /// 获取用户信息包含角色id列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AdminUser> GetAdminUserAsync(int id)
        {
            var user = await _mySqlContext.AdminUsers
                .AsNoTracking().Where(u=>u.Id==id)
                .Include(u=>u.UserRoles)
                .FirstOrDefaultAsync();
            return user;
        }

        /// <summary>
        /// 获取用户信息包含角色id列表跟踪状态用于修改
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AdminUser> GetAdminUserTrackingAsync(int id)
        {
            var user = await _mySqlContext.AdminUsers
                .Where(u => u.Id == id)
                .Include(u => u.UserRoles)
                .FirstOrDefaultAsync();
            return user;
        }

        /// <summary>
        /// 获取用户角色id列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<int>> GetUserRolesAsync(int id)
        {
            var roleIds = await _mySqlContext.AdminUserRoles.AsNoTracking().Where(u=>u.UserId== id).Select(u=>u.RoleId).ToListAsync();
            return roleIds;
        }



        //public async Task<int> AddUserAsync(AdminUser user)
        //{
        //    _mySqlContext.AdminUsers.Add(user);
        //    return await _mySqlContext.SaveChangesAsync();

        //}

            /// <summary>
            /// 获取用户列表包含角色详情
            /// </summary>
            /// <param name="pageIndex"></param>
            /// <param name="pageSize"></param>
            /// <returns></returns>
        public async Task<List<AdminUser>> GetAdminsUsersAsync(int pageIndex, int pageSize)
        {
            var skipCount = (pageIndex - 1) * pageSize;
            var result = await _mySqlContext.AdminUsers.Include(u=>u.UserRoles).ThenInclude(urs=>urs.Role).AsNoTracking().Skip(skipCount).Take(pageSize).ToListAsync(); 
            return result;
        }

        /// <summary>
        /// 通过账户名获取用户信息，查看是否同名用户已存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AdminUser> GetAdminUserByNameAsync(string name)
        {
            var res = await _mySqlContext.AdminUsers.AsNoTracking().Where(u => u.Name == name).FirstOrDefaultAsync();
            return res;
        }
    }
}
