using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Model;
using Service.Interface.Service;
using WebCore.Filter;
using WebCore.Models;
using WebCore.Utility;
using WebCore.ViewModel;

namespace WebCore.Controllers
{
    //[CheckTokenFilter(NeedCheck = true)]
    //[CheckMenuFilter(NeedCheck = true)]
    public class AdminRoleController : Controller
    {

        private IAdminRoleService _roleService;
        private IAdminMenuService _menuService;
        public AdminRoleController(IAdminRoleService roleService,IAdminMenuService menuService)
        {
            _roleService = roleService;
            _menuService = menuService;
        }


        public async Task<IActionResult> Index()
        {

            var roles = await _roleService.GetListByPageAsync(1, 1000);
            ViewBag.Roles = roles;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var result = new List<ViewMenu>();
            
            var menusIenum = await _menuService.GetListByPageAsync(1, 1000);

            var menus = menusIenum.Select(m=>new ViewMenu {
                Id = m.Id,
                Name = m.Name,
                Url = m.Url,
                Type = m.Type,
                ParentId = m.ParentId,
                OrderNum = m.OrderNum
            }).ToList();

            foreach (var it in menus)
            {

                if (it.Type==1)
                {
                    result.Add(it);
                    it.SubMenus = menus.Where(m => m.ParentId == it.Id)
                        .OrderBy(m=>m.OrderNum)
                        .ToList();
                }
            }

            foreach (var it in result)
            {
                it.SubMenus.ForEach(m => m.SubMenus = menus.Where(s => s.ParentId == m.Id)
                        .OrderBy(s => s.OrderNum)
                        .ToList());
            }

            //foreach (var it in folderMenus)
            //{
            //    FolderMenu folderMenu = new FolderMenu();
            //    folderMenu.MenuFolder = it;
            //    folderMenu.ViewMenus =
            //        menus.Where(m => m.ParentId == it.Id).OrderBy(m => m.OrderNum)
            //        .Select(m => new ViewMenu {
            //            Menu = m,
            //            SubMenus = menus.Where(sub => sub.ParentId == m.Id)
            //            .OrderBy(sub => sub.OrderNum)
            //            .ToList()
            //        }).ToList();

            //    result.Add(folderMenu);
            //}

            ViewBag.Menus = result.OrderBy(m=>m.OrderNum).ToList();
            return View();
        }

        [HttpPost]
        public async Task<ResponseResult<int>> Add_Save()
        {
           
            //获取参数
            var streamReader = new StreamReader(Request.Body);
            var paramStr = streamReader.ReadToEnd();
            RequestParamterHelper requestParamterHelper = new RequestParamterHelper(paramStr);

            var roleNames = requestParamterHelper.GetParamValue("roleName");
            var roleName = roleNames[0];
            var menus = requestParamterHelper.GetParamValue("menuId");

            var role = new AdminRole();
            role.Name = roleName;

            var serviceResult = await _roleService.AddRoleAsync(role, menus.Select(m=>int.Parse(m)).ToList());
            return new ResponseResult<int>(true, serviceResult);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var result = new List<ViewMenu>();

            var menusIenum = await _menuService.GetListByPageAsync(1, 1000);

            var RoleMenu =await _menuService.GetMenusOfRole(id,true);

            var role = await _roleService.GetByIdAsync(id);
            ViewBag.Role = role;
            //var res = await Task.WhenAll(_menuService.GetListByPageAsync(1, 100), _menuService.GetMenusOfRole(id));


            var menus = menusIenum.Select(m => new ViewMenu
            {
                Id = m.Id,
                Name = m.Name,
                Url = m.Url,
                Type = m.Type,
                ParentId = m.ParentId,
                OrderNum = m.OrderNum,
                Own = RoleMenu.FindIndex(r => r.Id == m.Id) > -1,

            })
            .OrderBy(m => m.Type).ThenBy(m => m.ParentId).ToList();

            foreach (var it in menus)
            {

                if (it.Type == 1)
                {
                    result.Add(it);
                    it.SubMenus = menus.Where(m => m.ParentId == it.Id)
                        .OrderBy(m => m.OrderNum)
                        .ToList();
                }
            }

            foreach (var it in result)
            {
                it.SubMenus.ForEach(m => m.SubMenus = menus.Where(s => s.ParentId == m.Id)
                        .OrderBy(s => s.OrderNum)
                        .ToList());
            }

         

            ViewBag.Menus = result.OrderBy(m => m.OrderNum).ToList();
            return View();

        
        }

        [HttpPost]
        public async Task<ResponseResult<int>> Edit_Save()
        {
            //获取参数
            var streamReader = new StreamReader(Request.Body);
            var paramStr = streamReader.ReadToEnd();
            RequestParamterHelper requestParamterHelper = new RequestParamterHelper(paramStr);

            var roleNames = requestParamterHelper.GetParamValue("roleName");
            var roleName = roleNames[0];

            var roleIds = requestParamterHelper.GetParamValue("id");
            var roleId = roleIds[0];

            var menus = requestParamterHelper.GetParamValue("menuId");

            var role = new AdminRole();
            role.Name = roleName;
            role.Id = int.Parse(roleId);
            var serviceResult = await _roleService.EditRoleAsync(role, menus.Select(m => int.Parse(m)).ToList());
            return new ResponseResult<int>(true, serviceResult);
        }

        [HttpPost]
        public async Task<ResponseResult<int>> Delete(AdminRole model)
        {
            var serviceResult = await _roleService.DeleteModelAsync(model);
            return new ResponseResult<int>(true, serviceResult);
        }

        [HttpPost]
        public async Task<ResponseResult<int>> DeleteRange(List<int> ids)
        {
            var adminRoles = new List<AdminRole>();
            foreach (var it in ids)
            {
                adminRoles.Add(new AdminRole { Id = it });
            }
            var serviceResult = await _roleService.DeleteRangeAsync(adminRoles);
            return new ResponseResult<int>(true);
        }
    }
}