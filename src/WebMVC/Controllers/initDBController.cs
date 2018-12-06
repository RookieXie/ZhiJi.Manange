using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCore;
using Microsoft.AspNetCore.Mvc;

namespace WebMVC.Controllers
{
    public class InitDBController : Controller
    {

        //private readonly MySqlContext _eFContext;
        //private readonly OfficialMySqlContext _officialMySqlContext;
        //public InitDBController(MySqlContext eFContext, OfficialMySqlContext officialMySqlContext)
        //{
        //    _eFContext = eFContext;
        //    _officialMySqlContext = officialMySqlContext;
        //}
        //public string Index()
        //{
        //    try
        //    {
        //        var list = _eFContext.AdminMenus.ToList();
        //        _officialMySqlContext.AddRange(list);
        //        var rolelist = _eFContext.AdminRoles.ToList();
        //        _officialMySqlContext.AddRange(rolelist);
        //        var userlist = _eFContext.AdminUsers.ToList();
        //        _officialMySqlContext.AddRange(userlist);
        //        //_officialMySqlContext.SaveChanges();
        //        var rmlist = _eFContext.AdminRoleMenus.ToList();
        //        _officialMySqlContext.AddRange(rmlist);
        //        var urlist = _eFContext.AdminUserRoles.ToList();
        //        _officialMySqlContext.AddRange(urlist);
        //        //_officialMySqlContext.SaveChanges();

        //        //业务数据
        //        var zjulist = _eFContext.ZJ_Users.ToList();
        //        _officialMySqlContext.AddRange(zjulist);

        //        var wulist = _eFContext.ZJ_WXCommon_Users.ToList();
        //        _officialMySqlContext.AddRange(wulist);

        //        var paylist = _eFContext.ZJ_PayOrders.ToList();
        //        _officialMySqlContext.AddRange(paylist);

        //        var balancelist = _eFContext.ZJ_Balances.ToList();
        //        _officialMySqlContext.AddRange(balancelist);

        //        var chatMessages = _eFContext.ZJ_ChatMessages.ToList();
        //        _officialMySqlContext.AddRange(chatMessages);

        //        var chats = _eFContext.ZJ_Chats.ToList();
        //        _officialMySqlContext.AddRange(chats);
        //        _officialMySqlContext.SaveChanges();
        //        return "ok";
        //    }
        //    catch (Exception ex)
        //    {

        //        return ex.Message;
        //    }
        //}
    }
}