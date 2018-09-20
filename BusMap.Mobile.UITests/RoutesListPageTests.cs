using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusMap.Mobile.Services;
using BusMap.Mobile.ViewModels;
using BusMap.Mobile.Views;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Xamarin.Forms;
using Xamarin.UITest;

namespace BusMap.Mobile.UITests
{
    [TestFixture(Platform.Android)]
    class RoutesListPageTests : TestAbstractClass
    {
        public RoutesListPageTests(Platform platform) : base(platform)
        {
        }


        [Test]
        public void TitleIsGenerating_FromMainPageEntries()
        {
            app.EnterText("MainPage_FromEntry", "title1");
            app.EnterText("MainPage_ToEntry", "title2");
            app.Tap(x => x.Text("Search"));

            var correctTitle = "title1 - title2";
            var result = app.WaitForElement(correctTitle);

            Assert.IsTrue(result.Length == 1);
            Assert.AreEqual(correctTitle, result[0].Text);
        }

        
    }
}
