using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SummaryUtilityLib
{
    public class SummaryUtility<T>
    {
        //private ISummaryDAO<T> _summaryDao;

        //public SummaryUtility(ISummaryDAO<T> summaryDao)
        //{
        //    this._summaryDao = summaryDao;
        //}

        public IEnumerable<int> GetGroupSumWithColumn(IEnumerable<T> source,uint GroupCount, Func<T, int> FuncSum)
        {
            return source.Select((item, index) => new { Item = item, GroupIndex = index / GroupCount }).GroupBy(item => item.GroupIndex).Select(groupItem => groupItem.Select(x => x.Item).Sum(FuncSum)).ToList();
        }
    }
}
