using NLog;
using Tessa.Education.BLL.Services.Interfaces;

namespace Tessa.Education.BLL.Services
{
    public class LoggerService : ILog
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();

        public LoggerService()
        {
        }

        public void Information(string message)
        {
            logger.Info(message);
        }

        public void Warning(string message)
        {
            logger.Warn(message);
        }

        public void Debug(string message)
        {
            logger.Debug(message);
        }

        public void Error(string message)
        {
            logger.Error(message);
        }
    }
}