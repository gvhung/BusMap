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
        public static string RoutePunctualityPercentage(Route route)
        {
            var punctualitySum = 0.0;

            foreach (var busStop in route.BusStops)
            {
                punctualitySum += BusStopPercentageOfPunctuality(busStop);
            }

            //var result = Convert.ToDouble(punctualitySum / busStops.Count());
            var result = Math.Round(punctualitySum / route.BusStops.Count(), 0);
            return $"{result}%";
        }

        public static string BusStopPunctualityPercentage(BusStop busStop)
        {
            var punctualitySum = BusStopPercentageOfPunctuality(busStop);
            var result = Math.Round(punctualitySum, 0);
            return $"{result}%";
        }

        public static TimeSpan BusStopPunctualityHourMode(BusStop busStop) //Most frequent hour
        {
            var traces = busStop.BusStopTraces;
            if (traces == null || traces.Count < 1)
            {
                return busStop.Hour;
            }

            var tracesHourList = traces.Select(t => t.Hour).ToList();

            var mode = tracesHourList.GroupBy(t => t)
                .OrderByDescending(t => t.Count())
                .First()
                .Key;

            return mode;
        }

        public static (int avgTimeBefore, int avgTimeAfter) BusStopPunctualityHourAvgBeforeAvgAfterTime(BusStop busStop)
        {
            //If busStop have not traces, then just return (0,0)
            if (busStop.BusStopTraces == null || busStop.BusStopTraces.Count < 1)
                return (0, 0);

            var hours = busStop.BusStopTraces.Select(t => t.Hour).ToList();
            var hoursAfter = hours.Where(h => h > busStop.Hour && (h - busStop.Hour) < TimeSpan.FromHours(1)).ToList();
            var hoursBefore = hours.Where(h => h < busStop.Hour && (h - busStop.Hour) > TimeSpan.FromHours(-1)).ToList();

            if (hoursBefore.ToList().Count == 0) hoursBefore.Add(busStop.Hour);
            if (hoursAfter.ToList().Count == 0) hoursAfter.Add(busStop.Hour);

            var avgMin = hoursBefore.AverageTimespan();
            var avgMax = hoursAfter.AverageTimespan();

            var resultMin = (busStop.Hour - avgMin).Minutes;    //timespan always under 1hr
            var resultMax = (avgMax - busStop.Hour).Minutes;

            return (resultMin, resultMax);
        }

        public static (int avgTimeBefore, int avgTimeAfter) RoutePunctualityHourAvgBeforeAvgAfterTime(Route route)
        {
            var avgList = new List<(int avgTimeBefore, int avgTimeAfter)>();
            foreach (var busStop in route.BusStops)
            {
                avgList.Add(BusStopPunctualityHourAvgBeforeAvgAfterTime(busStop));
            }

            var resultBefore = 0;
            foreach (var item in avgList)
            {
                resultBefore += item.avgTimeBefore;
            }
            resultBefore = resultBefore / avgList.Count;

            var resultAfter = 0;
            foreach (var item in avgList)
            {
                resultAfter += item.avgTimeAfter;
            }

            resultAfter = resultAfter / avgList.Count;

            return (resultBefore, resultAfter);
        }



        public static TimeSpan AverageTimespan(this IEnumerable<TimeSpan> timeSpans)
        {
            double avgTicks = timeSpans.Average(ts => ts.Ticks);
            long longAvgTicks = Convert.ToInt64(avgTicks);

            return new TimeSpan(longAvgTicks);
        }

        private static double BusStopPercentageOfPunctuality(BusStop busStop) //Todo: timespan in parameter
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
