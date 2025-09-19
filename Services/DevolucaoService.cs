using LUDOTECA.Utils;
using LUDOTECA.Models;
using System.Linq;

namespace LUDOTECA.Service
{
    public static class DevolucaoService
    {
        /// <summary>
        /// Realiza o processo de devolução de um jogo.
        /// - Verifica se o membro possui jogo alugado.
        /// - Calcula multa em caso de atraso.
        /// - Atualiza a disponibilidade do jogo e o status do membro.
        /// - Persiste as alterações nos arquivos JSON.
        /// </summary>
        public static void DevolverJogo()
        {
            // Carrega os membros e jogos do JSON e transforma em dicionários com o ID como chave.
            // Se o JSON estiver vazio ou não existir, cria dicionários vazios para evitar null.
            var lista_de_membros = JsonHelper.CarregarLista<Membro>("Data/Membros.json")
                                            ?.ToDictionary(m => m._Id, m => m)
                                            ?? new Dictionary<int, Membro>();

            var lista_de_jogos = JsonHelper.CarregarLista<Jogo>("Data/Biblioteca.json")
                                           ?.ToDictionary(j => j._Id, j => j)
                                           ?? new Dictionary<int, Jogo>();

            while (true)
            {
                try
                {
                    Console.WriteLine("\n\n\n\n==== Devolver Jogo ====\n");

                    Console.Write("Informe o ID do membro\n-> ");
                    if (!int.TryParse(Helpers.LerEntradaDeDados().Trim(), out int id_membro))
                        throw new FormatException("ID do membro inválido! Informe um número inteiro.");

                    if (!lista_de_membros.ContainsKey(id_membro))
                        throw new KeyNotFoundException("ATENÇÃO: Membro não encontrado");
                    
                    // Obtém o objeto Membro correspondente ao ID informado pelo usuário
                    var membro = lista_de_membros[id_membro];

                    if (membro.Jogo_alugado == "Nenhum")
                        throw new Exception($"{membro.Nome} não possui jogo alugado.");

                    // Procura na lista de jogos o objeto Jogo que possui o mesmo nome do jogo atualmente alugado pelo membro
                    var jogo = lista_de_jogos.Values.FirstOrDefault(j => j.Nome == membro.Jogo_alugado);

                    if (jogo == null)
                        throw new Exception("Jogo não encontrado na biblioteca.");

                    int multa = CalculoDeMulta(membro.Data_de_devolucao, membro.Data_do_aluguel);
                    
                    // Caso o usuário resolva não tentar novamente uma data válida
                    if (multa == -1)
                    {
                        Console.WriteLine("\nOperação cancelada. Voltando ao menu...");
                        Helpers.AnimacaoDePontos(2);
                        break;
                    }

                    // Verificando se existe uma multa
                    if (multa > 0)
                    {
                        Console.WriteLine($"\nO aluguel atrasou {multa / 2} dias. Multa: R$ {multa}");

                        string forma_de_pagamento = "";

                        while (true)
                        {
                            Console.Write("\nForma de Pagamento\n\n1- PIX\n2- Dinheiro\n\n-> ");
                            string opcao = Helpers.LerEntradaDeDados().Trim();

                            switch (opcao)
                            {
                                case "1":
                                    forma_de_pagamento = "PIX";
                                    string chavePix = Guid.NewGuid().ToString();
                                    Console.WriteLine($"\nUse esta chave PIX para pagar: {chavePix}");
                                    Console.Write("Aperte ENTER para realizar o pagamento");
                                    Console.ReadLine();
                                    break;
                                case "2":
                                    forma_de_pagamento = "Dinheiro";
                                    break;
                                default:
                                    Console.WriteLine("Opção inválida. Digite 1 para PIX ou 2 para Dinheiro.");
                                    continue;
                            }

                            break;
                        }

                        Console.Write($"\nPagamento realizado com {forma_de_pagamento}. Multa quitada!");
                        Helpers.AnimacaoDePontos(3);
                    }
                    else
                    {
                        Console.WriteLine("\nSem atraso. Nenhuma multa.");
                    }

                    // Atualiza status do jogo e membro
                    jogo.MudarJogoParaDisponivel();
                    membro.AlterarNomeDoJogoAlugado("Nenhum");

                    // Salva alterações nos JSONs
                    JsonHelper.SalvarLista(lista_de_jogos.Values.ToList(), "Data/Biblioteca.json");
                    JsonHelper.SalvarLista(lista_de_membros.Values.ToList(), "Data/Membros.json");

                    Console.Write("\n\nDevolvendo jogo");
                    Helpers.AnimacaoDePontos(3);
                    Console.WriteLine($"\n{membro.Nome} devolveu o jogo {jogo.Nome} com sucesso!\n");
                    Console.Write("Aperte ENTER para continuar");
                    Console.ReadLine();
                    break;
                }
                catch (Exception ex)
                {
                    Helpers.MensagemDeExcessao(ex.Message);
                    if (!Helpers.VerificarSeUsuarioDesejaContinuar())
                        break;
                }
            }
        }

        /// <summary>
        /// Solicita ao usuário a data de devolução e calcula multa por atraso.
        /// - Considera R$2,00 por dia de atraso.
        /// - Impede datas anteriores à data do aluguel.
        /// - Retorna -1 caso o usuário cancele a operação.
        /// </summary>
        /// <param name="data_de_devolucao">Data prevista para devolução.</param>
        /// <param name="data_de_aluguel">Data em que o jogo foi alugado.</param>
        /// <returns>
        /// Valor da multa (em reais). Retorna 0 se não houver atraso ou -1 se a operação for cancelada.
        /// </returns>
        public static int CalculoDeMulta(DateTime data_de_devolucao, DateTime data_de_aluguel)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("\n\nINFORME A DATA DE DEVOLUÇÃO\n");

                    Console.Write("Dia: ");
                    if (!int.TryParse(Helpers.LerEntradaDeDados().Trim(), out int dia))
                        throw new FormatException("DIA INVALIDO: Informe um número inteiro");

                    Console.Write("Mês: ");
                    if (!int.TryParse(Helpers.LerEntradaDeDados().Trim(), out int mes))
                        throw new FormatException("MES INVALIDO: Informe um número inteiro");

                    Console.Write("Ano: ");
                    if (!int.TryParse(Helpers.LerEntradaDeDados().Trim(), out int ano))
                        throw new FormatException("ANO INVALIDO: Informe um número inteiro");

                    // Criando a Data de devolução informada pelo usuário
                    // Calculando quantos dias se passaram após a data que o jogo devia ser devolvido
                    DateTime data_que_esta_devolvendo = new DateTime(ano, mes, dia);
                    TimeSpan diferenca = data_que_esta_devolvendo - data_de_devolucao;

                    // Piada idiota caso o usuário informe que o dia devolvido foi ANTES do dia de aluguel
                    if (data_que_esta_devolvendo < data_de_aluguel)
                        throw new Exception("\nTem um DeLorean? Viajante do tempo? Informe uma data válida");

                    return diferenca.Days * 2;
                }
                catch (Exception ex)
                {
                    Helpers.MensagemDeExcessao(ex.Message);
                    if (!Helpers.VerificarSeUsuarioDesejaContinuar())
                        return -1; // Indica que o usuário cancelou a operação
                }
            }
        }
    }
}
