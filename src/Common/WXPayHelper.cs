using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Common
{
    public class WXPayHelper : IWXPayHelper
    {
        //=======【基本信息设置】=====================================
        /* 微信小程序支付信息配置
        * APPID：绑定支付的APPID（必须配置）小程序appid
        * MCHID：商户号（必须配置）
        * KEY：商户支付密钥，参考开户邮件设置（必须配置）
        * APPSECRET：小程序secert（仅JSAPI支付的时候需要配置） 
        */
        public const string APPID = "wxc6ba72372cd614a3";
        public const string MCHID = "1516575911";
        public const string KEY = "BAOMIHUA2SPSJboyiboZHIJI20181120";
        public const string APPSECRET = "3567588abf92e45586b6aa6de0577ff7";

        public const string SSLCERT_PATH = "C:\\cert\\apiclient_cert.p12";
        public const string SSLCERT_PASSWORD = "1516575911";
        private readonly ILogHelper logger;
        public WXPayHelper(ILogHelper logger)
        {
            this.logger = logger;
        }
        /// <summary>
        /// 统一下单
        /// </summary>
        public async Task<SortedDictionary<string, object>> Unifiedorder(string strBody, string ip, string openid, string payOrderNo, int total_fee)
        {

            SortedDictionary<string, object> data = new SortedDictionary<string, object>();
            data.Add("appid", APPID);
            data.Add("mch_id", MCHID);
            data.Add("device_info", "WEB");
            data.Add("nonce_str", GenerateNonceStr());
            data.Add("sign_type", "MD5");
            data.Add("body", strBody);
            data.Add("detail", "");
            data.Add("attach", "");
            data.Add("out_trade_no", payOrderNo);
            data.Add("fee_type", "CNY");

            data.Add("total_fee", total_fee);
            data.Add("spbill_create_ip", ip);//终端ip	
            data.Add("time_start", DateTime.Now.ToString("yyyyMMddHHmmss"));
            data.Add("time_expire", DateTime.Now.AddMinutes(10).ToString("yyyyMMddHHmmss"));
            data.Add("goods_tag", "");
            data.Add("notify_url", "https://wenzhiji.com/Api/v1/WXCommon/WxPayReturnBack");//异步通知url


            data.Add("trade_type", "JSAPI");

            data.Add("product_id", "");
            data.Add("limit_pay", "");
            data.Add("openid", openid);
            //签名
            data.Add("sign", MakeSign(data));
            string xml = ToXml(data);
            logger.LogDebug($"Unifiedorder  xml={xml}");
            using (HttpClient httpClient = new HttpClient())
            {
                var url = "https://api.mch.weixin.qq.com/pay/unifiedorder";
                HttpContent httpContent = new StringContent(xml);
                var reps = await httpClient.PostAsync(url, httpContent);
                var resStr = await reps.Content.ReadAsStringAsync();
                logger.LogDebug($"Unifiedorder  resStr={resStr}");
                data = FromXml(resStr);
                // UnifiedorderModel
                return data;
            }
        }
        /// <summary>
        /// 查询订单
        /// </summary>
        /// <param name="payOrderNo"></param>
        /// <returns></returns>
        public async Task<SortedDictionary<string, object>> Queryorder(string payOrderNo)
        {

            SortedDictionary<string, object> data = new SortedDictionary<string, object>();
            data.Add("appid", APPID);
            data.Add("mch_id", MCHID);
            data.Add("out_trade_no", "payOrderNo");
            data.Add("nonce_str", GenerateNonceStr());
            data.Add("sign_type", "MD5");
            //签名
            data.Add("sign", MakeSign(data));
            string xml = ToXml(data);
            using (HttpClient httpClient = new HttpClient())
            {
                var url = "https://api.mch.weixin.qq.com/pay/orderquery";
                HttpContent httpContent = new StringContent(xml);
                var reps = await httpClient.PostAsync(url, httpContent);
                var resStr = await reps.Content.ReadAsStringAsync();
                data = FromXml(resStr);
                return data;
            }
        }
        /// <summary>
        /// 退款
        /// </summary>
        /// <param name="refundNo"></param>
        /// <param name="openid"></param>
        /// <param name="payOrderNo"></param>
        /// <param name="total_fee"></param>
        /// <returns></returns>
        public async Task<SortedDictionary<string, object>> Refund(string refundNo, string openid, string payOrderNo, int total_fee)
        {

            SortedDictionary<string, object> data = new SortedDictionary<string, object>();
            data.Add("appid", APPID);
            data.Add("mch_id", MCHID);
            data.Add("device_info", "WEB");
            data.Add("nonce_str", GenerateNonceStr());
            data.Add("sign_type", "MD5");
            data.Add("out_trade_no", payOrderNo);
            data.Add("out_refund_no", refundNo);
            data.Add("refund_fee_type", "CNY");

            data.Add("total_fee", total_fee);
            data.Add("refund_fee", total_fee);
            data.Add("refund_desc", "答主未回答，退还付款");
            data.Add("notify_url", "https://wenzhiji.com/Api/v1/WXCommon/WxRefundReturnBack");//异步通知url


            //签名
            data.Add("sign", MakeSign(data));
            string xml = ToXml(data);
            logger.LogDebug($"Refund  xml={xml}");
            try
            {
                using (var handler = new HttpClientHandler())
                {
                    logger.LogDebug($"Refund  handler=1");
                    handler.ClientCertificates.Add(new X509Certificate2(SSLCERT_PATH, SSLCERT_PASSWORD));

                    logger.LogDebug($"Refund  handler=2");
                    using (HttpClient httpClient = new HttpClient(handler))
                    {
                        var url = "https://api.mch.weixin.qq.com/secapi/pay/refund";
                        HttpContent httpContent = new StringContent(xml);
                        var reps = await httpClient.PostAsync(url, httpContent);
                        var resStr = await reps.Content.ReadAsStringAsync();
                        data = FromXml(resStr);
                        logger.LogDebug($"Refund  resStr={resStr}");
                        return data;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogDebug($"Refund  Exception={ex.Message}");
                //throw;
                return data;
            }

        }
        private string ToXml(SortedDictionary<string, object> keyValues)
        {
            //数据为空时不能转化为xml格式
            if (0 == keyValues.Count)
            {

            }

            string xml = "<xml>";
            foreach (var pair in keyValues)
            {
                //字段值不能为null，会影响后续流程
                if (pair.Value == null)
                {
                }

                if (pair.Value.GetType() == typeof(int))
                {
                    xml += "<" + pair.Key + ">" + pair.Value + "</" + pair.Key + ">";
                }
                else if (pair.Value.GetType() == typeof(string))
                {
                    xml += "<" + pair.Key + ">" + "<![CDATA[" + pair.Value + "]]></" + pair.Key + ">";
                }
                else//除了string和int类型不能含有其他数据类型
                {
                }
            }
            xml += "</xml>";
            return xml;
        }
        private static string GenerateNonceStr()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
        private string MakeSign(SortedDictionary<string, object> keyValues)
        {
            //转url格式

            string str = ToUrl(keyValues);


            //在string后加入API KEY
            str += "&key=" + KEY;
            //MD5加密
            var md5 = MD5.Create();
            var bs = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            var sb = new StringBuilder();
            foreach (byte b in bs)
            {
                sb.Append(b.ToString("x2"));
            }
            //所有字符转为大写
            return sb.ToString().ToUpper();
        }
        private string ToUrl(SortedDictionary<string, object> keyValues)
        {
            string buff = "";
            if (null != keyValues)
            {
                foreach (var pair in keyValues)
                {

                    if (pair.Key != "sign" && Convert.ToString(pair.Value) != "")
                    {
                        buff += pair.Key + "=" + pair.Value + "&";
                    }
                }
            }
            buff = buff.Trim('&');
            return buff;
        }

        private SortedDictionary<string, object> FromXml(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {

            }
            SortedDictionary<string, object> keyValues = new SortedDictionary<string, object>();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);
            XmlNode xmlNode = xmlDoc.FirstChild;//获取到根节点<xml>
            XmlNodeList nodes = xmlNode.ChildNodes;
            foreach (XmlNode xn in nodes)
            {
                XmlElement xe = (XmlElement)xn;
                keyValues[xe.Name] = xe.InnerText;//获取xml的键值对到WxPayData内部的数据中
            }

            try
            {
                if (keyValues["return_code"].ToString() != "SUCCESS")
                {
                    return keyValues;
                }
                CheckSign(keyValues);//验证签名,不通过会抛异常
            }
            catch (Exception ex)
            {

            }

            return keyValues;
        }
        private bool CheckSign(SortedDictionary<string, object> keyValues)
        {
            //获取接收到的签名
            string return_sign = keyValues["sign"].ToString();

            //在本地计算新的签名
            string cal_sign = MakeSign(keyValues);

            if (cal_sign == return_sign)
            {
                return true;
            }
            return true;
        }

        /**
       * 根据当前系统时间加随机序列来生成订单号
        * @return 订单号
       */
        public string GenerateOutTradeNo()
        {
            var ran = new Random();
            return $"{MCHID}{DateTime.Now.ToString("yyyyMMddHHmmss")}{ ran.Next(999)}";
        }
        /// <summary>
        /// 时间截，自1970年以来的秒数
        /// </summary>
        public string GetTimestamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }
        public string GetPaymentMD5(string nonceStr, string package, string timeStamp)
        {
            //MD5加密
            var signType = "MD5";
            var str = $"appId={APPID}&nonceStr={nonceStr}&package={package}&signType={signType}&timeStamp={timeStamp}&key={KEY}";
            var md5 = MD5.Create();
            var bs = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            var sb = new StringBuilder();
            foreach (byte b in bs)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString().ToUpper();
        }

        /// <summary>
        /// 提现功能
        /// </summary>
        public async Task<SortedDictionary<string, object>> WithdrawTransfers(string realName, string ip, string openid, string withdrawNo, int total_fee)
        {
            SortedDictionary<string, object> data = new SortedDictionary<string, object>();
            data.Add("mch_appid", APPID);
            data.Add("mchid", MCHID);
            data.Add("device_info", "WEB");
            data.Add("nonce_str", GenerateNonceStr());
            //data.Add("sign_type", "MD5");

            //data.Add("detail", "");
            //data.Add("attach", "");
            data.Add("partner_trade_no", withdrawNo);

            data.Add("openid", openid);



            data.Add("check_name", "FORCE_CHECK");

            data.Add("re_user_name", realName);
            data.Add("amount", total_fee);
            data.Add("desc", "提现");
            data.Add("spbill_create_ip", ip);//终端ip	

            //签名
            data.Add("sign", MakeSign(data));
            string xml = ToXml(data);
            logger.LogDebug($"WithdrawTransfers  xml={xml}");
            using (var handler = new HttpClientHandler())
            {
                handler.ClientCertificates.Add(new X509Certificate2(SSLCERT_PATH, SSLCERT_PASSWORD));
                using (HttpClient httpClient = new HttpClient(handler))
                {
                    var url = "https://api.mch.weixin.qq.com/mmpaymkttransfers/promotion/transfers";
                    HttpContent httpContent = new StringContent(xml);
                    var reps = await httpClient.PostAsync(url, httpContent);
                    var resStr = await reps.Content.ReadAsStringAsync();
                    data = FromXml(resStr);
                    logger.LogDebug($"WithdrawTransfers  resStr={resStr}");
                    return data;
                }
            }
        }
    }
}
