using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Model;
using Service.Interface.Service;
using WebCore.Filter;
using WebCore.Models;
using WebCore.ViewModel;

namespace WebCore.Controllers
{
    //[CheckTokenFilter(NeedCheck = true)]
    //[CheckMenuFilter(NeedCheck = true)]
    public class AdminMenuController : Controller
    {
        private IAdminMenuService _adminMenuService;
        public AdminMenuController(IAdminMenuService adminMenuService)
        {
            _adminMenuService = adminMenuService;
        }
        public async Task<IActionResult> Index(string token)
        {
            var result = new List<ViewMenu>();
            var menusIenum = await _adminMenuService.GetListByPageDescAsync(1,1000);
            var menus = menusIenum.Select(m => new ViewMenu
            {
                Id = m.Id,
                Name = m.Name,
                Url = m.Url,
                Type = m.Type,
                ParentId = m.ParentId,
                OrderNum = m.OrderNum
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

        public IActionResult Add(int id,int type)
        {
            ViewBag.ParentId = id;
            ViewBag.Type = type;
            return View();
        }

        [HttpPost]
        public async Task<ResponseResult<int>> Add_Save(AdminMenu model)
        {
            var serviceResult = await _adminMenuService.AddModelAsync(model);
            return new ResponseResult<int>(true, serviceResult);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = await _adminMenuService.GetByIdAsync(id);
            ViewBag.Menu = model;
            return View();
        }

        [HttpPost]
        public async Task<ResponseResult<int>> Edit_Save(AdminMenu model)
        {
            var dbModel =await _adminMenuService.GetByIdAsync(model.Id);
            dbModel.OrderNum = model.OrderNum;
            dbModel.Name = model.Name;
            dbModel.Url = model.Url;
            dbModel.IsDisplay = model.IsDisplay;
            var serviceResult = await _adminMenuService.UpdateModelAsync(dbModel);
            return new ResponseResult<int>(true, serviceResult);
        }

        [HttpPost]
        public async Task<ResponseResult<int>> Delete(AdminMenu model)
        {
            var serviceResult = await _adminMenuService.DeleteMenuAsync(model.Id);
            return new ResponseResult<int>(true, serviceResult);
        }

        [HttpPost]
        public async Task<ResponseResult<int>> DeleteRange(List<int> ids)
        {
            var adminMenus = new List<AdminMenu>();
            foreach (var it in ids)
            {
                adminMenus.Add(new AdminMenu { Id = it});
            }
            var serviceResult = await _adminMenuService.DeleteRangeAsync(adminMenus);
            return new ResponseResult<int>(true);
        }
    }
}