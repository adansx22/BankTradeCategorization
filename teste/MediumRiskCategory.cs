using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace teste
{
    public class MediumRiskCategory : ITradeCategory
    {
        //Uma trade é "MEDIUMRISK" se o valor for superior a 1.000.000 e o setor do cliente for "Private"
        public string CategoryName => "MEDIUMRISK";

        public bool IsMatch(ITrade trade, DateTime referenceDate)
        {
            return trade.Value > 1000000 && trade.ClientSector == "Private";
        }
    }
}
