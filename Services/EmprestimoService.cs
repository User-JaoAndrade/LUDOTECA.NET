using LUDOTECA.Models;
using LUDOTECA.Utils;
using LUDOTECA.Exceptions;

namespace LUDOTECA.Service
{
    public static class EmprestimoService
    {
        public static void EmprestarJogo(Biblioteca biblioteca)
        {
            while (true)
            {
                try // [AV1-5]
                {
                    Console.WriteLine("\n=== EMPRÉSTIMO DE JOGO ===");
                    Console.WriteLine("Prazo: 7 dias | Multa: R$2 por dia\n");

                    Console.Write("ID do membro: ");
                    if (!int.TryParse(Helpers.LerEntradaDeDados(), out int idMembro))

                    if (!biblioteca.Membros.ContainsKey(idMembro))
                        throw new MembroNaoEncontradoException(idMembro);

                    var membro = biblioteca.Membros[idMembro];

                    if (membro.JogoAlugado != "Nenhum")
                        throw new MembroComJogoException(membro.Nome, membro.JogoAlugado);

                    Console.Write($"\nOlá {membro.Nome}!\nInforme o ID do jogo que deseja alugar: ");
                    int idJogo = int.Parse(Helpers.LerEntradaDeDados());

                    if (!biblioteca.Jogos.ContainsKey(idJogo))
                        throw new JogoNaoEncontradoException(idJogo);

                    var jogo = biblioteca.Jogos[idJogo];

                    if (!jogo.Disponivel)
                        throw new JogoIndisponivelException(jogo.Nome);

                    jogo.TornarIndisponivel();
                    membro.AlterarJogoAlugado(jogo.Nome);
                    membro.AlterarDataAluguel(DateTime.Now);
                    membro.AlterarDataDevolucao(DateTime.Now.AddDays(7));

                    Biblioteca.SalvarBiblioteca(biblioteca);
                    RelatorioService.AtualizarRelatorio(biblioteca);
                    Logger.Log("Arquivos JSON e txt atualizados em 'EmprestarJogo'");

                    Console.Write("Pegando jogo da prateleira");
                    Helpers.AnimacaoDePontos(3);
                    Console.WriteLine($"\n{membro.Nome} alugou {jogo.Nome} com sucesso!");
                    Console.Write("Aperte ENTER para continuar...");
                    Console.ReadLine();
                    break;
                }
                catch (KeyNotFoundException) // [AV1-5]
                {
                    Logger.Log("KeyNotFoundException: MEMBRO NAO ENCONTRADO");
                    Console.WriteLine("ERRO: membro não encontrado");
                    if (!Helpers.VerificarSeUsuarioDesejaContinuar()) break;
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
    }
}
