using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface.Service
{
    public interface IAdminUserService:IBaseService<AdminUser>
    {
        /// <summary>
        /// 获取用户信息包含角色id链表
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<AdminUser> GetAdminUserAsync(int id);

        /// <summary>
        /// 获取用户信息包含角色id链表跟踪状态用于修改
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<AdminUser> GetAdminUserTrackingAsync(int id);


        /// <summary>
        /// 通过账户名获取用户信息，查看是否同名用户已存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<AdminUser> GetAdminUserByNameAsync(string name);

        ///// <summary>
        ///// 添加用户
        ///// </summary>
        ///// <param name="user"></param>
        ///// <returns></returns>
        //Task<int> AddUserAsync(AdminUser user,List<int> roleIds);

        ///// <summary>
        ///// 修改用户
        ///// </summary>
        ///// <param name="user"></param>
        ///// <returns></returns>
        //Task<int> EditUserAsync(AdminUser user, List<int> roleIds);

        /// <summary>
        /// 获取用户列表包含角色详情
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<List<AdminUser>> GetAdminsUsersAsync(int pageIndex, int pageSize);


        Task<int> UpdateUserPassword(int userId,string name, string password);
    }
}
