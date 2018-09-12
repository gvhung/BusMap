using System;
using System.Collections.Generic;
using System.Text;

namespace BusMap.Mobile.Helpers
{
    public interface ILogger
    {
        void Trace(string message, params object[] args);
        void Debug(string message, params object[] args);
        void Info(string message, params object[] args);
        void Warn(string message, params object[] args);
        void Error(string message, params object[] args);
        void Error(Exception e, string message, params object[] args);
        void Fatal(string message, params object[] args);
        void Fatal(Exception e, string message, params object[] args);
    }
}
