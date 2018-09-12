using System;
using System.Collections.Generic;
using System.Text;

namespace BusMap.Mobile.Helpers
{
    public interface ILogManager
    {
        ILogger GetLog([System.Runtime.CompilerServices.CallerFilePath]string callerFilePath = "");
    }
}
