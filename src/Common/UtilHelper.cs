using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public static class UtilHelper
    {
        public static string GetNewGuid()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}
