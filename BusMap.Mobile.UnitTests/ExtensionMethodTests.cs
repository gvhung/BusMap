using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusMap.Mobile.Helpers;
using BusMap.Mobile.Models;
using BusMap.Mobile.ViewModels;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using Xamarin.Forms.Maps;

namespace BusMap.Mobile.UnitTests
{
    [TestFixture]
    public class ExtensionMethodTests
    {
        [Test]
        public void ConvertToMapPins_WhenListHaveCorrectType_Succeed()
        {
            var busStopList = new List<BusStop>
            {
                new BusStop
                {
                    Longitude = 1,
                    Latitude = 2,
                    Route = new object() as Route,
                    Address = "Address",
                    Label = "Label",
                    Id = 1
                }
            };

            var result = busStopList.ToMapPins();

            Assert.IsInstanceOf<ObservableCollection<Pin>>(result);
            Assert.IsTrue(result[0].Address.Equals("Address"));
        }

        [Test]
        public void ConvertToMapPins_WhenListIsNull_ThrowsNullReferenceException()
        {
            List<BusStop> busStopList = null;

            Assert.Throws<NullReferenceException>(() => busStopList.ToMapPins());
        }


        [Test]
        public void ToObservableCollection_WhenCollectionIsNotNull_Succeed()
        {
            var list = new List<string>
            {
                "Test1", "Test2", "Test3", "Test4", "Test5", "Test6"
            };

            var result = list.ToObservableCollection();

            Assert.IsInstanceOf<ObservableCollection<string>>(result);
            Assert.IsTrue(result[5].Equals("Test6"));
        }

        [Test]
        public void ToObservableCollection_WhenCollectionIsNull_ThrowsNullReferenceException()
        {
            List<string> list = null;

            Assert.Throws<NullReferenceException>(() => list.ToObservableCollection());
        }

    }
}
