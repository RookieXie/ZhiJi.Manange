using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class DateTimeHelper
    {
        //public static string GetTimeStamp()
        //{
        //    var span = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0);
        //    var secondCount = Convert.ToInt32(span.TotalSeconds);
        //    return secondCount.ToString();
        //}
        public static string GetTimeStamp()
        {
            DateTimeOffset timeOffset = DateTimeOffset.Now;
            return Convert.ToInt32(timeOffset.ToUnixTimeSeconds()).ToString() ;
        }
    }
}
