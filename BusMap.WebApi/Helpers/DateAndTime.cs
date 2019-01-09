using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusMap.WebApi.Helpers
{
    public struct DateAndTime
    {
        public static DateTime Now => NowImpl();

        public static Func<DateTime> NowImpl = () => DateTime.Now;

        public static bool IsToday(DateTime date)
        {
            if (date == DateTime.Now) return true;
            return false;
        }
    }
}
