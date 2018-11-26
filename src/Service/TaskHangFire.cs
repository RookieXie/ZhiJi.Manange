using Common;
using EFCore;
using Model;
using Service.Interface.Service;
using Service.Interface.task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class TaskHangFire : ITaskHangFire
    {
        private MySqlContext mySqlContent;
        private readonly IRedisService redis;
        private readonly ILogHelper logHelper;

        public TaskHangFire(MySqlContext _mySqlContent, IRedisService redisHelper, ILogHelper _logHelper)
        {
            mySqlContent = _mySqlContent;
            redis = redisHelper;
            logHelper = _logHelper;
        }
        public async Task<int> CheckNoAnswerChatFourtyEightHours()
        {
            var noAnswerChats = mySqlContent.ZJ_Chats.Where(a => a.Status == 0 && a.IsDelete == 0 && a.CreateTime <= DateTime.Now.AddHours(-48)).ToList();
            foreach (var item in noAnswerChats)
            {
                item.Status = 2;
                //还钱                
            }
            var res = await mySqlContent.SaveChangesAsync();
            return res;
        }
        public async Task<int> CheckChatEnd()
        {
            var endChats = mySqlContent.ZJ_Chats.Where(a => a.Status == 1 && a.IsDelete == 0 && a.EndTime <= DateTime.Now).ToList();
            foreach (var item in endChats)
            {
                item.Status = 2;
                //转移到账上
                var zj_UserA = mySqlContent.ZJ_Users.Where(a => a.FId == item.AnswerId)
                .FirstOrDefault();
                var _payOrder = mySqlContent.ZJ_PayOrders.Where(a=>a.ChatId== item.FId).FirstOrDefault();
                if (zj_UserA != null && _payOrder != null)
                {
                    var totalMoney = zj_UserA.TotalMoney + _payOrder.Money;

                    var zj_balance = new ZJ_Balance
                    {
                        FId = UtilHelper.GetNewGuid(),
                        UserId = zj_UserA.FId,
                        AdverseId = item.FId,
                        AfterMoney = totalMoney,
                        BeforeMoney = zj_UserA.TotalMoney,
                        ChangeMoney = _payOrder.Money,
                        ChangeType = 0,
                        CreateTime = DateTime.Now,
                    };
                    zj_UserA.TotalMoney = totalMoney;
                    mySqlContent.ZJ_Users.Update(zj_UserA);
                    mySqlContent.ZJ_Balances.Add(zj_balance);
                }
            }
            var res = await mySqlContent.SaveChangesAsync();
            return res;
        }
    }
}
