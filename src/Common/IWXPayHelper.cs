using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public interface IWXPayHelper
    {
        Task<SortedDictionary<string, object>> Unifiedorder(string strBody, string ip, string openid, string payOrderNo, int total_fee);
        Task<SortedDictionary<string, object>> Queryorder(string payOrderNo);
        Task<SortedDictionary<string, object>> Refund(string refundNo, string openid, string payOrderNo, int total_fee);
        string GenerateOutTradeNo();
        string GetTimestamp();
        string GetPaymentMD5(string nonceStr, string package, string timeStamp);
        Task<SortedDictionary<string, object>> WithdrawTransfers(string realName, string ip, string openid, string withdrawNo, int total_fee);
    }
}
