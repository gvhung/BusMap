using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Xamarin.UITest;

namespace BusMap.Mobile.UITests
{
    [TestFixture(Platform.Android)]
    //[TestFixture(Platform.iOS)]  // cause of testing is not working on windows
    class NavigationTests : TestAbstractClass
    {
        public NavigationTests(Platform platform) : base(platform)
        {
        }

        [Test]
        public void ChangePage_MainPage_NearestStopsMapPage()
        {
            const string elementLabel = "NearestStopsMapPage_Map";
            app.Tap("MainPage_NearestStopsButton");

            var result = app.WaitForElement(elementLabel);

            Assert.IsTrue(result.Length == 1);
            Assert.IsTrue(result[0].Label == elementLabel);
        }

        
    }
}
