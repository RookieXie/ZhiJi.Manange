using NLog;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class NLogHelper : ILogHelper
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        public void LogDebug(string message)
        {
            _logger.Debug(message);
        }
        public void LogWarn(string message)
        {
            _logger.Warn(message);
        }
        public void LogTrace(string message)
        {
            _logger.Trace(message);
        }
        public void LogError(string message)
        {
            _logger.Error(message);
        }
        public void LogInfo(string message)
        {
            _logger.Info(message);
        }
    }
}
