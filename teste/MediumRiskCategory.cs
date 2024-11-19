using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace teste
{
    public class MediumRiskCategory : ITradeCategory
    {
        public string CategoryName => "MEDIUMRISK";

        public bool IsMatch(ITrade trade, DateTime referenceDate)
        {
            return trade.Value > 1000000 && trade.ClientSector == "Private";
        }
    }
}
