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
        private readonly IWXPayHelper wXPayHelper;

        public TaskHangFire(MySqlContext _mySqlContent, IRedisService redisHelper, ILogHelper _logHelper, IWXPayHelper _wXPayHelper)
        {
            mySqlContent = _mySqlContent;
            redis = redisHelper;
            logHelper = _logHelper;
            wXPayHelper = _wXPayHelper;
        }
        public async Task<int> CheckNoAnswerChatFourtyEightHours()
        {
            var noAnswerChats = mySqlContent.ZJ_Chats.Where(a => a.Status == 0 && a.IsDelete == 0 && a.CreateTime <= DateTime.Now.AddHours(-48)).ToList();
            foreach (var item in noAnswerChats)
            {
                item.Status = 2;
                var zj_UserQ = mySqlContent.ZJ_Users.Where(a => a.FId == item.QuestionId)
               .FirstOrDefault();
                var _payOrder = mySqlContent.ZJ_PayOrders.Where(a => a.ChatId == item.FId).FirstOrDefault();
                if (zj_UserQ != null && _payOrder != null&&_payOrder.Status==1)
                {
                    _payOrder.Status = 3;
                    mySqlContent.ZJ_PayOrders.Update(_payOrder);
                    var money = (int)(_payOrder.Money * 100);
                    var refundNo = wXPayHelper.GenerateOutTradeNo();
                    await wXPayHelper.Refund(refundNo, zj_UserQ.OpenID, _payOrder.OrderNo, money);

                }
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
                var _payOrder = mySqlContent.ZJ_PayOrders.Where(a =>  a.ChatId == item.FId).FirstOrDefault();
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
