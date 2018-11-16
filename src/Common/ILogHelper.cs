using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public interface ILogHelper
    {
        void LogDebug(string message);
        void LogWarn(string message);
        void LogTrace(string message);
        void LogError(string message);
        void LogInfo(string message);
    }
}
