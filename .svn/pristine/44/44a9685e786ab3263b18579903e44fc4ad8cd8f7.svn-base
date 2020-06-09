using System;
using System.Text;
using log4net;
using System.Reflection;

namespace Spectrum.Logging
{
    public static class Logger
    {
        public enum LogingLevel
        {
            All,
            Debug,
            Info,
            Warn,
            Error,
            Fatal,
            Off
        }
        private static readonly ILog _Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static void Log(Exception ex, LogingLevel loggingLevel)
        {
            Log(string.Empty, ex, loggingLevel);
        }

        public static void Log(string messageText, Exception ex, LogingLevel loggingLevel)
        {
            var buffer = new StringBuilder();
            buffer.AppendFormat("{0}Spectrum - Back Office{0}", Environment.NewLine);

            if (!string.IsNullOrEmpty(messageText))
                buffer.AppendFormat("User Message   : {0}{1}", messageText, Environment.NewLine);

            buffer.AppendFormat("Date           : {0}{1}", DateTime.Now, Environment.NewLine);
            buffer.AppendFormat("Error Type     : {0} -> {1}{2}", ex.GetType().Name, ex.TargetSite.Name, Environment.NewLine);
            buffer.AppendFormat("Error Message  : {0}{1}", ex.Message, Environment.NewLine);
            buffer.AppendFormat("StackTrace     : {0}", Environment.NewLine);

            foreach (var stackTrace in ex.StackTrace.Split('\n'))
                buffer.AppendFormat("                 {0}{1}", stackTrace.Trim(), Environment.NewLine);

            while (ex.InnerException != null)
            {
                ex = ex.InnerException;

                buffer.AppendFormat("Caused By{0}", Environment.NewLine);
                buffer.AppendFormat("Error Type     : {0} -> {1}{2}", ex.GetType().Name, ex.TargetSite.Name, Environment.NewLine);
                buffer.AppendFormat("Error Message  : {0}{1}", ex.Message, Environment.NewLine);
                buffer.AppendFormat("StackTrace     : {0}", Environment.NewLine);

                foreach (var stackTrace in ex.StackTrace.Split('\n'))
                    buffer.AppendFormat("                 {0}{1}", stackTrace.Trim(), Environment.NewLine);

                buffer.AppendFormat(Environment.NewLine);
            }
            Log(buffer.ToString(), loggingLevel);
        }

        public static void Log(string message, LogingLevel loggingLevel)
        {
            log4net.Config.XmlConfigurator.Configure();
            switch (loggingLevel)
            {
                case LogingLevel.All:
                    if (_Logger.Logger.IsEnabledFor(log4net.Core.Level.All)) _Logger.Debug(message);
                    break;
                case LogingLevel.Debug:
                    _Logger.Debug(message);
                    break;
                case LogingLevel.Info:
                    _Logger.Info(message);
                    break;
                case LogingLevel.Warn:
                    _Logger.Warn(message);
                    break;
                case LogingLevel.Error:
                    _Logger.Error(message);
                    break;
                case LogingLevel.Fatal:
                    _Logger.Fatal(message);
                    break;
                case LogingLevel.Off:
                    break;
                default:
                    break;
            }
        }
    }
}
