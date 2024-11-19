using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace teste
{
    public class PepCategory : ITradeCategory
    {
        public string CategoryName => "PEP";
        public bool IsMatch(ITrade trade, DateTime referenceDate)
        {
            return trade.IsPoliticallyExposed;
        }
    }
}
