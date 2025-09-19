using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;

namespace Presentation
{
    public sealed class Logger
    {
        static Logger _instance;

        public static Logger Instance
        {
            get { return _instance ?? (_instance = new Logger()); }
        }

        private Logger()
        { }

        private static ILog theLogger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static void LogDebug(string theMessage)
        {
            theLogger.Debug(theMessage);
        }
        public static void LogInfo(string theMessage)
        {
            theLogger.Info(theMessage);
        }
        public static void LogWarning(string theMessage)
        {
            theLogger.Warn(theMessage);
        }
        public static void LogError(string theMessage)
        {
            theLogger.Error(theMessage);
        }
        public static void LogFatal(string theMessage)
        {
            theLogger.Fatal(theMessage);
        }
    }
}