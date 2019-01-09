using BusMap.WebApi.Helpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusMap.WebApiTests.HelpersTests
{
    [TestFixture]
    public class DateAndTimeTests
    {
        [Test]
        public void Now_Default_ReturningCorrectCurrentDateTime()
        {
            var currentDateTime = DateTime.Now;
            var currentDateAndTime = DateAndTime.Now;

            Assert.AreEqual(currentDateTime.Date, currentDateAndTime.Date);
            Assert.AreEqual(currentDateTime.Hour, currentDateAndTime.Hour);
            Assert.AreEqual(currentDateTime.Minute, currentDateAndTime.Minute);
            Assert.AreEqual(currentDateTime.Second, currentDateAndTime.Second);
            //Assert for every chunk, because they can be verry little different on miliseconds
        }

        [Test]
        public void Now_Injected_ReturningCorrectCurrentDateTime()
        {
            var currentDateTime = DateTime.Now;
            var date = new DateTime(1998, 2, 11, 2, 23, 15);
            var injectedDateAndTime = DateAndTime.NowImpl = () => date;
            
            var result = DateAndTime.Now;

            Assert.AreNotEqual(currentDateTime, injectedDateAndTime);
            Assert.AreEqual(date, result);
        }

    }
}
