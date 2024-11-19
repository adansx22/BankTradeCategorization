using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace teste
{
    public class ExpiredCategory : ITradeCategory
    {
        //Uma trade é considerada "EXPIRED" se a diferença entre a data de referência e a data do próximo pagamento for superior a 30 dias
        public string CategoryName => "EXPIRED";

        public bool IsMatch(ITrade trade, DateTime referenceDate)
        {
            return (referenceDate - trade.NextPaymentDate).TotalDays > 30;
        }
    }
}
