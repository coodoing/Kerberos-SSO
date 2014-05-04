using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;

namespace Sso_Core.Helper
{
    public static class LogHelper
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void Debug(object message)
        {
            log.Debug(message);
        }

        public static void Info(object message)
        {
            log.Info(message);
        }

        public static void Warn(object message)
        {
            log.Warn(message);
        }

        public static void Error(object message)
        {
            log.Error(message);
        }

        public static void Fatal(object message)
        {
            log.Fatal(message);
        }
    }
}
