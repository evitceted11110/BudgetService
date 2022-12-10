using System;
using System.Collections.Generic;
using System.Text;
using NSubstitute;
using NUnit.Framework;

namespace BudgetProject
{
    [TestFixture]
    class BudgetServiceTest
    {
        private BudgetService service;

        [SetUp]
        public void SetUp()
        {
            service = new BudgetService(new TDDRepo());
        }

        [Test]
        public void sameDay()
        {
            DateTime day = new DateTime(2022, 1, 1);

            decimal result = service.Query(day, day);

            Assert.AreEqual(1, result);
        }


        [Test]
        public void daysInMonth()
        {
            DateTime startDay = new DateTime(2022, 1, 1);
            DateTime endDay = new DateTime(2022, 1, 13);
            decimal result = service.Query(startDay, endDay);

            Assert.AreEqual(13, result);
        }    
        
        
        [Test]
        public void fullMonth()
        {
            DateTime startDay = new DateTime(2022, 1, 1);
            DateTime endDay = new DateTime(2022, 1, 31);
            decimal result = service.Query(startDay, endDay);

            Assert.AreEqual(31, result);
        }       
        
        
        [Test]
        public void crossFullMonth()
        {
            DateTime startDay = new DateTime(2022, 1, 1);
            DateTime endDay = new DateTime(2022, 2, 28);
            decimal result = service.Query(startDay, endDay);

            Assert.AreEqual(31+280, result);
        }




    }
}
