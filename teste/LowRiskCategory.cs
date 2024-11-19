using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace teste
{
    public class LowRiskCategory : ITradeCategory
    {
        //Uma trade é "LOWRISK" se: O valor da trade for maior que 1.000.000. O setor do cliente for "Public".
        public string CategoryName => "LOWRISK";

        public bool IsMatch(ITrade trade, DateTime referenceDate)
        {
            return trade.Value > 1000000 && trade.ClientSector == "Public";
        }
    }
}
