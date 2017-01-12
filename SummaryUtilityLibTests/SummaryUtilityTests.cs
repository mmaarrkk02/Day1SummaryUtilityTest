using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Collections.Generic;

namespace SummaryUtilityLib.Tests
{
    [TestClass()]
    public class SummaryUtilityTests
    {
        List<Ordering> orderings;
        ISummaryDAO<Ordering> summaryDao;
        SummaryUtility<Ordering> target;
        [TestInitialize]
        public void OrderingTestInitialize()
        {
            orderings = new List<Ordering>();
            orderings.Add(new Ordering { Id = 1, Cost = 10, Revenue = 3, SellPrice = 13 });
            orderings.Add(new Ordering { Id = 2, Cost = 15, Revenue = 5, SellPrice = 20 });
            orderings.Add(new Ordering { Id = 3, Cost = 16, Revenue = 27, SellPrice = 43 });
            orderings.Add(new Ordering { Id = 4, Cost = 18, Revenue = 5, SellPrice = 23 });
            orderings.Add(new Ordering { Id = 5, Cost = 20, Revenue = 4, SellPrice = 24 });
            orderings.Add(new Ordering { Id = 6, Cost = 21, Revenue = 16, SellPrice = 37 });
            orderings.Add(new Ordering { Id = 7, Cost = 7, Revenue = 2, SellPrice = 9 });
            orderings.Add(new Ordering { Id = 8, Cost = 8, Revenue = 7, SellPrice = 15 });
            orderings.Add(new Ordering { Id = 9, Cost = 9, Revenue = 8, SellPrice = 17 });
            orderings.Add(new Ordering { Id = 10, Cost = 10, Revenue = 20, SellPrice = 30 });
            orderings.Add(new Ordering { Id = 11, Cost = 12, Revenue = 4, SellPrice = 16 });

            summaryDao = Substitute.For<ISummaryDAO<Ordering>>();
            summaryDao.GetAll().ReturnsForAnyArgs(orderings);
            target = new SummaryUtility<Ordering>(summaryDao);
        }

        [TestCleanup]
        public void OrderingTestCleanup()
        {
            orderings = null;
        }

        [TestMethod()]
        public void GetGroupSumWithColumnTest_Group_0_Throw_DivideByZeroException()
        {
            Action act = () => target.GetGroupSumWithColumn(0, x => x.SellPrice);
            //Throw DivideByZeroException
            act.ShouldThrow<DivideByZeroException>();
        }

        [TestMethod()]
        public void GetGroupSumWithColumnTest_Group_3_Column_Revenue_Actual_35_25_17_24()
        {
            var expected = new List<int> { 35, 25, 17, 24 };

            var actual = target.GetGroupSumWithColumn(3, x => x.Revenue);

            expected.ShouldBeEquivalentTo(actual);
        }

        [TestMethod()]
        public void GetGroupSumWithColumnTest_Group_5_Column_SellPrice_Actual_123_108_16()
        {
            var expected = new List<int> { 123, 108, 16 };

            var actual = target.GetGroupSumWithColumn(5, x => x.SellPrice);

            expected.ShouldBeEquivalentTo(actual);
        }

        [TestMethod()]
        public void GetGroupSumWithColumnTest_Group_7_Column_CostAddId_Actual_135_77()
        {
            var expected = new List<int> { 135, 77 };

            var actual = target.GetGroupSumWithColumn(7, x => x.Cost + x.Id);

            expected.ShouldBeEquivalentTo(actual);
        }

        [TestMethod()]
        public void GetGroupSumWithColumnTest_Cost_MaxInt_Throw_OverflowException()
        {
            //Add Max Value Item To orderings
            orderings.Add(new Ordering { Id = int.MaxValue });

            Action act = () => target.GetGroupSumWithColumn((uint)orderings.Count, x => x.Id);

            //Throw OverflowException
            act.ShouldThrow<OverflowException>();
        }
    }
}