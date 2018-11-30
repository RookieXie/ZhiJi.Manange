using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCore;
using Microsoft.AspNetCore.Mvc;
using Model.DTO;
using Service.Interface.Service;

namespace WebMVC.Controllers
{
    public class KeyValueController : Controller
    {
        private readonly MySqlContext _eFContext;
        private readonly IRedisService _redisService;
        public KeyValueController(MySqlContext eFContext, IRedisService redisService)
        {
            _eFContext = eFContext;
            _redisService = redisService;
        }
        public IActionResult KeyValue()
        {
            List<KeyValueInfo> list = _redisService.GetKeyValues();
            if (list == null)
            {
                list = new List<KeyValueInfo>();
            }
            ViewBag.KeyValueInfo = list;
            return View();
        }
        public IActionResult KeyValue_Add()
        {
            return View();
        }
        public IActionResult KeyValue_Update(string key)
        {
            List<KeyValueInfo> list = _redisService.GetKeyValues();
            if (list != null)
            {
                KeyValueInfo keyValueInfo = list.Where(a => a.Key == key).FirstOrDefault();
                if (keyValueInfo != null)
                {
                    ViewBag.KeyValueInfo = keyValueInfo;
                }
                else
                {
                    ViewBag.KeyValueInfo = new KeyValueInfo();
                }

            }
            return View();
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public string KeyValue_AddSave(string key, string value, string description)
        {

            List<KeyValueInfo> list = _redisService.GetKeyValues();
            if (list == null)
            {
                list = new List<KeyValueInfo>();
                KeyValueInfo keyValueInfo = new KeyValueInfo();
                keyValueInfo.Key = key;
                keyValueInfo.Value = value;
                keyValueInfo.Description = description;
                list.Add(keyValueInfo);
            }
            else
            {
                KeyValueInfo keyValueInfo = list.Where(a => a.Key == key).FirstOrDefault();
                if (keyValueInfo == null)
                {
                    KeyValueInfo _keyValueInfo = new KeyValueInfo();
                    _keyValueInfo.Key = key;
                    _keyValueInfo.Value = value;
                    _keyValueInfo.Description = description;
                    list.Add(_keyValueInfo);
                }
            }
            _redisService.SetKeyValues(list);

            return "1";
        }
        /// <summary>
        /// 根据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value">状态</param>
        /// <returns></returns>
        public string KeyValue_UpdateSave(string key, string value, string description)
        {
            List<KeyValueInfo> list = _redisService.GetKeyValues();

            KeyValueInfo keyValueInfo = list.Where(a => a.Key == key).FirstOrDefault();
            if (keyValueInfo != null)
            {
                keyValueInfo.Value = value;
                keyValueInfo.Description = description;
            }
            _redisService.SetKeyValues(list);
            return "1";
        }
        /// <summary>
        /// 删除对应缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string KeyValue_Delete(string key)
        {
            List<KeyValueInfo> list = _redisService.GetKeyValues();
            if (list != null)
            {
                KeyValueInfo keyValueInfo = list.Where(a => a.Key == key).FirstOrDefault();
                if (keyValueInfo != null)
                {
                    list.Remove(keyValueInfo);
                }
                _redisService.SetKeyValues(list);
            }
            return "1";
        }
    }
}