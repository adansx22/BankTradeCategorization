using System;
using System.Collections.Generic;
using System.Globalization;
using teste;

namespace BankTradeCategorization
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                try //ao redor do loop principal para capturar exceções inesperadas e fornecer uma mensagem de erro amigável.
                {

                    Console.WriteLine("Digite a data de referência (MM/dd/yyyy):");
                    DateTime referenceDate;
                    if (!DateTime.TryParseExact(Console.ReadLine(), "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out referenceDate))
                    {
                        Console.WriteLine("Data de referência inválida. Use o formato MM/dd/yyyy.");
                        continue;
                    }


                    Console.WriteLine("Digite o número de trades:");
                    if (!int.TryParse(Console.ReadLine(), out int n) || n <= 0)
                    {
                        Console.WriteLine("Número de trades inválido. Por favor, insira um número positivo.");
                        continue;
                    }

                    List<ITrade> trades = new List<ITrade>();

                    Console.WriteLine("Digite os dados das trades (valor, setor, data de próximo pagamento, exposto politicamente - ex: 2000000 Private 12/29/2025 true):");
                    for (int i = 0; i < n; i++)
                    {
                        try //lê os dados das trades, garantindo que a leitura e análise de cada trade sejam protegidas contra erros.
                        {
                            string[] tradeData = Console.ReadLine().Split(' ');

                            if (tradeData.Length < 3 || tradeData.Length > 4)
                            {
                                Console.WriteLine("Entrada inválida. Por favor, insira os dados corretamente.");
                                i--;
                                continue;
                            }

                            if (!double.TryParse(tradeData[0], out double value))
                            {
                                Console.WriteLine("Valor inválido. Por favor, insira um número válido.");
                                i--;
                                continue;
                            }

                            string clientSector = tradeData[1];
                            if (clientSector != "Public" && clientSector != "Private")
                            {
                                Console.WriteLine("Setor do cliente inválido. Use 'Public' ou 'Private'.");
                                i--;
                                continue;
                            }

                            if (!DateTime.TryParseExact(tradeData[2], "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime nextPaymentDate))
                            {
                                Console.WriteLine("Data inválida. Use o formato MM/dd/yyyy.");
                                i--;
                                continue;
                            }
                            //Validação de Entrada: Usei TryParse para verificar a validade das entradas, fornecendo mensagens de erro detalhadas quando as entradas são inválidas.
                            // Verifica se o parâmetro "exposto politicamente" foi fornecido , falso como default
                            bool isPoliticallyExposed = tradeData.Length == 4 && bool.TryParse(tradeData[3], out bool exposed) ? exposed : false;

                            trades.Add(new Trade(value, clientSector, nextPaymentDate, isPoliticallyExposed));
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Erro ao processar a trade: {ex.Message}");
                            i--;
                        }
                    }

                    List<ITradeCategory> categories = new List<ITradeCategory>
                    {
                        new PepCategory(),
                        new ExpiredCategory(),
                        new MediumRiskCategory(),
                        new LowRiskCategory()
                    };

                    Console.WriteLine("\nCategorias das trades:");
                    foreach (var trade in trades)
                    {
                        foreach (var category in categories)
                        {
                            if (category.IsMatch(trade, referenceDate))
                            {
                                Console.WriteLine(category.CategoryName);
                                break;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro inesperado: {ex.Message}");
                }

                Console.WriteLine("\nPressione qualquer tecla para limpar e repetir, ou 'Esc' para sair...");
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.Escape) break;

                Console.Clear();
            }
        }
    }
}