using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using Xamarin.Forms;
using Xamarin.UITest;
using Xamarin.UITest.Configuration;

namespace BusMap.Mobile.UITests
{
    [TestFixture(Platform.Android)]
    //[TestFixture(Platform.iOS)]  // cause of testing is not working on windows
    public class NearestStopsMapPageTests : TestAbstractClass
    {
        public NearestStopsMapPageTests(Platform platform) : base(platform)
        {

        }

        [Test]
        public void SetCurrentUserLocation_WhenGPSisOn_ShouldShowSuccessfulToast()
        {
            //var x = 1;
            //app.Tap("MainPage_NearestStopsButton");
            app.Tap(x => x.Text("Nearest stops"));
            var result = app.WaitForElement("Position obtained successfully.");

            Assert.IsTrue(result.Length == 1);
            Assert.IsTrue(result[0].Text == "Position obtained successfully.");
        }


        
    }
}
