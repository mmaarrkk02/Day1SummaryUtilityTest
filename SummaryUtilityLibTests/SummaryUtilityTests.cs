using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SummaryUtilityLib.Tests
{
    [TestClass()]
    public class SummaryUtilityTests
    {
        /*
        List<Ordering> orderings;
        ISummaryDAO<Ordering> summaryDao;
        SummaryUtility<Ordering> target;
        [TestInitialize]
        public void OrderingTestInitialize()
        {
            orderings = new List<Ordering>() {
                new Ordering() { Id=1 ,Cost=1 ,Revenue=11,SellPrice=21 },
                new Ordering() { Id=2 ,Cost=2 ,Revenue=12,SellPrice=22 },
                new Ordering() { Id=3 ,Cost=3 ,Revenue=13,SellPrice=23 },
                new Ordering() { Id=4 ,Cost=4 ,Revenue=14,SellPrice=24 },
                new Ordering() { Id=5 ,Cost=5 ,Revenue=15,SellPrice=25 },
                new Ordering() { Id=6 ,Cost=6 ,Revenue=16,SellPrice=26 },
                new Ordering() { Id=7 ,Cost=7 ,Revenue=17,SellPrice=27 },
                new Ordering() { Id=8 ,Cost=8 ,Revenue=18,SellPrice=28 },
                new Ordering() { Id=9 ,Cost=9 ,Revenue=19,SellPrice=29 },
                new Ordering() { Id=10,Cost=10,Revenue=20,SellPrice=30 },
                new Ordering() { Id=11,Cost=11,Revenue=21,SellPrice=31 }
            };

            summaryDao = Substitute.For<ISummaryDAO<Ordering>>();
            summaryDao.GetAll().ReturnsForAnyArgs(orderings);
            target = new SummaryUtility<Ordering>(summaryDao);
        }

        [TestCleanup]
        public void OrderingTestCleanup()
        {
            orderings = null;
        }
        */

        private List<Ordering> GetOrderings()
        {
            List<Ordering> orderings = new List<Ordering>() {
                new Ordering() { Id=1 ,Cost=1 ,Revenue=11,SellPrice=21 },
                new Ordering() { Id=2 ,Cost=2 ,Revenue=12,SellPrice=22 },
                new Ordering() { Id=3 ,Cost=3 ,Revenue=13,SellPrice=23 },
                new Ordering() { Id=4 ,Cost=4 ,Revenue=14,SellPrice=24 },
                new Ordering() { Id=5 ,Cost=5 ,Revenue=15,SellPrice=25 },
                new Ordering() { Id=6 ,Cost=6 ,Revenue=16,SellPrice=26 },
                new Ordering() { Id=7 ,Cost=7 ,Revenue=17,SellPrice=27 },
                new Ordering() { Id=8 ,Cost=8 ,Revenue=18,SellPrice=28 },
                new Ordering() { Id=9 ,Cost=9 ,Revenue=19,SellPrice=29 },
                new Ordering() { Id=10,Cost=10,Revenue=20,SellPrice=30 },
                new Ordering() { Id=11,Cost=11,Revenue=21,SellPrice=31 }
            };
            return orderings;
        }

        [TestMethod]
        public void GetGroupSumWithColumnTest_Group_3_Column_Cost_Actual_6_15_24_21()
        {
            List<Ordering> orderings = GetOrderings();
            SummaryUtility<Ordering> target = new SummaryUtility<Ordering>();
            var expected = new List<int> { 6, 15, 24, 21 };

            var actual = target.GetGroupSumWithColumn(orderings, 3, x => x.Cost).ToList();

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetGroupSumWithColumnTest_Group_4_Column_Revenue_Actual_50_66_60()
        {
            List<Ordering> orderings = GetOrderings();
            SummaryUtility<Ordering> target = new SummaryUtility<Ordering>();
            var expected = new List<int> { 50, 66, 60 };

            var actual = target.GetGroupSumWithColumn(orderings, 4, x => x.Revenue).ToList();

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetGroupSumWithColumnTest_Group_0_Throw_DivideByZeroException()
        {
            List<Ordering> orderings = GetOrderings();
            SummaryUtility<Ordering> target = new SummaryUtility<Ordering>();
            Action act = () => target.GetGroupSumWithColumn(orderings, 0, x => x.SellPrice);
            //Throw DivideByZeroException
            act.ShouldThrow<DivideByZeroException>();
        }

        [TestMethod()]
        public void GetGroupSumWithColumnTest_Group_3_Column_Revenue_Actual_36_45_54_41()
        {
            List<Ordering> orderings = GetOrderings();
            SummaryUtility<Ordering> target = new SummaryUtility<Ordering>();
            var expected = new List<int> { 36, 45, 54, 41 };

            var actual = target.GetGroupSumWithColumn(orderings, 3, x => x.Revenue).ToList();

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetGroupSumWithColumnTest_Group_5_Column_SellPrice_Actual_115_140_31()
        {
            List<Ordering> orderings = GetOrderings();
            SummaryUtility<Ordering> target = new SummaryUtility<Ordering>();
            var expected = new List<int> { 115, 140, 31 };

            var actual = target.GetGroupSumWithColumn(orderings, 5, x => x.SellPrice).ToList();

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetGroupSumWithColumnTest_Group_7_Column_CostAddId_Actual_56_76()
        {
            List<Ordering> orderings = GetOrderings();
            SummaryUtility<Ordering> target = new SummaryUtility<Ordering>();
            var expected = new List<int> { 56, 76 };

            var actual = target.GetGroupSumWithColumn(orderings, 7, x => x.Cost + x.Id).ToList();

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetGroupSumWithColumnTest_Cost_MaxInt_Throw_OverflowException()
        {
            List<Ordering> orderings = GetOrderings();
            //Add Max Value Item To orderings
            orderings.Add(new Ordering { Id = int.MaxValue });
            SummaryUtility<Ordering> target = new SummaryUtility<Ordering>();

            Action act = () => target.GetGroupSumWithColumn(orderings, (uint)orderings.Count, x => x.Id);

            //Throw OverflowException
            act.ShouldThrow<OverflowException>();
        }
    }
}