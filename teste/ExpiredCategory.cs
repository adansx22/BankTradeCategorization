using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace teste
{
    public class ExpiredCategory : ITradeCategory
    {
        public string CategoryName => "EXPIRED";

        public bool IsMatch(ITrade trade, DateTime referenceDate)
        {
            return (referenceDate - trade.NextPaymentDate).TotalDays > 30;
        }
    }
}
