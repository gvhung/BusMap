using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusMap.WebApi.DatabaseModels;

namespace BusMap.WebApi.Helpers
{
    public static class PunctualityConverter
    {
        public static string BusStopsTracesPunctualityPercentage(IEnumerable<BusStop> busStops)
        {
            var punctualitySum = 0.0;

            foreach (var busStop in busStops)
            {
                punctualitySum += BusStopPercentageOfPunctuality(busStop);
            }

            //var result = Convert.ToDouble(punctualitySum / busStops.Count());
            var result = Math.Round(punctualitySum / busStops.Count(), 0);
            return $"{result}%";
        }

        public static double BusStopPercentageOfPunctuality(BusStop busStop) //Todo: timespan in parameter
        {
            var result = 0.0;
            var nOfMatchingTraces = 0;
            var busStopTime = busStop.Hour;
            var timeSpanValue = new TimeSpan(0, 5, 0);
            var startTime = busStopTime.Subtract(timeSpanValue);
            var endTime = busStopTime.Add(timeSpanValue);

            foreach (var trace in busStop.BusStopTraces)
            {

                if (trace.Hour >= startTime && trace.Hour <= endTime)
                {
                    nOfMatchingTraces++;
                }
            }

            if (nOfMatchingTraces > 0)
            {
                result = Convert.ToDouble((nOfMatchingTraces * 100) / busStop.BusStopTraces.Count);
            }

            return result;
        }

        public static int CalculateBusStopDelay(BusStop busStop)
        {
            var avgTime = BusStopAverageArrivedHour(busStop);
            var result = avgTime - busStop.Hour;

            return result.Minutes;
        }

        public static TimeSpan BusStopAverageArrivedHour(BusStop busStop)
        {
            var timeSpanList = new List<TimeSpan>(ConvertBusStopToTraceTimeSpanList(busStop));

            double avgTicks = timeSpanList.Average(ts => ts.Ticks);
            long longAvgTicks = Convert.ToInt64(avgTicks);

            return new TimeSpan(longAvgTicks);
        }

        private static List<TimeSpan> ConvertBusStopToTraceTimeSpanList(BusStop busStop)
        {
            var result = new List<TimeSpan>();
            foreach (var trace in busStop.BusStopTraces)
            {
                result.Add(trace.Hour);
            }

            return result;
        }


    }
}
