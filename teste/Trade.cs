using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace teste
{
    public class Trade : ITrade
    {
        public double Value { get; }
        public string ClientSector { get; }
        public DateTime NextPaymentDate { get; }
        public bool IsPoliticallyExposed { get; }

        public Trade(double value, string clientSector, DateTime nextPaymentDate, bool isPoliticallyExposed)
        {
            Value = value;
            ClientSector = clientSector;
            NextPaymentDate = nextPaymentDate;
            IsPoliticallyExposed = isPoliticallyExposed;
        }
    }
}
