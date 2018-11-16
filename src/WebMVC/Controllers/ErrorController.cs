using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebCore.Models;
using WebMVC.Filter;

namespace WebCore.Controllers
{
    [NoFilter]
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 不允许访问
        /// </summary>
        /// <returns></returns>
        public IActionResult Inadmissibility()
        {
            ViewBag.Title = "权限不足";
            return View();
        }

        public IActionResult TokenOverdue()
        {
            ViewBag.Title = "权限不足";
            return View();
        }
    }
}