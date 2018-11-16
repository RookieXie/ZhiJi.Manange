using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public class EnvConfig
    {
        //private static EnvConfig _instance = new EnvConfig();
        //public static readonly bool _isDevelopment;
        //private  EnvConfig()
        //{

        //}

        //public static EnvConfig Instance {
        //    get { return _instance; }
        //    set { }
        //}

        public static bool IsDevelopment { get; set; } = false;
    }
}
