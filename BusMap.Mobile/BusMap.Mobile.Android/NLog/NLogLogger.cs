using System;
using BusMap.Mobile.Droid;
using NLog;
using Xamarin.Forms;
using ILogger = BusMap.Mobile.Helpers;

[assembly: Dependency(typeof(NLogLogger))]
namespace BusMap.Mobile.Droid
{
    public class NLogLogger : ILogger.ILogger
    {
        private readonly Logger _logger;

        public NLogLogger(Logger logger)
        {
            _logger = logger;
        }

        public void Debug(string message, params object[] args)
        {
            _logger.Debug(message, args);
        }

        public void Error(string message, params object[] args)
        {
            _logger.Error(message, args);
        }

        public void Error(Exception e, string message, params object[] args)
        {
            _logger.Error(e, message, args);
        }

        public void Fatal(string message, params object[] args)
        {
            _logger.Fatal(message, args);
        }

        public void Fatal(Exception e, string message, params object[] args)
        {
            _logger.Fatal(e, message, args);
        }

        public void Info(string message, params object[] args)
        {
            _logger.Info(message, args);
        }

        public void Trace(string message, params object[] args)
        {
            _logger.Trace(message, args);
        }

        public void Warn(string message, params object[] args)
        {
            _logger.Warn(message, args);
        }
    }
}