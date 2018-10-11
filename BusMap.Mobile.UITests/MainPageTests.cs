using System;
using System.IO;
using System.Linq;
using System.Security;
using System.Threading.Tasks;
using NUnit.Framework;
using Xamarin.Forms;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace BusMap.Mobile.UITests
{
    [TestFixture(Platform.Android)]
    //[TestFixture(Platform.iOS)]  // cause of testing is not working on windows
    public class MainPageTests : TestAbstractClass
    {
        public MainPageTests(Platform platform) : base(platform)
        {
        }

        [Test]
        public void ClickingSearchButton_ShowingTestToastToast()
        {
            app.Tap("SearchButton");
            var results = app.WaitForElement("test toast");

            Assert.IsTrue(results.Length > 0);
        }


        
    }
}
