using System;
using System.IO;

namespace MKDBFramwork
{
    public class LogHelper
    {
        private const string SError = "Error";
        private const string SDebug = "Debug";
        private const string DefaultName = "Info";
        
         
        static LogHelper()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory + @"log4net.config";
            log4net.Config.XmlConfigurator.Configure(new FileInfo(path));
        }
         
        public static log4net.ILog GetLog(string logName)
        {

            var log = log4net.LogManager.GetLogger(logName);
            return log;
        }

        #region debug
        public static void Debug(string message)
        {
            var log = log4net.LogManager.GetLogger(SDebug);
            log.Debug(message);            
        }

        public static void Debug(Exception ex)
        {
            var log = log4net.LogManager.GetLogger(SDebug);
            if (log.IsDebugEnabled)
                log.Debug(ex.Message, ex);
        }

        public static void Debug(string message, Exception ex)
        {
            var log = log4net.LogManager.GetLogger(SDebug);
            if (log.IsDebugEnabled)
                log.Debug(message, ex);
        }
        #endregion debug

        #region error
        public static void Error(string message)
        {
            var log = log4net.LogManager.GetLogger(SError);
            if (log.IsErrorEnabled)
                log.Error(message);
        }

        public static void Error(Exception ex)
        {
            var log = log4net.LogManager.GetLogger(SError);
            if (log.IsErrorEnabled)
                log.Error(ex.Message, ex);
        }

        public static void Error(string message, Exception ex)
        {
            var log = log4net.LogManager.GetLogger(SError);
            if (log.IsErrorEnabled)
                log.Error(message, ex);
        }
        #endregion error

        #region
        public static void Write(LogType type, String message)
        {
            switch (type)
            {
                case LogType.ERROR:

                    var logError = log4net.LogManager.GetLogger(SError);
                    if (logError.IsErrorEnabled)
                        logError.Error(message);

                    break;
                case LogType.DEBUG:

                    var logDebug = log4net.LogManager.GetLogger(SDebug);
                    if (logDebug.IsDebugEnabled)
                        logDebug.Debug(message);

                    break;
                case LogType.INFO:

                    log4net.ILog logInfo = log4net.LogManager.GetLogger(DefaultName);
                    if (logInfo.IsInfoEnabled)
                        logInfo.Info(message);

                    break;
            }
        }
        #endregion

        public static void Fatal(string message)
        {
            var log = log4net.LogManager.GetLogger(DefaultName);
            if (log.IsFatalEnabled)
                log.Fatal(message);
        }

        public static void Info(string message)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(DefaultName);
            if (log.IsInfoEnabled)
                log.Info(message);
        }

        public static void Warn(string message)
        {
            var log = log4net.LogManager.GetLogger(DefaultName);
            if (log.IsWarnEnabled)
                log.Warn(message);
        }
    }
    public enum LogType : byte
    {
        ERROR = 0x01,
        WARN,
        INFO,
        DEBUG,
        EXPANDB,
        EXPANDC,
        EXPANDD,
        EXPANDE
    }
}
