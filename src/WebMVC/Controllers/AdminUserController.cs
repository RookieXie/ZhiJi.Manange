using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Microsoft.AspNetCore.Mvc;
using Model;
using Service.Interface.Service;
using WebCore.Filter;
using WebCore.Utility;
using WebCore.ViewModel;
using WebMVC.Filter;

namespace WebCore.Controllers
{
    //[CheckTokenFilter(NeedCheck = true)]
    //[CheckMenuFilter(NeedCheck = true)]
    [NoFilter]
    public class AdminUserController : Controller
    {
        private IAdminUserService _userService;
        private IAdminRoleService _roleService;
        public AdminUserController(IAdminUserService userService,IAdminRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        public async Task<IActionResult> Index()
        {

            var users = await _userService.GetAdminsUsersAsync(1,100);
            ViewBag.AdminUserList = users;
            return View();
        }

        public async  Task<IActionResult> Add()
        {
            var roles = await _roleService.GetListByPageAsync(1, 100);
            ViewBag.Roles = roles;
            return View();
        }

        [HttpPost]
        public async Task<ResponseResult<int>>  Add_Save()
        {
            //获取参数
            StreamReader streamReader = new StreamReader(Request.Body);
            var paramStr = streamReader.ReadToEnd();
            RequestParamterHelper requestParamterHelper = new RequestParamterHelper(paramStr);

            AdminUser adminUser = new AdminUser();
            adminUser.Name = requestParamterHelper.GetParamValue("name")[0];
            var user = await _userService.GetAdminUserByNameAsync(adminUser.Name);
            if (user!=null)
            {
                return new ResponseResult<int>(false,-1);
            }
            //adminUser.Password = requestParamterHelper.GetParamValue("password")[0];
            var phonedic = requestParamterHelper.GetParamValue("phone");

            if (phonedic != null && phonedic.Count > 0)
            {
                adminUser.Phone = phonedic[0];
            }

            adminUser.Gender = int.Parse(requestParamterHelper.GetParamValue("gender")[0]);
            adminUser.UserRoles = new List<AdminUserRole>();
            foreach (var it in requestParamterHelper.GetParamValue("roleId"))
            {
                var roleId = int.Parse(it);
                var adminUserRole = new AdminUserRole
                {
                    RoleId = roleId
                };
                adminUser.UserRoles.Add(adminUserRole);
            }

            //adminUser.Password = MD5Helper.MD5Crypto16("123456");
            //123456 加密后的值
            adminUser.Password = "E10ADC3949BA59AB";

            var serviceResult = await _userService.AddModelAsync(adminUser);
            return new ResponseResult<int>(true, serviceResult) ;
        }

        /// <summary>
        /// 添加公会管理/公会长
        /// </summary>
        /// <param name="adminUser"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<CusJsonResult<int>> AddGuildMaster_Save([FromBody]AdminUser adminUser)
        {
          
            var user = await _userService.GetAdminUserByNameAsync(adminUser.Name);
            if (user != null)
            {
                return new CusJsonResult<int>(false, "","账户名已存在");
            }

            AdminUserRole adminUserRole = new AdminUserRole
            {
                RoleId = 48
            };

            adminUser.Type = 2;
            adminUser.Password = "E10ADC3949BA59AB";
            adminUser.CreateTime = DateTime.Now;
            adminUser.UserRoles = new List<AdminUserRole>()
            {
                adminUserRole
            };
            adminUser.Status = 1;
            var serviceResult = await _userService.AddModelAsync(adminUser);
            return new CusJsonResult<int>(true, "","", adminUser.Id);
        }

        public async  Task<IActionResult> Edit(int id)
        {
            var model =await _userService.GetAdminUserAsync(id);
            var roles = await _roleService.GetListByPageAsync(1, 100);

            ViewBag.Roles = roles;
            ViewBag.User = model;
            return View();
        }

        [HttpPost]
        public async Task<ResponseResult<int>> Edit_Save()
        {

            //获取参数
            StreamReader streamReader = new StreamReader(Request.Body);
            var paramStr = streamReader.ReadToEnd();
            RequestParamterHelper requestParamterHelper = new RequestParamterHelper(paramStr);

           
            var id = int.Parse(requestParamterHelper.GetParamValue("id")[0]);
            AdminUser adminUser =await _userService.GetAdminUserTrackingAsync(id);
            adminUser.Name = requestParamterHelper.GetParamValue("name")[0];
            //adminUser.Password = requestParamterHelper.GetParamValue("password")[0];
            var phonedic = requestParamterHelper.GetParamValue("phone");
            if (phonedic != null && phonedic.Count > 0)
            {
                adminUser.Phone = phonedic[0];
            }
            adminUser.Gender = int.Parse(requestParamterHelper.GetParamValue("gender")[0]);
            adminUser.UserRoles.RemoveRange(0, adminUser.UserRoles.Count);
            foreach (var it in requestParamterHelper.GetParamValue("roleId"))
            {
                var roleId = int.Parse(it);
                var adminUserRole = new AdminUserRole
                {
                    RoleId = roleId
                };
                adminUser.UserRoles.Add(adminUserRole);
            }
            //adminUser.Password = MD5Helper.MD5Crypto16(adminUser.Password);
            var serviceResult = await _userService.UpdateModelAsync(adminUser);
            return new ResponseResult<int>(true, serviceResult);
        }

        /// <summary>
        /// 重置用户密码 密码为 123456
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ResponseResult<int>> Edit_ResettingPassword(int id)
        {
            AdminUser adminUser = await _userService.GetAdminUserTrackingAsync(id);
            //adminUser.Password = MD5Helper.MD5Crypto16("123456");
            //123456 加密后的值
            adminUser.Password = "E10ADC3949BA59AB";
            var serviceResult = await _userService.UpdateModelAsync(adminUser);
            return new ResponseResult<int>(true, serviceResult);
        }

        [HttpPost]
        public async Task<ResponseResult<int>>  Delete(AdminUser adminUser)
        {
            var serviceResult = await _userService.DeleteModelAsync(adminUser);
            return new ResponseResult<int>(true, serviceResult);
        }

        [HttpPost]
        public async Task<ResponseResult<int>> Edit_Stop(int id)
        {
            var res = 0;
            var user = await _userService.GetByIdAsync(id);
            if (user.Status != 0)
            {
                user.Status = 0;
                res = await _userService.UpdateModelAsync(user);
            }
            return new ResponseResult<int>(true, res);
        }

        [HttpPost]
        public async Task<ResponseResult<int>> Edit_Start(int id)
        {
            var res = 0;
            var user = await _userService.GetByIdAsync(id);
            if (user.Status != 1)
            {
                user.Status = 1;
                res = await _userService.UpdateModelAsync(user);
            }
            return new ResponseResult<int>(true, res);
        }

        [HttpPost]
        public async Task<ResponseResult<int>> DeleteRange(List<int> ids)
        {
            var adminUsers = new List<AdminUser>();
            foreach (var it in ids)
            {
                adminUsers.Add(new AdminUser { Id = it });
            }
            var serviceResult = await _userService.DeleteRangeAsync(adminUsers);
            return new ResponseResult<int>(true);
        }

      
    }
}