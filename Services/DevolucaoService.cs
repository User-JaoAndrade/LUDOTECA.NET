using LUDOTECA.Models;
using LUDOTECA.Utils;
using LUDOTECA.Exceptions;

namespace LUDOTECA.Service
{
    public static class DevolucaoService
    {
        public static void DevolverJogo(Biblioteca biblioteca)
        {
            while (true)
            {
                try // [AV1-5]
                {
                    Console.WriteLine("\n=== DEVOLUÇÃO DE JOGO ===");

                    Console.Write("ID do membro: ");
                    if (!int.TryParse(Helpers.LerEntradaDeDados(), out int idMembro))
                        throw new ArgumentException("ID inválido. Informe um número inteiro.");

                    if (!biblioteca.Membros.ContainsKey(idMembro))
                        throw new MembroNaoEncontradoException(idMembro);

                    var membro = biblioteca.Membros[idMembro];

                    if (membro.JogoAlugado == "Nenhum")
                        throw new MembroSemJogoException(membro.Nome);

                    var jogo = biblioteca.Jogos.Values.FirstOrDefault(j => j.Nome == membro.JogoAlugado);
                    if (jogo == null)
                        throw new JogoNaoEncontradoException(0);

                    int multa = CalcularMulta(membro.DataAluguel, membro.DataDevolucao);
                    if (multa > 0)
                    {
                        Logger.Log($"Multa de R${multa} gerada para {membro.Nome}");
                        Console.WriteLine($"\nAtraso: {multa / 2} dias. Multa: R$ {multa}");
                        EfetuarPagamento(multa);
                    }
                    else
                    {
                        Console.WriteLine("\nSem atraso. Nenhuma multa.");
                    }

                    jogo.TornarDisponivel();
                    membro.AlterarJogoAlugado("Nenhum");
                    membro.AlterarDataAluguel(default);
                    membro.AlterarDataDevolucao(default);

                    Biblioteca.SalvarBiblioteca(biblioteca);
                    RelatorioService.AtualizarRelatorio(biblioteca);
                    Logger.Log("Arquivos JSON e txt atualizados em 'DevolverJogo()'");

                    Console.Write("\nColocando jogo na prateleira");
                    Helpers.AnimacaoDePontos(3);
                    Console.WriteLine($"\n\n{membro.Nome} devolveu {jogo.Nome} com sucesso!");
                    Console.Write("Aperte ENTER para continuar...");
                    Console.ReadLine();
                    break;
                }
                catch (ArgumentException ex) // [AV1-5]
                {
                    Logger.LogErro(ex);
                    Console.WriteLine(ex.Message);
                    if (!Helpers.VerificarSeUsuarioDesejaContinuar()) break;
                }
                catch (FormatException ex) // [AV1-5]
                {
                    Logger.LogErro(ex);
                    Console.WriteLine("ERRO: Por favor, informe um número inteiro.");
                    if (!Helpers.VerificarSeUsuarioDesejaContinuar()) break;
                }
                catch (LudotecaException ex) // [AV1-5]
                {
                    Logger.LogErro(ex);
                    Console.WriteLine($"ERRO: {ex.Message}");
                    if (!Helpers.VerificarSeUsuarioDesejaContinuar()) break;
                }
            }
        }

        private static int CalcularMulta(DateTime dataAluguel, DateTime dataDevolucaoPrevista)
        {
            Console.WriteLine("\nInforme a data de devolução (dd/mm/aaaa):");
            Console.Write("Dia: "); int dia = int.Parse(Helpers.LerEntradaDeDados());
            Console.Write("Mês: "); int mes = int.Parse(Helpers.LerEntradaDeDados());
            Console.Write("Ano: "); int ano = int.Parse(Helpers.LerEntradaDeDados());

            DateTime dataEntrega = new DateTime(ano, mes, dia);

            if (dataEntrega < dataAluguel)
                throw new ArgumentException("Data de devolução inválida: não pode ser anterior à data do aluguel.");

            int atraso = Math.Max(0, (dataEntrega - dataDevolucaoPrevista).Days);

            return atraso * 2;
        }

        private static void EfetuarPagamento(int valor)
        {
            while (true)
            {
                Console.Write("\nEscolha forma de pagamento:\n1- PIX\n2- Dinheiro\n-> ");
                string opcao = Helpers.LerEntradaDeDados().Trim();

                if (opcao == "1")
                {
                    string chavePix = Guid.NewGuid().ToString();
                    Console.WriteLine($"\nUse esta chave PIX para pagar: {chavePix}");
                    Console.Write("Aperte ENTER para pagar...");
                    Console.ReadLine();
                    Console.Write("Abrindo aplicativo do banco");
                    Helpers.AnimacaoDePontos(2);
                    Console.WriteLine("Finalizando pagamento");
                    Helpers.AnimacaoDePontos(2);
                    Console.WriteLine($"\nPagamento realizado via PIX. Multa quitada: R$ {valor}");
                    break;
                }
                else if (opcao == "2")
                {
                    Console.WriteLine("Abrindo a carteira");
                    Helpers.AnimacaoDePontos(2);
                    Console.WriteLine("Procurando dinheiro");
                    Helpers.AnimacaoDePontos(2);
                    Console.WriteLine("Finalizando pagamento");
                    Helpers.AnimacaoDePontos(2);
                    Console.WriteLine($"\nPagamento realizado em Dinheiro. Multa quitada: R$ {valor}");
                    break;
                }
                else
                {
                    Console.WriteLine("Opção inválida.");
                }
            }
        }
    }
}
