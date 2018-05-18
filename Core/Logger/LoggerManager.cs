using log4net;
using log4net.Config;
using log4net.Repository;
using System;
using System.IO;

namespace Core.Logger
{
    public static class LoggerManager
    {
        private const string LoggerName = "NETCoreRepository";//设置全局一个仓储器
        static LoggerManager()
        {
            ILoggerRepository repository = LogManager.CreateRepository(LoggerName);
            XmlConfigurator.Configure(repository, new FileInfo("log4net.config"));
        }

        public static ILog GetLogger(Type t)
        {
             return LogManager.GetLogger(LoggerName, t);
        }
    }
}
