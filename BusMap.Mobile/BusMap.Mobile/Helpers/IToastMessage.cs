using System;
using System.Collections.Generic;
using System.Text;

namespace BusMap.Mobile.Helpers
{
    public interface IToastMessage
    {
        void LongTime(string message);
        void ShortTime(string message);
    }
}
