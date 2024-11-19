using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace teste
{

    public class PepCategory : ITradeCategory
    {
        //Uma trade é "PEP" se o cliente for politicamente exposto (IsPoliticallyExposed for true).

        public string CategoryName => "PEP";
        public bool IsMatch(ITrade trade, DateTime referenceDate)
        {
            return trade.IsPoliticallyExposed;
        }

        //Para incorporar a nova categoria PEP ao projeto, é necessário adicionar a propriedade IsPoliticallyExposed na interface ITrade e na classe Trade,
        //criando uma nova classe PepCategory que implementa a interface ITradeCategory e verifica se essa propriedade é verdadeira.
        //A nova categoria deve então ser incluída na lista de categorias dentro da classe principal (Program), mantendo o design flexível e facilmente extensível para futuras adições.
    }
}
