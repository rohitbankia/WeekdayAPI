using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Weekday.Controllers;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void TestGetWeekday()
        {
            ILogger<WeekdayController> logger = Mock.Of<ILogger<WeekdayController>>();
            var controller = new WeekdayController(logger);
            var result = controller.Get();
            Assert.AreEqual(7, result.Count());
        }

        [TestMethod]
        public void TestPostWeekday()
        {
            ILogger<WeekdayController> logger = Mock.Of<ILogger<WeekdayController>>();
            var controller = new WeekdayController(logger);

            var result = controller.Post(TestDates());
        }

        private List<string> TestDates()
        {
            DateTime dateTime = DateTime.Today;
            return new List<string>()
            {
                  dateTime.ToString()
                , dateTime.AddDays(1).ToString()
                , dateTime.AddDays(2).ToString()
                , dateTime.AddDays(3).ToString()
                , dateTime.AddDays(4).ToString()
                , dateTime.AddDays(5).ToString()
                , dateTime.AddDays(6).ToString()
            };
        }
    }
}
