using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;

namespace BusMap.Mobile.Helpers
{
    public static class LocalizationHelpers
    {

        public static async Task<Position> GetCurrentUserPositionAsync(bool displayToast)
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 20;

            if (displayToast)
                MessagingHelper.Toast("Getting your localization...", ToastTime.ShortTime);

            var geoPosition =
                await locator.GetPositionAsync(timeout: TimeSpan.FromSeconds(10));

            if (geoPosition != null && displayToast)
                MessagingHelper.Toast("Position obtained successfully.", ToastTime.ShortTime);

            return geoPosition;
        }
    }
}
