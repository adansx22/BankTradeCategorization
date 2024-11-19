using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace teste
{
    public interface ITradeCategory
    {
        string CategoryName { get; }
        bool IsMatch(ITrade trade, DateTime referenceDate);
    }
}
