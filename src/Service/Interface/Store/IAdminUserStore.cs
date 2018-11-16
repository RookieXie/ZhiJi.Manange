using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Service.Interface.Store
{
    public interface IAdminUserStore:IBaseStore<AdminUser>
    {
        /// <summary>
        /// 登陆时获取用户信息包含角色详情
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<AdminUser> GetAdminUserAsync(string userName,string password);

        /// <summary>
        /// 修改密码时验证原密码
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<AdminUser> CheckAdminUserAsync(string userName, string password);

        /// <summary>
        /// 获取用户信息包含角色id列表
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<AdminUser> GetAdminUserAsync(int id);

        /// <summary>
        /// 通过账户名获取用户信息，查看是否同名用户已存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<AdminUser> GetAdminUserByNameAsync(string name);

        /// <summary>
        /// 获取用户角色id列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<int>> GetUserRolesAsync(int userId);

        ///// <summary>
        ///// 添加管理员
        ///// </summary>
        ///// <param name="user"></param>
        ///// <returns></returns>
        //Task<int> AddUserAsync(AdminUser user, int roleIds);

        /// <summary>
        /// 获取用户列表包含角色详情
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<List<AdminUser>> GetAdminsUsersAsync(int pageIndex,int pageSize);

        /// <summary>
        /// 获取用户信息包含角色id跟踪状态用于修改
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<AdminUser> GetAdminUserTrackingAsync(int id);
    }
}
