using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummaryUtilityLib
{
    public interface ISummaryDAO<T>
    {
        IEnumerable<T> GetAll();
    }
}
