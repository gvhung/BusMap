using System;
using BusMap.Mobile.ViewModels;
using NSubstitute;
using NUnit.Framework;
using Plugin.Geolocator;

namespace ViewModelsTests
{
    [TestFixture]
    public class NearestStopsMapPageViewModelTests
    {
        async void LocationTest()
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 20.0;

            var location = await locator.GetPositionAsync(timeout:TimeSpan.FromSeconds(5));

            string s = location.Latitude.ToString();
            int x = 0;
        }
    }
}
